# Check tblCropInvertMaster columns first
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Checking tblCropInvertMaster structure..." -ForegroundColor Cyan

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $Query = @"
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblCropInvertMaster'
ORDER BY ORDINAL_POSITION
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    Write-Host "Columns in tblCropInvertMaster:" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "  $($Reader['COLUMN_NAME']) ($($Reader['DATA_TYPE']))" -ForegroundColor White
    }
    $Reader.Close()

    # Get a sample record
    Write-Host ""
    Write-Host "Sample record from tblCropInvertMaster:" -ForegroundColor Yellow
    $Query2 = "SELECT TOP 1 * FROM tblCropInvertMaster WHERE ISDELETED = 0"

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "  $fieldName = $fieldValue" -ForegroundColor White
        }
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
