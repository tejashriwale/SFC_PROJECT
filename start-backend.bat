@echo off
REM =============================================
REM Start Backend Only
REM Opens Backend in current CMD window
REM =============================================

cd /d %~dp0Backend\Project

echo.
echo ======================================
echo    SFC BACKEND - .NET API SERVER
echo ======================================
echo.
echo Starting backend server...
echo Directory: %CD%
echo.
echo Press Ctrl+C to stop the server
echo.

dotnet run

pause
