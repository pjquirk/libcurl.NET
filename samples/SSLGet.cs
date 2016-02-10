// $Id: SSLGet.cs,v 1.1 2005/02/17 22:47:24 jeffreyphillips Exp $
// SSLGet.cs - demonstrate SSL get capability
// Compile with "csc /r:../bin/LibCurlNet.dll /out:../bin/SSLGet.exe SSLGet.cs"

// usage: SSLGet url, e.g. SSLGet https://sourceforge.net

using System;
using SeasideResearch.LibCurlNet;

class SSLGet
{
    public static void Main(String[] args)
    {
        try {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            Easy easy = new Easy();

            Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);

            Easy.SSLContextFunction sf = new Easy.SSLContextFunction(OnSSLContext);
            easy.SetOpt(CURLoption.CURLOPT_SSL_CTX_FUNCTION, sf);

            easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
            easy.SetOpt(CURLoption.CURLOPT_CAINFO, "ca-bundle.crt");

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

    public static CURLcode OnSSLContext(SSLContext ctx, Object extraData)
    {
        // To do anything useful with the SSLContext object, you'll need
        // to call the OpenSSL native methods on your own. So for this
        // demo, we just return what cURL is expecting.
        return CURLcode.CURLE_OK;
    }
}
