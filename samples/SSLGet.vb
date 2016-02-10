' $Id: SSLGet.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' SSLGet.vb - demonstrate trivial SSL get capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/SSLGet.exe SSLGet.vb"
 
' usage: SSLGet url, e.g. SSLGet https://sourceforge.net

Imports System
Imports SeasideResearch.LibCurlNet

Module SSLGet

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim easy As Easy
            easy = new Easy

            ' Set up write delegate
	    Dim wf As Easy.WriteFunction
            wf = new Easy.WriteFunction(AddressOf OnWriteData)

            ' and the SSL delegate
            Dim sf As Easy.SSLContextFunction
            sf = new Easy.SSLContextFunction(AddressOf OnSSLContext)

            ' and the rest of the cURL options
            easy.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)
            easy.SetOpt(CURLoption.CURLOPT_SSL_CTX_FUNCTION, sf)
            easy.SetOpt(CURLoption.CURLOPT_CAINFO, "ca-bundle.crt")

            easy.Perform()
            easy.Cleanup()

            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    ' Called by libcURL.NET when it has data for your program
    Public Function OnWriteData(ByVal buf() As Byte, _
        ByVal size As Int32, ByVal nmemb As Int32, _
        ByVal extraData As Object) As Int32
        Console.Write(System.Text.Encoding.UTF8.GetString(buf))
        return size * nmemb
    End Function

    Public Function OnSSLContext(ByVal ctx As SSLContext, _
        ByVal extraData As Object) As CURLcode
        ' To do anything useful with the SSLContext object, you'll need
        ' to call the OpenSSL native methods on your own. So for this
        ' demo, we just return what cURL is expecting.
        return CURLcode.CURLE_OK
    End Function

End Module
