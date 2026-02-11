-- =============================================
-- DIAGNOSTIC ONLY: Show current SP without updating
-- Run this first to see what's in the database
-- =============================================

USE [InventoryManagement]
GO

PRINT ''
PRINT '=========================================='
PRINT 'CURRENT DATABASE STATE'
PRINT '=========================================='
PRINT ''

-- =============================================
-- Check if stored procedure exists
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections')
BEGIN
    PRINT '✓ SP_AddUpdAllTransections EXISTS in database'
    PRINT ''

    -- =============================================
    -- Show all current parameters
    -- =============================================

    PRINT 'Current Parameters:'
    PRINT '-------------------'
    PRINT ''

    SELECT
        ORDINAL_POSITION AS [#],
        PARAMETER_NAME AS [Parameter Name],
        DATA_TYPE AS [Data Type],
        CASE
            WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
            THEN CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)
            ELSE 'N/A'
        END AS [Max Length],
        CASE
            WHEN PARAMETER_MODE = 'IN' THEN 'Input'
            WHEN PARAMETER_MODE = 'OUT' THEN 'Output'
            ELSE PARAMETER_MODE
        END AS [Mode]
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
    ORDER BY ORDINAL_POSITION

    PRINT ''

    -- =============================================
    -- Count parameters
    -- =============================================

    DECLARE @TotalParams INT
    SELECT @TotalParams = COUNT(*)
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'

    PRINT 'Total Parameters: ' + CAST(@TotalParams AS VARCHAR)
    PRINT ''

    -- =============================================
    -- Check for PAYMENTDT parameter
    -- =============================================

    IF EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.PARAMETERS
        WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
        AND PARAMETER_NAME = '@PAYMENTDT'
    )
    BEGIN
        PRINT '✓ @PAYMENTDT parameter EXISTS'
        PRINT ''
        PRINT 'Status: Stored procedure is up to date!'
        PRINT 'No action needed.'
    END
    ELSE
    BEGIN
        PRINT '✗ @PAYMENTDT parameter MISSING'
        PRINT ''
        PRINT 'Status: Stored procedure needs update!'
        PRINT ''
        PRINT 'To fix, run one of these scripts:'
        PRINT '  • Get_Current_SP_And_Update.sql'
        PRINT '  • Complete_SP_Diagnostic_And_Fix.sql'
    END

    PRINT ''

    -- =============================================
    -- Show stored procedure definition
    -- =============================================

    PRINT '=========================================='
    PRINT 'STORED PROCEDURE DEFINITION'
    PRINT '=========================================='
    PRINT ''

    DECLARE @SPDefinition NVARCHAR(MAX)
    SELECT @SPDefinition = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdAllTransections'))

    -- Show first 500 characters
    PRINT 'First 500 characters of SP definition:'
    PRINT '---------------------------------------'
    PRINT SUBSTRING(@SPDefinition, 1, 500) + '...'
    PRINT ''

    -- Full definition (shown in results grid)
    SELECT
        OBJECT_DEFINITION(OBJECT_ID('dbo.SP_AddUpdAllTransections')) AS [Full_SP_Definition]

END
ELSE
BEGIN
    PRINT '✗ SP_AddUpdAllTransections DOES NOT EXIST in database'
    PRINT ''
    PRINT 'This is unusual. The stored procedure should exist.'
    PRINT ''
    PRINT 'Possible reasons:'
    PRINT '  • Wrong database selected'
    PRINT '  • Procedure was deleted'
    PRINT '  • Procedure has a different name'
    PRINT ''
    PRINT 'Check available stored procedures:'
    PRINT ''

    SELECT
        name AS [Available_Stored_Procedures]
    FROM sys.objects
    WHERE type = 'P'
    AND name LIKE '%Trans%'
    ORDER BY name
END

PRINT ''

-- =============================================
-- Check table column
-- =============================================

PRINT '=========================================='
PRINT 'TABLE COLUMN CHECK'
PRINT '=========================================='
PRINT ''

IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblAllTransections'
    AND COLUMN_NAME = 'PAYMENTDT'
)
BEGIN
    PRINT '✓ PAYMENTDT column EXISTS in tblAllTransections table'
    PRINT ''

    SELECT
        COLUMN_NAME,
        DATA_TYPE,
        IS_NULLABLE,
        CASE
            WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL
            THEN CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)
            ELSE 'N/A'
        END AS [Max_Length]
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblAllTransections'
    AND COLUMN_NAME = 'PAYMENTDT'
END
ELSE
BEGIN
    PRINT '✗ PAYMENTDT column MISSING in tblAllTransections table'
    PRINT ''
    PRINT 'You need to add this column first!'
    PRINT ''
    PRINT 'Run this command:'
    PRINT 'ALTER TABLE tblAllTransections ADD PAYMENTDT DATETIME NULL'
    PRINT ''
    PRINT 'Or run: Add_PaymentDate_Column.sql'
END

PRINT ''
PRINT '=========================================='
PRINT 'DIAGNOSTIC COMPLETE'
PRINT '=========================================='
PRINT ''
