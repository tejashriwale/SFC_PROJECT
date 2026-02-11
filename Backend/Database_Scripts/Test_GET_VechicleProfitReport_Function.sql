-- ===============================================================
-- TEST GET_VechicleProfitReport FUNCTION
-- This script tests the profit loss report function
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'TESTING GET_VechicleProfitReport FUNCTION'
PRINT '=========================================='
PRINT ''

-- Check if function exists
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NULL
BEGIN
    PRINT '✗ ERROR: GET_VechicleProfitReport function does not exist!'
    PRINT ''
    PRINT 'Please run Create_GET_VechicleProfitReport_Function.sql first'
    PRINT ''
    GOTO EndScript
END

PRINT '✓ Function exists'
PRINT ''

-- ===============================================================
-- STEP 1: CHECK TABLE STRUCTURES
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 1: VERIFYING TABLES EXIST'
PRINT '=========================================='
PRINT ''

-- Check if required tables exist
DECLARE @TablesExist BIT = 1

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoice')
    PRINT '✓ tblCropTransportInvoice exists'
ELSE
BEGIN
    PRINT '✗ tblCropTransportInvoice NOT FOUND'
    SET @TablesExist = 0
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoiceDetail')
    PRINT '✓ tblCropTransportInvoiceDetail exists'
ELSE
BEGIN
    PRINT '✗ tblCropTransportInvoiceDetail NOT FOUND'
    SET @TablesExist = 0
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
    PRINT '✓ tblCropInvertMaster exists'
ELSE
BEGIN
    PRINT '✗ tblCropInvertMaster NOT FOUND'
    SET @TablesExist = 0
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
    PRINT '✓ tblCropInvertDetail exists'
ELSE
BEGIN
    PRINT '✗ tblCropInvertDetail NOT FOUND'
    SET @TablesExist = 0
END

PRINT ''

IF @TablesExist = 0
BEGIN
    PRINT '⚠ Some required tables are missing!'
    GOTO EndScript
END

-- ===============================================================
-- STEP 2: CHECK COLUMN NAMES
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 2: CHECKING COLUMN NAMES'
PRINT '=========================================='
PRINT ''

-- Check vehicle number column in tblCropInvertMaster
PRINT 'Checking vehicle column in tblCropInvertMaster:'
PRINT '-----------------------------------------------'

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME = 'VEHNO'
)
    PRINT '✓ Column name is: VEHNO'
ELSE IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME = 'VEHCHNO'
)
BEGIN
    PRINT '⚠ Column name is: VEHCHNO (not VEHNO)'
    PRINT '  You may need to update the function to use VEHCHNO instead of VEHNO'
END
ELSE IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME LIKE '%VEH%'
)
BEGIN
    SELECT '⚠ Vehicle column found: ' + COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    AND COLUMN_NAME LIKE '%VEH%'

    PRINT '  Please update the function to use this column name'
END
ELSE
    PRINT '✗ No vehicle column found in tblCropInvertMaster'

PRINT ''

-- Check RATE column in tblCropInvertDetail
PRINT 'Checking RATE column in tblCropInvertDetail:'
PRINT '--------------------------------------------'

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertDetail'
    AND COLUMN_NAME = 'RATE'
)
    PRINT '✓ RATE column exists'
ELSE
BEGIN
    PRINT '✗ RATE column NOT FOUND'
    PRINT '  Run Add_Rate_To_CropInvertDetail.sql to add this column'
END

PRINT ''

-- ===============================================================
-- STEP 3: TEST WITH SAMPLE DATA
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 3: TESTING FUNCTION WITH SAMPLE DATA'
PRINT '=========================================='
PRINT ''

-- Get a sample date range from existing data
DECLARE @SampleStartDate VARCHAR(50)
DECLARE @SampleEndDate VARCHAR(50)
DECLARE @SampleCompanyID VARCHAR(50)
DECLARE @SampleDeptID VARCHAR(50)

-- Try to get actual company/dept from transport invoice table
SELECT TOP 1
    @SampleCompanyID = COMPANYID,
    @SampleDeptID = DEPTID,
    @SampleStartDate = CONVERT(VARCHAR(50), DATEADD(DAY, -30, SYSDT), 106),
    @SampleEndDate = CONVERT(VARCHAR(50), SYSDT, 106)
FROM tblCropTransportInvoice
WHERE ISDELETED = 0
ORDER BY SYSDT DESC

IF @SampleCompanyID IS NOT NULL
BEGIN
    PRINT 'Testing with:'
    PRINT '  Company ID: ' + @SampleCompanyID
    PRINT '  Dept ID: ' + @SampleDeptID
    PRINT '  Date Range: ' + @SampleStartDate + ' to ' + @SampleEndDate
    PRINT ''
    PRINT 'Sample Results:'
    PRINT '---------------'

    -- Execute function
    SELECT TOP 5
        SYSDT AS [Date],
        CUSTOMERNAME AS [Customer],
        CROPNAME AS [Crop],
        VEHCHNO AS [Vehicle],
        CRTCNT AS [Crates],
        TOTEX AS [Expense],
        TOTIWDAMT AS [Purchase Amt],
        TOTSAMT AS [Sales Amt],
        (TOTSAMT - (TOTIWDAMT + TOTEX)) AS [Profit]
    FROM dbo.GET_VechicleProfitReport(
        @SampleCompanyID,
        @SampleDeptID,
        @SampleStartDate,
        @SampleEndDate
    )
    ORDER BY SYSDT DESC

    PRINT ''
    PRINT '✓ Function executed successfully'
    PRINT ''

    -- Show row count
    DECLARE @RowCount INT
    SELECT @RowCount = COUNT(*)
    FROM dbo.GET_VechicleProfitReport(
        @SampleCompanyID,
        @SampleDeptID,
        @SampleStartDate,
        @SampleEndDate
    )

    PRINT 'Total rows returned: ' + CAST(@RowCount AS VARCHAR(10))
END
ELSE
BEGIN
    PRINT '⚠ No sample data found in tblCropTransportInvoice'
    PRINT '  Cannot test function without data'
    PRINT ''
    PRINT 'To test manually, use:'
    PRINT 'SELECT * FROM dbo.GET_VechicleProfitReport(''YourCompanyID'', ''YourDeptID'', ''1 Jan 2025'', ''31 Jan 2025'')'
END

PRINT ''

-- ===============================================================
-- STEP 4: SUMMARY
-- ===============================================================

PRINT '=========================================='
PRINT 'TEST SUMMARY'
PRINT '=========================================='
PRINT ''

PRINT 'The function uses these tables:'
PRINT '• tblCropTransportInvoice (Sales invoices)'
PRINT '• tblCropTransportInvoiceDetail (Sales details)'
PRINT '• tblCropInvertMaster (Purchase/Inward master)'
PRINT '• tblCropInvertDetail (Purchase/Inward details)'
PRINT '• tblMaster (Customer master)'
PRINT '• tblCropMaster (Crop master)'
PRINT ''

PRINT 'Data Matching:'
PRINT '• Sales and Purchase data are matched by Vehicle Number and Crop ID'
PRINT '• Purchase Amount = SUM(Weight * Rate) from tblCropInvertDetail'
PRINT '• Profit = Sales Amount - (Purchase Amount + Expenses)'
PRINT ''

EndScript:

PRINT '=========================================='
PRINT 'TEST COMPLETE'
PRINT '=========================================='
PRINT ''

GO
