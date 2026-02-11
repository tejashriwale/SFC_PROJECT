-- ===============================================================
-- CREATE GET_VechicleProfitReport FUNCTION (CORRECTED VERSION)
-- Fixed column names based on actual table structure
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
    PRINT '✓ Dropped existing function'
    PRINT ''
END

PRINT 'Creating function with corrected column names...'
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

    -- Use CTE to calculate purchase amounts per vehicle/crop
    -- NOTE: VEHNO is in tblCropInvertDetail, not Master!
    ;WITH InwardData AS (
        SELECT
            cid.VEHNO AS VEHCHNO,
            cid.CROPID,
            SUM(ISNULL(cid.WEIGHT, 0) * ISNULL(cid.RATE, 0)) AS TOTIWDAMT
        FROM tblCropInvertMaster cim
        INNER JOIN tblCropInvertDetail cid ON cim.CDAID = cid.CDAID
        WHERE cim.COMPANYID = @COMPANYID
        AND cim.DEPTID = @DEPTID
        AND CONVERT(DATE, cim.SYSDT) BETWEEN CONVERT(DATE, @SDATE, 106) AND CONVERT(DATE, @EDATE, 106)
        AND cid.ISDELETED = 0
        AND cim.ISDELETED = 0
        GROUP BY cid.VEHNO, cid.CROPID
    )

    INSERT INTO @ResultTable
    SELECT
        CONVERT(VARCHAR(50), ti.SYSDT, 106) AS SYSDT,
        ISNULL(m.MANAME, '') AS CUSTOMERNAME,
        ISNULL(c.CROPNAME, '') AS CROPNAME,
        ISNULL(SUM(tid.SAMT), 0) AS TOTSAMT,
        ISNULL(c.CROPNAME, '') AS CROP,
        ISNULL(SUM(tid.CRTCNT), 0) AS CRTCNT,
        ISNULL(MAX(ISNULL(ti.EXPENSES, 0)), 0) AS TOTEX,
        ISNULL(MAX(iwd.TOTIWDAMT), 0) AS TOTIWDAMT,
        ISNULL(ti.TRADERNAME, '') AS TREADER,
        ISNULL(ti.VEHCHNO, '') AS VEHCHNO

    FROM tblCropTransportInvoice ti
    INNER JOIN tblCropTransportInvoiceDetail tid ON ti.CTAID = tid.CTAID  -- Fixed: CTAID not CTRAID
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

    RETURN
END
GO

PRINT '✓ Function created successfully!'
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
    PRINT '✓ Function exists in database'
    PRINT ''
    PRINT 'Corrected Column Mappings:'
    PRINT '-------------------------'
    PRINT '• tblCropInvertMaster.CDAID (Primary Key)'
    PRINT '• tblCropInvertDetail.CDAID (Foreign Key)'
    PRINT '• tblCropInvertDetail.VEHNO (Vehicle - in Detail table)'
    PRINT '• tblCropInvertDetail.RATE and WEIGHT'
    PRINT '• tblCropTransportInvoice.CTAID (Primary Key) - FIXED'
    PRINT '• tblCropTransportInvoiceDetail.CTAID (Foreign Key) - FIXED'
    PRINT '• tblCropTransportInvoiceDetail.SAMT (Sales Amount)'
    PRINT '• tblCropTransportInvoiceDetail.CRTCNT (Crate Count)'
    PRINT ''
    PRINT '✓ CREATION SUCCESSFUL!'
    PRINT ''
    PRINT 'Next: Run Test_GET_VechicleProfitReport_Function.sql'
END
ELSE
BEGIN
    PRINT '✗ VERIFICATION FAILED!'
    PRINT 'Function was not created - check for errors above'
END

PRINT ''
PRINT '=========================================='
PRINT 'COMPLETE'
PRINT '=========================================='
PRINT ''

GO
