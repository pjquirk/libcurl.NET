' $Id: MultiDemo.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' MultiDemo.vb - demonstrate multi capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/MultiDemo.exe MultiDemo.vb"
 
' usage: MultiDemo url1 url2, e.g. MultiDemo http://www.google.com http://www.yahoo.com

Imports System
Imports SeasideResearch.LibCurlNet

Module MultiDemo

    Public Sub Main(ByVal args As String())

        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            ' Set up write delegate
	    Dim wf As Easy.WriteFunction
            wf = new Easy.WriteFunction(AddressOf OnWriteData)

            Dim easy1 As Easy, easy2 As Easy
            Dim s1 As String, s2 As String

            easy1 = new Easy
            s1 = args(0).Clone()
            easy1.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy1.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)
            easy1.SetOpt(CURLoption.CURLOPT_WRITEDATA, s1)
    
            easy2 = new Easy
            s2 = args(1).Clone()
            easy2.SetOpt(CURLoption.CURLOPT_URL, args(1))
            easy2.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)
            easy2.SetOpt(CURLoption.CURLOPT_WRITEDATA, s2)

            Dim multi As Multi
            multi = new Multi()
            multi.AddHandle(easy1)
            multi.AddHandle(easy2)

            Dim stillRunning As Int32
            stillRunning = 1
            ' call Multi.Perform right away
            While (multi.Perform(stillRunning) = _
                CURLMcode.CURLM_CALL_MULTI_PERFORM)
            End while
 
            Dim rc As Int32
            While (stillRunning <> 0)
                multi.FDSet()
                rc = multi.Select(1000)
                If (rc = -1) Then
                    Console.WriteLine("Multi.Select() returned -1")
                    stillRunning = 0
                    Exit While
                End If
                ' call multi.Perform
                While (multi.Perform(stillRunning) = _
                    CURLMcode.CURLM_CALL_MULTI_PERFORM)
                End While
            End While

            multi.Cleanup()
            easy1.Cleanup()
            easy2.Cleanup()
            Curl.GlobalCleanup()

        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

    End Sub

    ' Called by libcURL.NET when it has data for your program
    Public Function OnWriteData(ByVal buf() As Byte, _
        ByVal size As Int32, ByVal nmemb As Int32, _
        ByVal extraData As Object) As Int32
        Dim nbytes As Int32
        nbytes = size * nmemb
        Console.WriteLine("Obtained {0} bytes from {1}", nbytes, extraData)
        return nbytes
    End Function

End Module
