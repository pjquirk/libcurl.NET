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
 * $Id: SSLContext.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;

namespace SeasideResearch.LibCurlNet
{
	/// <summary>
	/// An instance of this class is passed to the delegate
	/// <see cref="Easy.SSLContextFunction"/>, if it's implemented.
	/// Within that delegate, the code will have to make native calls to
	/// the <c>OpenSSL</c> library with the value returned from the
	/// <see cref="SSLContext.Context"/> property cast to an
	/// <c>SSL_CTX</c> pointer.
	/// </summary>
	public sealed class SSLContext
	{
        private IntPtr m_pvContext;

		internal SSLContext(IntPtr pvContext)
		{
            m_pvContext = pvContext;
		}

        /// <summary>
        /// Get the underlying OpenSSL context.
        /// </summary>
        public IntPtr Context
        {
            get
            {
                return m_pvContext;
            }
        }
	}
}
