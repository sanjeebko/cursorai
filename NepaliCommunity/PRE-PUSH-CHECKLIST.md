# Pre-Push Checklist for GitHub Actions

This document provides a step-by-step checklist to run before pushing code to GitHub to ensure all GitHub Actions workflows pass successfully.

## 🚀 Quick Start Commands

Run these commands in sequence from the project root:

```bash
# 1. Navigate to project root
cd cursorai/NepaliCommunity

# 2. Run the complete checklist
./run-pre-push-checks.ps1
```

## 📋 Detailed Checklist

### **Phase 1: .NET API Checks**

#### 1.1 Navigate to API Directory
```bash
cd NepaliCommunityApi
```

#### 1.2 Restore Dependencies
```bash
dotnet restore
```
**✅ Expected**: No errors, packages restored successfully

#### 1.3 Build the API
```bash
dotnet build --configuration Release
```
**✅ Expected**: Build successful, no compilation errors

#### 1.4 Run Tests (if any)
```bash
dotnet test --configuration Release
```
**✅ Expected**: All tests pass

#### 1.5 Check Code Formatting
```bash
dotnet format --verify-no-changes .
```
**✅ Expected**: No formatting changes needed

**❌ If formatting issues found:**
```bash
dotnet format .
git add .
git commit -m "Apply code formatting"
```

#### 1.6 Verify API Project Structure
```bash
# Check if all required files exist
ls Controllers/ Models/ Services/ DTOs/ Data/
```
**✅ Expected**: All directories and files present

---

### **Phase 2: Angular Frontend Checks**

#### 2.1 Navigate to Frontend Directory
```bash
cd ../NepaliCommunityUi
```

#### 2.2 Install Dependencies
```bash
npm ci
```
**✅ Expected**: All packages installed, no errors

#### 2.3 Run Linting
```bash
npm run lint
```
**✅ Expected**: "All files pass linting"

**❌ If linting errors found:**
```bash
npm run lint -- --fix
# Review any remaining errors manually
```

#### 2.4 Build the Frontend
```bash
npm run build
```
**✅ Expected**: Build successful, no compilation errors

#### 2.5 Verify Frontend Project Structure
```bash
# Check if all required files exist
ls src/app/components/ src/app/services/
```
**✅ Expected**: All directories and files present

---

### **Phase 3: Configuration Checks**

#### 3.1 Verify Package.json Dependencies
```bash
# Check for invalid package names
grep -n "common/http" package.json
```
**✅ Expected**: No invalid package references

#### 3.2 Check ESLint Configuration
```bash
# Verify ESLint config exists
ls eslint.config.js
```
**✅ Expected**: `eslint.config.js` file exists

#### 3.3 Verify Angular Configuration
```bash
# Check angular.json for lint configuration
grep -A 5 -B 5 "lint" angular.json
```
**✅ Expected**: Lint configuration present

---

### **Phase 4: GitHub Workflow Checks**

#### 4.1 Verify Workflow Files
```bash
cd ..
ls .github/workflows/
```
**✅ Expected**: All workflow files present:
- `build-and-test.yml`
- `deploy.yml`
- `dependency-check.yml`

#### 4.2 Check Workflow Syntax
```bash
# Verify YAML syntax (if yamllint is available)
yamllint .github/workflows/*.yml
```
**✅ Expected**: No YAML syntax errors

---

### **Phase 5: Final Verification**

#### 5.1 Run Complete Build Test
```bash
# Test API build
cd NepaliCommunityApi
dotnet build --configuration Release --no-restore

# Test Frontend build
cd ../NepaliCommunityUi
npm run build
```

#### 5.2 Check Git Status
```bash
cd ..
git status
```
**✅ Expected**: Clean working directory or only expected changes

#### 5.3 Verify File Paths in Workflows
```bash
# Check that workflow paths match actual structure
grep -n "NepaliCommunity/" .github/workflows/*.yml
```
**✅ Expected**: All paths use `NepaliCommunity/NepaliCommunityApi` and `NepaliCommunity/NepaliCommunityUi`

---

## 🚨 Common Issues and Solutions

