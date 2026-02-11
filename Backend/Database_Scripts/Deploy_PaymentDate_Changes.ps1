# Deploy Payment Date changes
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Deploying Payment Date Changes" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    # Step 1: Add PAYMENTDT column
    Write-Host "Step 1: Adding PAYMENTDT column..." -ForegroundColor Yellow
    $SqlFile1 = Join-Path $PSScriptRoot "Add_PaymentDate_Column.sql"
    $SqlScript1 = Get-Content -Path $SqlFile1 -Raw
    $batches1 = $SqlScript1 -split '\r?\nGO\r?\n'

    foreach ($batch in $batches1) {
        $trimmedBatch = $batch.Trim()
        if ($trimmedBatch.Length -gt 0 -and -not $trimmedBatch.StartsWith("PRINT") -and -not $trimmedBatch.StartsWith("USE")) {
            $Command = $Connection.CreateCommand()
            $Command.CommandText = $trimmedBatch
            $Command.CommandTimeout = 120
            $Command.ExecuteNonQuery() | Out-Null
        }
    }
    Write-Host "  Column added successfully!" -ForegroundColor Green

    # Step 2: Create GET_FarmerPaymentSummary function
    Write-Host ""
    Write-Host "Step 2: Creating GET_FarmerPaymentSummary function..." -ForegroundColor Yellow
    $SqlFile2 = Join-Path $PSScriptRoot "GET_FarmerPaymentSummary.sql"
    $SqlScript2 = Get-Content -Path $SqlFile2 -Raw
    $batches2 = $SqlScript2 -split '\r?\nGO\r?\n'

    foreach ($batch in $batches2) {
        $trimmedBatch = $batch.Trim()
        if ($trimmedBatch.Length -gt 0 -and -not $trimmedBatch.StartsWith("PRINT") -and -not $trimmedBatch.StartsWith("USE")) {
            $Command = $Connection.CreateCommand()
            $Command.CommandText = $trimmedBatch
            $Command.CommandTimeout = 120
            $Command.ExecuteNonQuery() | Out-Null
        }
    }
    Write-Host "  Function created successfully!" -ForegroundColor Green

    # Verify
    Write-Host ""
    Write-Host "Step 3: Verifying changes..." -ForegroundColor Yellow

    # Check column exists
    $Query = @"
SELECT COUNT(*) AS ColumnExists
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblAllTransections'
AND COLUMN_NAME = 'PAYMENTDT'
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()
    if ($Reader.Read() -and $Reader["ColumnExists"] -gt 0) {
        Write-Host "  PAYMENTDT column verified!" -ForegroundColor Green
    }
    $Reader.Close()

    # Check function exists
    $Query2 = @"
SELECT COUNT(*) AS FunctionExists
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GET_FarmerPaymentSummary]')
AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT')
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()
    if ($Reader.Read() -and $Reader["FunctionExists"] -gt 0) {
        Write-Host "  GET_FarmerPaymentSummary function verified!" -ForegroundColor Green
    }
    $Reader.Close()

    $Connection.Close()

    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Deployment completed successfully!" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Changes made:" -ForegroundColor Yellow
    Write-Host "  1. Added PAYMENTDT column to tblAllTransections" -ForegroundColor White
    Write-Host "  2. Created GET_FarmerPaymentSummary function" -ForegroundColor White
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "  1. Update backend API to use GET_FarmerPaymentSummary" -ForegroundColor White
    Write-Host "  2. Update frontend to show both dates" -ForegroundColor White
    Write-Host "  3. Make PAYMENTDATE editable in UI" -ForegroundColor White
    Write-Host ""

} catch {
    Write-Host ""
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
