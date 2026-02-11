# Check payment data structure
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$REFAID = "PK16D9708BD73A88292725"
$MAID = "PK16D970871FE541842435"

Write-Host "Checking payment data structure..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. Check payment transactions (TRNSID=20):" -ForegroundColor Yellow
    $Query1 = @"
SELECT *
FROM tblAllTransections
WHERE REFAID = '$REFAID'
AND MAID = '$MAID'
AND TRNSID = '20'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    if ($Reader.Read()) {
        Write-Host "   Payment transaction columns:" -ForegroundColor Green
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "     $fieldName = $fieldValue" -ForegroundColor White
        }
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Check if there's a separate payment date column:" -ForegroundColor Yellow
    $Query2 = @"
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblAllTransections'
AND (COLUMN_NAME LIKE '%DATE%' OR COLUMN_NAME LIKE '%DT%')
ORDER BY ORDINAL_POSITION
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    Write-Host "   Date columns in tblAllTransections:" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "     $($Reader['COLUMN_NAME']) ($($Reader['DATA_TYPE']))" -ForegroundColor White
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
