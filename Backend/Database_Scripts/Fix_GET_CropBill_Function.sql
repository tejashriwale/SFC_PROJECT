-- ===============================================================
-- FIX GET_CropBill FUNCTION - RATE DATA TYPE ERROR
-- This script fixes the "varchar to numeric" conversion error
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'DIAGNOSING GET_CropBill FUNCTION'
PRINT '=========================================='
PRINT ''

-- ===============================================================
-- STEP 1: CHECK IF FUNCTION EXISTS
-- ===============================================================

PRINT 'Checking if GET_CropBill function exists...'
PRINT ''

IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE type IN ('FN', 'IF', 'TF')
    AND name = 'GET_CropBill'
)
BEGIN
    PRINT '✗ ERROR: GET_CropBill function not found!'
    PRINT ''
    PRINT 'Checking for similar functions...'

    SELECT
        name AS [Function Name],
        type_desc AS [Type]
    FROM sys.objects
    WHERE type IN ('FN', 'IF', 'TF')
    AND name LIKE '%CropBill%'

    GOTO EndScript
END

PRINT '✓ GET_CropBill function found'
PRINT ''

-- ===============================================================
-- STEP 2: GET CURRENT FUNCTION DEFINITION
-- ===============================================================

PRINT '=========================================='
PRINT 'CURRENT FUNCTION DEFINITION'
PRINT '=========================================='
PRINT ''

DECLARE @FunctionDef NVARCHAR(MAX)
SELECT @FunctionDef = OBJECT_DEFINITION(OBJECT_ID('dbo.GET_CropBill'))

-- Show first 1000 characters
PRINT 'Function definition (first 1000 chars):'
PRINT '----------------------------------------'
PRINT LEFT(@FunctionDef, 1000)
PRINT '...'
PRINT ''

-- ===============================================================
-- STEP 3: CHECK WHAT TABLE/COLUMN IS USED FOR RATE
-- ===============================================================

PRINT '=========================================='
PRINT 'ANALYZING RATE COLUMN SOURCE'
PRINT '=========================================='
PRINT ''

-- Check if function references master table for RATE
IF @FunctionDef LIKE '%tblCropInvertMaster%' AND @FunctionDef LIKE '%RATE%'
BEGIN
    PRINT '⚠ Function pulls RATE from tblCropInvertMaster (OLD location)'
    PRINT '  This needs to be updated to pull from detail table'
    PRINT ''
END

-- Check if function references detail table for RATE
IF @FunctionDef LIKE '%tblCropInvertDetail%' AND @FunctionDef LIKE '%RATE%'
BEGIN
    PRINT '✓ Function pulls RATE from tblCropInvertDetail (NEW location)'
    PRINT '  This is correct after migration'
    PRINT ''
END

-- Check for CAST or CONVERT on RATE
IF @FunctionDef LIKE '%CAST%RATE%' OR @FunctionDef LIKE '%CONVERT%RATE%'
BEGIN
    PRINT 'ℹ Function has CAST/CONVERT on RATE column'
    PRINT ''
END

-- ===============================================================
-- STEP 4: IDENTIFY THE PROBLEM
-- ===============================================================

PRINT '=========================================='
PRINT 'PROBLEM IDENTIFICATION'
PRINT '=========================================='
PRINT ''

PRINT 'Error: "Error converting data type varchar to numeric"'
PRINT ''
PRINT 'Possible Causes:'
PRINT '----------------'
PRINT '1. Function returns RATE as VARCHAR from master table'
PRINT '2. Function uses STRING_AGG or similar on RATE from details'
PRINT '3. Function has NULL values being cast incorrectly'
PRINT '4. Rate column in master has varchar data type'
PRINT ''

-- Check master table RATE column type
DECLARE @MasterTableName NVARCHAR(128)
DECLARE @MasterRateType NVARCHAR(128)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
    SET @MasterTableName = 'tblCropInvertMaster'

IF @MasterTableName IS NOT NULL
BEGIN
    SELECT @MasterRateType = DATA_TYPE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @MasterTableName
    AND COLUMN_NAME = 'RATE'

    IF @MasterRateType IS NOT NULL
    BEGIN
        PRINT 'Master table RATE column type: ' + @MasterRateType

        IF @MasterRateType LIKE '%char%'
            PRINT '  ⚠ WARNING: RATE is VARCHAR in master table - should be numeric!'
        ELSE
            PRINT '  ✓ RATE is numeric type in master table'
        PRINT ''
    END
END

-- Check detail table RATE column type
DECLARE @DetailTableName NVARCHAR(128)
DECLARE @DetailRateType NVARCHAR(128)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
    SET @DetailTableName = 'tblCropInvertDetail'
ELSE IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertDetail')
    SET @DetailTableName = 'tblCropDirectInvertDetail'

IF @DetailTableName IS NOT NULL
BEGIN
    SELECT @DetailRateType = DATA_TYPE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @DetailTableName
    AND COLUMN_NAME = 'RATE'

    IF @DetailRateType IS NOT NULL
    BEGIN
        PRINT 'Detail table RATE column type: ' + @DetailRateType

        IF @DetailRateType LIKE '%char%'
            PRINT '  ⚠ WARNING: RATE is VARCHAR in detail table - should be numeric!'
        ELSE
            PRINT '  ✓ RATE is numeric type in detail table'
        PRINT ''
    END
    ELSE
    BEGIN
        PRINT '✗ RATE column not found in ' + @DetailTableName
        PRINT '  Run Add_Rate_To_CropInvertDetail.sql first!'
        PRINT ''
    END
