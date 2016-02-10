// $Id: EasyGet.cs,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
// EasyGet.cs - demonstrate trivial get capability
// Compile with "csc /r:../bin/LibCurlNet.dll /out:../bin/EasyGet.exe EasyGet.cs"

// usage: EasyGet url, e.g. EasyGet http://www.google.com

using System;
using SeasideResearch.LibCurlNet;

class EasyGet
{
    public static void Main(String[] args)
    {
        try {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            Easy easy = new Easy();
            Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);

            easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
            easy.Perform();
            easy.Cleanup();

            Curl.GlobalCleanup();
        }
        catch(Exception ex) {
            Console.WriteLine(ex);
        }
    }

    public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb,
        Object extraData)
    {
        Console.Write(System.Text.Encoding.UTF8.GetString(buf));
        return size * nmemb;
    }
}

