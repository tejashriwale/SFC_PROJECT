@echo off
REM =============================================
REM Start Development Environment
REM Opens separate CMD windows for Backend and Frontend
REM =============================================

echo.
echo ========================================
echo Starting SFC Development Environment
echo ========================================
echo.

REM Start Backend in new CMD window
echo [1/2] Starting Backend Server...
start "SFC Backend - .NET API" cmd /k "cd /d %~dp0Backend\Project && echo. && echo ====================================== && echo    SFC BACKEND - .NET API SERVER && echo ====================================== && echo. && echo Starting backend server... && echo. && dotnet run"

REM Wait 3 seconds before starting frontend
timeout /t 3 /nobreak >nul

REM Start Frontend in new CMD window
echo [2/2] Starting Frontend Server...
start "SFC Frontend - React App" cmd /k "cd /d %~dp0Frontend && echo. && echo ====================================== && echo    SFC FRONTEND - REACT APP (VITE) && echo ====================================== && echo. && echo Starting frontend server... && echo. && npm run dev"

echo.
echo ========================================
echo Both servers are starting!
echo ========================================
echo.
echo Backend will run on: http://localhost:5000
echo Frontend will run on: http://localhost:3000
echo.
echo Press any key to close this window...
pause >nul
