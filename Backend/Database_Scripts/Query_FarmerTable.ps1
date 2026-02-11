# Query database to find farmer name column
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Querying farmer and item tables..." -ForegroundColor Yellow

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $tables = @('tblFarmer', 'tblVisitorMaster', 'tblITEM')

    foreach ($table in $tables) {
        $Query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '$table' ORDER BY ORDINAL_POSITION"

        $Command = $Connection.CreateCommand()
        $Command.CommandText = $Query
        $Reader = $Command.ExecuteReader()

        Write-Host "`n$table columns:" -ForegroundColor Green
        while ($Reader.Read()) {
            Write-Host "  - $($Reader['COLUMN_NAME'])" -ForegroundColor White
        }
        $Reader.Close()
    }

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
