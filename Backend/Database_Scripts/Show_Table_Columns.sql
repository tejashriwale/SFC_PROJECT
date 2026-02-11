-- ===============================================================
-- SHOW ACTUAL TABLE COLUMN NAMES
-- This script shows all column names for the required tables
-- ===============================================================

USE [InventoryManagement]
GO

SET NOCOUNT ON
GO

PRINT ''
PRINT '=========================================='
PRINT 'TABLE COLUMN INSPECTION'
PRINT '=========================================='
PRINT ''

-- ===============================================================
-- tblCropInvertMaster
-- ===============================================================

PRINT '1. tblCropInvertMaster Columns:'
PRINT '================================'

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertMaster')
BEGIN
    SELECT
        ORDINAL_POSITION AS [#],
        COLUMN_NAME AS [Column Name],
        DATA_TYPE AS [Data Type],
        CASE
            WHEN COLUMN_NAME LIKE '%VEHIMPLEMENT%' OR COLUMN_NAME LIKE '%VEH%' THEN '← VEHICLE COLUMN'
            WHEN COLUMN_NAME LIKE '%ID%' AND ORDINAL_POSITION <= 3 THEN '← PRIMARY KEY?'
            ELSE ''
        END AS [Notes]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertMaster'
    ORDER BY ORDINAL_POSITION
END
ELSE
    PRINT '✗ Table not found'

PRINT ''
PRINT ''

-- ===============================================================
-- tblCropInvertDetail
-- ===============================================================

PRINT '2. tblCropInvertDetail Columns:'
PRINT '==============================='

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropInvertDetail')
BEGIN
    SELECT
        ORDINAL_POSITION AS [#],
        COLUMN_NAME AS [Column Name],
        DATA_TYPE AS [Data Type],
        CASE
            WHEN COLUMN_NAME LIKE '%ID%' AND ORDINAL_POSITION <= 3 THEN '← PRIMARY/FOREIGN KEY?'
            WHEN COLUMN_NAME = 'RATE' THEN '← RATE COLUMN'
            WHEN COLUMN_NAME = 'WEIGHT' THEN '← WEIGHT COLUMN'
            ELSE ''
        END AS [Notes]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropInvertDetail'
    ORDER BY ORDINAL_POSITION
END
ELSE
    PRINT '✗ Table not found'

PRINT ''
PRINT ''

-- ===============================================================
-- tblCropTransportInvoice
-- ===============================================================

PRINT '3. tblCropTransportInvoice Columns:'
PRINT '===================================='

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoice')
BEGIN
    SELECT
        ORDINAL_POSITION AS [#],
        COLUMN_NAME AS [Column Name],
        DATA_TYPE AS [Data Type],
        CASE
            WHEN COLUMN_NAME LIKE '%ID%' AND ORDINAL_POSITION <= 3 THEN '← PRIMARY KEY?'
            WHEN COLUMN_NAME LIKE '%VEH%' THEN '← VEHICLE COLUMN'
            WHEN COLUMN_NAME LIKE '%EXPENSE%' THEN '← EXPENSE COLUMN'
            ELSE ''
        END AS [Notes]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropTransportInvoice'
    ORDER BY ORDINAL_POSITION
END
ELSE
    PRINT '✗ Table not found'

PRINT ''
PRINT ''

-- ===============================================================
-- tblCropTransportInvoiceDetail
-- ===============================================================

PRINT '4. tblCropTransportInvoiceDetail Columns:'
PRINT '=========================================='

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblCropTransportInvoiceDetail')
BEGIN
    SELECT
        ORDINAL_POSITION AS [#],
        COLUMN_NAME AS [Column Name],
        DATA_TYPE AS [Data Type],
        CASE
            WHEN COLUMN_NAME LIKE '%ID%' AND ORDINAL_POSITION <= 3 THEN '← PRIMARY/FOREIGN KEY?'
            WHEN COLUMN_NAME LIKE '%AMT%' OR COLUMN_NAME LIKE '%AMOUNT%' THEN '← AMOUNT COLUMN'
            WHEN COLUMN_NAME LIKE '%CRT%' OR COLUMN_NAME LIKE '%CRATE%' THEN '← CRATE COUNT'
            ELSE ''
        END AS [Notes]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblCropTransportInvoiceDetail'
    ORDER BY ORDINAL_POSITION
END
ELSE
    PRINT '✗ Table not found'

PRINT ''
PRINT ''

-- ===============================================================
-- SUMMARY
-- ===============================================================

PRINT '=========================================='
PRINT 'KEY COLUMNS TO IDENTIFY'
PRINT '=========================================='
PRINT ''
PRINT 'For tblCropInvertMaster, find:'
PRINT '• Primary Key (ID column)'
PRINT '• Vehicle Number column'
PRINT '• Company ID, Dept ID'
PRINT '• System Date (SYSDT or similar)'
PRINT ''
PRINT 'For tblCropInvertDetail, find:'
PRINT '• Foreign Key to Master table'
PRINT '• RATE and WEIGHT columns'
PRINT '• CROPID column'
PRINT ''
PRINT 'For tblCropTransportInvoice, find:'
PRINT '• Primary Key (ID column)'
PRINT '• Vehicle Number column'
PRINT '• EXPENSES column'
PRINT ''
PRINT 'For tblCropTransportInvoiceDetail, find:'
PRINT '• Foreign Key to Invoice table'
PRINT '• Amount column (TAMT or similar)'
PRINT '• Crate count column (CRTCNT or similar)'
PRINT ''

GO
