-- =============================================
-- GET_FarmerPaymentSummary - Get payment details with transaction date and payment date
-- =============================================

USE [InventoryManagement]
GO

CREATE OR ALTER FUNCTION [dbo].[GET_FarmerPaymentSummary]
(
    @REFAID VARCHAR(50),
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        ft.TRAID,
        COALESCE(CONVERT(VARCHAR(24), ft.TRNDT, 106), '') AS TRANSACTIONDATE,  -- Transaction Date
        COALESCE(CONVERT(VARCHAR(24), ft.PAYMENTDT, 106), CONVERT(VARCHAR(24), ft.TRNDT, 106)) AS PAYMENTDATE,  -- Payment Date (defaults to TRNDT if null)
        ft.TRNSR,
        COALESCE(ft.REFAID, '') AS REFAID,
        COALESCE(ft.MAID, '') AS MAID,
        COALESCE(ABS(ft.PAMT), 0) AS PAYMENTAMOUNT,
        COALESCE('Paid Bill Amount', '') AS NARRATION,  -- Default narration
        ft.TRNSID,
        ft.TRNTID
    FROM tblAllTransections ft
    WHERE ft.REFAID = @REFAID
        AND ft.COMPANYID = @COMPANYID
        AND ft.DEPTID = @DEPTID
        AND ft.TRNSID = '20'  -- Only payment transactions
        AND ft.TRNTID = '10'  -- Only farmer transactions
        AND ft.ISDELETED = 0
)
GO

PRINT 'GET_FarmerPaymentSummary function created successfully!'
PRINT ''
PRINT 'Returns:'
PRINT '  - TRANSACTIONDATE: Original transaction date (TRNDT)'
PRINT '  - PAYMENTDATE: Payment date (PAYMENTDT), editable'
PRINT '  - PAYMENTAMOUNT: Payment amount'
PRINT '  - NARRATION: Payment description'
PRINT ''
