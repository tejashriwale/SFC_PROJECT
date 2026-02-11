-- ===============================================================
-- CREATE GET_VechicleProfitReport FUNCTION (SAFE VERSION)
-- This version checks column names before creating the function
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'CREATING GET_VechicleProfitReport FUNCTION'
PRINT '=========================================='
PRINT ''

-- ===============================================================
-- STEP 1: CHECK REQUIRED TABLES
-- ===============================================================

PRINT 'Checking required tables...'
PRINT ''

DECLARE @AllTablesExist BIT = 1

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoice')
BEGIN
    PRINT '✗ ERROR: tblCropTransportInvoice not found!'
    SET @AllTablesExist = 0
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoiceDetail')
BEGIN
    PRINT '✗ ERROR: tblCropTransportInvoiceDetail not found!'
    SET @AllTablesExist = 0
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
BEGIN
    PRINT '✗ ERROR: tblCropInvertMaster not found!'
    SET @AllTablesExist = 0
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
BEGIN
    PRINT '✗ ERROR: tblCropInvertDetail not found!'
    SET @AllTablesExist = 0
END

IF @AllTablesExist = 0
BEGIN
    PRINT ''
    PRINT 'Cannot create function - required tables are missing!'
    GOTO EndScript
END

PRINT '✓ All required tables exist'
PRINT ''

-- ===============================================================
-- STEP 2: DETECT VEHICLE COLUMN NAME
-- ===============================================================

PRINT 'Detecting vehicle column name in tblCropInvertMaster...'
PRINT ''

DECLARE @VehicleColumn NVARCHAR(128)

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME = 'VEHNO'
)
BEGIN
    SET @VehicleColumn = 'VEHNO'
    PRINT '✓ Using column: VEHNO'
END
ELSE IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME = 'VEHCHNO'
)
BEGIN
    SET @VehicleColumn = 'VEHCHNO'
    PRINT '✓ Using column: VEHCHNO'
END
ELSE IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME LIKE '%VEH%'
)
BEGIN
    SELECT TOP 1 @VehicleColumn = COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME LIKE '%VEH%'

    PRINT '⚠ Using column: ' + @VehicleColumn
END
ELSE
BEGIN
    PRINT '✗ ERROR: No vehicle column found in tblCropInvertMaster!'
    PRINT ''
    PRINT 'Available columns:'
    SELECT COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    ORDER BY ORDINAL_POSITION

    GOTO EndScript
END

PRINT ''

-- ===============================================================
-- STEP 3: DROP EXISTING FUNCTION
-- ===============================================================

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing GET_VechicleProfitReport function'
    PRINT ''
END

-- ===============================================================
-- STEP 4: CREATE FUNCTION
-- ===============================================================

PRINT 'Creating GET_VechicleProfitReport function...'
PRINT ''

DECLARE @SQL NVARCHAR(MAX)

