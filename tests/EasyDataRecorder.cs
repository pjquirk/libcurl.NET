// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EasyDataRecorder.cs">
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
    using System.Collections.Generic;
    using System.Text;

    internal class EasyDataRecorder
    {
        readonly List<byte> header = new List<byte>();
        readonly List<byte> written = new List<byte>();

        public IList<byte> Header => header;

        public string HeaderAsString => Header == null ? null : Encoding.UTF8.GetString(header.ToArray());

        public IList<byte> Written => written;

        public string WrittenAsString => Written == null ? null : Encoding.UTF8.GetString(written.ToArray());

        public int HandleHeader(byte[] buf, int size, int nmemb, object extradata)
        {
            int totalBytes = size * nmemb;
            byte[] temp = new byte[totalBytes];
            buf.CopyTo(temp, 0);
            header.AddRange(temp);
            return totalBytes;
        }

        public int HandleWrite(byte[] buf, int size, int nmemb, object extradata)
        {
            int totalBytes = size * nmemb;
            byte[] temp = new byte[totalBytes];
            buf.CopyTo(temp, 0);
            written.AddRange(temp);
            return totalBytes;
        }
    }
}