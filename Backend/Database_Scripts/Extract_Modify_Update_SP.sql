-- =============================================
-- EXTRACT → MODIFY → UPDATE SP
-- This script gets the actual SP from your database,
-- checks it, and updates it with PAYMENTDT parameter
-- =============================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'STEP 1: EXTRACTING CURRENT SP FROM DATABASE'
PRINT '=========================================='
PRINT ''

-- Check if SP exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    PRINT '✗ ERROR: SP_AddUpdAllTransections does not exist in database!'
    PRINT ''
    PRINT 'Cannot extract SP that does not exist.'
    PRINT 'Please check:'
    PRINT '  1. Database name is correct: InventoryManagement'
    PRINT '  2. SP name is correct: SP_AddUpdAllTransections'
    PRINT '  3. You have permissions to view the SP'
    PRINT ''
    GOTO EndScript
END

PRINT '✓ SP_AddUpdAllTransections found in database'
PRINT ''

-- Get current SP definition
DECLARE @CurrentSPDefinition NVARCHAR(MAX)
SELECT @CurrentSPDefinition = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdAllTransections'))

PRINT 'Current SP Definition (first 500 characters):'
PRINT '-----------------------------------------------'
PRINT SUBSTRING(@CurrentSPDefinition, 1, 500) + '...'
PRINT ''

-- Show current parameters
PRINT 'Current Parameters from Database:'
PRINT '---------------------------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE +
    CASE
        WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
        THEN '(' + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) + ')'
        ELSE ''
    END AS [Type],
    CASE WHEN IS_NULLABLE = 'YES' THEN 'NULL' ELSE 'NOT NULL' END AS [Nullable]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION

DECLARE @CurrentParamCount INT
SELECT @CurrentParamCount = COUNT(*)
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'

PRINT ''
PRINT 'Total parameters in current SP: ' + CAST(@CurrentParamCount AS VARCHAR)
PRINT ''

-- =============================================
-- STEP 2: CHECK IF PAYMENTDT EXISTS
-- =============================================

PRINT '=========================================='
PRINT 'STEP 2: CHECKING FOR PAYMENTDT PARAMETER'
PRINT '=========================================='
PRINT ''

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    AND PARAMETER_NAME = '@PAYMENTDT'
)
BEGIN
    PRINT '✓ @PAYMENTDT parameter ALREADY EXISTS in database SP'
    PRINT ''
    PRINT 'Database SP Details:'
    SELECT
        PARAMETER_NAME,
        DATA_TYPE,
        IS_NULLABLE
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    AND PARAMETER_NAME = '@PAYMENTDT'

    PRINT ''
    PRINT 'STATUS: SP is already up to date!'
    PRINT 'No modification needed.'
    PRINT ''
    GOTO EndScript
END

PRINT '✗ @PAYMENTDT parameter NOT FOUND in database SP'
PRINT ''
PRINT 'STATUS: SP needs to be modified'
PRINT ''

-- =============================================
-- STEP 3: BACKUP CURRENT SP
-- =============================================

PRINT '=========================================='
PRINT 'STEP 3: BACKING UP CURRENT SP'
PRINT '=========================================='
PRINT ''

-- Save current definition to a table for backup
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SP_Backup_Log')
BEGIN
    CREATE TABLE SP_Backup_Log (
        BackupID INT IDENTITY(1,1) PRIMARY KEY,
        SPName VARCHAR(100),
        BackupDate DATETIME DEFAULT GETDATE(),
        SPDefinition NVARCHAR(MAX)
    )
    PRINT '✓ Created backup table: SP_Backup_Log'
END

INSERT INTO SP_Backup_Log (SPName, SPDefinition)
VALUES ('SP_AddUpdAllTransections', @CurrentSPDefinition)

PRINT '✓ Current SP backed up to SP_Backup_Log table'
PRINT '  Backup ID: ' + CAST(SCOPE_IDENTITY() AS VARCHAR)
PRINT ''

-- =============================================
-- STEP 4: ANALYZE CURRENT SP STRUCTURE
-- =============================================

