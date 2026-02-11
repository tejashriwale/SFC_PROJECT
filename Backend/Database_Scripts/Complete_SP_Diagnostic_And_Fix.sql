-- =============================================
-- COMPLETE DIAGNOSTIC AND FIX SCRIPT
-- Checks current SP, shows parameters, and updates it
-- =============================================

USE [InventoryManagement]
GO

PRINT ''
PRINT '=========================================='
PRINT '  DIAGNOSTIC: SP_AddUpdAllTransections'
PRINT '=========================================='
PRINT ''

-- =============================================
-- STEP 1: Check if stored procedure exists
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    PRINT '✓ Stored procedure EXISTS'
    PRINT ''

    -- =============================================
    -- STEP 2: Show current parameters
    -- =============================================
    PRINT 'Current Parameters:'
    PRINT '-------------------'

    SELECT
        ORDINAL_POSITION AS [#],
        PARAMETER_NAME AS [Parameter],
        DATA_TYPE AS [Type],
        CHARACTER_MAXIMUM_LENGTH AS [Length]
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    ORDER BY ORDINAL_POSITION

    PRINT ''

    -- =============================================
    -- STEP 3: Check if PAYMENTDT parameter exists
    -- =============================================
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
        WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
        AND PARAMETER_NAME = '@PAYMENTDT'
    )
    BEGIN
        PRINT '✓ @PAYMENTDT parameter ALREADY EXISTS'
        PRINT ''
        PRINT 'No update needed! Stored procedure is already correct.'
        PRINT ''
    END
    ELSE
    BEGIN
        PRINT '✗ @PAYMENTDT parameter MISSING'
        PRINT ''
        PRINT 'Updating stored procedure...'
        PRINT ''

        -- Drop and recreate with PAYMENTDT parameter
        DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]
        PRINT 'Old procedure dropped.'
        PRINT ''
    END
END
ELSE
BEGIN
    PRINT '✗ Stored procedure DOES NOT EXIST'
    PRINT ''
    PRINT 'Creating new stored procedure...'
    PRINT ''
END

GO

-- =============================================
-- STEP 4: Create/Update the stored procedure
-- =============================================

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
    @PAYMENTDT DATETIME = NULL  -- Payment Date Parameter
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsSuccessful INT = 0
    DECLARE @ResponseCode VARCHAR(50) = 'ERROR'
    DECLARE @ResponseMessage VARCHAR(500) = ''
    DECLARE @ResponseValues VARCHAR(500) = ''

    BEGIN TRY
        -- Check if record exists
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
                PAYMENTDT = @PAYMENTDT,  -- Update payment date
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
                CROPID, PAYMENTDT, STATUS
            )
            VALUES (
                @TRAID, @TRNSR, @TRNSID, @TRNTID, @TRNDT, @REFAID, @MAID,
                @PAMT, @CBAL, @ADVAMT, @ASID, @ISDELETED, @COMPANYID, @DEPTID,
                @CROPID, @PAYMENTDT, 0
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

PRINT ''
PRINT '=========================================='
PRINT '  ✓ STORED PROCEDURE UPDATED SUCCESSFULLY'
PRINT '=========================================='
PRINT ''

-- =============================================
-- STEP 5: Verify the update
-- =============================================
PRINT 'Verification:'
PRINT '-------------'

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type],
    CASE
        WHEN PARAMETER_NAME = '@PAYMENTDT' THEN '← NEW!'
        ELSE ''
    END AS [Status]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION

PRINT ''
PRINT 'Total Parameters: ' + CAST((SELECT COUNT(*) FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections') AS VARCHAR)
PRINT ''

-- =============================================
-- STEP 6: Check PAYMENTDT column in table
-- =============================================
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblAllTransections'
    AND COLUMN_NAME = 'PAYMENTDT'
)
BEGIN
    PRINT '✓ PAYMENTDT column exists in tblAllTransections table'
END
ELSE
BEGIN
    PRINT '✗ WARNING: PAYMENTDT column MISSING in tblAllTransections table'
    PRINT ''
    PRINT 'Run this command to add it:'
    PRINT 'ALTER TABLE tblAllTransections ADD PAYMENTDT DATETIME NULL'
    PRINT ''
END

PRINT ''
PRINT '=========================================='
PRINT '  SETUP COMPLETE!'
PRINT '=========================================='
PRINT ''
PRINT 'Next steps:'
PRINT '  1. Restart your backend server'
PRINT '  2. Test payment date functionality'
PRINT '  3. Check that dates are saved correctly'
PRINT ''
