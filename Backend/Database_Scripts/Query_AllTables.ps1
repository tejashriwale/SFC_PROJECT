# Query database to find all table names
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Querying database for all table names..." -ForegroundColor Yellow

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $Query = @"
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
AND (TABLE_NAME LIKE '%TRA%' OR TABLE_NAME LIKE '%BILL%')
ORDER BY TABLE_NAME
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()

    Write-Host "`nTables found:" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "  - $($Reader['TABLE_NAME'])" -ForegroundColor White
    }
    $Reader.Close()
    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
