' $Id: EasyGet.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' EasyGet.vb - demonstrate trivial get capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/EasyGet.exe EasyGet.vb"
 
' usage: EasyGet url, e.g. EasyGet http://www.google.com

Imports System
Imports SeasideResearch.LibCurlNet

Module EasyGet

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim easy As Easy
            easy = new Easy

            ' Set up write delegate
	    Dim wf As Easy.WriteFunction
            wf = new Easy.WriteFunction(AddressOf OnWriteData)

            ' and the rest of the cURL options
            easy.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)

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

End Module
