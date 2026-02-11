-- ===============================================================
-- CREATE OR UPDATE GET_VechicleProfitReport FUNCTION
-- This function retrieves profit/loss data from transport invoices
-- and crop inward tables for the Profit Loss Report
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'CREATING GET_VechicleProfitReport FUNCTION'
PRINT '=========================================='
PRINT ''

-- Drop function if it exists
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.GET_VechicleProfitReport
    PRINT '✓ Dropped existing GET_VechicleProfitReport function'
    PRINT ''
END
ELSE
BEGIN
    PRINT 'No existing function to drop'
    PRINT ''
END
GO

PRINT 'Creating function...'
PRINT ''
GO

-- Create the function
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
    TOTSAMT DECIMAL(18,2),
    CROP VARCHAR(200),
    CRTCNT INT,
    TOTEX DECIMAL(18,2),
    TOTIWDAMT DECIMAL(18,2),
    TREADER VARCHAR(200),
    VEHCHNO VARCHAR(50)
)
AS
BEGIN
    -- This function combines data from:
    -- 1. tblCropTransportInvoice & tblCropTransportInvoiceDetail (Sales data)
    -- 2. tblCropInvertMaster & tblCropInvertDetail (Purchase/Inward data)

    -- Use CTE to calculate purchase amounts per vehicle/invoice
    ;WITH InwardData AS (
        SELECT
            cim.VEHNO AS VEHCHNO,
            cid.CROPID,
            SUM(ISNULL(cid.WEIGHT, 0) * ISNULL(cid.RATE, 0)) AS TOTIWDAMT
        FROM tblCropInvertMaster cim
        INNER JOIN tblCropInvertDetail cid ON cim.CDAID = cid.CDAID
        WHERE cim.COMPANYID = @COMPANYID
        AND cim.DEPTID = @DEPTID
        AND CONVERT(DATE, cim.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
        AND cid.ISDELETED = 0
        AND cim.ISDELETED = 0
        GROUP BY cim.VEHNO, cid.CROPID
    )

    INSERT INTO @ResultTable
    SELECT
        -- Date from transport invoice
        CONVERT(VARCHAR(50), ti.SYSDT, 106) AS SYSDT,

        -- Customer name from master table
        ISNULL(m.MANAME, '') AS CUSTOMERNAME,

        -- Crop name from crop master
        ISNULL(c.CROPNAME, '') AS CROPNAME,

        -- Total Sales Amount from transport invoice detail
        ISNULL(SUM(tid.TAMT), 0) AS TOTSAMT,

        -- Crop name again (for backward compatibility)
        ISNULL(c.CROPNAME, '') AS CROP,

        -- Total Crate Count from transport invoice detail
        ISNULL(SUM(CAST(tid.CRTCNT AS INT)), 0) AS CRTCNT,

        -- Total Expenses from transport invoice
        ISNULL(MAX(ti.EXPENSES), 0) AS TOTEX,

        -- Total Inward Amount (Purchase Amount) from crop invert detail
        -- Match by vehicle number and crop
        ISNULL(MAX(iwd.TOTIWDAMT), 0) AS TOTIWDAMT,

        -- Trader/Farmer name (from transport invoice or master)
        ISNULL(ti.TRADERNAME, '') AS TREADER,

        -- Vehicle number from transport invoice
        ISNULL(ti.VEHCHNO, '') AS VEHCHNO

    FROM tblCropTransportInvoice ti
    INNER JOIN tblCropTransportInvoiceDetail tid ON ti.CTRAID = tid.CTRAID
    LEFT JOIN tblMaster m ON ti.MAID = m.MAID
    LEFT JOIN tblCropMaster c ON tid.CROPID = c.CROPID
    LEFT JOIN InwardData iwd ON ti.VEHCHNO = iwd.VEHCHNO AND tid.CROPID = iwd.CROPID

    WHERE ti.COMPANYID = @COMPANYID
    AND ti.DEPTID = @DEPTID
    AND CONVERT(DATE, ti.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
    AND ti.ISDELETED = 0
    AND tid.ISDELETED = 0

    GROUP BY
        ti.SYSDT,
        m.MANAME,
        c.CROPNAME,
        ti.TRADERNAME,
        ti.VEHCHNO

    -- Note: ORDER BY removed - caller can sort results
    RETURN
END
GO

PRINT '✓ GET_VechicleProfitReport function created successfully'
PRINT ''
PRINT '=========================================='
PRINT 'VERIFICATION'
PRINT '=========================================='
PRINT ''

-- Verify the function exists
IF OBJECT_ID('dbo.GET_VechicleProfitReport', 'TF') IS NOT NULL
BEGIN
    PRINT '✓ Function exists in database'
    PRINT ''
    PRINT 'Function Parameters:'
    PRINT '-------------------'
    PRINT '• @COMPANYID VARCHAR(50)'
    PRINT '• @DEPTID VARCHAR(50)'
    PRINT '• @SDATE VARCHAR(50) - Format: ''1 Jan 2025'''
    PRINT '• @EDATE VARCHAR(50) - Format: ''31 Jan 2025'''
    PRINT ''
    PRINT 'Data Sources:'
    PRINT '-------------'
    PRINT '• tblCropTransportInvoice - Sales invoice master'
    PRINT '• tblCropTransportInvoiceDetail - Sales invoice details'
    PRINT '• tblCropInvertMaster - Purchase/Inward master'
    PRINT '• tblCropInvertDetail - Purchase/Inward details'
    PRINT '• tblMaster - Customer master'
    PRINT '• tblCropMaster - Crop master'
    PRINT ''
    PRINT 'Return Columns:'
    PRINT '--------------'
    PRINT '• SYSDT - Date'
    PRINT '• CUSTOMERNAME - Customer Name'
    PRINT '• CROPNAME - Crop Name'
    PRINT '• TOTSAMT - Total Sales Amount'
    PRINT '• CROP - Crop (duplicate for compatibility)'
    PRINT '• CRTCNT - Total Crate Count'
    PRINT '• TOTEX - Total Expenses'
    PRINT '• TOTIWDAMT - Total Inward/Purchase Amount'
    PRINT '• TREADER - Trader Name'
    PRINT '• VEHCHNO - Vehicle Number'
    PRINT ''
    PRINT '✓ FUNCTION CREATED SUCCESSFULLY!'
END
ELSE
BEGIN
    PRINT '✗ ERROR: Function not found after creation'
END

PRINT ''
PRINT 'Next Steps:'
PRINT '----------'
PRINT '1. Test the function with sample data:'
PRINT '   SELECT * FROM GET_VechicleProfitReport(''COMP001'', ''DEPT001'', ''1 Jan 2025'', ''31 Jan 2025'')'
PRINT ''
PRINT '2. Verify data is being pulled correctly from all tables'
PRINT ''
PRINT '3. Restart backend server if needed'
PRINT ''

GO
