# =============================================
# Deploy GET_FarmerCropBillData Function
# This script deploys the SQL function to the database
# =============================================

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Deploying GET_FarmerCropBillData Function" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Database connection details from appsettings.json
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

# SQL file path
$SqlFilePath = Join-Path $PSScriptRoot "GET_FarmerCropBillData.sql"

Write-Host "Connection Details:" -ForegroundColor Yellow
Write-Host "  Server: $Server" -ForegroundColor White
Write-Host "  Database: $Database" -ForegroundColor White
Write-Host "  SQL File: $SqlFilePath" -ForegroundColor White
Write-Host ""

# Check if SQL file exists
if (-not (Test-Path $SqlFilePath)) {
    Write-Host "ERROR: SQL file not found at $SqlFilePath" -ForegroundColor Red
    exit 1
}

try {
    Write-Host "Reading SQL file..." -ForegroundColor Yellow
    $SqlScript = Get-Content -Path $SqlFilePath -Raw

    Write-Host "Connecting to database..." -ForegroundColor Yellow

    # Create connection string
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"

    # Create SQL connection
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "Connected successfully!" -ForegroundColor Green
    Write-Host "Executing SQL script..." -ForegroundColor Yellow

    # Split script by GO statements and execute each batch
    $batches = $SqlScript -split '\r?\nGO\r?\n'

    foreach ($batch in $batches) {
        $trimmedBatch = $batch.Trim()
        if ($trimmedBatch.Length -gt 0 -and -not $trimmedBatch.StartsWith("PRINT")) {
            $Command = $Connection.CreateCommand()
            $Command.CommandText = $trimmedBatch
            $Command.CommandTimeout = 120
            $Command.ExecuteNonQuery() | Out-Null
        }
    }

    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "SUCCESS! Function deployed successfully!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""

    # Test the function
    Write-Host "Testing the function..." -ForegroundColor Yellow
    $TestCommand = $Connection.CreateCommand()
    $TestCommand.CommandText = "SELECT COUNT(*) AS FunctionExists FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_FarmerCropBillData]') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT')"

    $Reader = $TestCommand.ExecuteReader()
    if ($Reader.Read()) {
        $exists = $Reader["FunctionExists"]
        if ($exists -gt 0) {
            Write-Host "Function GET_FarmerCropBillData verified in database!" -ForegroundColor Green
        } else {
            Write-Host "Function not found in database!" -ForegroundColor Red
        }
    }
    $Reader.Close()

    $Connection.Close()

    Write-Host ""
    Write-Host "Deployment completed successfully!" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "1. Test the function with your application" -ForegroundColor White
    Write-Host "2. Verify BILLAMOUNT and PAYABLEAMT are now correct" -ForegroundColor White
    Write-Host ""

} catch {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "ERROR during deployment!" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    Write-Host ""
    exit 1
}
