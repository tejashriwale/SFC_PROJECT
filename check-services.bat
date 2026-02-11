@echo off
echo.
echo ====================================
echo  Checking Service Status
echo ====================================
echo.

REM Check if Backend is running on port 5200
echo Checking Backend (Port 5200)...
netstat -ano | findstr ":5200" > nul
if %errorlevel% equ 0 (
    echo [OK] Backend is RUNNING on port 5200
    echo     Swagger: http://localhost:5200/swagger
) else (
    echo [X] Backend is NOT running on port 5200
)

echo.

REM Check if Frontend is running on port 5173
echo Checking Frontend (Port 5173)...
netstat -ano | findstr ":5173" > nul
if %errorlevel% equ 0 (
    echo [OK] Frontend is RUNNING on port 5173
    echo     App: http://localhost:5173
) else (
    echo [X] Frontend is NOT running on port 5173
)

echo.
echo ====================================
echo.
pause
