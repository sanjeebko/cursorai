@echo off
echo Starting Nepali Community API...
echo.
echo API will be available at:
echo - HTTP:  http://localhost:5106
echo - HTTPS: https://localhost:7250
echo - Swagger: https://localhost:7250/swagger
echo.
echo Press Ctrl+C to stop the server
echo.

cd NepaliCommunityApi
dotnet run --urls "http://localhost:5106;https://localhost:7250" 