USE [InventoryManagement]
GO

/******
  UPDATED VERSION OF YOUR ACTUAL SP
  Based on SP extracted from database: 02-Jan-26 4:33:01 PM
  ADDED: @PAYMENTDT parameter
******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Drop existing procedure
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]
    PRINT 'Existing SP_AddUpdAllTransections dropped'
END
GO

PRINT 'Creating updated SP_AddUpdAllTransections with PAYMENTDT parameter...'
GO

CREATE PROCEDURE [dbo].[SP_AddUpdAllTransections]
    @TRAID       VARCHAR(24) = '',
    @TRNSR       INT         = NULL,
    @TRNSID      VARCHAR(24) = '',
    @TRNTID      VARCHAR(24) = '',
    @TRNDT       VARCHAR(50) = '01 jan 1900',
    @REFAID      VARCHAR(24) = '',
    @MAID        VARCHAR(24) = '',
    @PAMT        DECIMAL(18,2) = 0,
    @CBAL        DECIMAL(18,2) = 0,
    @ADVAMT      DECIMAL(18,2) = 0,
    @ASID        VARCHAR(24) = '',
    @ISDELETED   BIT         = 0,
    @COMPANYID   VARCHAR(24) = '',
    @DEPTID      VARCHAR(24) = '',
    @CROPID      NVARCHAR(MAX) = '',
    @PAYMENTDT   VARCHAR(50) = NULL  -- ← ADDED: Payment Date parameter
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @RMSG        NVARCHAR(MAX)    = '',
            @RVAL        NVARCHAR(MAX)    = '',
            @ERRORCODE   VARCHAR(10)      = '',
            @ISSUCCESS   INT              = 1,
            @CNT         INT              = 0,
            @PPAMT       FLOAT            = 0,
            @PCBAL       FLOAT            = 0,
            @PTRNSR      INT              = 0

    SELECT   @CNT = COUNT(*)
    FROM tblAllTransections
    WHERE ISDELETED = 0
      AND TRAID = @TRAID



    IF @CNT > 0
    BEGIN
        SELECT   @PPAMT   = PAMT
                ,@PCBAL   = CBAL
                ,@PTRNSR  = TRNSR
        FROM tblAllTransections
        WHERE ISDELETED = 0
          AND TRAID = @TRAID

        SET @PCBAL = @PCBAL - @PPAMT

        UPDATE tblAllTransections
        SET     TRNSID   = @TRNSID,
                TRNTID   = @TRNTID,
                TRNDT    = @TRNDT,
                REFAID   = @REFAID,
                MAID     = @MAID,
                PAMT     = @PAMT,
                CBAL     = @PCBAL + @PAMT,
                ADVAMT   = @ADVAMT,
                CROPID   = @CROPID,
                PAYMENTDT = @PAYMENTDT  -- ← ADDED: Update payment date
        WHERE ISDELETED = 0
          AND TRAID = @TRAID

        SET @RMSG = N'Transection details update successfully';
        SET @RVAL = 'UPDATED';
        SET @ERRORCODE = 'SUCCESS';
        SET @ISSUCCESS = 1;

        SELECT @ERRORCODE AS ResponseCode,
               @RMSG AS ResponseMessage,
               @RVAL AS ResponseValues,
               @ISSUCCESS AS IsSuccessful;
        RETURN;
    END

    IF @CNT = 0
    BEGIN
        INSERT INTO tblAllTransections
        (
            TRAID,
            TRNSR,
            TRNSID,
            TRNTID,
            TRNDT,
            CROPID,
            REFAID,
            MAID,
            PAMT,
            CBAL,
            ADVAMT,
            ASID,
            ISDELETED,
            COMPANYID,
            DEPTID,
            PAYMENTDT  -- ← ADDED: Include payment date in INSERT
        )
        VALUES
        (
            @TRAID,
            @TRNSR,
            @TRNSID,
            @TRNTID,
            @TRNDT,
            @CROPID,
            @REFAID,
            @MAID,
            @PAMT,
            @CBAL,
            @ADVAMT,
            @ASID,
            @ISDELETED,
            @COMPANYID,
            @DEPTID,
            @PAYMENTDT  -- ← ADDED: Insert payment date value
        );

        SET @RMSG = N'Transection details save successfully';
        SET @RVAL = 'ADDED';
        SET @ERRORCODE = 'SUCCESS';
        SET @ISSUCCESS = 1;

        SELECT @ERRORCODE AS ResponseCode,
               @RMSG AS ResponseMessage,
               @RVAL AS ResponseValues,
               @ISSUCCESS AS IsSuccessful;
        RETURN;
    END
END
GO

PRINT ''
PRINT '=========================================='
PRINT '✓ SP UPDATED SUCCESSFULLY'
PRINT '=========================================='
PRINT ''
PRINT 'Changes made to YOUR actual SP:'
PRINT '  • Added parameter: @PAYMENTDT VARCHAR(50) = NULL'
PRINT '  • Added to UPDATE: PAYMENTDT = @PAYMENTDT'
PRINT '  • Added to INSERT: PAYMENTDT column and value'
PRINT ''
PRINT 'Verification:'
PRINT ''

SELECT
    ORDINAL_POSITION AS [#],
    PARAMETER_NAME AS [Parameter],
    DATA_TYPE AS [Type],
    CASE WHEN PARAMETER_NAME = '@PAYMENTDT' THEN '← NEW!' ELSE '' END AS [Status]
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION

DECLARE @TotalParams INT
SELECT @TotalParams = COUNT(*)
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'

PRINT ''
PRINT 'Total parameters: ' + CAST(@TotalParams AS VARCHAR)
PRINT ''
PRINT 'Next steps:'
PRINT '  1. Restart backend (Ctrl+C, then dotnet run)'
PRINT '  2. Test payment functionality'
PRINT '  3. Payment dates should now save correctly!'
PRINT ''
