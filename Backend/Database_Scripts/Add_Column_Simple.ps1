# Simple script to add PAYMENTDT column
$Server = "ADSVR\SQLEXPRESS01"
$Database = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Adding PAYMENTDT column..." -ForegroundColor Cyan

try {
    $ConnectionString = "Server=$Server;Database=$Database;User Id=$Username;Password=$Password;TrustServerCertificate=True;Connection Timeout=30;"
    $Connection = New-Object System.Data.SqlClient.SqlConnection
    $Connection.ConnectionString = $ConnectionString
    $Connection.Open()

    # Check if exists
    $Query = @"
SELECT COUNT(*) AS ColumnExists
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tblAllTransections'
AND COLUMN_NAME = 'PAYMENTDT'
"@

    $Command = $Connection.CreateCommand()
    $Command.CommandText = $Query
    $Reader = $Command.ExecuteReader()
    $exists = $false
    if ($Reader.Read()) {
        $exists = $Reader["ColumnExists"] -gt 0
    }
    $Reader.Close()

    if ($exists) {
        Write-Host "Column already exists!" -ForegroundColor Yellow
    } else {
        Write-Host "Adding column..." -ForegroundColor Yellow
        $AlterQuery = "ALTER TABLE tblAllTransections ADD PAYMENTDT DATETIME NULL"
        $Command = $Connection.CreateCommand()
        $Command.CommandText = $AlterQuery
        $Command.ExecuteNonQuery() | Out-Null
        Write-Host "Column added successfully!" -ForegroundColor Green

        # Initialize with TRNDT
        $UpdateQuery = @"
UPDATE tblAllTransections
SET PAYMENTDT = TRNDT
WHERE TRNSID = '20' AND PAYMENTDT IS NULL
"@
        $Command = $Connection.CreateCommand()
        $Command.CommandText = $UpdateQuery
        $rows = $Command.ExecuteNonQuery()
        Write-Host "Initialized $rows payment records" -ForegroundColor Green
    }

    $Connection.Close()

} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
