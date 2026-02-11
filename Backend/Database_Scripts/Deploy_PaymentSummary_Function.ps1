# Deploy GET_FarmerPaymentSummary function
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Deploying GET_FarmerPaymentSummary function..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $SqlFile = Join-Path $PSScriptRoot "GET_FarmerPaymentSummary.sql"
    $SqlScript = Get-Content -Path $SqlFile -Raw
    $batches = $SqlScript -split '\r?\nGO\r?\n'

    foreach ($batch in $batches) {
        $trimmedBatch = $batch.Trim()
        if ($trimmedBatch.Length -gt 0 -and -not $trimmedBatch.StartsWith("PRINT") -and -not $trimmedBatch.StartsWith("USE")) {
            $Command = $Connection.CreateCommand()
            $Command.CommandText = $trimmedBatch
            $Command.CommandTimeout = 120
            $Command.ExecuteNonQuery() | Out-Null
        }
    }

    Write-Host "Function created successfully!" -ForegroundColor Green

    # Test the function
    Write-Host ""
    Write-Host "Testing function..." -ForegroundColor Yellow
    $TestQuery = "SELECT * FROM GET_FarmerPaymentSummary('PK16D9708BD73A88292725', 'CM001', 'DP001')"
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $TestQuery
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host ""
        Write-Host "Sample output:" -ForegroundColor Green
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "  $fieldName = $fieldValue" -ForegroundColor White
        }
    }
    $Reader.Close()

    $Connection.Close()

    Write-Host ""
    Write-Host "Deployment completed successfully!" -ForegroundColor Cyan

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
