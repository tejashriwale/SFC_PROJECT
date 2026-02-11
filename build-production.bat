@echo off
echo ========================================
echo  SFC Production Build Script
echo ========================================
echo.

REM Create backups
echo [1/5] Creating backups...
copy "Frontend\.env.production" "Frontend\.env.production.backup" >nul 2>&1
copy "Backend\Project\appsettings.json" "Backend\Project\appsettings.json.backup" >nul 2>&1
echo   - Configuration backups created

REM Build Frontend
echo.
echo [2/5] Building Frontend...
echo   - Increasing Node.js memory limit...
set NODE_OPTIONS=--max-old-space-size=4096
cd Frontend
call npm run build
if errorlevel 1 (
    echo.
    echo ====================================
    echo  ERROR: Frontend build FAILED!
    echo ====================================
    echo.
    pause
    exit /b 1
)
cd ..
echo   - Frontend build completed successfully

REM Build Backend
echo.
echo [3/5] Building Backend...
dotnet publish Backend\Project\Project.csproj -c Release -o Backend\Project\bin\Release\net8.0\publish
if errorlevel 1 (
    echo.
    echo ====================================
    echo  ERROR: Backend build FAILED!
    echo ====================================
    echo.
    pause
    exit /b 1
)
echo   - Backend build completed successfully

echo.
echo [4/6] Verifying build outputs...
if not exist "Frontend\dist\index.html" (
    echo.
    echo ====================================
    echo  ERROR: Frontend build incomplete!
    echo  Missing: Frontend\dist\index.html
    echo ====================================
    echo.
    pause
    exit /b 1
)
if not exist "Backend\Project\bin\Release\net8.0\publish\Project.dll" (
    echo.
    echo ====================================
    echo  ERROR: Backend build incomplete!
    echo  Missing: Backend\Project\bin\Release\net8.0\publish\Project.dll
    echo ====================================
    echo.
    pause
    exit /b 1
)
echo   - All build outputs verified

echo.
echo [5/6] Organizing production files...
REM Create production folder structure
if exist "production" (
    echo   - Cleaning previous production folder...
    rmdir /s /q "production" >nul 2>&1
)
mkdir "production" >nul 2>&1
mkdir "production\frontend" >nul 2>&1
mkdir "production\backend" >nul 2>&1

REM Copy frontend build to production folder
echo   - Copying frontend build to production\frontend\...
xcopy "Frontend\dist\*" "production\frontend\" /E /I /Y >nul
if errorlevel 1 (
    echo   ERROR: Failed to copy frontend files!
    pause
    exit /b 1
)

REM Copy backend build to production folder
echo   - Copying backend build to production\backend\...
xcopy "Backend\Project\bin\Release\net8.0\publish\*" "production\backend\" /E /I /Y >nul
if errorlevel 1 (
    echo   ERROR: Failed to copy backend files!
    pause
    exit /b 1
)

REM Create deployment info file
echo   - Creating deployment-info.txt...
echo SFC Full-Stack Application - Production Build > "production\deployment-info.txt"
echo Build Date: %date% %time% >> "production\deployment-info.txt"
echo. >> "production\deployment-info.txt"
echo Frontend URL: http://adsvr:333/ >> "production\deployment-info.txt"
echo Backend URL:  http://adsvr:333/Backend >> "production\deployment-info.txt"
echo Swagger URL:  http://adsvr:333/Backend/swagger >> "production\deployment-info.txt"
echo. >> "production\deployment-info.txt"
echo Deployment Instructions: >> "production\deployment-info.txt"
echo 1. Deploy production\frontend\ to IIS site at http://adsvr:333 >> "production\deployment-info.txt"
echo 2. Deploy production\backend\ to IIS application at http://adsvr:333/Backend >> "production\deployment-info.txt"
echo. >> "production\deployment-info.txt"
echo For detailed instructions, see DEPLOYMENT.md in the root folder >> "production\deployment-info.txt"

echo   - Production folder organized successfully!

echo.
echo [6/6] Production build complete!
echo.
echo ========================================
echo  BUILD SUCCESSFUL!
echo ========================================
echo.
echo Production Folder Structure:
echo  %CD%\production\
echo  ├── frontend\         (Deploy to IIS site)
echo  ├── backend\          (Deploy to IIS application)
echo  └── deployment-info.txt
echo.
echo ========================================
echo  DEPLOYMENT INSTRUCTIONS
echo ========================================
echo.
echo All production files are organized in: %CD%\production\
echo.
echo 1. Open IIS Manager on server 'adsvr'
echo.
echo 2. Create IIS Site for Frontend:
echo    - Site Name: SFC Frontend
echo    - Physical Path: %CD%\production\frontend\
echo    - Binding: http://adsvr:333
echo.
echo 3. Create IIS Application for Backend:
echo    - Right-click "SFC Frontend" site
echo    - Select "Add Application"
echo    - Alias: backend (lowercase!)
echo    - Physical Path: %CD%\production\backend\
echo    - Application Pool: Create new pool
echo      * Name: SFC_Backend_Pool
echo      * .NET CLR Version: No Managed Code
echo      * Managed Pipeline Mode: Integrated
echo.
echo 4. Test the deployment:
echo    - Frontend: http://adsvr:333/
echo    - Backend:  http://adsvr:333/Backend/swagger
echo.
echo For detailed deployment instructions, see DEPLOYMENT.md
echo.
echo ========================================
pause
