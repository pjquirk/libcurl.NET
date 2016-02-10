/***************************************************************************
 *
 * Project: libcurl.NET
 *
 * Copyright (c) 2004, 2005 Jeff Phillips (jeff@jeffp.net)
 *
 * This software is licensed as described in the file COPYING, which you
 * should have received as part of this distribution.
 *
 * You may opt to use, copy, modify, merge, publish, distribute and/or sell
 * copies of this Software, and permit persons to whom the Software is
 * furnished to do so, under the terms of the COPYING file.
 *
 * This software is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
 * ANY KIND, either express or implied.
 *
 * $Id: MultiInfo.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;

namespace SeasideResearch.LibCurlNet
{
	/// <summary>
	/// Wraps the <c>cURL</c> struct <c>CURLMsg</c>. This class provides
	/// status information following a <see cref="Multi"/> transfer.
	/// </summary>
	public sealed class MultiInfo
	{
        // private members
        CURLMSG  m_msg;
        Easy     m_easy;
        CURLcode m_result;

		internal MultiInfo(CURLMSG msg, Easy easy, CURLcode result)
		{
            m_msg = msg;
            m_easy = easy;
            m_result = result;
		}

        /// <summary>
        /// Get the status code from the <see cref="CURLMSG"/> enumeration.
        /// </summary>
        public CURLMSG Msg
        {
            get
            {
                return m_msg;
            }
        }

        /// <summary>
        /// Get the <see cref="Easy"/> object for this child.
        /// </summary>
        public Easy EasyHandle
        {
            get
            {
                return m_easy;
            }
        }

        /// <summary>
        /// Get the return code for the transfer, as a
        /// <see cref="CURLcode"/>.
        /// </summary>
        public CURLcode Result
        {
            get
            {
                return m_result;
            }
        }
    }
}
