@echo off

:: This script requests administrator privileges if needed and adds the project's
:: Release build directory (bin\Release\net8.0) to the system PATH environment variable
:: if it is not already registered.

net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Asking administrator permissions...
    powershell start-process -FilePath '%0' -verb runas
    exit /b
)

setlocal enabledelayedexpansion

set "base_dir=%~dp0"
set "base_dir=%base_dir:~0,-1%"
set "target_dir=%base_dir%\bin\Release\net8.0"

echo Checking if path exists in System PATH: %target_dir%

for /f "tokens=2*" %%A in ('reg query "HKLM\System\CurrentControlSet\Control\Session Manager\Environment" /v Path') do set "sys_path=%%B"

echo %sys_path% | findstr /C:"%target_dir%" >nul

if %errorlevel% equ 0 (
    echo This path is already registered in System PATH.
) else (
    echo Path not found in registry. Adding now...
    
    setx /M PATH "%sys_path%;%target_dir%" >nul
    
    if !errorlevel! equ 0 (
        echo System PATH modified.
    ) else (
        echo Failed to modify System PATH.
    )
)

pause