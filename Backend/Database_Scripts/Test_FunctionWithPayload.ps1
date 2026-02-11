# Test the function with actual payload data
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Testing GET_FarmerCropBillData with actual payload..." -ForegroundColor Cyan
Write-Host ""

# Actual payload from user
$CompanyID = "CM001"
$DeptID = "DP001"
$MAID = "PK16D970871FE541842435"
$CropID = "PK16D970891DDD14842589"

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. Checking if data exists in tblAllTransections..." -ForegroundColor Yellow
    $Query1 = @"
SELECT COUNT(*) AS TotalRecords
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()
    if ($Reader.Read()) {
        $total = $Reader["TotalRecords"]
        Write-Host "   Total records in tblAllTransections: $total" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Checking records for this specific farmer and crop..." -ForegroundColor Yellow
    $Query2 = @"
SELECT COUNT(*) AS MatchingRecords,
       COUNT(CASE WHEN TRNSID = '1' THEN 1 END) AS BillRecords,
       COUNT(CASE WHEN TRNSID = '2' THEN 1 END) AS PaymentRecords,
       COUNT(CASE WHEN TRNSID = '10' OR TRNTID = '10' THEN 1 END) AS AdvanceRecords
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND CROPID = '$CropID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()
    if ($Reader.Read()) {
        Write-Host "   Matching records: $($Reader['MatchingRecords'])" -ForegroundColor White
        Write-Host "   Bill records (TRNSID='1'): $($Reader['BillRecords'])" -ForegroundColor White
        Write-Host "   Payment records (TRNSID='2'): $($Reader['PaymentRecords'])" -ForegroundColor White
        Write-Host "   Advance records (TRNSID='10'): $($Reader['AdvanceRecords'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "3. Checking records for this farmer (any crop)..." -ForegroundColor Yellow
    $Query3 = @"
SELECT COUNT(*) AS RecordsForFarmer
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query3
    $Reader = $Command.ExecuteReader()
    if ($Reader.Read()) {
        Write-Host "   Records for this farmer (any crop): $($Reader['RecordsForFarmer'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "4. Sample data for this farmer..." -ForegroundColor Yellow
    $Query4 = @"
SELECT TOP 5 TRAID, TRNSR, TRNSID, TRNTID, CROPID, REFAID, PAMT, TRNDT
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND ISDELETED = 0
ORDER BY TRNDT DESC
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query4
    $Reader = $Command.ExecuteReader()

    $rowCount = 0
    while ($Reader.Read()) {
        $rowCount++
        Write-Host "   Row $rowCount : TRNSID=$($Reader['TRNSID']), CROPID=$($Reader['CROPID']), REFAID=$($Reader['REFAID']), PAMT=$($Reader['PAMT'])" -ForegroundColor White
    }

    if ($rowCount -eq 0) {
        Write-Host "   No records found for this farmer!" -ForegroundColor Red
    }

    $Reader.Close()

    Write-Host ""
    Write-Host "5. Testing the function directly..." -ForegroundColor Yellow
    $Query5 = "SELECT * FROM GET_FarmerCropBillData('$CompanyID', '$DeptID', '$MAID', '$CropID')"

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query5
    $Reader = $Command.ExecuteReader()

    $functionRows = 0
    while ($Reader.Read()) {
        $functionRows++
        Write-Host "   Function returned row $functionRows" -ForegroundColor Green
        Write-Host "     TRAID: $($Reader['TRAID'])" -ForegroundColor White
        Write-Host "     FNAME: $($Reader['FNAME'])" -ForegroundColor White
        Write-Host "     CROPNAME: $($Reader['CROPNAME'])" -ForegroundColor White
        Write-Host "     BILLAMOUNT: $($Reader['BILLAMOUNT'])" -ForegroundColor White
        Write-Host "     ADVANCEAMT: $($Reader['ADVANCEAMT'])" -ForegroundColor White
        Write-Host "     PAIDAMT: $($Reader['PAIDAMT'])" -ForegroundColor White
        Write-Host "     PAYABLEAMT: $($Reader['PAYABLEAMT'])" -ForegroundColor White
    }

    if ($functionRows -eq 0) {
        Write-Host "   Function returned NO ROWS!" -ForegroundColor Red
    }

    $Reader.Close()
    $Connection.Close()

    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Diagnostics completed!" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
