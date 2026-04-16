-- =====================================================
-- SQL CHANGES SCRIPT - SFC Project
-- Run this script on the production/online database
-- Date: 13 Apr 2026
-- =====================================================

-- =====================================================
-- 1. StockLoadingVehicleMaster Table - Add PONO column
-- =====================================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'StockLoadingVehicleMaster' AND COLUMN_NAME = 'PONO'
)
BEGIN
    ALTER TABLE StockLoadingVehicleMaster ADD PONO NVARCHAR(100) NULL DEFAULT '';
    PRINT 'Added PONO column to StockLoadingVehicleMaster';
END
ELSE
    PRINT 'PONO column already exists in StockLoadingVehicleMaster';
GO

-- =====================================================
-- 2. Update SP_AddUpdtStockLoadingVehicleMaster
--    to accept @PONO parameter
-- =====================================================
-- Check if @PONO parameter already exists in the SP
IF NOT EXISTS (
    SELECT 1 FROM sys.parameters p
    JOIN sys.objects o ON p.object_id = o.object_id
    WHERE o.name = 'SP_AddUpdtStockLoadingVehicleMaster' AND p.name = '@PONO'
)
BEGIN
    PRINT 'WARNING: @PONO parameter missing in SP_AddUpdtStockLoadingVehicleMaster - Please add it manually';
    PRINT 'Add this parameter: @PONO NVARCHAR(100) = ''''';
    PRINT 'Add this in INSERT/UPDATE: PONO = @PONO';
END
ELSE
    PRINT '@PONO parameter already exists in SP_AddUpdtStockLoadingVehicleMaster';
GO

-- =====================================================
-- 3. Update GET_StockLoadingVehicleMaster function
--    to return PONO column
-- =====================================================
-- Check if the function returns PONO
-- You need to ALTER the function to include PONO in SELECT
PRINT '--- CHECK: GET_StockLoadingVehicleMaster function ---';
PRINT 'Ensure the function returns PONO column in its SELECT statement.';
PRINT 'Example: SELECT ..., ISNULL(M.PONO, '''') AS PONO FROM StockLoadingVehicleMaster M ...';
GO

-- =====================================================
-- 4. CustomerLedger - Add new columns to base table
--    (if data is stored in a table, not just computed)
-- =====================================================

-- Check if a CustomerLedger base table exists and add columns
-- These columns may be computed in the function from joined tables
-- If your GET_CustomerLedger function joins with other tables,
-- you may not need table changes - just function changes

-- Option A: If columns come from a base table
IF OBJECT_ID('CustomerLedger', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'CROPNAME')
        ALTER TABLE CustomerLedger ADD CROPNAME NVARCHAR(500) NULL DEFAULT '';

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'WTPERSKU')
        ALTER TABLE CustomerLedger ADD WTPERSKU FLOAT NULL DEFAULT 0;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'QTYCRT')
        ALTER TABLE CustomerLedger ADD QTYCRT FLOAT NULL DEFAULT 0;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'QTYKG')
        ALTER TABLE CustomerLedger ADD QTYKG FLOAT NULL DEFAULT 0;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'RATE')
        ALTER TABLE CustomerLedger ADD RATE FLOAT NULL DEFAULT 0;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'RELEASEDATE')
        ALTER TABLE CustomerLedger ADD RELEASEDATE NVARCHAR(50) NULL DEFAULT '';

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerLedger' AND COLUMN_NAME = 'PCHARGES')
        ALTER TABLE CustomerLedger ADD PCHARGES FLOAT NULL DEFAULT 0;

    PRINT 'Added new columns to CustomerLedger table';
END
ELSE
    PRINT 'CustomerLedger table not found - columns may need to be added to the function directly';
GO

-- =====================================================
-- 5. UPDATE GET_CustomerLedger function
--    to return new columns from joined tables
-- =====================================================
-- The GET_CustomerLedger function is called as:
-- SELECT * FROM [GET_CustomerLedger](@COMPANYID, @DEPTID, @REFAID, @MAID, @SDATE, @EDATE)
--
-- Current columns returned: TRAID, REFAID, MAID, CCOMPANYNAME, CBAL, TRNDT, PAMT, TOTSAMT, VEHICLENO, ADVAMT
-- New columns needed:       CROPNAME, WTPERSKU, QTYCRT, QTYKG, RATE, RELEASEDATE, PCHARGES
--
-- RECOMMENDED: Join with StockLoadingVehicleDetail to get crop data
-- Below is a TEMPLATE - adjust table/column names to match your schema