### **Issue 1: .NET Formatting Errors**
```bash
# Solution: Apply formatting
cd NepaliCommunityApi
dotnet format .
git add .
git commit -m "Apply code formatting"
```

### **Issue 2: Angular Linting Errors**
```bash
# Solution: Fix linting issues
cd NepaliCommunityUi
npm run lint -- --fix
# Review remaining errors and fix manually
```

### **Issue 3: Build Failures**
```bash
# Solution: Clean and rebuild
cd NepaliCommunityApi
dotnet clean
dotnet restore
dotnet build

cd ../NepaliCommunityUi
npm ci
npm run build
```

### **Issue 4: Missing Dependencies**
```bash
# Solution: Update package.json
cd NepaliCommunityUi
npm install
git add package.json package-lock.json
git commit -m "Update dependencies"
```

---

## 📝 Pre-Push Script

Create a PowerShell script `run-pre-push-checks.ps1` in the project root:

```powershell
#!/usr/bin/env pwsh

Write-Host "🚀 Starting Pre-Push Checks..." -ForegroundColor Green

# Phase 1: .NET API Checks
Write-Host "📦 Checking .NET API..." -ForegroundColor Yellow
cd NepaliCommunityApi

Write-Host "  Restoring dependencies..." -ForegroundColor Cyan
dotnet restore
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Restore failed" -ForegroundColor Red; exit 1 }

Write-Host "  Building API..." -ForegroundColor Cyan
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Build failed" -ForegroundColor Red; exit 1 }

Write-Host "  Checking formatting..." -ForegroundColor Cyan
dotnet format --verify-no-changes .
if ($LASTEXITCODE -ne 0) { 
    Write-Host "⚠️  Formatting issues found. Applying fixes..." -ForegroundColor Yellow
    dotnet format .
    git add .
    git commit -m "Apply code formatting"
}

# Phase 2: Angular Frontend Checks
Write-Host "🌐 Checking Angular Frontend..." -ForegroundColor Yellow
cd ../NepaliCommunityUi

Write-Host "  Installing dependencies..." -ForegroundColor Cyan
npm ci
if ($LASTEXITCODE -ne 0) { Write-Host "❌ npm install failed" -ForegroundColor Red; exit 1 }

Write-Host "  Running linting..." -ForegroundColor Cyan
npm run lint
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Linting failed" -ForegroundColor Red; exit 1 }

Write-Host "  Building frontend..." -ForegroundColor Cyan
npm run build
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Build failed" -ForegroundColor Red; exit 1 }

# Phase 3: Final Check
Write-Host "✅ All checks passed!" -ForegroundColor Green
Write-Host "🚀 Ready to push to GitHub!" -ForegroundColor Green

cd ..
```

---

## 🔧 Automation Setup

### **Git Hooks (Optional)**
Create `.git/hooks/pre-push`:

```bash
#!/bin/bash
echo "Running pre-push checks..."
./run-pre-push-checks.ps1
if [ $? -ne 0 ]; then
    echo "❌ Pre-push checks failed. Push aborted."
    exit 1
fi
echo "✅ Pre-push checks passed."
```

Make it executable:
```bash
chmod +x .git/hooks/pre-push
```

---

## 📊 Checklist Summary

Before pushing to GitHub, ensure:

- [ ] **API builds successfully** (`dotnet build --configuration Release`)
- [ ] **API formatting is correct** (`dotnet format --verify-no-changes .`)
- [ ] **Frontend dependencies installed** (`npm ci`)
- [ ] **Frontend linting passes** (`npm run lint`)
- [ ] **Frontend builds successfully** (`npm run build`)
- [ ] **All workflow files present** (`.github/workflows/`)
- [ ] **Git status is clean** (no unexpected changes)
- [ ] **File paths are correct** in workflow files

---

## 🎯 Success Criteria

Your code is ready to push when:

1. ✅ All build commands complete without errors
2. ✅ All linting/formatting checks pass
3. ✅ No compilation warnings or errors
4. ✅ All required files and directories exist
5. ✅ Git working directory is clean

---

## 🚀 Push Commands

Once all checks pass:

```bash
git add .
git commit -m "Your commit message"
git push origin main
```

The GitHub Actions should now run successfully! 🎉 