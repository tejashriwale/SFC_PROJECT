-- =============================================
-- QUICK FIX: Add PAYMENTDT parameter to SP_AddUpdAllTransections
-- Run this in SQL Server Management Studio
-- =============================================

USE [InventoryManagement]
GO

PRINT 'Checking stored procedure SP_AddUpdAllTransections...'
PRINT ''

-- First, let's see the current parameters
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    PRINT 'Current stored procedure found. Dropping it...'
    DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]
    PRINT 'Dropped successfully.'
    PRINT ''
END

PRINT 'Creating updated stored procedure with PAYMENTDT parameter...'
PRINT ''

GO

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
    @PAYMENTDT DATETIME = NULL  -- NEW PARAMETER
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
                PAYMENTDT = @PAYMENTDT,  -- Include payment date
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
                @CROPID, @PAYMENTDT, 0  -- Include payment date
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
PRINT 'SUCCESS! Stored procedure updated!'
PRINT '=========================================='
PRINT ''
PRINT 'Changes made:'
PRINT '  ✓ Added @PAYMENTDT DATETIME parameter'
PRINT '  ✓ Updated INSERT to include PAYMENTDT column'
PRINT '  ✓ Updated UPDATE to set PAYMENTDT column'
PRINT ''
PRINT 'The stored procedure now accepts 16 parameters:'
PRINT '  1. @TRAID'
PRINT '  2. @TRNSR'
PRINT '  3. @TRNSID'
PRINT '  4. @TRNTID'
PRINT '  5. @TRNDT'
PRINT '  6. @REFAID'
PRINT '  7. @MAID'
PRINT '  8. @PAMT'
PRINT '  9. @CBAL'
PRINT ' 10. @ADVAMT'
PRINT ' 11. @ASID'
PRINT ' 12. @ISDELETED'
PRINT ' 13. @COMPANYID'
PRINT ' 14. @DEPTID'
PRINT ' 15. @CROPID'
PRINT ' 16. @PAYMENTDT (NEW!)'
PRINT ''
PRINT 'You can now save transactions with payment dates!'
PRINT ''
