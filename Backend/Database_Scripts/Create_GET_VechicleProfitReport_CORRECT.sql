-- ===============================================================
-- CREATE GET_VechicleProfitReport FUNCTION (CORRECT VERSION)
-- Based on actual table structure shown by user
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'CLEANING AND CREATING FUNCTION'
PRINT '=========================================='
PRINT ''

-- Drop any existing object (view, procedure, or function)
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'V') IS NOT NULL
    DROP VIEW dbo.GET_VechicleProfitReport

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GET_VechicleProfitReport

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
    DROP FUNCTION dbo.GET_VechicleProfitReport

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'FN') IS NOT NULL
    DROP FUNCTION dbo.GET_VechicleProfitReport

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'IF') IS NOT NULL
    DROP FUNCTION dbo.GET_VechicleProfitReport

PRINT 'Creating function...'
PRINT ''

GO

CREATE FUNCTION dbo.GET_VechicleProfitReport
(
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @SDATE VARCHAR(50),
    @EDATE VARCHAR(50)
)
RETURNS @ResultTable TABLE
(
    SYSDT VARCHAR(50),
    CUSTOMERNAME VARCHAR(200),
    CROPNAME VARCHAR(200),
    TOTSAMT FLOAT,
    CROP VARCHAR(200),
    CRTCNT INT,
    TOTEX FLOAT,
    TOTIWDAMT FLOAT,
    TREADER VARCHAR(200),
    VEHCHNO VARCHAR(50)
)
AS
BEGIN
    -- Calculate purchase amounts from tblCropInvertDetail
    -- Match by vehicle number and crop within date range
    ;WITH InwardData AS (
        SELECT
            cid.VEHNO AS VEHCHNO,
            cid.CROPID,
            SUM(ISNULL(cid.WEIGHT, 0) * ISNULL(cid.RATE, 0)) AS TOTIWDAMT
        FROM tblCropInvertDetail cid
        WHERE cid.COMPANYID = @COMPANYID
        AND cid.DEPTID = @DEPTID
        AND CONVERT(DATE, cid.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
        AND cid.ISDELETED = 0
        GROUP BY cid.VEHNO, cid.CROPID
    )

    INSERT INTO @ResultTable
    SELECT
        -- Date
        CONVERT(VARCHAR(50), ti.SYSDT, 106) AS SYSDT,

        -- Customer name (use TREADER as customer name)
        ISNULL(ti.TREADER, '') AS CUSTOMERNAME,

        -- Crop name (from CROP column)
        ISNULL(ti.CROP, '') AS CROPNAME,

        -- Total Sales Amount (from invoice)
        ISNULL(ti.TOTSAMT, 0) AS TOTSAMT,

        -- Crop (duplicate for compatibility)
        ISNULL(ti.CROP, '') AS CROP,

        -- Crate count (from invoice)
        ISNULL(ti.CRTCNT, 0) AS CRTCNT,

        -- Total Expense (from invoice)
        ISNULL(ti.TOTEX, 0) AS TOTEX,

        -- Total Inward/Purchase Amount (calculated from tblCropInvertDetail)
        -- Match by vehicle number and crop
        ISNULL(iwd.TOTIWDAMT, 0) AS TOTIWDAMT,

        -- Trader name
        ISNULL(ti.TREADER, '') AS TREADER,

        -- Vehicle number
        ISNULL(ti.VEHCHNO, '') AS VEHCHNO

    FROM tblCropTransportInvoice ti
    LEFT JOIN InwardData iwd ON ti.VEHCHNO = iwd.VEHCHNO AND ti.CROP = iwd.CROPID

    WHERE ti.COMPANYID = @COMPANYID
    AND ti.DEPTID = @DEPTID
    AND CONVERT(DATE, ti.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
    AND ti.ISDELETED = 0

    RETURN
END
GO

PRINT '✓ Function created!'
PRINT ''

-- ===============================================================
-- VERIFICATION
-- ===============================================================

PRINT '=========================================='
PRINT 'VERIFICATION'
PRINT '=========================================='
PRINT ''

IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    PRINT '✓ GET_VechicleProfitReport function exists'
    PRINT ''
    PRINT 'Function Details:'
    PRINT '----------------'
    PRINT 'This function queries tblCropTransportInvoice which already'
    PRINT 'contains aggregated totals for:'
    PRINT '• TOTSAMT - Total Sales Amount'
    PRINT '• TOTEX - Total Expense'
    PRINT '• TOTIWDAMT - Total Inward/Purchase Amount'
    PRINT '• CRTCNT - Total Crate Count'
    PRINT '• TREADER - Trader/Customer Name'
    PRINT '• VEHCHNO - Vehicle Number'
    PRINT '• CROP - Crop Name'
    PRINT ''
    PRINT '✓ SUCCESS! Function is ready.'
    PRINT ''
    PRINT 'Profit = TOTSAMT - (TOTIWDAMT + TOTEX)'
    PRINT ''
    PRINT 'Next: Run Test_GET_VechicleProfitReport_Function.sql'
END
ELSE
BEGIN
    PRINT '✗ ERROR: Function not created'
    PRINT 'Check errors above'
END

PRINT ''
PRINT '=========================================='
PRINT 'COMPLETE'
PRINT '=========================================='
PRINT ''

GO
