-- ===============================================================
-- CHECK DIRECT INWARD TABLES AND COLUMNS
-- This script shows what tables are used and if RATE exists
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'DIRECT INWARD TABLE STRUCTURE CHECK'
PRINT '=========================================='
PRINT ''

-- ===============================================================
-- STEP 1: CHECK STORED PROCEDURES
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 1: STORED PROCEDURES'
PRINT '=========================================='
PRINT ''

-- Check if SPs exist
PRINT 'Checking stored procedures...'
PRINT ''

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdCropInvertMaster')
    PRINT '✓ SP_AddUpdCropInvertMaster exists'
ELSE
    PRINT '✗ SP_AddUpdCropInvertMaster NOT FOUND'

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdCropInvertDetail')
    PRINT '✓ SP_AddUpdCropInvertDetail exists'
ELSE
    PRINT '✗ SP_AddUpdCropInvertDetail NOT FOUND'

PRINT ''

-- ===============================================================
-- STEP 2: FIND TABLE NAMES FROM STORED PROCEDURES
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 2: EXTRACTING TABLE NAMES FROM SPs'
PRINT '=========================================='
PRINT ''

-- Get Master SP definition
PRINT 'Master Stored Procedure:'
PRINT '------------------------'
DECLARE @MasterSP NVARCHAR(MAX)
SELECT @MasterSP = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdCropInvertMaster'))

-- Find table name in Master SP (look for INSERT INTO or UPDATE)
IF @MasterSP LIKE '%INSERT INTO%tblCropInvertMaster%'
    PRINT 'Table used in Master SP: tblCropInvertMaster'
ELSE IF @MasterSP LIKE '%INSERT INTO%tblCropDirectInvertMaster%'
    PRINT 'Table used in Master SP: tblCropDirectInvertMaster'
ELSE IF @MasterSP LIKE '%INSERT INTO%'
BEGIN
    -- Extract table name from INSERT statement
    DECLARE @StartPos INT = CHARINDEX('INSERT INTO', @MasterSP)
    DECLARE @TableSection NVARCHAR(200) = SUBSTRING(@MasterSP, @StartPos, 200)
    PRINT 'INSERT statement found:'
    PRINT SUBSTRING(@TableSection, 1, 100)
END
ELSE
    PRINT 'Could not find INSERT statement in Master SP'

PRINT ''

-- Get Detail SP definition
PRINT 'Detail Stored Procedure:'
PRINT '------------------------'
DECLARE @DetailSP NVARCHAR(MAX)
SELECT @DetailSP = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdCropInvertDetail'))

-- Find table name in Detail SP
IF @DetailSP LIKE '%INSERT INTO%tblCropInvertDetail%'
    PRINT 'Table used in Detail SP: tblCropInvertDetail'
ELSE IF @DetailSP LIKE '%INSERT INTO%tblCropDirectInvertDetail%'
    PRINT 'Table used in Detail SP: tblCropDirectInvertDetail'
ELSE IF @DetailSP LIKE '%INSERT INTO%'
BEGIN
    -- Extract table name from INSERT statement
    DECLARE @StartPos2 INT = CHARINDEX('INSERT INTO', @DetailSP)
    DECLARE @TableSection2 NVARCHAR(200) = SUBSTRING(@DetailSP, @StartPos2, 200)
    PRINT 'INSERT statement found:'
    PRINT SUBSTRING(@TableSection2, 1, 100)
END
ELSE
    PRINT 'Could not find INSERT statement in Detail SP'

PRINT ''

-- ===============================================================
-- STEP 3: CHECK ALL CROP INVERT TABLES
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 3: ALL CROP INVERT TABLES IN DATABASE'
PRINT '=========================================='
PRINT ''

SELECT
    TABLE_NAME AS [Table Name],
    CASE
        WHEN TABLE_NAME LIKE '%Master%' THEN 'Master Table'
        WHEN TABLE_NAME LIKE '%Detail%' THEN 'Detail Table'
        ELSE 'Other'
    END AS [Type]
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME LIKE '%CropInvert%'
OR TABLE_NAME LIKE '%CropDirectInvert%'
ORDER BY TABLE_NAME

PRINT ''

-- ===============================================================
-- STEP 4: CHECK RATE COLUMN IN EACH TABLE
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 4: CHECKING RATE COLUMN IN TABLES'
PRINT '=========================================='
PRINT ''

-- Check in tblCropInvertMaster
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
BEGIN
    PRINT 'Table: tblCropInvertMaster'
    PRINT '--------------------------'
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = 'tblCropInvertMaster'
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE column EXISTS'
    ELSE
        PRINT '✗ RATE column NOT FOUND'
    PRINT ''
END

-- Check in tblCropInvertDetail
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
BEGIN
    PRINT 'Table: tblCropInvertDetail'
    PRINT '--------------------------'
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = 'tblCropInvertDetail'
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE column EXISTS'
    ELSE
        PRINT '✗ RATE column NOT FOUND - NEEDS TO BE ADDED'
    PRINT ''
END

-- Check in tblCropDirectInvertMaster
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertMaster')
BEGIN
    PRINT 'Table: tblCropDirectInvertMaster'
    PRINT '--------------------------------'
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = 'tblCropDirectInvertMaster'
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE column EXISTS'
    ELSE
        PRINT '✗ RATE column NOT FOUND'
    PRINT ''
END

-- Check in tblCropDirectInvertDetail
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertDetail')
BEGIN
    PRINT 'Table: tblCropDirectInvertDetail'
    PRINT '--------------------------------'
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = 'tblCropDirectInvertDetail'
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE column EXISTS'
    ELSE
        PRINT '✗ RATE column NOT FOUND - NEEDS TO BE ADDED'
    PRINT ''
