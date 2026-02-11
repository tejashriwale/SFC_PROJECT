@echo off
REM =============================================
REM AUTOMATED SCRIPT: Connect, Find, Check, and Fix SP
REM Batch script using sqlcmd to automatically fix SP_AddUpdAllTransections
REM =============================================

echo.
echo ==========================================
echo   AUTOMATED SP FIX SCRIPT
echo ==========================================
echo.

REM Database connection details from appsettings.json
set SERVER=ADSVR\SQLEXPRESS01
set DATABASE=InventoryManagement
set USERNAME=sa
set PASSWORD=123456

echo Database Connection:
echo   Server:   %SERVER%
echo   Database: %DATABASE%
echo   User:     %USERNAME%
echo.

echo Connecting to database...
echo.

REM Create temporary SQL file with all commands
echo USE [%DATABASE%] > "%TEMP%\fix_sp.sql"
echo GO >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo PRINT '' >> "%TEMP%\fix_sp.sql"
echo PRINT '==========================================' >> "%TEMP%\fix_sp.sql"
echo PRINT 'STEP 1: Checking if SP exists' >> "%TEMP%\fix_sp.sql"
echo PRINT '==========================================' >> "%TEMP%\fix_sp.sql"
echo PRINT '' >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo IF EXISTS ^(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_AddUpdAllTransections'^) >> "%TEMP%\fix_sp.sql"
echo BEGIN >> "%TEMP%\fix_sp.sql"
echo     PRINT 'V SP_AddUpdAllTransections found in database' >> "%TEMP%\fix_sp.sql"
echo     PRINT '' >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo     PRINT '==========================================' >> "%TEMP%\fix_sp.sql"
echo     PRINT 'STEP 2: Checking current parameters' >> "%TEMP%\fix_sp.sql"
echo     PRINT '==========================================' >> "%TEMP%\fix_sp.sql"
echo     PRINT '' >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo     SELECT >> "%TEMP%\fix_sp.sql"
echo         ORDINAL_POSITION AS [Order], >> "%TEMP%\fix_sp.sql"
echo         PARAMETER_NAME AS [Parameter], >> "%TEMP%\fix_sp.sql"
echo         DATA_TYPE AS [Type] >> "%TEMP%\fix_sp.sql"
echo     FROM INFORMATION_SCHEMA.PARAMETERS >> "%TEMP%\fix_sp.sql"
echo     WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections' >> "%TEMP%\fix_sp.sql"
echo     ORDER BY ORDINAL_POSITION >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo     PRINT '' >> "%TEMP%\fix_sp.sql"
echo. >> "%TEMP%\fix_sp.sql"
echo     IF EXISTS ^( >> "%TEMP%\fix_sp.sql"
echo         SELECT * FROM INFORMATION_SCHEMA.PARAMETERS >> "%TEMP%\fix_sp.sql"
echo         WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections' >> "%TEMP%\fix_sp.sql"
echo         AND PARAMETER_NAME = '@PAYMENTDT' >> "%TEMP%\fix_sp.sql"
echo     ^) >> "%TEMP%\fix_sp.sql"
echo     BEGIN >> "%TEMP%\fix_sp.sql"
echo         PRINT 'V @PAYMENTDT parameter already exists!' >> "%TEMP%\fix_sp.sql"
echo         PRINT '' >> "%TEMP%\fix_sp.sql"
echo         PRINT 'Status: Stored procedure is already up to date.' >> "%TEMP%\fix_sp.sql"
echo         PRINT 'No update needed.' >> "%TEMP%\fix_sp.sql"
echo     END >> "%TEMP%\fix_sp.sql"
echo     ELSE >> "%TEMP%\fix_sp.sql"
echo     BEGIN >> "%TEMP%\fix_sp.sql"
echo         PRINT 'X @PAYMENTDT parameter NOT FOUND' >> "%TEMP%\fix_sp.sql"
echo         PRINT '' >> "%TEMP%\fix_sp.sql"
echo         PRINT 'Updating stored procedure...' >> "%TEMP%\fix_sp.sql"
echo         PRINT '' >> "%TEMP%\fix_sp.sql"
echo         DROP PROCEDURE [dbo].[SP_AddUpdAllTransections] >> "%TEMP%\fix_sp.sql"
echo         PRINT 'V Old procedure dropped' >> "%TEMP%\fix_sp.sql"
echo     END >> "%TEMP%\fix_sp.sql"
echo END >> "%TEMP%\fix_sp.sql"
echo ELSE >> "%TEMP%\fix_sp.sql"
echo BEGIN >> "%TEMP%\fix_sp.sql"
echo     PRINT 'X SP_AddUpdAllTransections NOT FOUND' >> "%TEMP%\fix_sp.sql"
echo     PRINT '' >> "%TEMP%\fix_sp.sql"
echo     PRINT 'Creating new stored procedure...' >> "%TEMP%\fix_sp.sql"
echo END >> "%TEMP%\fix_sp.sql"
echo GO >> "%TEMP%\fix_sp.sql"

REM Append the full SQL script to create/update the SP
type "%~dp0Get_Current_SP_And_Update.sql" >> "%TEMP%\fix_sp.sql"

echo.
echo Running SQL commands...
echo.

REM Execute the SQL script
sqlcmd -S %SERVER% -U %USERNAME% -P %PASSWORD% -d %DATABASE% -i "%TEMP%\fix_sp.sql"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ==========================================
    echo   V SCRIPT COMPLETED SUCCESSFULLY
    echo ==========================================
    echo.
    echo Next steps:
    echo   1. Restart backend server ^(Ctrl+C then dotnet run^)
    echo   2. Test payment date functionality
    echo   3. Verify dates are saved in database
    echo.
) else (
    echo.
    echo ==========================================
    echo   X ERROR OCCURRED
    echo ==========================================
    echo.
    echo Possible solutions:
    echo   - Check if SQL Server is running
    echo   - Verify connection details are correct
    echo   - Ensure sqlcmd is installed
    echo.
    echo Alternatively, run the SQL script manually in SSMS:
    echo   %~dp0Get_Current_SP_And_Update.sql
    echo.
)

REM Clean up temporary file
del "%TEMP%\fix_sp.sql" 2>nul

echo.
echo Press any key to exit...
pause >nul
