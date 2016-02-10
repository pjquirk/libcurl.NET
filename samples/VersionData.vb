' $Id: VersionData.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' VersionData.vb - dump cURL version data
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/VersionData.exe VersionData.vb"
 
' usage: VersionData

Imports System
Imports SeasideResearch.LibCurlNet

Module VersionData

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim vd As VersionInfoData
            vd = Curl.GetVersionInfo(CURLversion.CURLVERSION_NOW)

            Console.WriteLine("           Age: {0}", vd.Age)
            Console.WriteLine("Version String: {0}", vd.Version)
            Console.WriteLine("Version Number: {0}", vd.VersionNum)
            Console.WriteLine("   Host System: {0}", vd.Host)
            Console.WriteLine("Feature Bitmap: {0}", vd.Features)
            Console.WriteLine("   SSL Version: {0}", vd.SSLVersion)
            Console.WriteLine("SSL VersionNum: {0}", vd.SSLVersionNum)
            Console.WriteLine("  LibZ Version: {0}", vd.LibZVersion)
            Console.WriteLine("  ARES Version: {0}", vd.ARes)
            Console.WriteLine("  ARES Ver Num: {0}", vd.AResNum)
            Console.WriteLine("LibIDN Version: {0}", vd.LibIDN)
            Console.WriteLine()
            Console.WriteLine("Protocols:")
            Dim protocols As String()
            Dim prot As STring
            protocols = vd.Protocols
            For Each prot in protocols
                Console.WriteLine("  {0}", prot)
            Next

            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

End Module
