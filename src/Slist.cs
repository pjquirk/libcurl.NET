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
 * $Id: Slist.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace SeasideResearch.LibCurlNet
{
    /// <summary>
	/// This class wraps a linked list of strings used in <c>cURL</c>. Use it
	/// to build string lists where they're required, such as when calling
	/// <see cref="Easy.SetOpt"/> with <see cref="CURLoption.CURLOPT_QUOTE"/>
	/// as the option.
	/// </summary>
	public class Slist
	{
        private IntPtr m_pStringList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This is thrown
        /// if <see cref="Curl"/> hasn't bee properly initialized.</exception>
        public Slist()
		{
            Curl.EnsureCurl();
            m_pStringList = IntPtr.Zero;
		}

        /// <summary>
        /// Destructor
        /// </summary>
        ~Slist()
        {
            Dispose(false);
        }

        /// <summary>
        /// Append a string to the list.
        /// </summary>
        /// <param name="str">The <c>string</c> to append.</param>
        public void Append(string str)
        {
            m_pStringList = External.curl_shim_add_string_to_slist(
                m_pStringList, str);
        }

        /// <summary>
        /// Free all internal strings.
        /// </summary>
        public void FreeAll()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        internal IntPtr GetHandle() { return m_pStringList; }

        private void Dispose(bool disposing)
        {
            lock(this) {
                // no if (disposing) pattern to clean up managed objects
                if (m_pStringList != IntPtr.Zero)
                {
                    External.curl_shim_free_slist(m_pStringList);
                    m_pStringList = IntPtr.Zero;
                }
            }
        }
    }
}
