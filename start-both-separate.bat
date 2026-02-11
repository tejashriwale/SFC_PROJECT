@echo off
echo ====================================
echo  Starting Frontend and Backend in Separate Windows
echo ====================================
echo.
start "SFC Backend" cmd /k "start-backend.bat"
timeout /t 2 /nobreak > nul
start "SFC Frontend" cmd /k "start-frontend.bat"
timeout /t 8 /nobreak > nul
echo.
echo Opening browsers...
start http://localhost:5200/swagger
timeout /t 2 /nobreak > nul
start http://localhost:5173
