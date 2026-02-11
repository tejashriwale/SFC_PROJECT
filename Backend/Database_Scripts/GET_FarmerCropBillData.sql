-- =============================================
-- Author:      System Generated
-- Create date: 2026-01-02
-- Description: Get Farmer Crop Bill Data with correct amount calculations
--
-- TRNSID Mapping:
--   10 = Booking amount
--   20 = Payment amount
--   30 = Extra advance
--   40 = Single bill amount
--
-- TRNTID Mapping:
--   10 = Farmer transactions
--   20 = Vehicle transactions
--
-- Parameters:
--   @COMPANYID: Company ID
--   @DEPTID: Department ID
--   @MAID: Farmer ID (Member ID)
--   @CROPID: Crop ID
-- Returns: Table with farmer bill details including corrected amounts
-- =============================================

CREATE OR ALTER FUNCTION [dbo].[GET_FarmerCropBillData]
(
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @MAID VARCHAR(50),
    @CROPID VARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        -- Use REFAID as primary identifier instead of TRAID to avoid duplicates
        COALESCE(ft.REFAID, '') AS TRAID,  -- Return REFAID as TRAID for compatibility
        COALESCE('', '') AS NARR,  -- NARR column doesn't exist in table
        MAX(ft.TRNSR) AS TRNSR,  -- Use MAX to get one value when grouping
        COALESCE(CONVERT(VARCHAR(24), MAX(ft.TRNDT), 106), '') AS TRNDT,
        COALESCE(ft.CROPID, '') AS CROPID,
        COALESCE(ft.REFAID, '') AS REFAID,
        COALESCE(ft.MAID, '') AS MAID,
        COALESCE(MAX(i.ITEMNE), '') AS CROPNAME,
        COALESCE(MAX(f.FNAME), '') AS FNAME,
        COALESCE(ft.REFAID, '') AS PKID,  -- Using REFAID as PKID (bill reference)
        COALESCE(MAX(cim.BILLNO), '') AS BILLNO, -- Using BILLNO from tblCropInvertMaster
        COALESCE(MAX(cim.CVARIETY), '') AS CVARIETY, -- Crop variety from master
        COALESCE((
            SELECT TOP 1 cid.CGRADE
            FROM tblCropInvertDetail cid
            WHERE cid.CROPID = ft.CROPID
                AND cid.ISDELETED = 0
        ), '') AS CGRADE, -- Grade from detail table

        -- Calculate BILLAMOUNT: Sum of ALL crops in this bill (REFAID) - booking + single bill amounts
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID - sum all crops in the bill
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'  -- Only farmer transactions
                AND (ft2.TRNSID = '10' OR ft2.TRNSID = '40')  -- Booking + Single bill
                AND ft2.ISDELETED = 0
        ), 0) AS BILLAMOUNT,

        -- Calculate ADVANCEAMT: Sum of extra advances (TRNSID='30') for this bill
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID - sum all advances for the bill
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'  -- Only farmer transactions
                AND ft2.TRNSID = '30'  -- Extra advance
                AND ft2.ISDELETED = 0
        ), 0) AS ADVANCEAMT,

        -- Calculate PAIDAMT: Sum of all payments (TRNSID='20') for this bill
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID - sum all payments for the bill
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'  -- Only farmer transactions
                AND ft2.TRNSID = '20'  -- Payment amount
                AND ft2.ISDELETED = 0
        ), 0) AS PAIDAMT,

        -- Calculate PAYABLEAMT: BILLAMOUNT - ADVANCEAMT - PAIDAMT (for entire bill)
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND (ft2.TRNSID = '10' OR ft2.TRNSID = '40')
                AND ft2.ISDELETED = 0
        ), 0) -
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND ft2.TRNSID = '30'
                AND ft2.ISDELETED = 0
        ), 0) -
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND ft2.TRNSID = '20'
                AND ft2.ISDELETED = 0
        ), 0) AS PAYABLEAMT,

        -- Calculate CBAL (Current Balance): Same as PAYABLEAMT (for entire bill)
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND (ft2.TRNSID = '10' OR ft2.TRNSID = '40')
                AND ft2.ISDELETED = 0
        ), 0) -
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND ft2.TRNSID = '30'
                AND ft2.ISDELETED = 0
        ), 0) -
        COALESCE((
            SELECT SUM(ABS(ft2.PAMT))
            FROM tblAllTransections ft2
            WHERE ft2.REFAID = ft.REFAID
                AND ft2.MAID = ft.MAID
                -- Do NOT filter by CROPID
                AND ft2.COMPANYID = ft.COMPANYID
                AND ft2.DEPTID = ft.DEPTID
                AND ft2.TRNTID = '10'
                AND ft2.TRNSID = '20'
                AND ft2.ISDELETED = 0
        ), 0) AS CBAL

    FROM tblAllTransections ft
    LEFT JOIN tblFarmer f ON ft.MAID = f.FAID AND f.ISDELETED = 0
    LEFT JOIN tblITEM i ON ft.CROPID = i.ITEMAID AND i.ISDELETED = 0
    LEFT JOIN tblCropInvertMaster cim ON ft.REFAID = cim.ASID AND cim.ISDELETED = 0

    WHERE ft.COMPANYID = @COMPANYID
        AND ft.DEPTID = @DEPTID
        AND ft.MAID LIKE @MAID
        AND (
            -- Support both types of CROPID:
            -- 1. CropInvertMaster's CROPID (primary key from crop bill)
            -- 2. Item/Crop Name ID (from farmer bill detail)
            cim.CROPID LIKE @CROPID
            OR ft.CROPID LIKE @CROPID
        )
        AND ft.ISDELETED = 0
        AND ft.TRNTID = '10'  -- Only farmer transactions (TRNTID='10')
        AND (ft.TRNSID = '10' OR ft.TRNSID = '20' OR ft.TRNSID = '30' OR ft.TRNSID = '40')

    GROUP BY
        ft.REFAID, ft.CROPID, ft.MAID, ft.COMPANYID, ft.DEPTID
        -- Removed TRAID, TRNSR, TRNDT to avoid duplicate rows per bill
)
GO

