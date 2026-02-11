-- ===============================================================
-- DROP ANY EXISTING OBJECT AND CREATE FUNCTION
-- This will drop view/procedure/function and create the correct one
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'CLEANING UP EXISTING OBJECTS'
PRINT '=========================================='
PRINT ''

-- Check what type of object exists
DECLARE @ObjectType VARCHAR(20)

SELECT @ObjectType = type_desc
FROM sys.objects
WHERE name = 'GET_VechicleProfitReport'

IF @ObjectType IS NOT NULL
BEGIN
    PRINT 'Found existing object: ' + @ObjectType
    PRINT 'Dropping it...'
    PRINT ''
END

-- Drop if it's a VIEW
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'V') IS NOT NULL
BEGIN
    DROP VIEW dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing VIEW'
    PRINT ''
END

-- Drop if it's a STORED PROCEDURE
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing STORED PROCEDURE'
    PRINT ''
END

-- Drop if it's a TABLE-VALUED FUNCTION
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing TABLE-VALUED FUNCTION'
    PRINT ''
END

-- Drop if it's a SCALAR FUNCTION
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'FN') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing SCALAR FUNCTION'
    PRINT ''
END

-- Drop if it's an INLINE TABLE-VALUED FUNCTION
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'IF') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing INLINE FUNCTION'
    PRINT ''
END

PRINT 'All existing objects cleaned up'
PRINT ''

GO

-- ===============================================================
-- NOW CREATE THE FUNCTION
-- ===============================================================

PRINT '=========================================='
PRINT 'CREATING NEW FUNCTION'
PRINT '=========================================='
PRINT ''

GO

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

    -- Calculate purchase amounts per vehicle/crop from tblCropInvertDetail
    ;WITH InwardData AS (
        SELECT
            cid.VEHNO AS VEHCHNO,
            cid.CROPID,
            SUM(ISNULL(cid.WEIGHT, 0) * ISNULL(cid.RATE, 0)) AS TOTIWDAMT
        FROM tblCropInvertMaster cim
        INNER JOIN tblCropInvertDetail cid ON cim.CDAID = cid.CDAID
        WHERE cim.COMPANYID = @COMPANYID
        AND cim.DEPTID = @DEPTID
        AND CONVERT(DATE, cim.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
        AND cid.ISDELETED = 0
        AND cim.ISDELETED = 0
        GROUP BY cid.VEHNO, cid.CROPID
    )

    INSERT INTO @ResultTable
    SELECT
        CONVERT(VARCHAR(50), ti.SYSDT, 106) AS SYSDT,
        ISNULL(m.MANAME, '') AS CUSTOMERNAME,
        ISNULL(c.CROPNAME, '') AS CROPNAME,
        ISNULL(SUM(tid.SAMT), 0) AS TOTSAMT,
        ISNULL(c.CROPNAME, '') AS CROP,
        ISNULL(SUM(tid.CRTCNT), 0) AS CRTCNT,
        ISNULL(MAX(ISNULL(ti.EXPENSES, 0)), 0) AS TOTEX,
        ISNULL(MAX(iwd.TOTIWDAMT), 0) AS TOTIWDAMT,
        ISNULL(ti.TRADERNAME, '') AS TREADER,
        ISNULL(ti.VEHCHNO, '') AS VEHCHNO

    FROM tblCropTransportInvoice ti
    INNER JOIN tblCropTransportInvoiceDetail tid ON ti.CTAID = tid.CTAID
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
GO

PRINT '✓ Function created successfully!'
PRINT ''

-- ===============================================================
-- VERIFICATION
-- ===============================================================

PRINT '=========================================='
PRINT 'VERIFICATION'
PRINT '=========================================='
PRINT ''

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    PRINT '✓ GET_VechicleProfitReport function exists'
    PRINT '✓ Type: Table-Valued Function'
    PRINT ''
    PRINT 'Data Sources:'
    PRINT '• tblCropTransportInvoice (Sales)'
    PRINT '• tblCropTransportInvoiceDetail (Sales Details)'
    PRINT '• tblCropInvertMaster (Purchase)'
    PRINT '• tblCropInvertDetail (Purchase Details)'
    PRINT ''
    PRINT '✓ SUCCESS! Function is ready to use.'
    PRINT ''
    PRINT 'Next: Run Test_GET_VechicleProfitReport_Function.sql'
END
ELSE
BEGIN
    PRINT '✗ ERROR: Function not found after creation'
    PRINT 'Check error messages above'
END

PRINT ''
PRINT '=========================================='
PRINT 'COMPLETE'
PRINT '=========================================='
PRINT ''

GO
