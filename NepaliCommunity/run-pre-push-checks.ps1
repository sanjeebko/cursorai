#!/usr/bin/env pwsh

Write-Host "🚀 Starting Pre-Push Checks for Nepali Community Project..." -ForegroundColor Green
Write-Host "==================================================" -ForegroundColor Green

$ErrorActionPreference = "Stop"
$success = $true

# Function to check command result
function Test-CommandResult {
    param($Command, $Description)
    
    Write-Host "  $Description..." -ForegroundColor Cyan
    & $Command
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ $Description failed" -ForegroundColor Red
        $script:success = $false
        return $false
    }
    Write-Host "✅ $Description passed" -ForegroundColor Green
    return $true
}

# Function to check if directory exists
function Test-DirectoryExists {
    param($Path, $Description)
    
    if (Test-Path $Path) {
        Write-Host "✅ $Description exists" -ForegroundColor Green
        return $true
    }
    else {
        Write-Host "❌ $Description missing: $Path" -ForegroundColor Red
        $script:success = $false
        return $false
    }
}

# Phase 1: .NET API Checks
Write-Host "`n📦 Phase 1: Checking .NET API..." -ForegroundColor Yellow

# Check if API directory exists
if (-not (Test-DirectoryExists "NepaliCommunityApi" "API directory")) {
    exit 1
}

# Navigate to API directory
Set-Location "NepaliCommunityApi"

# Check required directories
$requiredDirs = @("Controllers", "Models", "Services", "DTOs", "Data")
foreach ($dir in $requiredDirs) {
    Test-DirectoryExists $dir "API $dir directory"
}

# Restore dependencies
Test-CommandResult { dotnet restore } "Restoring .NET dependencies"

# Build API
Test-CommandResult { dotnet build --configuration Release } "Building .NET API"

# Check formatting
Write-Host "  Checking code formatting..." -ForegroundColor Cyan
dotnet format --verify-no-changes .
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  Formatting issues found. Applying fixes..." -ForegroundColor Yellow
    dotnet format .
    git add .
    git commit -m "Apply code formatting"
    Write-Host "✅ Formatting applied and committed" -ForegroundColor Green
}
else {
    Write-Host "✅ Code formatting is correct" -ForegroundColor Green
}

# Phase 2: Angular Frontend Checks
Write-Host "`n🌐 Phase 2: Checking Angular Frontend..." -ForegroundColor Yellow

# Navigate to frontend directory
Set-Location "../NepaliCommunityUi"

# Check if frontend directory exists
if (-not (Test-DirectoryExists "." "Frontend directory")) {
    exit 1
}

# Check required directories
$requiredFrontendDirs = @("src/app/components", "src/app/services")
foreach ($dir in $requiredFrontendDirs) {
    Test-DirectoryExists $dir "Frontend $dir directory"
}

# Install dependencies
Test-CommandResult { npm ci } "Installing npm dependencies"

# Check for invalid package references
Write-Host "  Checking package.json for invalid references..." -ForegroundColor Cyan
$packageContent = Get-Content "package.json" -Raw
if ($packageContent -match "@angular/common/http") {
    Write-Host "❌ Invalid package reference found in package.json" -ForegroundColor Red
    $success = $false
}
else {
    Write-Host "✅ Package.json is valid" -ForegroundColor Green
}

# Check ESLint configuration
if (-not (Test-DirectoryExists "eslint.config.js" "ESLint configuration")) {
    $success = $false
}

# Run linting
Test-CommandResult { npm run lint } "Running ESLint"

# Build frontend
Test-CommandResult { npm run build } "Building Angular frontend"

# Phase 3: GitHub Workflow Checks
Write-Host "`n🔧 Phase 3: Checking GitHub Workflows..." -ForegroundColor Yellow

# Navigate back to project root
Set-Location ".."

# Check workflow files
$requiredWorkflows = @(
    ".github/workflows/build-and-test.yml",
    ".github/workflows/deploy.yml", 
    ".github/workflows/dependency-check.yml"
)

foreach ($workflow in $requiredWorkflows) {
    Test-DirectoryExists $workflow "Workflow file"
}

# Check workflow paths
Write-Host "  Verifying workflow paths..." -ForegroundColor Cyan
$workflowFiles = Get-ChildItem ".github/workflows/*.yml"
foreach ($file in $workflowFiles) {
    $content = Get-Content $file.FullName -Raw
    if ($content -match "NepaliCommunityApi" -and $content -notmatch "NepaliCommunity/NepaliCommunityApi") {
        Write-Host "❌ Incorrect path in $($file.Name)" -ForegroundColor Red
        $success = $false
    }
    if ($content -match "NepaliCommunityUi" -and $content -notmatch "NepaliCommunity/NepaliCommunityUi") {
        Write-Host "❌ Incorrect path in $($file.Name)" -ForegroundColor Red
        $success = $false
    }
}

if ($success) {
    Write-Host "✅ All workflow paths are correct" -ForegroundColor Green
}

# Phase 4: Final Verification
Write-Host "`n🔍 Phase 4: Final Verification..." -ForegroundColor Yellow

# Check git status
Write-Host "  Checking git status..." -ForegroundColor Cyan
$gitStatus = git status --porcelain
if ($gitStatus) {
    Write-Host "⚠️  Uncommitted changes detected:" -ForegroundColor Yellow
    Write-Host $gitStatus -ForegroundColor Gray
    Write-Host "  Consider committing these changes before pushing" -ForegroundColor Yellow
}
else {
    Write-Host "✅ Working directory is clean" -ForegroundColor Green
}

# Final Result
Write-Host "`n==================================================" -ForegroundColor Green
if ($success) {
    Write-Host "🎉 All pre-push checks passed!" -ForegroundColor Green
    Write-Host "🚀 Ready to push to GitHub!" -ForegroundColor Green
    Write-Host "`nNext steps:" -ForegroundColor Cyan
    Write-Host "  git add ." -ForegroundColor White
    Write-Host "  git commit -m 'Your commit message'" -ForegroundColor White
    Write-Host "  git push origin main" -ForegroundColor White
    exit 0
}
else {
    Write-Host "❌ Pre-push checks failed!" -ForegroundColor Red
    Write-Host "Please fix the issues above before pushing to GitHub." -ForegroundColor Red
    exit 1
} 