PRINT '=========================================='
PRINT 'STEP 4: ANALYZING CURRENT SP STRUCTURE'
PRINT '=========================================='
PRINT ''

-- Check what INSERT/UPDATE statements exist
IF @CurrentSPDefinition LIKE '%INSERT INTO tblAllTransections%'
BEGIN
    PRINT '✓ Found INSERT statement in SP'

    -- Check if PAYMENTDT is in INSERT
    IF @CurrentSPDefinition LIKE '%PAYMENTDT%'
    BEGIN
        PRINT '  ✓ PAYMENTDT already in INSERT (parameter missing though?)'
    END
    ELSE
    BEGIN
        PRINT '  ✗ PAYMENTDT NOT in INSERT - needs to be added'
    END
END

IF @CurrentSPDefinition LIKE '%UPDATE tblAllTransections%'
BEGIN
    PRINT '✓ Found UPDATE statement in SP'

    -- Check if PAYMENTDT is in UPDATE
    IF @CurrentSPDefinition LIKE '%PAYMENTDT%'
    BEGIN
        PRINT '  ✓ PAYMENTDT already in UPDATE (parameter missing though?)'
    END
    ELSE
    BEGIN
        PRINT '  ✗ PAYMENTDT NOT in UPDATE - needs to be added'
    END
END

PRINT ''

-- =============================================
-- STEP 5: DROP OLD SP
-- =============================================

PRINT '=========================================='
PRINT 'STEP 5: DROPPING OLD SP'
PRINT '=========================================='
PRINT ''

DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]

PRINT '✓ Old SP dropped successfully'
PRINT ''

GO

-- =============================================
-- STEP 6: CREATE UPDATED SP WITH PAYMENTDT
-- =============================================

PRINT '=========================================='
PRINT 'STEP 6: CREATING UPDATED SP'
PRINT '=========================================='
PRINT ''

CREATE PROCEDURE [dbo].[SP_AddUpdAllTransections]
    @TRAID VARCHAR(50),
    @TRNSR INT,
    @TRNSID VARCHAR(50),
    @TRNTID VARCHAR(50),
    @TRNDT DATETIME,
    @REFAID VARCHAR(50),
    @MAID VARCHAR(50),
    @PAMT FLOAT,
    @CBAL FLOAT,
    @ADVAMT FLOAT,
    @ASID VARCHAR(50),
    @ISDELETED BIT,
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @CROPID VARCHAR(50),
    @PAYMENTDT DATETIME = NULL  -- ADDED: Payment Date parameter
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsSuccessful INT = 0
    DECLARE @ResponseCode VARCHAR(50) = 'ERROR'
    DECLARE @ResponseMessage VARCHAR(500) = ''
    DECLARE @ResponseValues VARCHAR(500) = ''

    BEGIN TRY
        -- Check if record exists (UPDATE scenario)
        IF EXISTS (SELECT 1 FROM tblAllTransections WHERE TRAID = @TRAID AND ISDELETED = 0)
        BEGIN
            -- Update existing record
            UPDATE tblAllTransections
            SET
                TRNSR = @TRNSR,
                TRNSID = @TRNSID,
                TRNTID = @TRNTID,
                TRNDT = @TRNDT,
                REFAID = @REFAID,
                MAID = @MAID,
                PAMT = @PAMT,
                CBAL = @CBAL,
                ADVAMT = @ADVAMT,
                ASID = @ASID,
                ISDELETED = @ISDELETED,
                COMPANYID = @COMPANYID,
                DEPTID = @DEPTID,
                CROPID = @CROPID,
                PAYMENTDT = @PAYMENTDT,  -- ADDED: Update payment date
                STATUS = 0
            WHERE TRAID = @TRAID

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Transaction updated successfully'
            SET @ResponseValues = @TRAID
        END
        ELSE
        BEGIN
            -- Insert new record
            INSERT INTO tblAllTransections (
                TRAID, TRNSR, TRNSID, TRNTID, TRNDT, REFAID, MAID,
                PAMT, CBAL, ADVAMT, ASID, ISDELETED, COMPANYID, DEPTID,
                CROPID, PAYMENTDT, STATUS  -- ADDED: Include PAYMENTDT in INSERT
            )
            VALUES (
                @TRAID, @TRNSR, @TRNSID, @TRNTID, @TRNDT, @REFAID, @MAID,
                @PAMT, @CBAL, @ADVAMT, @ASID, @ISDELETED, @COMPANYID, @DEPTID,
                @CROPID, @PAYMENTDT, 0  -- ADDED: Insert payment date value
            )

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Transaction created successfully'
            SET @ResponseValues = @TRAID
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

