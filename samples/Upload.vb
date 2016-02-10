' $Id: Upload.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' Upload.vb - demonstrate ftp upload capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/Upload.exe Upload.vb"
 
' usage: Upload srcFile destUrl username password
' e.g. upload myFile.dat ftp://ftp.myftp.com me myPassword

Imports System
Imports System.IO
Imports SeasideResearch.LibCurlNet

Module Upload

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            Dim fs As FileStream
            fs = new FileStream(args(0), FileMode.Open, _
                FileAccess.Read, FileShare.Read)

            Dim easy As Easy
            easy = new Easy

	    Dim rf As Easy.ReadFunction
            rf = new Easy.ReadFunction(AddressOf OnReadData)
            easy.SetOpt(CURLoption.CURLOPT_READFUNCTION, rf)
            easy.SetOpt(CURLoption.CURLOPT_READDATA, fs)

            Dim df As Easy.DebugFunction
            df = new Easy.DebugFunction(Addressof OnDebug)
            easy.SetOpt(CURLoption.CURLOPT_DEBUGFUNCTION, df)
            easy.SetOpt(CURLoption.CURLOPT_VERBOSE, true)

            Dim pf As Easy.ProgressFunction
            pf = new Easy.ProgressFunction(AddressOf OnProgress)
            easy.SetOpt(CURLoption.CURLOPT_PROGRESSFUNCTION, pf)

            easy.SetOpt(CURLoption.CURLOPT_URL, args(1))
            easy.SetOpt(CURLoption.CURLOPT_USERPWD, _
                args(2) + ":" + args(3))
            easy.SetOpt(CURLoption.CURLOPT_UPLOAD, true)
            easy.SetOpt(CURLoption.CURLOPT_INFILESIZE, fs.Length)

            easy.Perform()
            easy.Cleanup()

            fs.Close()

            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Function OnReadData(ByVal buf() As Byte, _
        ByVal size As Int32, ByVal nmemb As Int32, _
        ByVal extraData As Object) As Int32
        Dim fs As FileStream
        fs = extraData
        return fs.Read(buf, 0, size * nmemb)
    End Function


    Public Sub OnDebug(ByVal infoType As CURLINFOTYPE, _
        ByVal msg As String, ByVal extraData As Object)
        Console.WriteLine(msg)
    End Sub

    Public Function OnProgress(ByVal extraData As Object, _
        ByVal dlTotal As Double, ByVal dlNow As Double, _
        ByVal ulTotal As Double, ByVal ulNow As Double) As Int32
        Console.WriteLine("Progress: {0} {1} {2} {3}", dlTotal, dlNow, _
            ulTotal, ulNow)
        return 0
    End Function
End Module
