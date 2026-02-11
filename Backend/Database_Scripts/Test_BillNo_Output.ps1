# Test BILLNO output
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$CompanyID = "CM001"
$DeptID = "DP001"
$MAID = "PK16D970871FE541842435"
$CropID = "PK16D970891DDD14842589"

Write-Host "Testing BILLNO output from function..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    $Query = "SELECT * FROM GET_FarmerCropBillData('$CompanyID', '$DeptID', '$MAID', '$CropID')"

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host "Function output:" -ForegroundColor Green
        Write-Host ""
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "  $fieldName = $fieldValue" -ForegroundColor White
        }
    } else {
        Write-Host "No data returned!" -ForegroundColor Red
    }

    $Reader.Close()
    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
