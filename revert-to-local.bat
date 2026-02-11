@echo off
echo ========================================
echo  SFC Revert to Local Development
echo ========================================
echo.
echo This will restore local development configuration
echo and remove production build files.
echo.
echo Press any key to continue or close this window to cancel...
pause >nul
echo.

echo [1/3] Restoring environment files...
if exist "Frontend\.env.development.backup" (
    copy "Frontend\.env.development.backup" "Frontend\.env.development" >nul 2>&1
    echo   - Frontend .env.development restored
) else (
    echo   - No .env.development backup found (skipping)
)

if exist "Backend\Project\appsettings.json.backup" (
    copy "Backend\Project\appsettings.json.backup" "Backend\Project\appsettings.json" >nul 2>&1
    echo   - Backend appsettings.json restored
) else (
    echo   - No appsettings.json backup found (skipping)
)

echo.
echo [2/3] Cleaning build outputs...
if exist "production\" (
    rmdir /s /q "production\"
    echo   - Removed production\ folder
) else (
    echo   - Production folder not found (skipping)
)

if exist "Frontend\dist\" (
    rmdir /s /q "Frontend\dist\"
    echo   - Removed Frontend\dist\
) else (
    echo   - Frontend\dist\ not found (skipping)
)

if exist "Backend\Project\bin\Release\" (
    rmdir /s /q "Backend\Project\bin\Release\"
    echo   - Removed Backend\Project\bin\Release\
) else (
    echo   - Backend release build not found (skipping)
)

echo.
echo [3/3] Configuration restored!
echo.
echo ========================================
echo  REVERTED TO LOCAL DEVELOPMENT
echo ========================================
echo.
echo You can now run local development with:
echo    start.bat
echo.
echo Or manually start the servers:
echo    npm run dev
echo.
echo ========================================
pause
