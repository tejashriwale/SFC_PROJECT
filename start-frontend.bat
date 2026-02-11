@echo off
REM =============================================
REM Start Frontend Only
REM Opens Frontend in current CMD window
REM =============================================

cd /d %~dp0Frontend

echo.
echo ======================================
echo    SFC FRONTEND - REACT APP (VITE)
echo ======================================
echo.
echo Starting frontend server...
echo Directory: %CD%
echo.
echo Press Ctrl+C to stop the server
echo.

npm run dev

pause
