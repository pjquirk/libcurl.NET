@echo off
rem $Id: PostBuild.bat,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
rem post-build step in Visual Studio IDE to route outupt files
rem to their proper directories
move LibCurlNet.dll ..\bin
move LibCurlNet.xml ..\doc

