-- =============================================
-- Add PAYMENTDT column to tblAllTransections
-- This will store the payment date separately from transaction date
-- =============================================

USE [InventoryManagement]
GO

PRINT 'Adding PAYMENTDT column to tblAllTransections...'
PRINT ''

-- Check if column already exists
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblAllTransections'
    AND COLUMN_NAME = 'PAYMENTDT'
)
BEGIN
    -- Add the new column
    ALTER TABLE tblAllTransections
    ADD PAYMENTDT DATETIME NULL

    PRINT 'PAYMENTDT column added successfully!'

    -- Initialize existing payment records with TRNDT value
    UPDATE tblAllTransections
    SET PAYMENTDT = TRNDT
    WHERE TRNSID = '20'  -- Only for payment transactions
    AND PAYMENTDT IS NULL

    PRINT 'Initialized PAYMENTDT for existing payment records'
END
ELSE
BEGIN
    PRINT 'PAYMENTDT column already exists'
END

PRINT ''
PRINT 'Column addition completed!'
GO
