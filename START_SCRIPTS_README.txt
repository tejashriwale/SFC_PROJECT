================================================================================
                    SFC DEVELOPMENT STARTUP SCRIPTS
================================================================================

This folder contains startup scripts to automatically launch the backend and
frontend servers in separate command windows for easier debugging and error
tracking.

================================================================================
AVAILABLE SCRIPTS
================================================================================

1. start-dev.bat (RECOMMENDED)
   - Double-click to start BOTH backend and frontend in separate CMD windows
   - Windows Batch script
   - Easiest option for most users

2. start-dev.ps1
   - PowerShell version of start-dev.bat
   - Opens BOTH backend and frontend in separate PowerShell windows
   - Right-click → Run with PowerShell

3. start-backend.bat
   - Starts ONLY the backend server
   - Opens in current CMD window

4. start-frontend.bat
   - Starts ONLY the frontend server
   - Opens in current CMD window

================================================================================
HOW TO USE
================================================================================

METHOD 1: Start Both Servers (Recommended)
------------------------------------------
   1. Navigate to: D:\SFC
   2. Double-click: start-dev.bat
   3. Two separate CMD windows will open:
      - Window 1: Backend (.NET API) on http://localhost:5000
      - Window 2: Frontend (React App) on http://localhost:3000

METHOD 2: Start Servers Individually
------------------------------------------
   Backend Only:
      Double-click: start-backend.bat

   Frontend Only:
      Double-click: start-frontend.bat

METHOD 3: PowerShell Version
------------------------------------------
   1. Right-click: start-dev.ps1
   2. Select "Run with PowerShell"

   OR from PowerShell terminal:
      PS D:\SFC> .\start-dev.ps1

================================================================================
BENEFITS OF SEPARATE WINDOWS
================================================================================

✓ Clear Error Visibility
  - Backend errors appear in backend window
  - Frontend errors appear in frontend window
  - No mixing of log outputs

✓ Independent Control
  - Restart backend without affecting frontend
  - Restart frontend without affecting backend
  - Close either server independently

✓ Better Debugging
  - Monitor API requests in backend window
  - See React compilation in frontend window
  - Track payment date issues from backend logs

================================================================================
TROUBLESHOOTING
================================================================================

Problem: "dotnet: command not found"
Solution: Install .NET SDK from https://dotnet.microsoft.com/download

Problem: "npm: command not found"
Solution: Install Node.js from https://nodejs.org/

Problem: Backend window closes immediately
Solution: Check for compilation errors in Backend project

Problem: Frontend shows "Network Error"
Solution:
   1. Make sure backend is running first
   2. Check backend window for error messages
   3. Verify backend URL in frontend configuration

Problem: Port already in use
Solution:
   1. Close existing instances
   2. Or change port in configuration files

================================================================================
CHECKING FOR PAYMENT DATE ERRORS
================================================================================

After running start-dev.bat, watch the BACKEND window for:

❌ SqlException: Procedure has no parameter '@PAYMENTDT'
   → Run: D:\SFC\Backend\Database_Scripts\Update_SP_AddUpdAllTransections.sql

❌ SqlException: Invalid column name 'PAYMENTDT'
   → Run: D:\SFC\Backend\Database_Scripts\Add_PaymentDate_Column.sql

❌ CS0117: Does not contain definition for 'PAYMENTDT'
   → Already fixed in C# models, rebuild the project

✓ No errors = Payment date is working correctly!

================================================================================
STOPPING SERVERS
================================================================================

To stop a server:
   1. Click on its window
   2. Press Ctrl+C
   3. Confirm with 'Y' if asked

To stop all servers:
   - Close all CMD windows
   - Or press Ctrl+C in each window

================================================================================
NOTES
================================================================================

- Backend starts 3 seconds before frontend (to ensure API is ready)
- Both servers must be running for the app to work properly
- Keep both windows open while developing
- Backend logs show API calls and database queries
- Frontend logs show React compilation and console.log outputs

================================================================================
QUICK REFERENCE
================================================================================

Backend URL:  http://localhost:5000 (or https://localhost:5001)
Frontend URL: http://localhost:3000

Commands in Backend Window:
   - dotnet run          : Start server
   - dotnet build        : Build project
   - Ctrl+C              : Stop server

Commands in Frontend Window:
   - npm run dev         : Start dev server (Vite)
   - npm run build       : Build for production
   - npm run preview     : Preview production build
   - Ctrl+C              : Stop server

================================================================================
For more help, contact the development team.
================================================================================