END

-- ===============================================================
-- STEP 5: SHOW COLUMNS IN DETAIL TABLE
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 5: DETAIL TABLE COLUMNS'
PRINT '=========================================='
PRINT ''

-- Show columns for the actual detail table being used
DECLARE @DetailTableName NVARCHAR(128)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
    SET @DetailTableName = 'tblCropInvertDetail'
ELSE IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertDetail')
    SET @DetailTableName = 'tblCropDirectInvertDetail'

IF @DetailTableName IS NOT NULL
BEGIN
    PRINT 'Columns in ' + @DetailTableName + ':'
    PRINT REPLICATE('-', LEN(@DetailTableName) + 12)

    SELECT
        ORDINAL_POSITION AS [#],
        COLUMN_NAME AS [Column],
        DATA_TYPE AS [Type],
        CASE
            WHEN IS_NULLABLE = 'YES' THEN 'NULL'
            ELSE 'NOT NULL'
        END AS [Nullable],
        CASE
            WHEN COLUMN_NAME = 'RATE' THEN '← FOR RATE COLUMN'
            ELSE ''
        END AS [Notes]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @DetailTableName
    ORDER BY ORDINAL_POSITION

    PRINT ''
END
ELSE
BEGIN
    PRINT '⚠ No detail table found!'
    PRINT ''
END

-- ===============================================================
-- STEP 6: SHOW SP PARAMETERS
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 6: SP PARAMETERS'
PRINT '=========================================='
PRINT ''

PRINT 'SP_AddUpdCropInvertDetail Parameters:'
PRINT '-------------------------------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type],
    CASE
        WHEN PARAMETER_NAME = '@RATE' THEN '← RATE PARAMETER'
        ELSE ''
    END AS [Notes]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
ORDER BY ORDINAL_POSITION

PRINT ''

-- Check if @RATE exists
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
    AND PARAMETER_NAME = '@RATE'
)
    PRINT '✓ @RATE parameter EXISTS in SP'
ELSE
    PRINT '✗ @RATE parameter NOT FOUND in SP - NEEDS TO BE ADDED'

PRINT ''

-- ===============================================================
-- STEP 7: SUMMARY AND RECOMMENDATIONS
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 7: SUMMARY'
PRINT '=========================================='
PRINT ''

-- Determine the actual table names being used
DECLARE @ActualMasterTable NVARCHAR(128)
DECLARE @ActualDetailTable NVARCHAR(128)

-- Check what tables exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
    SET @ActualMasterTable = 'tblCropInvertMaster'
ELSE IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertMaster')
    SET @ActualMasterTable = 'tblCropDirectInvertMaster'

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
    SET @ActualDetailTable = 'tblCropInvertDetail'
ELSE IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertDetail')
    SET @ActualDetailTable = 'tblCropDirectInvertDetail'

PRINT 'Tables Used for Direct Inward:'
PRINT '------------------------------'
PRINT 'Master Table: ' + ISNULL(@ActualMasterTable, 'NOT FOUND')
PRINT 'Detail Table: ' + ISNULL(@ActualDetailTable, 'NOT FOUND')
PRINT ''

PRINT 'RATE Column Status:'
PRINT '-------------------'

-- Check master table
IF @ActualMasterTable IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @ActualMasterTable
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE exists in ' + @ActualMasterTable
    ELSE
        PRINT '✗ RATE NOT in ' + @ActualMasterTable
END

-- Check detail table
IF @ActualDetailTable IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @ActualDetailTable
        AND COLUMN_NAME = 'RATE'
    )
        PRINT '✓ RATE exists in ' + @ActualDetailTable
    ELSE
        PRINT '✗ RATE NOT in ' + @ActualDetailTable + ' ← NEEDS TO BE ADDED!'
END

PRINT ''

-- Check SP parameter
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
    AND PARAMETER_NAME = '@RATE'
)
    PRINT '✓ @RATE parameter exists in SP_AddUpdCropInvertDetail'
ELSE
    PRINT '✗ @RATE parameter NOT in SP_AddUpdCropInvertDetail ← NEEDS TO BE ADDED!'

PRINT ''

PRINT 'Next Steps:'
PRINT '-----------'

-- Determine what needs to be done
DECLARE @NeedsColumnAdd BIT = 0
DECLARE @NeedsSPUpdate BIT = 0

IF @ActualDetailTable IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @ActualDetailTable
        AND COLUMN_NAME = 'RATE'
    )
        SET @NeedsColumnAdd = 1
END

IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
    AND PARAMETER_NAME = '@RATE'
)
    SET @NeedsSPUpdate = 1

IF @NeedsColumnAdd = 1 OR @NeedsSPUpdate = 1
BEGIN
    PRINT '1. Run Add_Rate_To_CropInvertDetail.sql to:'

    IF @NeedsColumnAdd = 1
        PRINT '   • Add RATE column to ' + @ActualDetailTable

    IF @NeedsSPUpdate = 1
        PRINT '   • Add @RATE parameter to SP_AddUpdCropInvertDetail'

    PRINT ''
    PRINT '2. Restart backend server'
    PRINT '3. Test Direct Inward functionality'
END
ELSE
BEGIN
    PRINT '✓ All changes already applied!'
    PRINT '  No action needed.'
END

PRINT ''
PRINT '=========================================='
PRINT 'CHECK COMPLETE'
PRINT '=========================================='
PRINT ''

GO
