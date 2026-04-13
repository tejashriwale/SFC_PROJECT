-- =============================================
-- Add PONO column to StockLoadingVehicleMaster table
-- Run this script on your database
-- =============================================

-- Step 1: Add PONO column to the table
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'StockLoadingVehicleMaster' AND COLUMN_NAME = 'PONO'
)
BEGIN
    ALTER TABLE [dbo].[StockLoadingVehicleMaster]
    ADD [PONO] NVARCHAR(100) NULL DEFAULT '';
    PRINT 'Column PONO added to StockLoadingVehicleMaster';
END
ELSE
BEGIN
    PRINT 'Column PONO already exists in StockLoadingVehicleMaster';
END
GO

-- Step 2: Update the Stored Procedure to accept PONO parameter
-- NOTE: You need to ALTER your existing SP_AddUpdtStockLoadingVehicleMaster
-- to add @PONO parameter. Add the following parameter and include it in INSERT/UPDATE:
--
-- ALTER PROCEDURE [dbo].[SP_AddUpdtStockLoadingVehicleMaster]
--     @SLVMID NVARCHAR(100),
--     @TRADER NVARCHAR(100),
--     @VEHICLENO NVARCHAR(100),
--     @CARETCOUNT NVARCHAR(100),
--     @GRADE NVARCHAR(MAX),
--     @UAID NVARCHAR(100),
--     @COMPANYID NVARCHAR(100),
--     @DEPTID NVARCHAR(100),
--     @CROP NVARCHAR(MAX),
--     @TOTEX FLOAT,
--     @TOTIWDAMT FLOAT,
--     @LDATE NVARCHAR(100),
--     @PONO NVARCHAR(100) = ''    -- << ADD THIS PARAMETER
-- AS
-- BEGIN
--     ... your existing logic ...
--     -- Add PONO to your INSERT and UPDATE statements
-- END

-- Step 3: Update the GET function to return PONO
-- NOTE: ALTER your GET_StockLoadingVehicleMaster function to include PONO in the SELECT:
--
-- SELECT ..., ISNULL(m.PONO, '') AS PONO
-- FROM StockLoadingVehicleMaster m
-- ...

PRINT 'Script completed. Please manually update SP and Function to include PONO.';
GO
