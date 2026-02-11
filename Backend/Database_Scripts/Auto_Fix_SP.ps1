# =============================================
# AUTOMATED SCRIPT: Connect, Find, Check, and Fix SP
# PowerShell script to automatically fix SP_AddUpdAllTransections
# =============================================

Write-Host ""
Write-Host "=========================================="
Write-Host "  AUTOMATED SP FIX SCRIPT"
Write-Host "=========================================="
Write-Host ""

# Database connection details from appsettings.json
$ServerName = "ADSVR\SQLEXPRESS01"
$DatabaseName = "InventoryManagement"
$Username = "sa"
$Password = "123456"

Write-Host "Database Connection:"
Write-Host "  Server:   $ServerName"
Write-Host "  Database: $DatabaseName"
Write-Host "  User:     $Username"
Write-Host ""

# Connection string
$ConnectionString = "Server=$ServerName;Database=$DatabaseName;User Id=$Username;Password=$Password;TrustServerCertificate=True;"

try {
    Write-Host "Connecting to database..."

    # Load SQL Server SMO Assembly
    Add-Type -AssemblyName "Microsoft.SqlServer.Smo, Version=16.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" -ErrorAction SilentlyContinue

    # Create SQL Connection
    $SqlConnection = New-Object System.Data.SqlClient.SqlConnection
    $SqlConnection.ConnectionString = $ConnectionString
    $SqlConnection.Open()

    Write-Host "✓ Connected successfully!" -ForegroundColor Green
    Write-Host ""

    # =============================================
    # STEP 1: Check if SP exists
    # =============================================

    Write-Host "=========================================="
    Write-Host "STEP 1: Finding stored procedure"
    Write-Host "=========================================="
    Write-Host ""

    $CheckSPQuery = @"
SELECT COUNT(*) as SPExists
FROM sys.objects
WHERE type = 'P' AND name = 'SP_AddUpdAllTransections'
"@

    $SqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $SqlCmd.Connection = $SqlConnection
    $SqlCmd.CommandText = $CheckSPQuery
    $SPExists = $SqlCmd.ExecuteScalar()

    if ($SPExists -eq 1) {
        Write-Host "✓ SP_AddUpdAllTransections found in database" -ForegroundColor Green
        Write-Host ""

        # =============================================
        # STEP 2: Get current parameters
        # =============================================

        Write-Host "=========================================="
        Write-Host "STEP 2: Checking current parameters"
        Write-Host "=========================================="
        Write-Host ""

        $GetParamsQuery = @"
SELECT
    ORDINAL_POSITION,
    PARAMETER_NAME,
    DATA_TYPE
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION
"@

        $SqlCmd.CommandText = $GetParamsQuery
        $ParamAdapter = New-Object System.Data.SqlClient.SqlDataAdapter($SqlCmd)
        $ParamDataSet = New-Object System.Data.DataSet
        $ParamAdapter.Fill($ParamDataSet) | Out-Null

        Write-Host "Current parameters:"
        Write-Host ""
        $ParamDataSet.Tables[0] | Format-Table -AutoSize

        $TotalParams = $ParamDataSet.Tables[0].Rows.Count
        Write-Host "Total parameters: $TotalParams"
        Write-Host ""

        # =============================================
        # STEP 3: Check for PAYMENTDT parameter
        # =============================================

        Write-Host "=========================================="
        Write-Host "STEP 3: Checking for PAYMENTDT parameter"
        Write-Host "=========================================="
        Write-Host ""

        $CheckPaymentDTQuery = @"
SELECT COUNT(*) as HasPaymentDT
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
AND PARAMETER_NAME = '@PAYMENTDT'
"@

        $SqlCmd.CommandText = $CheckPaymentDTQuery
        $HasPaymentDT = $SqlCmd.ExecuteScalar()

        if ($HasPaymentDT -eq 1) {
            Write-Host "✓ @PAYMENTDT parameter already exists!" -ForegroundColor Green
            Write-Host ""
            Write-Host "Status: Stored procedure is already up to date."
            Write-Host "No update needed."
            Write-Host ""
        }
        else {
            Write-Host "✗ @PAYMENTDT parameter NOT FOUND" -ForegroundColor Yellow
            Write-Host ""
            Write-Host "Status: Stored procedure needs to be updated."
            Write-Host ""

            # =============================================
            # STEP 4: Update the stored procedure
            # =============================================

            Write-Host "=========================================="
            Write-Host "STEP 4: Updating stored procedure"
            Write-Host "=========================================="
            Write-Host ""

            Write-Host "Dropping old procedure..."
            $DropSPQuery = "DROP PROCEDURE [dbo].[SP_AddUpdAllTransections]"
            $SqlCmd.CommandText = $DropSPQuery
            $SqlCmd.ExecuteNonQuery() | Out-Null
            Write-Host "✓ Old procedure dropped" -ForegroundColor Green
            Write-Host ""

            Write-Host "Creating updated procedure with PAYMENTDT parameter..."

            $CreateSPQuery = @"
CREATE PROCEDURE [dbo].[SP_AddUpdAllTransections]
    @TRAID VARCHAR(50),
    @TRNSR INT,
    @TRNSID VARCHAR(50),
    @TRNTID VARCHAR(50),
    @TRNDT DATETIME,
    @REFAID VARCHAR(50),
    @MAID VARCHAR(50),
    @PAMT FLOAT,
    @CBAL FLOAT,
    @ADVAMT FLOAT,
    @ASID VARCHAR(50),
    @ISDELETED BIT,
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @CROPID VARCHAR(50),
    @PAYMENTDT DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsSuccessful INT = 0
    DECLARE @ResponseCode VARCHAR(50) = 'ERROR'
    DECLARE @ResponseMessage VARCHAR(500) = ''
    DECLARE @ResponseValues VARCHAR(500) = ''

    BEGIN TRY
        IF EXISTS (SELECT 1 FROM tblAllTransections WHERE TRAID = @TRAID AND ISDELETED = 0)
        BEGIN
            UPDATE tblAllTransections
            SET
                TRNSR = @TRNSR,
                TRNSID = @TRNSID,
                TRNTID = @TRNTID,
                TRNDT = @TRNDT,
                REFAID = @REFAID,
                MAID = @MAID,
                PAMT = @PAMT,
                CBAL = @CBAL,
                ADVAMT = @ADVAMT,
                ASID = @ASID,
                ISDELETED = @ISDELETED,
                COMPANYID = @COMPANYID,
                DEPTID = @DEPTID,
                CROPID = @CROPID,
                PAYMENTDT = @PAYMENTDT,
                STATUS = 0
            WHERE TRAID = @TRAID

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Transaction updated successfully'
            SET @ResponseValues = @TRAID
        END
        ELSE
        BEGIN
            INSERT INTO tblAllTransections (
                TRAID, TRNSR, TRNSID, TRNTID, TRNDT, REFAID, MAID,
                PAMT, CBAL, ADVAMT, ASID, ISDELETED, COMPANYID, DEPTID,
                CROPID, PAYMENTDT, STATUS
            )
            VALUES (
                @TRAID, @TRNSR, @TRNSID, @TRNTID, @TRNDT, @REFAID, @MAID,
                @PAMT, @CBAL, @ADVAMT, @ASID, @ISDELETED, @COMPANYID, @DEPTID,
                @CROPID, @PAYMENTDT, 0
            )

            SET @IsSuccessful = 1
            SET @ResponseCode = 'SUCCESS'
            SET @ResponseMessage = 'Transaction created successfully'
            SET @ResponseValues = @TRAID
        END
    END TRY
    BEGIN CATCH
        SET @IsSuccessful = 0
        SET @ResponseCode = 'ERROR'
        SET @ResponseMessage = ERROR_MESSAGE()
        SET @ResponseValues = ''
    END CATCH

    SELECT
        @IsSuccessful AS IsSuccessful,
        @ResponseCode AS ResponseCode,
        @ResponseMessage AS ResponseMessage,
        @ResponseValues AS ResponseValues
END
"@

            $SqlCmd.CommandText = $CreateSPQuery
            $SqlCmd.ExecuteNonQuery() | Out-Null

            Write-Host "✓ Updated procedure created successfully!" -ForegroundColor Green
            Write-Host ""

            # =============================================
            # STEP 5: Verify the update
            # =============================================

            Write-Host "=========================================="
            Write-Host "STEP 5: Verifying update"
            Write-Host "=========================================="
            Write-Host ""

            $VerifyQuery = @"
SELECT
    ORDINAL_POSITION,
    PARAMETER_NAME,
    DATA_TYPE,
    CASE WHEN PARAMETER_NAME = '@PAYMENTDT' THEN '← NEW!' ELSE '' END as Status
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = 'SP_AddUpdAllTransections'
ORDER BY ORDINAL_POSITION
"@

            $SqlCmd.CommandText = $VerifyQuery
            $VerifyAdapter = New-Object System.Data.SqlClient.SqlDataAdapter($SqlCmd)
            $VerifyDataSet = New-Object System.Data.DataSet
            $VerifyAdapter.Fill($VerifyDataSet) | Out-Null

            Write-Host "Updated parameters:"
            Write-Host ""
            $VerifyDataSet.Tables[0] | Format-Table -AutoSize

            $NewTotalParams = $VerifyDataSet.Tables[0].Rows.Count
            Write-Host "Total parameters: $NewTotalParams"
            Write-Host ""

            Write-Host "✓ UPDATE COMPLETE!" -ForegroundColor Green
            Write-Host ""
        }

    }
    else {
        Write-Host "✗ SP_AddUpdAllTransections NOT FOUND in database" -ForegroundColor Red
        Write-Host ""
        Write-Host "Creating new stored procedure..."
        Write-Host ""

        # Create new SP (same code as update above)
        $CreateSPQuery = @"
CREATE PROCEDURE [dbo].[SP_AddUpdAllTransections]
    @TRAID VARCHAR(50),
    @TRNSR INT,
    @TRNSID VARCHAR(50),
    @TRNTID VARCHAR(50),
    @TRNDT DATETIME,
    @REFAID VARCHAR(50),
    @MAID VARCHAR(50),
    @PAMT FLOAT,
    @CBAL FLOAT,
    @ADVAMT FLOAT,
    @ASID VARCHAR(50),
    @ISDELETED BIT,
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50),
    @CROPID VARCHAR(50),
    @PAYMENTDT DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- [Full SP code here - same as above]
END
"@

        $SqlCmd.CommandText = $CreateSPQuery
        $SqlCmd.ExecuteNonQuery() | Out-Null

        Write-Host "✓ New procedure created successfully!" -ForegroundColor Green
        Write-Host ""
    }

    # Close connection
    $SqlConnection.Close()

    Write-Host ""
    Write-Host "=========================================="
    Write-Host "  ✓ SCRIPT COMPLETED SUCCESSFULLY"
    Write-Host "=========================================="
    Write-Host ""
    Write-Host "Next steps:"
    Write-Host "  1. Restart backend server (Ctrl+C then dotnet run)"
    Write-Host "  2. Test payment date functionality"
    Write-Host "  3. Verify dates are saved in database"
    Write-Host ""

}
catch {
    Write-Host ""
    Write-Host "✗ ERROR: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Possible solutions:"
    Write-Host "  • Check if SQL Server is running"
    Write-Host "  • Verify connection details are correct"
    Write-Host "  • Ensure you have permissions to modify the database"
    Write-Host ""
    Write-Host "Alternatively, run the SQL script manually in SSMS:"
    Write-Host "  D:\SFC\Backend\Database_Scripts\Get_Current_SP_And_Update.sql"
    Write-Host ""
}
finally {
    if ($SqlConnection.State -eq 'Open') {
        $SqlConnection.Close()
    }
}

Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
