@ECHO OFF
SET MSBUILD="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
SET SOLUTION=LibCurlNet.sln
SET PLATFORMS="x86","x64"
SET CONFIGURATIONS=Debug,Release

FOR %%P IN (%PLATFORMS%) DO (
FOR %%C IN (%CONFIGURATIONS%) DO (
  %MSBUILD% %SOLUTION% /p:Configuration=%%C /p:Platform=%%P /target:Rebuild
  IF %ERRORLEVEL% NEQ 0 GOTO :Fail
)
)

GOTO :eof

:Fail
PAUSE
