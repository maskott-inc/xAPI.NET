@ECHO OFF
SET ROOT_DIR=%~dp0
mode con:cols=150 lines=50

powershell -Command "& { .\publish.ps1 }"

ECHO %ERRORLEVEL%
PAUSE
EXIT /B %ERRORLEVEL%
