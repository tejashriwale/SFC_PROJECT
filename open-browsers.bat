@echo off
echo Opening browsers...
echo.
echo Backend Swagger: http://localhost:5200/swagger
start http://localhost:5200/swagger
timeout /t 2 /nobreak > nul
echo Frontend App: http://localhost:5173
start http://localhost:5173
echo.
echo Done!
