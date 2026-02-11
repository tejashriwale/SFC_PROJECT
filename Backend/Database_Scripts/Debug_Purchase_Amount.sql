-- ===============================================================
-- DEBUG: Check why Purchase Amount is not showing
-- ===============================================================

USE [InventoryManagement]
GO

PRINT ''
PRINT '=========================================='
PRINT 'DEBUGGING PURCHASE AMOUNT ISSUE'
PRINT '=========================================='
PRINT ''

-- Check data for the specific vehicle shown in report
DECLARE @VehicleNo VARCHAR(50) = 'MH14HE3307'
DECLARE @DateStart VARCHAR(50) = '1 Jan 2026'
DECLARE @DateEnd VARCHAR(50) = '5 Jan 2026'

PRINT 'Looking for vehicle: ' + @VehicleNo
PRINT 'Date range: ' + @DateStart + ' to ' + @DateEnd
PRINT ''

-- ===============================================================
-- STEP 1: Check tblCropTransportInvoice
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 1: Transport Invoice Data'
PRINT '=========================================='
PRINT ''

SELECT
    CTAID,
    CROP,
    TREADER,
    VEHCHNO,
    CRTCNT,
    TOTSAMT,
    TOTEX,
    TOTIWDAMT,
    SYSDT
FROM tblCropTransportInvoice
WHERE VEHCHNO = @VehicleNo
AND CONVERT(DATE, SYSDT) BETWEEN CONVERT(DATE, @DateStart, 106) AND CONVERT(DATE, @DateEnd, 106)
AND ISDELETED = 0

PRINT ''

-- ===============================================================
-- STEP 2: Check tblCropInvertDetail for this vehicle
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 2: Inward Detail Data (by Vehicle)'
PRINT '=========================================='
PRINT ''

SELECT
    CDAID,
    CROPID,
    VEHNO,
    WEIGHT,
    RATE,
    (WEIGHT * RATE) AS AMOUNT,
    CRATECOUNT,
    SYSDT
FROM tblCropInvertDetail
WHERE VEHNO = @VehicleNo
AND ISDELETED = 0
ORDER BY SYSDT DESC

PRINT ''

-- ===============================================================
-- STEP 3: Check all inward data in date range
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 3: All Inward Data in Date Range'
PRINT '=========================================='
PRINT ''

SELECT
    CDAID,
    CROPID,
    VEHNO,
    WEIGHT,
    RATE,
    (WEIGHT * RATE) AS AMOUNT,
    CRATECOUNT,
    SYSDT
FROM tblCropInvertDetail
WHERE CONVERT(DATE, SYSDT) BETWEEN CONVERT(DATE, @DateStart, 106) AND CONVERT(DATE, @DateEnd, 106)
AND ISDELETED = 0
ORDER BY SYSDT DESC

PRINT ''

-- ===============================================================
-- STEP 4: Check CROP field type
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 4: What is in CROP field?'
PRINT '=========================================='
PRINT ''

PRINT 'Sample CROP values from tblCropTransportInvoice:'
SELECT DISTINCT TOP 5 CROP
FROM tblCropTransportInvoice
WHERE ISDELETED = 0
ORDER BY CROP

PRINT ''

PRINT 'Sample CROPID values from tblCropInvertDetail:'
SELECT DISTINCT TOP 5 CROPID
FROM tblCropInvertDetail
WHERE ISDELETED = 0
ORDER BY CROPID

PRINT ''

-- ===============================================================
-- STEP 5: Test the join
-- ===============================================================

PRINT '=========================================='
PRINT 'STEP 5: Test Join Conditions'
PRINT '=========================================='
PRINT ''

PRINT 'Test 1: Join by VEHCHNO and CROP = CROPID'
PRINT '-------------------------------------------'

SELECT
    ti.VEHCHNO,
    ti.CROP AS InvoiceCrop,
    iwd.CROPID AS InwardCropID,
    SUM(ISNULL(iwd.WEIGHT, 0) * ISNULL(iwd.RATE, 0)) AS PurchaseAmount
FROM tblCropTransportInvoice ti
LEFT JOIN tblCropInvertDetail iwd ON ti.VEHCHNO = iwd.VEHNO AND ti.CROP = iwd.CROPID
WHERE ti.VEHCHNO = @VehicleNo
AND ti.ISDELETED = 0
AND (iwd.ISDELETED = 0 OR iwd.ISDELETED IS NULL)
GROUP BY ti.VEHCHNO, ti.CROP, iwd.CROPID

PRINT ''

PRINT 'Test 2: Join only by VEHCHNO (ignore crop)'
PRINT '-------------------------------------------'

SELECT
    ti.VEHCHNO,
    ti.CROP AS InvoiceCrop,
    SUM(ISNULL(iwd.WEIGHT, 0) * ISNULL(iwd.RATE, 0)) AS PurchaseAmount
FROM tblCropTransportInvoice ti
LEFT JOIN tblCropInvertDetail iwd ON ti.VEHCHNO = iwd.VEHNO
WHERE ti.VEHCHNO = @VehicleNo
AND ti.ISDELETED = 0
AND (iwd.ISDELETED = 0 OR iwd.ISDELETED IS NULL)
GROUP BY ti.VEHCHNO, ti.CROP

PRINT ''

-- ===============================================================
-- SUMMARY
-- ===============================================================

PRINT '=========================================='
PRINT 'SUMMARY'
PRINT '=========================================='
PRINT ''

PRINT 'Possible Issues:'
PRINT '1. No inward data exists for this vehicle'
PRINT '2. CROP field contains name, not ID (mismatch)'
PRINT '3. Date ranges don''t overlap'
PRINT '4. COMPANYID/DEPTID filters exclude data'
PRINT ''
PRINT 'Check the results above to identify the issue.'
PRINT ''

GO
