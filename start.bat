@echo off
echo ====================================
echo  Starting Full-Stack Application
echo ====================================
echo.
echo Frontend: React + Vite (Port 5173)
echo Backend:  ASP.NET Core (Port 5200)
echo.
echo Installing dependencies...
call npm install
echo.
echo Starting both servers...
echo.

REM Start the dev servers in background
start "SFC-Backend-Frontend" cmd /k "npm run dev"

REM Wait for servers to start
echo Waiting for servers to start...
timeout /t 8 /nobreak > nul

REM Open browsers
echo Opening browsers...
start http://localhost:5200/swagger
timeout /t 2 /nobreak > nul
start http://localhost:5173

echo.
echo ====================================
echo  Both servers are running!
echo ====================================
echo Backend Swagger: http://localhost:5200/swagger
echo Frontend App:    http://localhost:5173
echo.
echo Press any key to close this window...
pause > nul