PRINT 'GET_FarmerCropBillData function created/updated successfully!'
PRINT ''
PRINT 'TRNSID Mapping:'
PRINT '  - 10 = Booking amount'
PRINT '  - 20 = Payment amount'
PRINT '  - 30 = Extra advance'
PRINT '  - 40 = Single bill amount'
PRINT ''
PRINT 'TRNTID Mapping:'
PRINT '  - 10 = Farmer transactions'
PRINT '  - 20 = Vehicle transactions'
PRINT ''
PRINT 'CROPID Parameter - Supports Two Types:'
PRINT '  - CropInvertMaster CROPID (primary key from crop bill)'
PRINT '  - Item/Crop Name ID (from farmer bill detail)'
PRINT '  - Function handles both automatically using OR condition'
PRINT ''
PRINT 'Key Calculations:'
PRINT '  - BILLAMOUNT: Sum of ALL crops in bill (TRNSID=10+40, TRNTID=10)'
PRINT '  - ADVANCEAMT: Sum of extra advances for bill (TRNSID=30, TRNTID=10)'
PRINT '  - PAIDAMT: Sum of payments for bill (TRNSID=20, TRNTID=10)'
PRINT '  - PAYABLEAMT: BILLAMOUNT - ADVANCEAMT - PAIDAMT'
PRINT '  - CBAL: Same as PAYABLEAMT (current balance)'
PRINT '  - CVARIETY: Crop variety from master table'
PRINT '  - CGRADE: Grade from detail table'
PRINT '  - NOTE: Amounts are calculated per BILL (REFAID), not per CROP'
PRINT '  - All filtered by TRNTID=10 (farmer transactions only)'
PRINT ''
