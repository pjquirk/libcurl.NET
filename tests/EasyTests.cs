// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EasyTests.cs">
//   Copyright (c) 2016 Patrick Quirk
// 
//   This software is licensed as described in the file LICENSE.md, which you should have received as part 
//   of this distribution.
// 
//   You may opt to use, copy, modify, merge, publish, distribute and/or sell copies of this Software, and 
//   permit persons to whom the Software is furnished to do so, under the terms of the LICENSE.md file.
// 
//   This software is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express 
//   or implied.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LibCurlNet.Tests
{
    using NUnit.Framework;
    using SeasideResearch.LibCurlNet;

    [TestFixture]
    public class EasyTests
    {
        [Test]
        public void Perform_WhenHeaderFunctionIsSet_RetrievesStringContainingContentType()
        {
            var dataRecorder = new EasyDataRecorder();

            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_DEFAULT);
            try
            {
                using (Easy easy = new Easy())
                {
                    easy.SetOpt(CURLoption.CURLOPT_HEADERFUNCTION, (Easy.HeaderFunction)dataRecorder.HandleHeader);
                    easy.SetOpt(CURLoption.CURLOPT_HEADER, true);
                    easy.SetOpt(CURLoption.CURLOPT_URL, "http://httpbin.org/get");
                    easy.Perform();
                }
            }
            finally
            {
                Curl.GlobalCleanup();
            }

            Assert.That(dataRecorder.HeaderAsString, Does.Contain("Content-Type: application/json\r\n"));
        }

        [Test]
        public void Perform_WhenHeaderFunctionIsSet_RetrievesStringStartingWithVersionAndEndingWithDelimiter()
        {
            var dataRecorder = new EasyDataRecorder();

            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_DEFAULT);
            try
            {
                using (Easy easy = new Easy())
                {
                    easy.SetOpt(CURLoption.CURLOPT_HEADERFUNCTION, (Easy.HeaderFunction)dataRecorder.HandleHeader);
                    easy.SetOpt(CURLoption.CURLOPT_HEADER, true);
                    easy.SetOpt(CURLoption.CURLOPT_URL, "http://httpbin.org/get");
                    easy.Perform();
                }
            }
            finally
            {
                Curl.GlobalCleanup();
            }

            Assert.That(dataRecorder.HeaderAsString, Does.StartWith("HTTP/1.1 200 OK"));
            Assert.That(dataRecorder.HeaderAsString, Does.EndWith("\r\n\r\n"));
        }

        [Test]
        public void Perform_WhenWriteFunctionIsSet_RetrievesExpectedBytes()
        {
            var dataRecorder = new EasyDataRecorder();

            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_DEFAULT);
            try
            {
                using (Easy easy = new Easy())
                {
                    easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, (Easy.WriteFunction)dataRecorder.HandleWrite);
                    easy.SetOpt(CURLoption.CURLOPT_URL, "http://httpbin.org/bytes/16?seed=12345");
                    easy.Perform();
                }
            }
            finally
            {
                Curl.GlobalCleanup();
            }

            byte[] expectedBytes = { 0x6A, 0x02, 0xD3, 0x4C, 0x5E, 0x31, 0x90, 0x29, 0x1F, 0x6E, 0x8F, 0x2C, 0x8D, 0x5A, 0xF5, 0x17 };
            Assert.That(dataRecorder.Written, Is.EqualTo(expectedBytes));
        }
    }
}