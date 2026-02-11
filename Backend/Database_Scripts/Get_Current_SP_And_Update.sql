-- =============================================
-- STEP 1: Get current SP_AddUpdAllTransections from database
-- STEP 2: Show what it currently has
-- STEP 3: Update it to add PAYMENTDT parameter
-- =============================================

USE [InventoryManagement]
GO

PRINT ''
PRINT '=========================================='
PRINT 'STEP 1: Getting current stored procedure'
PRINT '=========================================='
PRINT ''

-- Get the existing stored procedure definition
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    -- Show current definition
    PRINT 'Current SP_AddUpdAllTransections definition:'
    PRINT '---------------------------------------------'

    SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdAllTransections')) AS [Current_SP_Definition]

    PRINT ''
    PRINT '=========================================='
    PRINT 'STEP 2: Current parameters in database:'
    PRINT '=========================================='
    PRINT ''

    -- Show current parameters
    SELECT
        ORDINAL_POSITION AS [Order],
        PARAMETER_NAME AS [Parameter],
        DATA_TYPE AS [Type],
        CASE
            WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
            THEN DATA_TYPE + '(' + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) + ')'
            ELSE DATA_TYPE
        END AS [Full_Type],
        CASE WHEN IS_NULLABLE = 'YES' THEN 'NULL' ELSE 'NOT NULL' END AS [Nullable]
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    ORDER BY ORDINAL_POSITION

    PRINT ''

    -- Check if PAYMENTDT already exists
    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
        WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
        AND PARAMETER_NAME = '@PAYMENTDT'
    )
    BEGIN
        PRINT '✓ @PAYMENTDT parameter ALREADY EXISTS in database!'
        PRINT 'No update needed.'
        PRINT ''
    END
    ELSE
    BEGIN
        PRINT '✗ @PAYMENTDT parameter NOT FOUND in database'
        PRINT 'Proceeding with update...'
        PRINT ''

        -- Drop the existing procedure
        DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]
        PRINT '✓ Existing procedure dropped'
        PRINT ''
    END
END
ELSE
BEGIN
    PRINT '✗ SP_AddUpdAllTransections NOT FOUND in database'
    PRINT 'Will create new procedure...'
    PRINT ''
END

GO

PRINT '=========================================='
PRINT 'STEP 3: Creating updated procedure'
PRINT '=========================================='
PRINT ''

-- =============================================
-- Create the updated stored procedure
-- Based on the existing one + PAYMENTDT parameter
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
    @PAYMENTDT DATETIME = NULL  -- NEW PARAMETER ADDED
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
                PAYMENTDT = @PAYMENTDT,  -- UPDATE PAYMENT DATE
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
                CROPID, PAYMENTDT, STATUS  -- INCLUDE PAYMENT DATE
            )
            VALUES (
                @TRAID, @TRNSR, @TRNSID, @TRNTID, @TRNDT, @REFAID, @MAID,
                @PAMT, @CBAL, @ADVAMT, @ASID, @ISDELETED, @COMPANYID, @DEPTID,
                @CROPID, @PAYMENTDT, 0  -- INSERT PAYMENT DATE
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

PRINT '✓ Stored procedure created/updated successfully'
PRINT ''

-- =============================================
-- VERIFICATION: Show updated parameters
-- =============================================

PRINT '=========================================='
PRINT 'VERIFICATION: Updated parameters'
PRINT '=========================================='
PRINT ''

SELECT
    ORDINAL_POSITION AS [Order],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type],
    CASE
        WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
        THEN DATA_TYPE + '(' + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) + ')'
        ELSE DATA_TYPE
    END AS [Full_Type],
    CASE WHEN PARAMETER_NAME = '@PAYMENTDT' THEN '← NEW PARAMETER' ELSE '' END AS [Status]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION

PRINT ''

DECLARE @ParamCount INT
SELECT @ParamCount = COUNT(*)
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'

PRINT 'Total parameters: ' + CAST(@ParamCount AS VARCHAR)
PRINT ''

-- =============================================
-- BONUS: Export the updated SP definition
-- =============================================

PRINT '=========================================='
PRINT 'UPDATED SP Definition (for reference):'
PRINT '=========================================='
PRINT ''

SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdAllTransections')) AS [Updated_SP_Definition]

PRINT ''
PRINT '=========================================='
PRINT '✓ UPDATE COMPLETE!'
PRINT '=========================================='
PRINT ''
PRINT 'Changes made:'
PRINT '  • Added @PAYMENTDT DATETIME parameter'
PRINT '  • Updated INSERT statement to include PAYMENTDT column'
PRINT '  • Updated UPDATE statement to set PAYMENTDT column'
PRINT ''
PRINT 'Next steps:'
PRINT '  1. Restart backend server (Ctrl+C then dotnet run)'
PRINT '  2. Test payment functionality'
PRINT '  3. Verify dates are saved in database'
PRINT ''
