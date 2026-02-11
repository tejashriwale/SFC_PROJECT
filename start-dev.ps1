# =============================================
# Start Development Environment (PowerShell)
# Opens separate PowerShell windows for Backend and Frontend
# =============================================

Write-Host ""
Write-Host "========================================"
Write-Host "Starting SFC Development Environment"
Write-Host "========================================"
Write-Host ""

# Get script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Start Backend in new PowerShell window
Write-Host "[1/2] Starting Backend Server..."
Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "& {
        Set-Location '$ScriptDir\Backend\Project'
        Write-Host ''
        Write-Host '======================================'
        Write-Host '   SFC BACKEND - .NET API SERVER'
        Write-Host '======================================'
        Write-Host ''
        Write-Host 'Starting backend server...'
        Write-Host ''
        dotnet run
    }"
)

# Wait 3 seconds before starting frontend
Start-Sleep -Seconds 3

# Start Frontend in new PowerShell window
Write-Host "[2/2] Starting Frontend Server..."
Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "& {
        Set-Location '$ScriptDir\Frontend'
        Write-Host ''
        Write-Host '======================================'
        Write-Host '   SFC FRONTEND - REACT APP (VITE)'
        Write-Host '======================================'
        Write-Host ''
        Write-Host 'Starting frontend server...'
        Write-Host ''
        npm run dev
    }"
)

Write-Host ""
Write-Host "========================================"
Write-Host "Both servers are starting!"
Write-Host "========================================"
Write-Host ""
Write-Host "Backend will run on: http://localhost:5000"
Write-Host "Frontend will run on: http://localhost:3000"
Write-Host ""
Write-Host "Press any key to close this window..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
