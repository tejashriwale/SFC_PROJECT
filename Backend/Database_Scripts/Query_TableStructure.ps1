# Query database to see table structure
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Querying table structure..." -ForegroundColor Yellow

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $tables = @('tblAllTransections', 'tblCropBill', 'tblVisitorMaster')

    foreach ($table in $tables) {
        $Query = @"
SELECT TOP 5 COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '$table'
ORDER BY ORDINAL_POSITION
"@

        $Command = $Connection.CreateCommand()
        $Command.CommandText = $Query
        $Reader = $Command.ExecuteReader()

        Write-Host "`n$table columns:" -ForegroundColor Green
        while ($Reader.Read()) {
            Write-Host "  - $($Reader['COLUMN_NAME']) ($($Reader['DATA_TYPE']))" -ForegroundColor White
        }
        $Reader.Close()
    }

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
