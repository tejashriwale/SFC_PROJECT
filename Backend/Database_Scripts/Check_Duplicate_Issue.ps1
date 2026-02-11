# Check why there are duplicate rows
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$CompanyID = "CM001"
$DeptID = "DP001"
$MAID = "PK16D970871FE541842435"
$CropID = "PK16D970891DDD14842589"

Write-Host "Checking for duplicate rows issue..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. All transactions for this bill:" -ForegroundColor Yellow
    $Query1 = @"
SELECT TRAID, TRNSR, TRNSID, TRNTID, CROPID, REFAID, PAMT, TRNDT
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND ISDELETED = 0
AND TRNTID = '10'
ORDER BY TRNDT, TRNSR
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    while ($Reader.Read()) {
        Write-Host "   TRAID: $($Reader['TRAID']) | TRNSID: $($Reader['TRNSID']) | CROPID: $($Reader['CROPID']) | PAMT: $($Reader['PAMT']) | REFAID: $($Reader['REFAID'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Current function output:" -ForegroundColor Yellow
    $Query2 = "SELECT TRAID, BILLNO, CROPNAME, BILLAMOUNT, PAIDAMT, PAYABLEAMT, REFAID FROM GET_FarmerCropBillData('$CompanyID', '$DeptID', '$MAID', '$CropID')"

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    $rowNum = 0
    while ($Reader.Read()) {
        $rowNum++
        Write-Host "   Row $rowNum : TRAID: $($Reader['TRAID']) | BILLNO: $($Reader['BILLNO']) | REFAID: $($Reader['REFAID']) | BILLAMOUNT: $($Reader['BILLAMOUNT']) | PAIDAMT: $($Reader['PAIDAMT']) | PAYABLEAMT: $($Reader['PAYABLEAMT'])" -ForegroundColor White
    }
    $Reader.Close()

    if ($rowNum -gt 1) {
        Write-Host ""
        Write-Host "   WARNING: Function returned $rowNum rows (should be 1 per bill)" -ForegroundColor Red
    }

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
