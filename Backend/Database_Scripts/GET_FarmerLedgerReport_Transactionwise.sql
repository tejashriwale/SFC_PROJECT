USE [InventoryManagement]
GO

-- =============================================
-- GET_FarmerLedgerReport_Transactionwise Function
-- Shows individual transactions for Farmer Ledger Report
-- Each row represents a transaction (booking, payment, advance, etc.)
-- =============================================

CREATE OR ALTER FUNCTION [dbo].[GET_FarmerLedgerReport_Transactionwise]
(
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @MAID VARCHAR(50),      -- Farmer ID (use '%' for all farmers)
    @CROPID VARCHAR(50)     -- Crop ID (supports both types)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        -- Transaction details
        COALESCE(CONVERT(VARCHAR(24), ft.TRNDT, 106), '') AS TRNDT,
        COALESCE(cim.BILLNO, '') AS BILLNO,
        COALESCE(f.FNAME, '') AS FNAME,
        COALESCE(i.ITEMNE, '') AS CROPNAME,
        COALESCE(cim.CVARIETY, '') AS CVARIETY,

        -- Narration from master table
        COALESCE(cim.NARR, '') AS NARR,

        -- Transaction type description
        CASE ft.TRNSID
            WHEN '10' THEN 'Booking'
            WHEN '20' THEN 'Payment'
            WHEN '30' THEN 'Advance'
            WHEN '40' THEN 'Bill Amount'
            ELSE 'Other'
        END AS TRANSACTIONTYPE,

        -- Debit (Purchase/Bill) - shows for booking and bill amounts
        CASE
            WHEN ft.TRNSID IN ('10', '40') THEN ABS(ft.PAMT)
            ELSE 0
        END AS DEBIT,

        -- Credit (Payment/Advance) - shows for payments and advances
        CASE
            WHEN ft.TRNSID IN ('20', '30') THEN ABS(ft.PAMT)
            ELSE 0
        END AS CREDIT,

        -- Running balance (from transaction record)
        COALESCE(ft.CBAL, 0) AS BALANCE,

        -- Additional fields
        ft.TRAID,
        ft.TRNSR,
        ft.REFAID,
        ft.MAID AS FAID,
        ft.CROPID,
        ft.TRNDT AS SORTDATE

    FROM tblAllTransections ft
    LEFT JOIN tblCropInvertMaster cim ON ft.REFAID = cim.ASID AND cim.ISDELETED = 0
    LEFT JOIN tblFarmer f ON ft.MAID = f.FAID AND f.ISDELETED = 0
    LEFT JOIN tblITEM i ON ft.CROPID = i.ITEMAID AND i.ISDELETED = 0

    WHERE ft.COMPANYID = @COMPANYID
        AND ft.DEPTID = @DEPTID
        AND ft.MAID LIKE @MAID
        AND (
            -- Support both types of CROPID
            cim.CROPID LIKE @CROPID
            OR ft.CROPID LIKE @CROPID
        )
        AND ft.TRNTID = '10'  -- Farmer transactions only
        AND ft.TRNSID IN ('10', '20', '30', '40')  -- Booking, Payment, Advance, Bill
        AND ft.ISDELETED = 0
)
GO

PRINT 'GET_FarmerLedgerReport_Transactionwise function created successfully!'
PRINT ''
PRINT 'Purpose: Transaction-wise view for Farmer Ledger Report'
PRINT ''
PRINT 'Features:'
PRINT '  - Shows each individual transaction'
PRINT '  - Separates debits (purchases) and credits (payments)'
PRINT '  - Includes running balance'
PRINT '  - Transaction type labels'
PRINT ''
PRINT 'Transaction Types (TRNSID):'
PRINT '  - 10 = Booking (Debit)'
PRINT '  - 20 = Payment (Credit)'
PRINT '  - 30 = Advance (Credit)'
PRINT '  - 40 = Bill Amount (Debit)'
PRINT ''
PRINT 'Columns:'
PRINT '  - TRNDT: Transaction date'
PRINT '  - BILLNO: Bill number'
PRINT '  - FNAME: Farmer name'
PRINT '  - CROPNAME: Crop name'
PRINT '  - CVARIETY: Variety'
PRINT '  - NARR: Narration/Description'
PRINT '  - TRANSACTIONTYPE: Type description'
PRINT '  - DEBIT: Purchase/Bill amount'
PRINT '  - CREDIT: Payment/Advance amount'
PRINT '  - BALANCE: Running balance'
PRINT ''
GO
