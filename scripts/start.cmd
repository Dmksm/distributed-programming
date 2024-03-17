@echo off
start cmd /c "cd ..\Validator && dotnet run --urls "http://0.0.0.0:5001""
start cmd /c "cd ..\Validator && dotnet run --urls "http://0.0.0.0:5002""
start cmd /c "C:\nginx-1.24.0\nginx-1.24.0\nginx -c C:\nginx-1.24.0\nginx-1.24.0\conf\nginx.conf"