' $Id: Headers.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' Headers.vb - dump headers
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/Headers.exe Headers.vb"
 
' usage: Headers url, e.g. Headers http://www.google.com

Imports System
Imports SeasideResearch.LibCurlNet

Module Headers

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim easy As Easy
            easy = new Easy

            ' Set up write delegate
	    Dim hf As Easy.HeaderFunction
            hf = new Easy.HeaderFunction(AddressOf OnHeaderData)

            ' and the rest of the cURL options
            easy.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy.SetOpt(CURLoption.CURLOPT_HEADERFUNCTION, hf)

            easy.Perform()
            easy.Cleanup()

            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Function OnHeaderData(ByVal buf() As Byte, _
        ByVal size As Int32, ByVal nmemb As Int32, _
        ByVal extraData As Object) As Int32
        Console.Write(System.Text.Encoding.UTF8.GetString(buf))
        return size * nmemb
    End Function

End Module