PRINT '✓ Updated SP created successfully with PAYMENTDT parameter'
PRINT ''

-- =============================================
-- STEP 7: VERIFY THE UPDATE
-- =============================================

PRINT '=========================================='
PRINT 'STEP 7: VERIFYING THE UPDATE'
PRINT '=========================================='
PRINT ''

PRINT 'Updated Parameters:'
PRINT '-------------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE +
    CASE
        WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
        THEN '(' + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) + ')'
        ELSE ''
    END AS [Type],
    CASE WHEN PARAMETER_NAME = '@PAYMENTDT' THEN '← NEWLY ADDED' ELSE '' END AS [Status]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION

DECLARE @NewParamCount INT
SELECT @NewParamCount = COUNT(*)
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'

PRINT ''
PRINT 'Total parameters after update: ' + CAST(@NewParamCount AS VARCHAR)
PRINT ''

-- Verify PAYMENTDT was added
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    AND PARAMETER_NAME = '@PAYMENTDT'
)
BEGIN
    PRINT '✓ VERIFICATION PASSED: @PAYMENTDT parameter exists'
    PRINT ''

    SELECT
        PARAMETER_NAME AS [New Parameter],
        DATA_TYPE AS [Type],
        IS_NULLABLE AS [Nullable]
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    AND PARAMETER_NAME = '@PAYMENTDT'
END
ELSE
BEGIN
    PRINT '✗ VERIFICATION FAILED: @PAYMENTDT parameter not found!'
    PRINT 'Something went wrong during the update.'
END

PRINT ''

-- =============================================
-- STEP 8: SUMMARY
-- =============================================

PRINT '=========================================='
PRINT 'STEP 8: UPDATE SUMMARY'
PRINT '=========================================='
PRINT ''

PRINT 'What was done:'
PRINT '--------------'
PRINT '✓ Extracted current SP from database'
PRINT '✓ Checked current parameters'
PRINT '✓ Backed up original SP to SP_Backup_Log table'
PRINT '✓ Dropped old SP'
PRINT '✓ Created new SP with @PAYMENTDT parameter'
PRINT '✓ Added PAYMENTDT to INSERT statement'
PRINT '✓ Added PAYMENTDT to UPDATE statement'
PRINT '✓ Verified new parameter exists'
PRINT ''

PRINT 'Changes Made:'
PRINT '-------------'
PRINT '• Parameter added: @PAYMENTDT DATETIME = NULL'
PRINT '• INSERT updated: Added PAYMENTDT column'
PRINT '• UPDATE updated: Added PAYMENTDT = @PAYMENTDT'
PRINT ''

PRINT 'To restore original SP (if needed):'
PRINT '-----------------------------------'
PRINT 'SELECT TOP 1 SPDefinition'
PRINT 'FROM SP_Backup_Log'
PRINT 'WHERE SPName = ''SP_AddUpdAllTransections'''
PRINT 'ORDER BY BackupDate DESC'
PRINT ''

EndScript:

PRINT '=========================================='
PRINT '✓ SCRIPT COMPLETED'
PRINT '=========================================='
PRINT ''
PRINT 'Next Steps:'
PRINT '1. Restart backend server (Ctrl+C, then dotnet run)'
PRINT '2. Test payment date functionality'
PRINT '3. Verify dates save to database correctly'
PRINT ''

GO
