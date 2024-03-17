@echo off
start cmd /c "taskkill /f /im validator.exe"
start cmd /c "taskkill /f /im nginx.exe"