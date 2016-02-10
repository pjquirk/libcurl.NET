' $Id: BookPost.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' BookPost.vb - Look for Topology books on Amazon
' compile with "vbc /r:../LibCurlNet.dll /out:../bin/BookPost.exe BookPost.vb"
 
' usage: BookPost
' NOTE: you may have to tweak this, as Amazon's page changes from time-to-time

Imports System
Imports SeasideResearch.LibCurlNet

Module BookPost

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim easy As Easy
            easy = new Easy

            ' Set up write delegate
	    Dim wf As Easy.WriteFunction
            wf = new Easy.WriteFunction(AddressOf OnWriteData)
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)

            ' Simple post - with a string
            easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS, _
                "url=index%3Dstripbooks&field-keywords=Topology&Go.x=10&Go.y=10")

            ' and the rest of the cURL options
            easy.SetOpt(CURLoption.CURLOPT_USERAGENT, _
                "Mozilla 4.0 (compatible; MSIE 6.0; Win32")
            easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, true)
            easy.SetOpt(CURLoption.CURLOPT_URL, _
                "http://www.amazon.com/exec/obidos/search-handle-form/002-5928901-6229641")
            easy.SetOpt(CURLoption.CURLOPT_POST, true)

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