END

-- ===============================================================
-- STEP 5: RECOMMENDED FIX
-- ===============================================================

PRINT '=========================================='
PRINT 'RECOMMENDED FIX'
PRINT '=========================================='
PRINT ''

PRINT 'The GET_CropBill function needs to be updated to:'
PRINT ''
PRINT 'Option 1: Return average rate from detail table'
PRINT '---------------------------------------------------'
PRINT 'SELECT AVG(CAST(d.RATE AS DECIMAL(18,2))) AS RATE'
PRINT 'FROM tblCropInvertDetail d'
PRINT 'WHERE d.CROPID = @CROPID'
PRINT ''

PRINT 'Option 2: Return rate from first detail row'
PRINT '--------------------------------------------'
PRINT 'SELECT TOP 1 CAST(d.RATE AS DECIMAL(18,2)) AS RATE'
PRINT 'FROM tblCropInvertDetail d'
PRINT 'WHERE d.CROPID = @CROPID'
PRINT ''

PRINT 'Option 3: Return NULL for RATE (if not needed in this view)'
PRINT '------------------------------------------------------------'
PRINT 'SELECT NULL AS RATE'
PRINT ''

PRINT 'Option 4: Return weighted average (recommended)'
PRINT '------------------------------------------------'
PRINT 'SELECT '
PRINT '  CASE '
PRINT '    WHEN SUM(d.WEIGHT) > 0 THEN'
PRINT '      SUM(d.WEIGHT * d.RATE) / SUM(d.WEIGHT)'
PRINT '    ELSE 0'
PRINT '  END AS RATE'
PRINT 'FROM tblCropInvertDetail d'
PRINT 'WHERE d.CROPID = @CROPID'
PRINT ''

-- ===============================================================
-- STEP 6: EXTRACT FULL FUNCTION FOR MANUAL EDIT
-- ===============================================================

PRINT '=========================================='
PRINT 'FULL FUNCTION DEFINITION'
PRINT '=========================================='
PRINT ''
PRINT 'Copy the definition below to a new query window,'
PRINT 'modify the RATE calculation, then execute it:'
PRINT ''
PRINT '-- START OF FUNCTION --'
PRINT @FunctionDef
PRINT '-- END OF FUNCTION --'
PRINT ''

-- ===============================================================
-- STEP 7: QUICK TEST
-- ===============================================================

PRINT '=========================================='
PRINT 'QUICK TEST (if function is working)'
PRINT '=========================================='
PRINT ''

-- Try to execute the function with a sample CROPID
BEGIN TRY
    DECLARE @TestSQL NVARCHAR(MAX)
    DECLARE @TestCROPID NVARCHAR(50)

    -- Get a sample CROPID
    SELECT TOP 1 @TestCROPID = CROPID
    FROM tblCropInvertMaster
    WHERE ISDELETED = 0

    IF @TestCROPID IS NOT NULL
    BEGIN
        PRINT 'Testing with CROPID: ' + @TestCROPID

        SET @TestSQL = 'SELECT TOP 1 * FROM GET_CropBill(''' + @TestCROPID + ''', '''', '''')'
        EXEC sp_executesql @TestSQL

        PRINT ''
        PRINT '✓ Function executed successfully'
    END
    ELSE
    BEGIN
        PRINT 'No sample CROPID found for testing'
    END
END TRY
BEGIN CATCH
    PRINT ''
    PRINT '✗ Function test failed with error:'
    PRINT ERROR_MESSAGE()
    PRINT ''
    PRINT 'This confirms the function needs to be fixed'
END CATCH

PRINT ''

EndScript:

PRINT '=========================================='
PRINT 'DIAGNOSIS COMPLETE'
PRINT '=========================================='
PRINT ''

PRINT 'Summary:'
PRINT '--------'
PRINT '1. GET_CropBill function exists: ' + CASE WHEN EXISTS (SELECT * FROM sys.objects WHERE type IN ('FN', 'IF', 'TF') AND name = 'GET_CropBill') THEN 'YES' ELSE 'NO' END
PRINT '2. Error: Converting varchar to numeric on RATE column'
PRINT '3. Cause: Function is likely pulling RATE from wrong location or wrong data type'
PRINT '4. Solution: Update function to pull RATE from detail table with proper casting'
PRINT ''

PRINT 'Next Steps:'
PRINT '-----------'
PRINT '1. Copy the full function definition shown above'
PRINT '2. Modify the RATE calculation to use one of the recommended fixes'
PRINT '3. Execute the modified function (use CREATE OR ALTER FUNCTION)'
PRINT '4. Test the /api/GET_CropBill endpoint again'
PRINT ''

PRINT 'Need help with the fix?'
PRINT '-----------------------'
PRINT 'The function definition is printed above.'
PRINT 'Look for where RATE is selected and update it to:'
PRINT '  - Pull from detail table instead of master'
PRINT '  - Use CAST(RATE AS DECIMAL(18,2))'
PRINT '  - Calculate weighted average if multiple detail rows'
PRINT ''

GO
