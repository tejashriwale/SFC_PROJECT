-- =============================================
-- Update SP_AddUpdAllTransections to include PAYMENTDT parameter
-- This script modifies the stored procedure to accept and save the payment date
-- =============================================

USE [InventoryManagement]
GO

PRINT 'Updating SP_AddUpdAllTransections stored procedure...'
PRINT ''

-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]
    PRINT 'Existing procedure dropped'
END
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
    @PAYMENTDT DATETIME = NULL  -- New parameter for payment date
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
                @CROPID, @PAYMENTDT, 0  -- Include payment date in insert
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

PRINT 'SP_AddUpdAllTransections stored procedure updated successfully!'
PRINT ''
PRINT 'Changes:'
PRINT '  - Added @PAYMENTDT DATETIME parameter (nullable)'
PRINT '  - Updated INSERT statement to include PAYMENTDT column'
PRINT '  - Updated UPDATE statement to set PAYMENTDT column'
PRINT ''
PRINT 'Note: The payment date will be saved to the PAYMENTDT column in tblAllTransections'
PRINT ''
