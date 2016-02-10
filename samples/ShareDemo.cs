// $Id: ShareDemo.cs,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
// ShareDemo.cs - demonstrate share capability
// Compile with "csc /r:../bin/LibCurlNet.dll /out:../bin/ShareDemo.exe ShareDemo.cs"

// usage: ShareDemo url1 url2, e.g. ShareDemo http://www.google.com http://www.yahoo.com

using System;
using System.Threading;
using SeasideResearch.LibCurlNet;

public class EasyThread
{
    // state information
    private static Easy.WriteFunction wf;
    private String url;
    private Share share;

    // static class constructor to create static delegate
    static EasyThread() {
        Console.WriteLine("EasyThread class constructor");
        wf = new Easy.WriteFunction(OnWriteData);
    }

    // instance constructor for url
    public EasyThread(String s, Share shr) {
        Console.WriteLine("EasyThread instance constructor: url={0}", s);
        url = s;
        share = shr;
    }

    public void ThreadFunc() {
        Easy easy = new Easy();
        easy.SetOpt(CURLoption.CURLOPT_URL, url);
        easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
        easy.SetOpt(CURLoption.CURLOPT_WRITEDATA, url);
        easy.SetOpt(CURLoption.CURLOPT_SHARE, share);
        easy.Perform();
        easy.Cleanup();
    }

    public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb,
        Object extraData)
    {
        int nBytes = size * nmemb;
        Console.WriteLine("Obtained {0} bytes from {1}", nBytes, extraData);
        return nBytes;
    }        
}

class ShareDemo
{
    // synchronization objects for DNS and Cookies
    private static Object dnsLock, cookieLock;

    public static void Main(String[] args)
    {
        try {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
    
            dnsLock = new Object();
            cookieLock = new Object();

            Share share = new Share();
            Share.LockFunction lf = new Share.LockFunction(OnLock);
            Share.UnlockFunction ulf = new Share.UnlockFunction(OnUnlock);
            share.SetOpt(CURLSHoption.CURLSHOPT_LOCKFUNC, lf);
            share.SetOpt(CURLSHoption.CURLSHOPT_UNLOCKFUNC, ulf);
            share.SetOpt(CURLSHoption.CURLSHOPT_SHARE,
                CURLlockData.CURL_LOCK_DATA_COOKIE);
            share.SetOpt(CURLSHoption.CURLSHOPT_SHARE,
                CURLlockData.CURL_LOCK_DATA_DNS);

            EasyThread et1 = new EasyThread(args[0], share);
            EasyThread et2 = new EasyThread(args[1], share);
            Thread t1 = new Thread(new ThreadStart(et1.ThreadFunc));
            Thread t2 = new Thread(new ThreadStart(et2.ThreadFunc));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            share.Cleanup();
            Curl.GlobalCleanup();
        }
        catch(Exception ex) {
            Console.WriteLine(ex);
        }
    }

    public static void OnLock(CURLlockData data, CURLlockAccess access,
        Object extraData)
    {
        //Console.WriteLine("OnLock({0}, {1})", data, access);
        if (data == CURLlockData.CURL_LOCK_DATA_DNS)
            Monitor.Enter(dnsLock);
        else if (data == CURLlockData.CURL_LOCK_DATA_COOKIE)
            Monitor.Enter(cookieLock);
    }


    public static void OnUnlock(CURLlockData data, Object extraData)
    {
        //Console.WriteLine("OnUnlock({0})", data);
        if (data == CURLlockData.CURL_LOCK_DATA_DNS)
            Monitor.Exit(dnsLock);
        else if (data == CURLlockData.CURL_LOCK_DATA_COOKIE)
            Monitor.Exit(cookieLock);
    }
}
