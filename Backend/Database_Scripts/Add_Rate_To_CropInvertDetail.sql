-- ===============================================================
-- ADD RATE COLUMN TO CROP INVERT DETAIL
-- This script updates SP_AddUpdCropInvertDetail to include RATE
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'STEP 1: CHECKING CURRENT SP'
PRINT '=========================================='
PRINT ''

-- Check if SP exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdCropInvertDetail')
BEGIN
    PRINT '✗ ERROR: SP_AddUpdCropInvertDetail does not exist in database!'
    PRINT 'Cannot update SP that does not exist.'
    GOTO EndScript
END

PRINT '✓ SP_AddUpdCropInvertDetail found in database'
PRINT ''

-- Show current parameters
PRINT 'Current Parameters:'
PRINT '------------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
ORDER BY ORDINAL_POSITION

PRINT ''

-- ===============================================================
-- STEP 2: CHECK IF RATE PARAMETER EXISTS
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 2: CHECKING FOR @RATE PARAMETER'
PRINT '=========================================='
PRINT ''

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
    AND PARAMETER_NAME = '@RATE'
)
BEGIN
    PRINT '✓ @RATE parameter ALREADY EXISTS'
    PRINT 'No modification needed.'
    PRINT ''
    GOTO EndScript
END

PRINT '✗ @RATE parameter NOT FOUND'
PRINT 'Will add @RATE parameter to stored procedure...'
PRINT ''

-- ===============================================================
-- STEP 3: DETECT TABLE NAME AND CHECK RATE COLUMN
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 3: DETECTING TABLE AND CHECKING RATE'
PRINT '=========================================='
PRINT ''

-- Auto-detect which table name is actually used
DECLARE @DetailTableName NVARCHAR(128)
DECLARE @SQL NVARCHAR(MAX)

-- Check which detail table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
BEGIN
    SET @DetailTableName = 'tblCropInvertDetail'
    PRINT '✓ Found table: tblCropInvertDetail'
END
ELSE IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropDirectInvertDetail')
BEGIN
    SET @DetailTableName = 'tblCropDirectInvertDetail'
    PRINT '✓ Found table: tblCropDirectInvertDetail'
END
ELSE
BEGIN
    PRINT '✗ ERROR: No detail table found!'
    PRINT 'Expected: tblCropInvertDetail or tblCropDirectInvertDetail'
    GOTO EndScript
END

PRINT ''

-- Check if RATE column exists in the detected table
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @DetailTableName
    AND COLUMN_NAME = 'RATE'
)
BEGIN
    PRINT '⚠ RATE column does NOT exist in ' + @DetailTableName
    PRINT 'Adding RATE column to table...'

    SET @SQL = 'ALTER TABLE ' + @DetailTableName + ' ADD RATE DECIMAL(18,2) NULL'
    EXEC sp_executesql @SQL

    PRINT '✓ RATE column added to ' + @DetailTableName
    PRINT ''
END
ELSE
BEGIN
    PRINT '✓ RATE column already exists in ' + @DetailTableName
    PRINT ''
END

-- ===============================================================
-- STEP 4: DROP AND RECREATE SP
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 4: UPDATING STORED PROCEDURE'
PRINT '=========================================='
PRINT ''

-- Drop existing procedure
DROP PROCEDURE [dbo].[SP_AddUpdCropInvertDetail]
PRINT '✓ Old SP dropped'

GO

