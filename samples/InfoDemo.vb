' $Id: InfoDemo.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' InfoDemo.vb - demonstrate Easy.GetInfo() capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/InfoDemo.exe InfoDemo.vb"
 
' usage: InfoDemo url, e.g. InfoDemo http://www.google.com

Imports System
Imports SeasideResearch.LibCurlNet

Module InfoDemo

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim easy As Easy = new Easy
 
            easy.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy.SetOpt(CURLoption.CURLOPT_PRIVATE, "Private string")
            easy.SetOpt(CURLoption.CURLOPT_FILETIME, True)
            easy.Perform()

            ' now do the various get info stuff
            Dim d As double = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_CONNECT_TIME, d)
            Console.WriteLine("Connect Time: {0}", d)

            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_LENGTH_DOWNLOAD, d)
            Console.WriteLine("Content Length (Download): {0}", d)

            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_LENGTH_UPLOAD, d)
            Console.WriteLine("Content Length (Upload): {0}", d)
    
            Dim s As String = Nothing
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_TYPE, s)
            Console.WriteLine("Content Type: {0}", s)
    
            Dim dt As DateTime = new DateTime(0) 
            easy.GetInfo(CURLINFO.CURLINFO_FILETIME, dt)
            Console.WriteLine("File time: {0}", dt)

            Dim n As Integer = 0 
            easy.GetInfo(CURLINFO.CURLINFO_HEADER_SIZE, n)
            Console.WriteLine("Header Size: {0}", n)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_HTTPAUTH_AVAIL, n)
            Console.WriteLine("Authentication Bitmask: {0}", n)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_HTTP_CONNECTCODE, n)
            Console.WriteLine("HTTP Connect Code: {0}", n)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_NAMELOOKUP_TIME, d)
            Console.WriteLine("Name Lookup Time: {0}", d)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_OS_ERRNO, n)
            Console.WriteLine("OS Errno: {0}", n)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_PRETRANSFER_TIME, d)
            Console.WriteLine("Pretransfer time: {0}", d)
    
            Dim o As Object = Nothing 
            easy.GetInfo(CURLINFO.CURLINFO_PRIVATE, o)
            Console.WriteLine("Private Data: {0}", o)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_PROXYAUTH_AVAIL, n)
            Console.WriteLine("Proxy Authentication Schemes: {0}", n)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_REDIRECT_COUNT, n)
            Console.WriteLine("Redirect count: {0}", n)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_REDIRECT_TIME, d)
            Console.WriteLine("Redirect time: {0}", d)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_REQUEST_SIZE, n)
            Console.WriteLine("Request size: {0}", n)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_RESPONSE_CODE, n)
            Console.WriteLine("Response code: {0}", n)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_SIZE_DOWNLOAD, d)
            Console.WriteLine("Download size: {0}", d)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_SIZE_UPLOAD, d)
            Console.WriteLine("Upload size: {0}", d)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_SPEED_DOWNLOAD, d)
            Console.WriteLine("Download speed: {0}", d)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_SPEED_UPLOAD, d)
            Console.WriteLine("Upload speed: {0}", d)
    
            n = 0
            easy.GetInfo(CURLINFO.CURLINFO_SSL_VERIFYRESULT, n)
            Console.WriteLine("SSL verification result: {0}", n)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_STARTTRANSFER_TIME, d)
            Console.WriteLine("Start transfer time: {0}", d)
    
            d = 0.0
            easy.GetInfo(CURLINFO.CURLINFO_TOTAL_TIME, d)
            Console.WriteLine("Total time: {0}", d)
    
            easy.Cleanup()
            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

End Module
