================================================================================
                    AUTOMATED SP FIX - QUICK GUIDE
================================================================================

This folder contains automated scripts that will:
1. Connect to your database
2. Find SP_AddUpdAllTransections
3. Check if @PAYMENTDT parameter exists
4. Automatically fix it if needed

================================================================================
3 WAYS TO AUTO-FIX
================================================================================

Option 1: PowerShell Script (RECOMMENDED)
------------------------------------------
File: Auto_Fix_SP.ps1

How to run:
1. Right-click on Auto_Fix_SP.ps1
2. Select "Run with PowerShell"

OR from PowerShell terminal:
PS> cd D:\SFC\Backend\Database_Scripts
PS> .\Auto_Fix_SP.ps1

What it does:
✓ Connects to database automatically
✓ Shows current parameters
✓ Checks for @PAYMENTDT
✓ Updates if needed
✓ Verifies the fix
✓ Shows results in colored output

Option 2: Batch File (Simple)
------------------------------
File: Auto_Fix_SP.bat

How to run:
1. Double-click on Auto_Fix_SP.bat

What it does:
✓ Uses sqlcmd to connect
✓ Runs the fix script
✓ Shows results in console

Requirements:
• sqlcmd must be installed
• Comes with SQL Server tools

Option 3: SQL Script (Manual but Safe)
---------------------------------------
File: Get_Current_SP_And_Update.sql

How to run:
1. Open SQL Server Management Studio
2. Connect to: ADSVR\SQLEXPRESS01
3. Open: Get_Current_SP_And_Update.sql
4. Press F5 (Execute)

What it does:
✓ You control when it runs
✓ You can review before executing
✓ Shows detailed output
✓ Most reliable method

================================================================================
WHICH METHOD TO CHOOSE?
================================================================================

Use PowerShell (Auto_Fix_SP.ps1) if:
------------------------------------
✓ You want the fastest method
✓ You're comfortable with PowerShell
✓ You want colored output and detailed info

Use Batch File (Auto_Fix_SP.bat) if:
------------------------------------
✓ You prefer simple double-click
✓ You have sqlcmd installed
✓ You don't want to use PowerShell

Use SQL Script (Get_Current_SP_And_Update.sql) if:
--------------------------------------------------
✓ You want full control
✓ You want to review before running
✓ You're most comfortable with SSMS
✓ Other methods didn't work

================================================================================
DATABASE CONNECTION (Auto-detected from appsettings.json)
================================================================================

Server:   ADSVR\SQLEXPRESS01
Database: InventoryManagement
User:     sa
Password: 123456

All automated scripts use these connection details.

================================================================================
WHAT THE SCRIPTS DO
================================================================================

Step 1: Connect to Database
---------------------------
✓ Uses connection details from appsettings.json
✓ Connects to InventoryManagement database

Step 2: Find Stored Procedure
-----------------------------
✓ Searches for SP_AddUpdAllTransections
✓ Shows if it exists or not

Step 3: Check Current Parameters
--------------------------------
✓ Retrieves all current parameters
✓ Displays them in a table
✓ Shows total parameter count

Step 4: Check for @PAYMENTDT
----------------------------
✓ Looks for @PAYMENTDT parameter
✓ If exists: Shows success message, no update needed
✓ If missing: Proceeds to update

Step 5: Update (if needed)
--------------------------
✓ Drops old stored procedure
✓ Creates new version with @PAYMENTDT parameter
✓ Includes in both INSERT and UPDATE statements

Step 6: Verify
--------------
✓ Retrieves updated parameter list
✓ Confirms @PAYMENTDT was added
✓ Shows "NEW!" marker next to new parameter

================================================================================
EXPECTED OUTPUT
================================================================================

If already up to date:
---------------------
✓ SP_AddUpdAllTransections found in database
✓ @PAYMENTDT parameter already exists!
Status: Stored procedure is already up to date.
No update needed.

If update is needed:
-------------------
✓ SP_AddUpdAllTransections found in database

Current parameters:
#   Parameter      Type
1   @TRAID        varchar
2   @TRNSR        int
...
15  @CROPID       varchar

Total parameters: 15

✗ @PAYMENTDT parameter NOT FOUND
Updating stored procedure...
✓ Old procedure dropped
✓ Updated procedure created successfully!

Updated parameters:
#   Parameter      Type      Status
1   @TRAID        varchar
...
16  @PAYMENTDT    datetime  ← NEW!

Total parameters: 16

✓ UPDATE COMPLETE!

================================================================================
AFTER RUNNING ANY SCRIPT
================================================================================

1. Restart Backend Server
-------------------------
   Go to your backend CMD window:
   - Press Ctrl+C
   - Type: Y
   - Run: dotnet run

2. Test Payment Functionality
-----------------------------
   - Open frontend (http://localhost:5173)
   - Go to Farmer Bill Details
   - Select payment date
   - Save transaction
   - Should work without errors!

3. Verify in Database
--------------------
   Run this query in SSMS:

   SELECT TOP 5
       TRAID,
       TRNSID,
       PAMT,
       PAYMENTDT,  -- Should have value!
       MAID
   FROM tblAllTransections
   WHERE TRNSID = '20'
   ORDER BY TRNDT DESC

   Expected: PAYMENTDT column has the date you selected

================================================================================
TROUBLESHOOTING
================================================================================

PowerShell Script Error: "Execution policy"
-------------------------------------------
Solution:
   Open PowerShell as Administrator and run:
   Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned

   Then try again.

Batch File Error: "sqlcmd is not recognized"
-------------------------------------------
Solution:
   Install SQL Server Command Line Utilities:
   https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility

   OR use Option 3 (SQL Script in SSMS)

Connection Error: "Login failed"
--------------------------------
Solution:
   - Verify SQL Server is running
   - Check credentials in appsettings.json
   - Ensure SQL Server Authentication is enabled

Error: "Invalid column name 'PAYMENTDT'"
---------------------------------------
Solution:
   The table doesn't have the PAYMENTDT column yet.
   Run this first:

   D:\SFC\Backend\Database_Scripts\Add_PaymentDate_Column.sql

================================================================================
FILE SUMMARY
================================================================================

Auto_Fix_SP.ps1                        ← PowerShell auto-fix
Auto_Fix_SP.bat                        ← Batch auto-fix
Get_Current_SP_And_Update.sql          ← SQL manual fix
Show_Current_SP_Only.sql               ← Check only (no changes)
Complete_SP_Diagnostic_And_Fix.sql     ← Complete diagnostic
Add_PaymentDate_Column.sql             ← Add column to table
DATABASE_CONNECTION_INFO.txt           ← Connection details
DATABASE_UPDATE_WORKFLOW.txt           ← Workflow guide
README_AUTO_FIX.txt                    ← This file

================================================================================
QUICK START
================================================================================

FASTEST METHOD:
1. Double-click: Auto_Fix_SP.bat
   OR
   Right-click Auto_Fix_SP.ps1 → Run with PowerShell

2. Wait for "UPDATE COMPLETE!"

3. Restart backend server

4. Test payment dates

DONE! ✓

================================================================================
For more help, contact the development team.
================================================================================