-- Create updated procedure
CREATE PROCEDURE [dbo].[SP_AddUpdCropInvertDetail]
    @CDAID VARCHAR(50),
    @CROPID VARCHAR(50),
    @SHSDATE VARCHAR(50),
    @VEHNO VARCHAR(50),
    @SHDAID VARCHAR(50),
    @PACKER VARCHAR(50),
    @WORKER VARCHAR(50),
    @PSIZE VARCHAR(50),
    @PQUNTITY VARCHAR(50),
    @CROPINVERT VARCHAR(50),
    @CRATECOUNT VARCHAR(50),
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @SHAID VARCHAR(50),
    @CGRADE VARCHAR(50),
    @VDAID VARCHAR(50),
    @WEIGHT DECIMAL(18,2),
    @RATE DECIMAL(18,2) = NULL,  -- ← NEW PARAMETER
    @ISDELETED BIT,
    @ISCOMPLETED BIT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsSuccessful INT = 0
    DECLARE @ResponseCode VARCHAR(50) = 'ERROR'
    DECLARE @ResponseMessage VARCHAR(500) = ''
    DECLARE @ResponseValues VARCHAR(500) = ''

    BEGIN TRY
        -- Check if record exists (UPDATE scenario)
        IF EXISTS (SELECT 1 FROM tblCropDirectInvertDetail WHERE CDAID = @CDAID AND ISDELETED = 0)
        BEGIN
            -- Update existing record
            UPDATE tblCropDirectInvertDetail
            SET
                CROPID = @CROPID,
                SHSDATE = @SHSDATE,
                VEHNO = @VEHNO,
                SHDAID = @SHDAID,
                PACKER = @PACKER,
                WORKER = @WORKER,
                PSIZE = @PSIZE,
                PQUNTITY = @PQUNTITY,
                CROPINVERT = @CROPINVERT,
                CRATECOUNT = @CRATECOUNT,
                SHAID = @SHAID,
                CGRADE = @CGRADE,
                VDAID = @VDAID,
                WEIGHT = @WEIGHT,
                RATE = @RATE,  -- ← ADDED
                ISDELETED = @ISDELETED,
                ISCOMPLETED = @ISCOMPLETED
            WHERE CDAID = @CDAID

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Crop invert detail updated successfully'
            SET @ResponseValues = @CDAID
        END
        ELSE
        BEGIN
            -- Insert new record
            INSERT INTO tblCropDirectInvertDetail (
                CDAID, CROPID, SHSDATE, VEHNO, SHDAID, PACKER, WORKER,
                PSIZE, PQUNTITY, CROPINVERT, CRATECOUNT, COMPANYID, DEPTID,
                SHAID, CGRADE, VDAID, WEIGHT, RATE, ISDELETED, ISCOMPLETED  -- ← ADDED RATE
            )
            VALUES (
                @CDAID, @CROPID, @SHSDATE, @VEHNO, @SHDAID, @PACKER, @WORKER,
                @PSIZE, @PQUNTITY, @CROPINVERT, @CRATECOUNT, @COMPANYID, @DEPTID,
                @SHAID, @CGRADE, @VDAID, @WEIGHT, @RATE, @ISDELETED, @ISCOMPLETED  -- ← ADDED @RATE
            )

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Crop invert detail created successfully'
            SET @ResponseValues = @CDAID
        END
    END TRY
    BEGIN CATCH
        SET @IsSuccessful = 0
        SET @ResponseCode = 'ERROR'
        SET @ResponseMessage = ERROR_MESSAGE()
        SET @ResponseValues = ''
    END CATCH

    -- Return response
    SELECT
        @IsSuccessful AS IsSuccessful,
        @ResponseCode AS ResponseCode,
        @ResponseMessage AS ResponseMessage,
        @ResponseValues AS ResponseValues
END
GO

PRINT '✓ Updated SP created successfully with @RATE parameter'
PRINT ''

-- ===============================================================
-- STEP 5: VERIFY
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 5: VERIFICATION'
PRINT '=========================================='
PRINT ''

PRINT 'Updated Parameters:'
PRINT '------------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type],
    CASE WHEN PARAMETER_NAME = '@RATE' THEN '← NEWLY ADDED' ELSE '' END AS [Status]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
ORDER BY ORDINAL_POSITION

-- Verify RATE was added
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdCropInvertDetail'
    AND PARAMETER_NAME = '@RATE'
)
BEGIN
    PRINT ''
    PRINT '✓ VERIFICATION PASSED: @RATE parameter exists'
END
ELSE
BEGIN
    PRINT ''
    PRINT '✗ VERIFICATION FAILED: @RATE parameter not found!'
END

PRINT ''
PRINT '=========================================='
PRINT '✓ UPDATE COMPLETE!'
PRINT '=========================================='
PRINT ''
PRINT 'Changes Made:'
PRINT '• Added @RATE DECIMAL(18,2) = NULL parameter'
PRINT '• Added RATE column to table (if not existed)'
PRINT '• Added RATE to UPDATE statement'
PRINT '• Added RATE to INSERT statement'
PRINT ''

EndScript:

PRINT 'Next Steps:'
PRINT '1. Restart backend server (Ctrl+C, then dotnet run)'
PRINT '2. Test Direct Inward functionality'
PRINT '3. Verify Rate saves correctly in detail table'
PRINT ''

GO
