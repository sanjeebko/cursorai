# GitHub Workflows

This directory contains GitHub Actions workflows for the Nepali Community application.

## Workflows Overview

### 1. **build-and-test.yml** - Main Build and Test Workflow
**Triggers:** Push to main/develop, Pull Requests

**What it does:**
- ✅ Builds .NET API with Release configuration
- ✅ Runs unit tests for the API
- ✅ Builds Angular frontend
- ✅ Runs security scans with Trivy
- ✅ Checks code quality and formatting
- ✅ Uploads build artifacts

**Jobs:**
- `build-api`: .NET API build and test
- `build-frontend`: Angular frontend build
- `security-scan`: Vulnerability scanning
- `code-quality`: Code formatting and linting checks

### 2. **deploy.yml** - Deployment Workflow
**Triggers:** Release published, Manual dispatch

**What it does:**
- 🚀 Creates deployment packages for API and frontend
- 🚀 Supports staging and production environments
- 🚀 Provides deployment status notifications

**Jobs:**
- `deploy-api`: .NET API deployment package
- `deploy-frontend`: Angular frontend deployment package
- `notify-deployment`: Deployment status notification

### 3. **database-test.yml** - Database Testing Workflow
**Triggers:** Changes to database-related files

**What it does:**
- 🗄️ Tests database schema creation
- 🗄️ Tests data seeding functionality
- 🗄️ Uses SQL Server container for testing

**Jobs:**
- `test-database`: Database schema and migration tests

### 4. **dependency-check.yml** - Dependency Management Workflow
**Triggers:** Weekly schedule, Manual dispatch

**What it does:**
- 📦 Checks for outdated .NET and NPM packages
- 📦 Runs security audits
- 📦 Creates issues for outdated dependencies
- 📦 Can auto-update dependencies (manual trigger)

**Jobs:**
- `check-dotnet-dependencies`: .NET package checks
- `check-npm-dependencies`: NPM package checks
- `auto-update-dependencies`: Automatic dependency updates

## Workflow Features

### 🔒 Security
- **Trivy Vulnerability Scanner**: Scans for security vulnerabilities
- **CodeQL Integration**: Uploads security scan results to GitHub Security tab
- **Dependency Audits**: Regular checks for vulnerable packages

### 🧪 Testing
- **Unit Tests**: Automated .NET unit test execution
- **Database Tests**: Schema and migration testing with SQL Server
- **Build Verification**: Ensures both API and frontend build successfully

### 📦 Artifacts
- **API Artifacts**: Published .NET application
- **Frontend Artifacts**: Built Angular application
- **Deployment Packages**: Ready-to-deploy packages

### 🔄 Automation
- **Scheduled Checks**: Weekly dependency updates
- **Path-based Triggers**: Only runs when relevant files change
- **Environment Support**: Staging and production deployment

## Usage

### Automatic Triggers
Workflows run automatically on:
- **Push to main/develop**: Full build and test
- **Pull Requests**: Build and test validation
- **Release published**: Deployment workflow
- **Weekly schedule**: Dependency checks

### Manual Triggers
You can manually trigger workflows:
1. Go to **Actions** tab in GitHub
2. Select the workflow you want to run
3. Click **Run workflow**
4. Choose branch and options

### Environment Variables
Set these in your GitHub repository settings:

**For Deployment:**
- `DEPLOYMENT_SERVER`: Your deployment server
- `DEPLOYMENT_USERNAME`: Deployment username
- `DEPLOYMENT_PASSWORD`: Deployment password

**For Notifications:**
- `SLACK_WEBHOOK_URL`: Slack notifications (optional)
- `DISCORD_WEBHOOK_URL`: Discord notifications (optional)

## Workflow Status Badges

Add these badges to your README.md:

```markdown
![Build and Test](https://github.com/{owner}/{repo}/workflows/Build%20and%20Test/badge.svg)
![Deploy](https://github.com/{owner}/{repo}/workflows/Deploy/badge.svg)
![Database Test](https://github.com/{owner}/{repo}/workflows/Database%20Test/badge.svg)
![Dependency Check](https://github.com/{owner}/{repo}/workflows/Dependency%20Check/badge.svg)
```

## Troubleshooting

### Common Issues

1. **Build Failures**
   - Check the Actions tab for detailed error logs
   - Ensure all dependencies are properly configured
   - Verify .NET and Node.js versions match

2. **Database Test Failures**
   - SQL Server container might not be ready
   - Check connection string configuration
   - Verify database seeding logic

3. **Security Scan Issues**
   - Review Trivy scan results
   - Update vulnerable dependencies
   - Check for false positives

### Debugging
- All workflows include detailed logging
- Check the **Actions** tab for step-by-step execution
- Use workflow dispatch for manual testing
- Review artifact uploads for build outputs

## Customization

### Adding New Workflows
1. Create a new `.yml` file in this directory
2. Follow the existing workflow structure
3. Add appropriate triggers and jobs
4. Test with workflow dispatch

### Modifying Existing Workflows
- Update triggers as needed
- Add new steps to existing jobs
- Modify environment configurations
- Update artifact paths

### Environment-Specific Configurations
- Use GitHub Environments for different deployment targets
- Configure environment-specific variables
- Set up approval workflows for production 