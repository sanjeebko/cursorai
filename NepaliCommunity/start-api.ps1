Write-Host "Starting Nepali Community API..." -ForegroundColor Green
Write-Host ""
Write-Host "API will be available at:" -ForegroundColor Yellow
Write-Host "- HTTP:  http://localhost:5106" -ForegroundColor Cyan
Write-Host "- HTTPS: https://localhost:7250" -ForegroundColor Cyan
Write-Host "- Swagger: https://localhost:7250/swagger" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press Ctrl+C to stop the server" -ForegroundColor Red
Write-Host ""

Set-Location "NepaliCommunityApi"
dotnet run --urls "http://localhost:5106;https://localhost:7250" 