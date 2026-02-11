# Full-Stack Application - SFC Project

A full-stack application with React frontend and ASP.NET Core backend.

## Project Structure

```
D:\SFC\
├── Frontend/          # React + Vite application
├── Backend/Project/   # ASP.NET Core 8.0 API
├── package.json       # Root orchestration file
├── start.bat          # Windows quick start script
└── README.md          # This file
```

## Prerequisites

Before running this project, ensure you have the following installed:

1. **Node.js** (v18 or higher) - [Download](https://nodejs.org/)
2. **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
3. **SQL Server** (or SQL Server Express)
4. **npm** (comes with Node.js)

## Initial Setup

### 1. Install Dependencies

Run this command in the root directory (D:\SFC):

```bash
npm install
```

Then install frontend and backend dependencies:

```bash
npm run install:all
```

Or install them separately:

```bash
# Install frontend dependencies
npm run install:frontend

# Install backend dependencies
npm run install:backend
```

### 2. Configure Database

Update the connection string in `Backend/Project/appsettings.json`:

```json
"ConnectionStrings": {
  "shadimuharathConStrt": "Server=YOUR_SERVER;Database=InventoryManagement;User ID=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

### 3. Run Database Migrations

Navigate to the backend directory and run migrations:

```bash
cd Backend/Project
dotnet ef database update
cd ../..
```

## Running the Application

### Option 1: Run Both Frontend and Backend Together (Recommended)

**Using npm:**
```bash
npm run dev
```

**Using Windows batch script:**
```bash
start.bat
```

This will start:
- Frontend on `http://localhost:5173`
- Backend on `http://localhost:5200`

### Option 2: Run Services Individually

**Frontend only:**
```bash
npm run dev:frontend
```

**Backend only:**
```bash
npm run dev:backend
```

## Available Scripts

| Script | Description |
|--------|-------------|
| `npm run dev` | Run both frontend and backend concurrently |
| `npm run dev:frontend` | Run only the frontend (Vite dev server) |
| `npm run dev:backend` | Run only the backend (ASP.NET Core) |
| `npm run build` | Build both frontend and backend for production |
| `npm run build:frontend` | Build only the frontend |
| `npm run build:backend` | Build only the backend |
| `npm run install:all` | Install dependencies for both projects |
| `npm run install:frontend` | Install only frontend dependencies |
| `npm run install:backend` | Restore only backend dependencies |

## Access Points

After starting the application:

- **Frontend:** http://localhost:5173
- **Backend API:** http://localhost:5200
- **Swagger Documentation:** http://localhost:5200/swagger

## Technology Stack

### Frontend
- React 18
- Vite
- Ant Design
- Redux Toolkit
- React Router
- Axios
- Chart.js
- And more...

### Backend
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- AutoMapper
- JWT Authentication
- Swagger/OpenAPI

## Configuration

### Frontend Configuration

Frontend proxy is configured in `Frontend/vite.config.js`:

```javascript
server: {
  proxy: {
    '/API': {
      target: 'http://localhost:5200',
      changeOrigin: true,
      secure: false,
    }
  }
}
```

### Backend Configuration

Backend configuration is in `Backend/Project/appsettings.json`:

- Connection strings
- SMTP settings
- JWT settings (if enabled)

## Troubleshooting

### Port Already in Use

If you get a "port already in use" error:

**For frontend (port 5173):**
- Stop any other Vite dev servers
- Or change the port in `Frontend/vite.config.js`

**For backend (port 5200):**
- Stop any other .NET applications
- Or change the port in `Backend/Project/Properties/launchSettings.json`

### Database Connection Issues

1. Ensure SQL Server is running
2. Verify connection string in `appsettings.json`
3. Check if the database exists
4. Run migrations: `dotnet ef database update`

### Dependencies Not Installing

Try clearing caches:

```bash
# Clear npm cache
npm cache clean --force

# For .NET
dotnet nuget locals all --clear
```

## Development Workflow

1. Make sure SQL Server is running
2. Run `npm run dev` to start both servers
3. Frontend will automatically open at http://localhost:5173
4. Backend API will be available at http://localhost:5200
5. Make changes to code - both servers support hot reload

## Building for Production

### Build both:
```bash
npm run build
```

### Build individually:
```bash
# Frontend production build
npm run build:frontend

# Backend production build
npm run build:backend
```

Frontend production files will be in `Frontend/dist/`

## License

ISC
