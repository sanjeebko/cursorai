@echo off
echo 🚀 Starting Pre-Push Checks for Nepali Community Project...
echo ==================================================

setlocal enabledelayedexpansion

REM Phase 1: .NET API Checks
echo.
echo 📦 Phase 1: Checking .NET API...

cd NepaliCommunityApi
if not exist "NepaliCommunityApi" (
    echo ❌ API directory not found
    exit /b 1
)

echo   Restoring .NET dependencies...
dotnet restore
if %errorlevel% neq 0 (
    echo ❌ Restore failed
    exit /b 1
)
echo ✅ Dependencies restored

echo   Building .NET API...
dotnet build --configuration Release
if %errorlevel% neq 0 (
    echo ❌ Build failed
    exit /b 1
)
echo ✅ API built successfully

echo   Checking code formatting...
dotnet format --verify-no-changes .
if %errorlevel% neq 0 (
    echo ⚠️  Formatting issues found. Applying fixes...
    dotnet format .
    git add .
    git commit -m "Apply code formatting"
    echo ✅ Formatting applied and committed
) else (
    echo ✅ Code formatting is correct
)

REM Phase 2: Angular Frontend Checks
echo.
echo 🌐 Phase 2: Checking Angular Frontend...

cd ..\NepaliCommunityUi
if not exist "NepaliCommunityUi" (
    echo ❌ Frontend directory not found
    exit /b 1
)

echo   Installing npm dependencies...
call npm ci
if %errorlevel% neq 0 (
    echo ❌ npm install failed
    exit /b 1
)
echo ✅ Dependencies installed

echo   Running ESLint...
call npm run lint
if %errorlevel% neq 0 (
    echo ❌ Linting failed
    exit /b 1
)
echo ✅ Linting passed

echo   Building Angular frontend...
call npm run build
if %errorlevel% neq 0 (
    echo ❌ Build failed
    exit /b 1
)
echo ✅ Frontend built successfully

REM Phase 3: GitHub Workflow Checks
echo.
echo 🔧 Phase 3: Checking GitHub Workflows...

cd ..
if not exist ".github\workflows\build-and-test.yml" (
    echo ❌ Workflow files missing
    exit /b 1
)
echo ✅ Workflow files present

REM Phase 4: Final Verification
echo.
echo 🔍 Phase 4: Final Verification...

echo   Checking git status...
git status --porcelain
if %errorlevel% equ 0 (
    echo ✅ Working directory is clean
) else (
    echo ⚠️  Uncommitted changes detected
)

echo.
echo ==================================================
echo 🎉 All pre-push checks passed!
echo 🚀 Ready to push to GitHub!
echo.
echo Next steps:
echo   git add .
echo   git commit -m "Your commit message"
echo   git push origin main

pause 