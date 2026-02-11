# Check tblCropInvertMaster for BILLNO
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$REFAID = "PK16D9708BD73A88292725"
$MAID = "PK16D970871FE541842435"

Write-Host "Checking tblCropInvertMaster for BILLNO..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. Checking by REFAID (ASID or CMID):" -ForegroundColor Yellow
    $Query1 = @"
SELECT *
FROM tblCropInvertMaster
WHERE (CMID = '$REFAID' OR ASID = '$REFAID')
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host ""
        Write-Host "   Found in tblCropInvertMaster:" -ForegroundColor Green
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "     $fieldName = $fieldValue" -ForegroundColor White
        }
    } else {
        Write-Host "   No matching record by REFAID" -ForegroundColor Yellow
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Checking by MAID:" -ForegroundColor Yellow
    $Query2 = @"
SELECT TOP 3 CMID, BILLNO, MAID, CMDATE, ASID
FROM tblCropInvertMaster
WHERE MAID = '$MAID'
AND ISDELETED = 0
ORDER BY CMDATE DESC
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    while ($Reader.Read()) {
        Write-Host "   BILLNO: $($Reader['BILLNO']) | CMID: $($Reader['CMID']) | ASID: $($Reader['ASID']) | DATE: $($Reader['CMDATE'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "3. Check all columns in tblCropInvertMaster:" -ForegroundColor Yellow
    $Query3 = @"
SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblCropInvertMaster'
ORDER BY ORDINAL_POSITION
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query3
    $Reader = $Command.ExecuteReader()

    Write-Host "   Columns:" -ForegroundColor Green
    while ($Reader.Read()) {
        Write-Host "     - $($Reader['COLUMN_NAME'])" -ForegroundColor White
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
