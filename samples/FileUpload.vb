' $Id: FileUpload.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' FileUpload.vb - demonstrate RFC 1867 file upload capability
' compile with "vbc /r:..../bin/LibCurlNet.dll /out:../bin/FileUpload.exe FileUpload.vb"
 
' usage: FileUpload url fileName
' e.g. FileUpload http://mybox/cgi-bin/myscript.cgi myFile.at
' NOTE: you'll need to modify this as per your form's fields

Imports System
Imports SeasideResearch.LibCurlNet

Module FileUpload

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            ' <form action="http://mybox/cgi-bin/myscript.cgi
            '  method="post" enctype="multipart/form-data">
            Dim mf As MultiPartForm
            mf = new MultiPartForm()

            ' <input name = "frmUsername">
            mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmUsername", _
               CURLformoption.CURLFORM_COPYCONTENTS, "testtcc", _
               CURLformoption.CURLFORM_END)
               
            ' <input name = "frmPassword">
            mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmPassword", _
               CURLformoption.CURLFORM_COPYCONTENTS, "tcc", _
               CURLformoption.CURLFORM_END)

            ' <input name = "frmFileOrigPath">
            mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmFileOrigPath", _
               CURLformoption.CURLFORM_COPYCONTENTS, args(1), _
               CURLformoption.CURLFORM_END)

            ' <input name = "frmFileDate">
            mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmFileDate", _
               CURLformoption.CURLFORM_COPYCONTENTS, "08/01/2004", _
               CURLformoption.CURLFORM_END)

            ' <input type="File" name="f1">
            mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "f1", _
                CURLformoption.CURLFORM_FILE, args(1), _
                CURLformoption.CURLFORM_CONTENTTYPE, "application/binary", _
                CURLformoption.CURLFORM_END)

            Dim easy As Easy
            easy = new Easy

            Dim df As Easy.DebugFunction
            df = new Easy.DebugFunction(AddressOf OnDebug)
            easy.SetOpt(CURLoption.CURLOPT_DEBUGFUNCTION, df)
            easy.SetOpt(CURLoption.CURLOPT_VERBOSE, true)

            Dim pf As Easy.ProgressFunction
            pf = new Easy.ProgressFunction(AddressOf OnProgress)
            easy.SetOpt(CURLoption.CURLOPT_PROGRESSFUNCTION, pf)

            easy.SetOpt(CURLoption.CURLOPT_URL, args(0))
            easy.SetOpt(CURLoption.CURLOPT_HTTPPOST, mf)

            easy.Perform()
            easy.Cleanup()
            mf.Free()
            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Sub OnDebug(ByVal infoType As CURLINFOTYPE, _
        ByVal msg As String, ByVal extraData As Object)
         'only dump received data
         if (infoType = CURLINFOTYPE.CURLINFO_DATA_IN) then
            Console.WriteLine(msg)
         end if
    End Sub

    Public Function OnProgress(ByVal extraData As Object, _
        ByVal dlTotal As Double, ByVal dlNow As Double, _
        ByVal ulTotal As Double, ByVal ulNow As Double) As Int32
        Console.WriteLine("Progress: {0} {1} {2} {3}", dlTotal, dlNow, _
            ulTotal, ulNow)
        return 0
    End Function

End Module
