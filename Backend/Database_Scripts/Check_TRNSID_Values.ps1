# Check what TRNSID values exist in the system
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Checking TRNSID values in tblAllTransections..." -ForegroundColor Cyan

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $Query = @"
SELECT DISTINCT TRNSID, COUNT(*) AS RecordCount
FROM tblAllTransections
WHERE ISDELETED = 0
GROUP BY TRNSID
ORDER BY TRNSID
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    Write-Host "TRNSID values found:" -ForegroundColor Green
    Write-Host "===================" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "  TRNSID: $($Reader['TRNSID']) - Records: $($Reader['RecordCount'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "Checking TRNTID values..." -ForegroundColor Yellow
    $Query2 = @"
SELECT DISTINCT TRNTID, COUNT(*) AS RecordCount
FROM tblAllTransections
WHERE ISDELETED = 0
GROUP BY TRNTID
ORDER BY TRNTID
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    Write-Host "TRNTID values found:" -ForegroundColor Green
    Write-Host "===================" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "  TRNTID: $($Reader['TRNTID']) - Records: $($Reader['RecordCount'])" -ForegroundColor White
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
