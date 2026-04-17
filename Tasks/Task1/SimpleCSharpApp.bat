@echo off
rem
.\bin\Debug\net10.0\TaskChap1.exe

if "%ERRORLEVEL%"=="0" goto success

:fail
echo This appl ication has failed!
echo return value = %ERRORLEVEL%
goto end

:success
echo This application has succeeded!
echo return value = %ERRORLEVEL%
goto end

:end
echo All Done.