PRINT '=== GET_CustomerLedger Function Update Template ===';
PRINT 'Add these columns to the function return:';
PRINT '  ISNULL(D.CROPNAME, '''') AS CROPNAME,';
PRINT '  ISNULL(D.RATE, 0) AS WTPERSKU,';
PRINT '  ISNULL(D.COUNT, 0) AS QTYCRT,';
PRINT '  CASE WHEN D.RATE > 0 THEN ISNULL(D.TOTALAMOUNT / D.RATE, 0) ELSE 0 END AS QTYKG,';
PRINT '  ISNULL(D.RATE, 0) AS RATE,';
PRINT '  '''' AS RELEASEDATE,';
PRINT '  ISNULL(D.CHARGES, 0) AS PCHARGES';
PRINT '';
PRINT 'Join: LEFT JOIN StockLoadingVehicleDetail D ON T.REFAID = D.SLVMID';
GO

-- =====================================================
-- 6. VERIFY: Check existing function definitions
-- =====================================================
-- Run these to see current function code before modifying:

-- View GET_CustomerLedger function
PRINT '--- To view current function definition, run: ---';
PRINT 'EXEC sp_helptext ''GET_CustomerLedger''';
GO

-- View GET_StockLoadingVehicleMaster function
PRINT 'EXEC sp_helptext ''GET_StockLoadingVehicleMaster''';
GO

-- View SP_AddUpdtStockLoadingVehicleMaster procedure
PRINT 'EXEC sp_helptext ''SP_AddUpdtStockLoadingVehicleMaster''';
GO

-- =====================================================
-- 7. QUICK VERIFICATION QUERIES
--    Run after making changes to verify
-- =====================================================

-- Test GET_StockLoadingVehicleMaster returns PONO
-- SELECT TOP 1 * FROM [GET_StockLoadingVehicleMaster]('%', '%', '<your_companyid>', '<your_deptid>');

-- Test GET_CustomerLedger returns new columns
-- SELECT TOP 1 * FROM [GET_CustomerLedger]('<companyid>', '<deptid>', '%', '%', '01 Jan 2025', '31 Dec 2026');

-- Test SP_AddUpdtStockLoadingVehicleMaster accepts @PONO
-- Check parameter list:
-- SELECT p.name, t.name AS type, p.max_length
-- FROM sys.parameters p
-- JOIN sys.types t ON p.user_type_id = t.user_type_id
-- WHERE p.object_id = OBJECT_ID('SP_AddUpdtStockLoadingVehicleMaster')
-- ORDER BY p.parameter_id;

PRINT '';
PRINT '=== Script Complete ===';
PRINT 'IMPORTANT: After running this script, review the function definitions';
PRINT 'using sp_helptext and manually update them to include the new columns.';
GO

-- =====================================================
-- 8. SP_UpdateApplicationDate - Update UAPPLICATIONDATE
--    for all users in a company/branch (no login check)
-- =====================================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateApplicationDate]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_UpdateApplicationDate]
GO

CREATE PROCEDURE [dbo].[SP_UpdateApplicationDate]
    @DATE NVARCHAR(100),
    @COMPANYID NVARCHAR(100),
    @BRANCHID NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE st_userlogin
        SET UAPPLICATIONDATE = @DATE
        WHERE UCOMPANYID = @COMPANYID AND UDEPARTMENTID = @BRANCHID;

        SELECT 1 AS IsSuccessful,
               '200' AS ResponseCode,
               N'Application date updated successfully.' AS ResponseMessage,
               '' AS ResponseValues;
    END TRY
    BEGIN CATCH
        SELECT 0 AS IsSuccessful,
               '500' AS ResponseCode,
               ERROR_MESSAGE() AS ResponseMessage,
               '' AS ResponseValues;
    END CATCH
END
GO

-- =====================================================
-- 9. GET_StockLoadingVehicleDetail - Add MSIZE column
--    Required for expense calculation during edit mode
-- =====================================================
-- The GET_StockLoadingVehicleDetail function must return MSIZE column.
-- Check current function definition:
-- EXEC sp_helptext 'GET_StockLoadingVehicleDetail'
--
-- Add MSIZE to the SELECT statement in the function.
-- Example: ISNULL(CAST(S.MSIZE AS NVARCHAR(100)), '') AS MSIZE
-- (Join with the appropriate table that has MSIZE, e.g. StockAuctionMasters)
PRINT '=== GET_StockLoadingVehicleDetail Function Update ===';
PRINT 'Add MSIZE column to the function return.';
PRINT 'Run: EXEC sp_helptext ''GET_StockLoadingVehicleDetail''';
PRINT 'Then ALTER the function to include MSIZE in its SELECT.';
GO
