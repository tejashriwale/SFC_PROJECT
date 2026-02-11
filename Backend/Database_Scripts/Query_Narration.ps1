# Query to check if NARR column exists
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Checking for NARR or similar columns..." -ForegroundColor Yellow

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    # Check tblAllTransections for NARR column
    $Query = @"
SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblAllTransections'
AND (COLUMN_NAME LIKE '%NARR%' OR COLUMN_NAME LIKE '%DESC%' OR COLUMN_NAME LIKE '%REMARK%')
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()

    $hasNarr = $false
    Write-Host "`nColumns found:" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "  - $($Reader['COLUMN_NAME'])" -ForegroundColor White
        $hasNarr = $true
    }

    if (-not $hasNarr) {
        Write-Host "  - No NARR/DESC/REMARK column found" -ForegroundColor Yellow
    }

    $Reader.Close()
    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
