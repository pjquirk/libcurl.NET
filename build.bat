@ECHO OFF
SET MSBUILD="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
SET SOLUTION=build.proj

%MSBUILD% %SOLUTION% /target:Publish
IF %ERRORLEVEL% NEQ 0 GOTO :Fail

GOTO :eof

:Fail
PAUSE
