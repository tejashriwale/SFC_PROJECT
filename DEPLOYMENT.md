# SFC Full-Stack Application - Deployment Guide

## Production URLs
- **Frontend**: http://adsvr:333/
- **Backend API**: http://adsvr:333/backend
- **Swagger UI**: http://adsvr:333/backend/swagger

---

## Prerequisites

Before deploying, ensure the following are installed and configured on server 'adsvr':

### Required Software
- [ ] **IIS (Internet Information Services)** - Windows Server feature
- [ ] **.NET 8.0 Runtime** - ASP.NET Core Runtime
- [ ] **URL Rewrite Module for IIS** - [Download](https://www.iis.net/downloads/microsoft/url-rewrite)
- [ ] **SQL Server** - ADSVR\SQLEXPRESS01 must be accessible
- [ ] **Port 333** - Must be available and not blocked by firewall

### Required Permissions
- [ ] Administrator access to IIS on 'adsvr' server
- [ ] Read/Write permissions to deployment directories
- [ ] SQL Server access for application pool identity

---

## Quick Start - Build and Deploy

### Step 1: Build Production Files

Run the production build script from the project root:

```batch
D:\SFC\build-production.bat
```

This will:
- Create configuration backups
- Build frontend (creates `Frontend\dist\` folder)
- Build backend (creates `Backend\Project\bin\Release\net8.0\publish\` folder)
- Verify build outputs
- Copy builds to organized `production\` folder
- Create deployment info file
- Display deployment instructions

**Production Folder Structure:**
```
D:\SFC\production\
├── frontend\                  (Frontend build - ready for IIS)
├── backend\                   (Backend build - ready for IIS)
└── deployment-info.txt        (Build info and quick reference)
```

**Individual Build Locations:**
- Frontend: `D:\SFC\Frontend\dist\` → copied to `D:\SFC\production\frontend\`
- Backend: `D:\SFC\Backend\Project\bin\Release\net8.0\publish\` → copied to `D:\SFC\production\backend\`

### Step 2: Deploy to IIS

Follow the IIS configuration steps below.

---

## IIS Configuration (Detailed Steps)

### Part 1: Create IIS Website for Frontend

1. **Open IIS Manager**
   - Press `Win + R`, type `inetmgr`, press Enter
   - Or: Server Manager → Tools → Internet Information Services (IIS) Manager

2. **Add New Website**
   - In left panel, expand server node
   - Right-click **Sites** → **Add Website**

3. **Configure Website Settings**
   - **Site name**: `SFC Frontend`
   - **Physical path**: Browse to `D:\SFC\production\frontend\`
   - **Binding**:
     - Type: `http`
     - IP address: `All Unassigned`
     - Port: `333`
     - Host name: (leave blank)
   - Click **OK**

4. **Verify Frontend Deployment**
   - Browse to: http://adsvr:333/
   - You should see the React application load

### Part 2: Create IIS Application for Backend

1. **Add Application to Frontend Site**
   - In IIS Manager, expand **Sites**
   - Right-click **SFC Frontend** → **Add Application**

2. **Configure Application Settings**
   - **Alias**: `backend` (lowercase - must match frontend API calls!)
   - **Application pool**: Click **Select** → **New**
     - **Name**: `SFC_Backend_Pool`
     - **.NET CLR version**: **No Managed Code** (important!)
     - **Managed pipeline mode**: **Integrated**
     - Click **OK**
   - **Physical path**: Browse to `D:\SFC\production\backend\`
   - **Connect as**: Click **Connect as** → **Application user (pass-through authentication)**
   - Click **OK**

3. **Configure Application Pool Identity**
   - In IIS Manager, click **Application Pools**
   - Right-click **SFC_Backend_Pool** → **Advanced Settings**
   - Find **Identity** → Click **...** button
   - Select appropriate identity:
     - **Option 1**: `ApplicationPoolIdentity` (default) - Ensure this account has SQL Server access
     - **Option 2**: Specific service account with DB permissions
   - Click **OK**

4. **Grant SQL Server Access**

   If using ApplicationPoolIdentity, grant SQL Server access:
   - Open SQL Server Management Studio
   - Connect to `ADSVR\SQLEXPRESS01`
   - Security → Logins → New Login
   - Login name: `IIS APPPOOL\SFC_Backend_Pool`
   - User Mapping: Select `InventoryManagement` database
   - Database role: `db_owner` (or appropriate roles)
   - Click **OK**

5. **Verify Backend Deployment**
   - Browse to: http://adsvr:333/Backend/swagger
   - You should see Swagger UI with API documentation
   - Test an API endpoint to verify database connectivity

---

## Post-Deployment Verification

### Checklist

- [ ] **Frontend loads**: Navigate to http://adsvr:333/
- [ ] **React routing works**: Navigate to different pages (should not show 404)
- [ ] **Swagger UI loads**: Navigate to http://adsvr:333/Backend/swagger
- [ ] **API responds**: Test GET endpoint in Swagger
- [ ] **Frontend can call Backend**: Check browser console for API calls
- [ ] **No CORS errors**: Check browser developer console
- [ ] **Database connection works**: Test endpoint that queries database

### Testing API from Frontend

1. Open browser to http://adsvr:333/
2. Open Developer Tools (F12)
3. Go to **Network** tab
4. Perform an action that calls the API
5. Verify:
   - Request URL is `http://adsvr:333/Backend/...`
   - Status code is 200 (or expected code)
   - No CORS errors in console

---

## Troubleshooting

### Frontend Issues

#### Issue: 404 errors when navigating to routes
**Solution:**
- Ensure `web.config` exists in `D:\SFC\production\frontend\`
- Install IIS URL Rewrite Module: https://www.iis.net/downloads/microsoft/url-rewrite
- Restart IIS: `iisreset`

#### Issue: Static files (CSS/JS) not loading
**Solution:**
- Check IIS → **SFC Frontend** → MIME Types
- Ensure `.js` and `.css` MIME types are configured
- Verify file permissions on `D:\SFC\production\frontend\` folder

### Backend Issues

#### Issue: HTTP 500.19 - Configuration Error
**Solution:**
- Ensure .NET 8.0 ASP.NET Core Runtime is installed
- Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- Install **ASP.NET Core Runtime 8.0 - Windows Hosting Bundle**
- Restart IIS: `iisreset`

#### Issue: HTTP 500.30 - ANCM In-Process Start Failure
**Solution:**
- Check Application Pool settings:
  - .NET CLR version: **No Managed Code**
  - Managed pipeline mode: **Integrated**
- Check Event Viewer for details:
  - Windows Logs → Application
  - Look for ASP.NET Core errors

#### Issue: Swagger shows "Failed to load API definition"
**Solution:**
- Check `appsettings.Production.json` exists
- Verify PathBase is set correctly in `Program.cs`
- Try accessing: http://adsvr:333/Backend/swagger/v1/swagger.json

### CORS Issues

#### Issue: "Access-Control-Allow-Origin" error in browser console
**Solution:**
- Verify frontend is making requests to `http://adsvr:333/Backend`
- Check `Program.cs` has Production CORS policy with origin `http://adsvr:333`
- Ensure CORS policy is applied before other middleware
- Restart backend application pool

### Database Issues

#### Issue: Cannot connect to database
**Solution:**
- Verify SQL Server is running: `services.msc` → SQL Server (SQLEXPRESS01)
- Check connection string in `appsettings.Production.json`
- Verify application pool identity has SQL Server login and database permissions
- Test connection from server using SSMS

---

## Configuration Reference

### Production Environment Variables

**Frontend (`.env.production`)**:
```env
# Base URL WITHOUT /Backend path - the application code appends /backend
VITE_API_URL=http://adsvr:333
VITE_APP_NAME=SFC Application
VITE_ENABLE_DEBUG=false
```

**Important Note:** The `VITE_API_URL` should NOT include `/Backend`. The frontend code automatically appends `/backend` to all API calls.

**Backend (`appsettings.Production.json`)**:
```json
{
  "AllowedHosts": "adsvr",
  "ConnectionStrings": {
    "shadimuharathConStrt": "Server=ADSVR\\SQLEXPRESS01;Database=InventoryManagement;..."
  }
}
```

### Key URLs

| Component | URL | Purpose |
|-----------|-----|---------|
| Frontend | http://adsvr:333/ | Main application UI |
| Backend API | http://adsvr:333/Backend | REST API base path |
| Swagger UI | http://adsvr:333/Backend/swagger | API documentation |
| Swagger JSON | http://adsvr:333/Backend/swagger/v1/swagger.json | OpenAPI spec |

---

## Revert to Local Development

If you need to return to local development mode:

```batch
D:\SFC\revert-to-local.bat
```

This will:
- Restore development environment files
- Clean production build outputs
- Reset to local development configuration

Then start local development:
```batch
D:\SFC\start.bat
```

---

## Maintenance

### Updating the Application

1. Stop IIS site and application
2. Run `build-production.bat` to create new builds
3. Copy new files to IIS directories (or recycle app pool)
4. Restart IIS site and application
5. Verify deployment

### Monitoring

- **IIS Logs**: `C:\inetpub\logs\LogFiles\`
- **Application Logs**: Event Viewer → Windows Logs → Application
- **ASP.NET Core Logs**: Check stdout logs if enabled

### Backup

Before major updates, backup:
- `D:\SFC\production\` - Entire production folder (frontend + backend)
- Database: `InventoryManagement`

**Note:** The `production\` folder contains timestamped builds, so you can keep previous versions by renaming the folder before building (e.g., `production_2025-12-31`).

---

## Security Notes

⚠️ **Important Security Considerations:**

1. **SMTP Credentials**: Consider using environment variables or Azure Key Vault for production
2. **Database Password**: Use Windows Authentication or secure credential storage
3. **HTTPS**: Consider configuring SSL certificate for production
4. **Swagger**: Currently enabled in production - consider restricting access if needed
5. **AllowedHosts**: Currently set to "adsvr" - adjust if domain changes

---

## Support

For issues or questions:
1. Check Event Viewer for detailed error messages
2. Review IIS logs for HTTP-level issues
3. Check browser console for frontend errors
4. Verify SQL Server connectivity and permissions

---

**Last Updated**: 2025-12-31
**Version**: 1.0
