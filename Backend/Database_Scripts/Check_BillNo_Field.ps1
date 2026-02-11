# Check what should be used for BILLNO
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

$CompanyID = "CM001"
$DeptID = "DP001"
$MAID = "PK16D970871FE541842435"
$CropID = "PK16D970891DDD14842589"

Write-Host "Checking BILLNO field options..." -ForegroundColor Cyan
Write-Host ""

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    Write-Host "1. All fields from tblAllTransections for this farmer/crop:" -ForegroundColor Yellow
    $Query1 = @"
SELECT TOP 1 *
FROM tblAllTransections
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND MAID = '$MAID'
AND CROPID = '$CropID'
AND ISDELETED = 0
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query1
    $Reader = $Command.ExecuteReader()

    if ($Reader.Read()) {
        Write-Host ""
        for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
            $fieldName = $Reader.GetName($i)
            $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
            Write-Host "   $fieldName = $fieldValue" -ForegroundColor White
        }
    }
    $Reader.Close()

    Write-Host ""
    Write-Host "2. Check if there's a tblCropBill table with BILLNO:" -ForegroundColor Yellow
    $Query2 = @"
SELECT TOP 5 *
FROM tblCropBill
WHERE COMPANYID = '$CompanyID'
AND DEPTID = '$DeptID'
AND FAID = '$MAID'
ORDER BY DATE DESC
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query2

    try {
        $Reader = $Command.ExecuteReader()

        if ($Reader.HasRows) {
            Write-Host ""
            while ($Reader.Read()) {
                Write-Host "   Found bill record:" -ForegroundColor Green
                for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
                    $fieldName = $Reader.GetName($i)
                    $fieldValue = if ($Reader.IsDBNull($i)) { "NULL" } else { $Reader.GetValue($i) }
                    Write-Host "     $fieldName = $fieldValue" -ForegroundColor White
                }
                Write-Host ""
            }
        } else {
            Write-Host "   No records in tblCropBill" -ForegroundColor Yellow
        }
        $Reader.Close()
    } catch {
        Write-Host "   tblCropBill might not exist or error: $($_.Exception.Message)" -ForegroundColor Yellow
    }

    Write-Host ""
    Write-Host "3. Looking for BILLNO pattern in table columns:" -ForegroundColor Yellow
    $Query3 = @"
SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%BILL%'
AND TABLE_NAME LIKE '%CROP%'
ORDER BY TABLE_NAME, ORDINAL_POSITION
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query3
    $Reader = $Command.ExecuteReader()

    while ($Reader.Read()) {
        Write-Host "   $($Reader['TABLE_NAME']).$($Reader['COLUMN_NAME'])" -ForegroundColor White
    }
    $Reader.Close()

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
