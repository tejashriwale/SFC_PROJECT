# Find BILLNO using REFAID
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$REFAID = "PK16D9708BD73A88292725"
$MAID = "PK16D970871FE541842435"

Write-Host "Finding BILLNO for REFAID: $REFAID" -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. Check if REFAID matches ASID in tblCropInvertMaster:" -ForegroundColor Yellow
    $Query1 = @"
SELECT BILLNO, CROPID, FAID, CRDATE, ASID, AMOUNT
FROM tblCropInvertMaster
WHERE ASID = '$REFAID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host ""
        Write-Host "   Found BILLNO in tblCropInvertMaster:" -ForegroundColor Green
        Write-Host "     BILLNO: $($Reader['BILLNO'])" -ForegroundColor White
        Write-Host "     CROPID: $($Reader['CROPID'])" -ForegroundColor White
        Write-Host "     FAID: $($Reader['FAID'])" -ForegroundColor White
        Write-Host "     AMOUNT: $($Reader['AMOUNT'])" -ForegroundColor White
        Write-Host "     ASID: $($Reader['ASID'])" -ForegroundColor White
    } else {
        Write-Host "   REFAID not found in tblCropInvertMaster.ASID" -ForegroundColor Yellow
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Alternative: Check tblVisitorMaster for bill reference:" -ForegroundColor Yellow
    $Query2 = @"
SELECT TOP 5 VAID, FAID, VDATE, ASID
FROM tblVisitorMaster
WHERE (ASID = '$REFAID' OR VAID = '$REFAID')
AND FAID = '$MAID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host ""
        Write-Host "   Found in tblVisitorMaster:" -ForegroundColor Green
        Write-Host "     VAID: $($Reader['VAID'])" -ForegroundColor White
        Write-Host "     FAID: $($Reader['FAID'])" -ForegroundColor White
        Write-Host "     ASID: $($Reader['ASID'])" -ForegroundColor White
    } else {
        Write-Host "   Not found in tblVisitorMaster" -ForegroundColor Yellow
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
