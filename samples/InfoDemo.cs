// $Id: InfoDemo.cs,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
// InfoDemo.cs - demonstrate Easy.GetInfo
// Compile with "csc /r:../bin/LibCurlNet.dll /out:../bin/InfoDemo.exe InfoDemo.cs"

// usage: InfoDemo url, e.g. InfoDemo http://www.google.com

using System;
using SeasideResearch.LibCurlNet;

class InfoDemo
{
    public static void Main(String[] args)
    {
        try {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            Easy easy = new Easy();
            easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
            easy.SetOpt(CURLoption.CURLOPT_PRIVATE, "Private string");
            easy.SetOpt(CURLoption.CURLOPT_FILETIME, true);
            easy.Perform();

            // now, exercise the various GetInfo stuff
            double d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_CONNECT_TIME, ref d);
            Console.WriteLine("Connect Time: {0}", d);

            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_LENGTH_DOWNLOAD, ref d);
            Console.WriteLine("Content Length (Download): {0}", d);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_LENGTH_UPLOAD, ref d);
            Console.WriteLine("Content Length (Upload): {0}", d);
    
            string s = null;
            easy.GetInfo(CURLINFO.CURLINFO_CONTENT_TYPE, ref s);
            Console.WriteLine("Content Type: {0}", s);
    
            DateTime dt = new DateTime(0);
            easy.GetInfo(CURLINFO.CURLINFO_FILETIME, ref dt);
            Console.WriteLine("File time: {0}", dt);
    
            int n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_HEADER_SIZE, ref n);
            Console.WriteLine("Header Size: {0}", n);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_HTTPAUTH_AVAIL, ref n);
            Console.WriteLine("Authentication Bitmask: {0}", n);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_HTTP_CONNECTCODE, ref n);
            Console.WriteLine("HTTP Connect Code: {0}", n);
    
            d = 0.0;            
            easy.GetInfo(CURLINFO.CURLINFO_NAMELOOKUP_TIME, ref d);
            Console.WriteLine("Name Lookup Time: {0}", d);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_OS_ERRNO, ref n);
            Console.WriteLine("OS Errno: {0}", n);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_PRETRANSFER_TIME, ref d);
            Console.WriteLine("Pretransfer time: {0}", d);
    
            object o = null;
            easy.GetInfo(CURLINFO.CURLINFO_PRIVATE, ref o);
            Console.WriteLine("Private Data: {0}", o);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_PROXYAUTH_AVAIL, ref n);
            Console.WriteLine("Proxy Authentication Schemes: {0}", n);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_REDIRECT_COUNT, ref n);
            Console.WriteLine("Redirect count: {0}", n);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_REDIRECT_TIME, ref d);
            Console.WriteLine("Redirect time: {0}", d);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_REQUEST_SIZE, ref n);
            Console.WriteLine("Request size: {0}", n);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_RESPONSE_CODE, ref n);
            Console.WriteLine("Response code: {0}", n);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_SIZE_DOWNLOAD, ref d);
            Console.WriteLine("Download size: {0}", d);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_SIZE_UPLOAD, ref d);
            Console.WriteLine("Upload size: {0}", d);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_SPEED_DOWNLOAD, ref d);
            Console.WriteLine("Download speed: {0}", d);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_SPEED_UPLOAD, ref d);
            Console.WriteLine("Upload speed: {0}", d);
    
            n = 0;
            easy.GetInfo(CURLINFO.CURLINFO_SSL_VERIFYRESULT, ref n);
            Console.WriteLine("SSL verification result: {0}", n);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_STARTTRANSFER_TIME, ref d);
            Console.WriteLine("Start transfer time: {0}", d);
    
            d = 0.0;
            easy.GetInfo(CURLINFO.CURLINFO_TOTAL_TIME, ref d);
            Console.WriteLine("Total time: {0}", d);
    
            easy.Cleanup();
            Curl.GlobalCleanup();
        }
        catch(Exception ex) {
            Console.WriteLine(ex);
        }
    }

}