SET @SQL = N'
CREATE FUNCTION dbo.GET_VechicleProfitReport
(
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @SDATE VARCHAR(50),
    @EDATE VARCHAR(50)
)
RETURNS @ResultTable TABLE
(
    SYSDT VARCHAR(50),
    CUSTOMERNAME VARCHAR(200),
    CROPNAME VARCHAR(200),
    TOTSAMT DECIMAL(18,2),
    CROP VARCHAR(200),
    CRTCNT INT,
    TOTEX DECIMAL(18,2),
    TOTIWDAMT DECIMAL(18,2),
    TREADER VARCHAR(200),
    VEHCHNO VARCHAR(50)
)
AS
BEGIN
    -- This function combines data from:
    -- 1. tblCropTransportInvoice & tblCropTransportInvoiceDetail (Sales data)
    -- 2. tblCropInvertMaster & tblCropInvertDetail (Purchase/Inward data)

    -- Use CTE to calculate purchase amounts per vehicle/invoice
    ;WITH InwardData AS (
        SELECT
            cim.' + @VehicleColumn + N' AS VEHCHNO,
            cid.CROPID,
            SUM(ISNULL(cid.WEIGHT, 0) * ISNULL(cid.RATE, 0)) AS TOTIWDAMT
        FROM tblCropInvertMaster cim
        INNER JOIN tblCropInvertDetail cid ON cim.CDAID = cid.CDAID
        WHERE cim.COMPANYID = @COMPANYID
        AND cim.DEPTID = @DEPTID
        AND CONVERT(DATE, cim.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
        AND cid.ISDELETED = 0
        AND cim.ISDELETED = 0
        GROUP BY cim.' + @VehicleColumn + N', cid.CROPID
    )

    INSERT INTO @ResultTable
    SELECT
        CONVERT(VARCHAR(50), ti.SYSDT, 106) AS SYSDT,
        ISNULL(m.MANAME, '''') AS CUSTOMERNAME,
        ISNULL(c.CROPNAME, '''') AS CROPNAME,
        ISNULL(SUM(tid.TAMT), 0) AS TOTSAMT,
        ISNULL(c.CROPNAME, '''') AS CROP,
        ISNULL(SUM(CAST(tid.CRTCNT AS INT)), 0) AS CRTCNT,
        ISNULL(MAX(ti.EXPENSES), 0) AS TOTEX,
        ISNULL(MAX(iwd.TOTIWDAMT), 0) AS TOTIWDAMT,
        ISNULL(ti.TRADERNAME, '''') AS TREADER,
        ISNULL(ti.VEHCHNO, '''') AS VEHCHNO

    FROM tblCropTransportInvoice ti
    INNER JOIN tblCropTransportInvoiceDetail tid ON ti.CTRAID = tid.CTRAID
    LEFT JOIN tblMaster m ON ti.MAID = m.MAID
    LEFT JOIN tblCropMaster c ON tid.CROPID = c.CROPID
    LEFT JOIN InwardData iwd ON ti.VEHCHNO = iwd.VEHCHNO AND tid.CROPID = iwd.CROPID

    WHERE ti.COMPANYID = @COMPANYID
    AND ti.DEPTID = @DEPTID
    AND CONVERT(DATE, ti.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
    AND ti.ISDELETED = 0
    AND tid.ISDELETED = 0

    GROUP BY
        ti.SYSDT,
        m.MANAME,
        c.CROPNAME,
        ti.TRADERNAME,
        ti.VEHCHNO

    RETURN
END
'

BEGIN TRY
    EXEC sp_executesql @SQL
    PRINT '✓ Function created successfully!'
    PRINT ''
END TRY
BEGIN CATCH
    PRINT '✗ ERROR creating function:'
    PRINT ERROR_MESSAGE()
    PRINT ''
    PRINT 'SQL State: ' + CAST(ERROR_STATE() AS VARCHAR(10))
    PRINT 'Error Number: ' + CAST(ERROR_NUMBER() AS VARCHAR(10))
    PRINT ''
    GOTO EndScript
END CATCH

-- ===============================================================
-- STEP 5: VERIFY
-- ===============================================================

PRINT '=========================================='
PRINT 'VERIFICATION'
PRINT '=========================================='
PRINT ''

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    PRINT '✓ Function exists in database'
    PRINT ''
    PRINT 'Function Details:'
    PRINT '----------------'
    PRINT '• Name: GET_VechicleProfitReport'
    PRINT '• Type: Table-Valued Function'
    PRINT '• Vehicle Column Used: ' + @VehicleColumn
    PRINT ''
    PRINT 'Parameters:'
    PRINT '• @COMPANYID VARCHAR(50)'
    PRINT '• @DEPTID VARCHAR(50)'
    PRINT '• @SDATE VARCHAR(50)'
    PRINT '• @EDATE VARCHAR(50)'
    PRINT ''
    PRINT '✓ CREATION SUCCESSFUL!'
    PRINT ''
    PRINT 'You can now run Test_GET_VechicleProfitReport_Function.sql'
END
ELSE
BEGIN
    PRINT '✗ VERIFICATION FAILED!'
    PRINT 'Function was not created'
END

PRINT ''

EndScript:

PRINT '=========================================='
PRINT 'SCRIPT COMPLETE'
PRINT '=========================================='
PRINT ''

GO
