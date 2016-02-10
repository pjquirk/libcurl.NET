// $Id: Headers.cs,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
// Headers.cs - dump headers
// Compile with "csc /r:../bin//LibCurlNet.dll /out:../bin/Headers.exe Headers.cs"

// usage: Headers url, e.g. Headers http://www.google.com

using System;
using SeasideResearch.LibCurlNet;

class Headers
{
    public static void Main(String[] args)
    {
        try {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            Easy easy = new Easy();
            Easy.HeaderFunction hf = new Easy.HeaderFunction(OnHeaderData);

            easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
            easy.SetOpt(CURLoption.CURLOPT_HEADERFUNCTION, hf);
            easy.Perform();
            easy.Cleanup();

            Curl.GlobalCleanup();
        }
        catch(Exception ex) {
            Console.WriteLine(ex);
        }
    }

    public static Int32 OnHeaderData(Byte[] buf, Int32 size, Int32 nmemb,
        Object extraData)
    {
        Console.Write(System.Text.Encoding.UTF8.GetString(buf));
        return size * nmemb;
    }
}
