# Comprehensive verification of all calculations
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$CompanyID = "CM001"
$DeptID = "DP001"
$MAID = "PK16D970871FE541842435"
$CropID = "PK16D970891DDD14842589"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Comprehensive Calculation Verification" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. All transactions for this farmer and crop:" -ForegroundColor Yellow
    $Query1 = @"
SELECT TRAID, TRNSR, TRNSID, TRNTID, PAMT, TRNDT, REFAID
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND CROPID = '$CropID'
AND ISDELETED = 0
ORDER BY TRNDT
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    Write-Host ""
    while ($Reader.Read()) {
        Write-Host "   TRNSID=$($Reader['TRNSID']) | TRNTID=$($Reader['TRNTID']) | PAMT=$($Reader['PAMT']) | REFAID=$($Reader['REFAID'])" -ForegroundColor White
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Manual calculation breakdown:" -ForegroundColor Yellow

    # Get REFAID first
    $QueryRef = @"
SELECT DISTINCT REFAID
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND CROPID = '$CropID'
AND TRNTID = '10'
AND ISDELETED = 0
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $QueryRef
    $Reader = $Command.ExecuteReader()
    $REFAID = ""
    if ($Reader.Read()) {
        $REFAID = $Reader['REFAID']
    }
    $Reader.Close()

    # Calculate BILLAMOUNT for entire bill (all crops with same REFAID)
    $Query2 = @"
SELECT COALESCE(SUM(ABS(PAMT)), 0) AS Amount
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND REFAID = '$REFAID'
AND TRNTID = '10'
AND (TRNSID = '10' OR TRNSID = '40')
AND ISDELETED = 0
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2
    $Reader = $Command.ExecuteReader()
    $billAmount = 0
    if ($Reader.Read()) {
        $billAmount = $Reader['Amount']
    }
    $Reader.Close()
    Write-Host "   BILLAMOUNT (TRNSID 10+40, TRNTID 10): $billAmount" -ForegroundColor Green

    # Calculate ADVANCEAMT for entire bill (TRNSID 30, TRNTID 10)
    $Query3 = @"
SELECT COALESCE(SUM(ABS(PAMT)), 0) AS Amount
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND REFAID = '$REFAID'
AND TRNTID = '10'
AND TRNSID = '30'
AND ISDELETED = 0
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query3
    $Reader = $Command.ExecuteReader()
    $advanceAmount = 0
    if ($Reader.Read()) {
        $advanceAmount = $Reader['Amount']
    }
    $Reader.Close()
    Write-Host "   ADVANCEAMT (TRNSID 30, TRNTID 10): $advanceAmount" -ForegroundColor Green

    # Calculate PAIDAMT for entire bill (TRNSID 20, TRNTID 10)
    $Query4 = @"
SELECT COALESCE(SUM(ABS(PAMT)), 0) AS Amount
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND REFAID = '$REFAID'
AND TRNTID = '10'
AND TRNSID = '20'
AND ISDELETED = 0
"@
    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query4
    $Reader = $Command.ExecuteReader()
    $paidAmount = 0
    if ($Reader.Read()) {
        $paidAmount = $Reader['Amount']
    }
    $Reader.Close()
    Write-Host "   PAIDAMT (TRNSID 20, TRNTID 10): $paidAmount" -ForegroundColor Green

    $payableAmount = $billAmount - $advanceAmount - $paidAmount
    Write-Host "   PAYABLEAMT (Calculated): $payableAmount" -ForegroundColor Green

    Write-Host ""
    Write-Host "3. Function output:" -ForegroundColor Yellow
    $Query5 = "SELECT * FROM GET_FarmerCropBillData('$CompanyID', '$DeptID', '$MAID', '$CropID')"

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query5
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host "   FNAME: $($Reader['FNAME'])" -ForegroundColor White
        Write-Host "   CROPNAME: $($Reader['CROPNAME'])" -ForegroundColor White
        Write-Host "   BILLAMOUNT: $($Reader['BILLAMOUNT'])" -ForegroundColor White
        Write-Host "   ADVANCEAMT: $($Reader['ADVANCEAMT'])" -ForegroundColor White
        Write-Host "   PAIDAMT: $($Reader['PAIDAMT'])" -ForegroundColor White
        Write-Host "   PAYABLEAMT: $($Reader['PAYABLEAMT'])" -ForegroundColor White
        Write-Host "   CBAL: $($Reader['CBAL'])" -ForegroundColor White

        Write-Host ""
        Write-Host "4. Verification:" -ForegroundColor Yellow
        if ($Reader['BILLAMOUNT'] -eq $billAmount) {
            Write-Host "   BILLAMOUNT: CORRECT" -ForegroundColor Green
        } else {
            Write-Host "   BILLAMOUNT: INCORRECT (Expected $billAmount, Got $($Reader['BILLAMOUNT']))" -ForegroundColor Red
        }

        if ($Reader['ADVANCEAMT'] -eq $advanceAmount) {
            Write-Host "   ADVANCEAMT: CORRECT" -ForegroundColor Green
        } else {
            Write-Host "   ADVANCEAMT: INCORRECT (Expected $advanceAmount, Got $($Reader['ADVANCEAMT']))" -ForegroundColor Red
        }

        if ($Reader['PAIDAMT'] -eq $paidAmount) {
            Write-Host "   PAIDAMT: CORRECT" -ForegroundColor Green
        } else {
            Write-Host "   PAIDAMT: INCORRECT (Expected $paidAmount, Got $($Reader['PAIDAMT']))" -ForegroundColor Red
        }

        if ($Reader['PAYABLEAMT'] -eq $payableAmount) {
            Write-Host "   PAYABLEAMT: CORRECT" -ForegroundColor Green
        } else {
            Write-Host "   PAYABLEAMT: INCORRECT (Expected $payableAmount, Got $($Reader['PAYABLEAMT']))" -ForegroundColor Red
        }
    } else {
        Write-Host "   NO DATA RETURNED!" -ForegroundColor Red
    }

    $Reader.Close()
    $Connection.Close()

    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Verification completed!" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
