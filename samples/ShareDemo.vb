' $Id: ShareDemo.vb,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
' ShareDemo.vb - demonstrate share capability
' compile with "vbc /r:../bin/LibCurlNet.dll /out:../bin/ShareDemo.exe ShareDemo.vb"
 
' usage: ShareDemo url1 url2, e.g. ShareDemo http://www.google.com http://www.yahoo.com

Imports System
Imports System.Threading
Imports SeasideResearch.LibCurlNet

Public Class EasyThread
    ' state information
    Private Shared wf As Easy.WriteFunction
    Private url As String
    Private share As Share

    Shared Sub New()
        Console.WriteLine("EasyThread class constructor")
        wf = new Easy.WriteFunction(AddressOf OnWriteData)
    End Sub


    Public Sub New(ByVal s As String, ByVal shr As Share)
        Console.WriteLine("EasyThread instance constructor: url={0}", s)
        url = s
        share = shr
    End Sub

    Public Sub ThreadFunc()
        Dim easy As Easy
        easy = new Easy()
        easy.SetOpt(CURLoption.CURLOPT_URL, url)
        easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf)
        easy.SetOpt(CURLoption.CURLOPT_WRITEDATA, url)
        easy.SetOpt(CURLoption.CURLOPT_SHARE, share)
        easy.Perform()
        easy.Cleanup()
    End Sub

    Public Shared Function OnWriteData(ByVal buf() As Byte, _
        ByVal size As Int32, ByVal nmemb As Int32, _
        ByVal extraData As Object) As Int32
        Dim nbytes As Int32
        nbytes = size * nmemb
        Console.WriteLine("Obtained {0} bytes from {1}", nbytes, extraData)
        return nbytes
    End Function
End Class

Module ShareDemo

    ' synchronization objects for DNS and cookies
    Private dnsLock As Object, cookieLock As Object

    Public Sub Main(ByVal args As String())
        Try
            Curl.GlobalInit(CURLinitFlag.CURL_GLOBAL_ALL)

            dnsLock = new Object()
            cookieLock = new Object()

            Dim share As Share
            share = new Share()
            Dim lf As Share.LockFunction, ulf As Share.UnlockFunction
            lf = new Share.LockFunction(AddressOf OnLock)
            ulf = new Share.UnlockFunction(AddressOf OnUnlock)
            share.SetOpt(CURLSHoption.CURLSHOPT_LOCKFUNC, lf)
            share.SetOpt(CURLSHoption.CURLSHOPT_UNLOCKFUNC, ulf)
            share.SetOpt(CURLSHoption.CURLSHOPT_SHARE, _
                CURLlockData.CURL_LOCK_DATA_COOKIE)
            share.SetOpt(CURLSHoption.CURLSHOPT_SHARE, _
                CURLlockData.CURL_LOCK_DATA_DNS)

            Dim et1 As EasyThread, et2 As EasyThread
            Dim t1 As Thread, t2 As Thread
            et1 = new EasyThread(args(0), share)
            et2 = new EasyThread(args(1), share)
            t1 = new Thread(new ThreadStart(AddressOf et1.ThreadFunc))
            t2 = new Thread(new ThreadStart(AddressOf et2.ThreadFunc))
            t1.Start()
            t2.Start()
            t1.Join()
            t2.Join()
  
            share.Cleanup()
            Curl.GlobalCleanup()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Sub OnLock(ByVal data As CURLlockData, _
        ByVal access As CURLlockAccess, ByVal extraData As Object)
        'Console.WriteLine("OnLock({0}, {1})", data, access)
        If (data = CURLlockData.CURL_LOCK_DATA_DNS) Then
            Monitor.Enter(dnsLock)
        ElseIf (data = CURLlockData.CURL_LOCK_DATA_COOKIE) Then
            Monitor.Enter(cookieLock)
        End If 
    End Sub

    Public Sub OnUnlock(ByVal data As CURLlockData, _
        ByVal extraData As Object)
        'Console.WriteLine("OnUnlock({0})", data)
        If (data = CURLlockData.CURL_LOCK_DATA_DNS) Then
            Monitor.Exit(dnsLock)
        ElseIf (data = CURLlockData.CURL_LOCK_DATA_COOKIE) Then
            Monitor.Exit(cookieLock)
        End If 
    End Sub

End Module
