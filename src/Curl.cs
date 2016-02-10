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
 * $Id: Curl.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace SeasideResearch.LibCurlNet
{
    /// <summary>
    /// Top-level class for initialization and cleanup.
    /// </summary>
    /// <remarks>
    /// It also implements static methods for capabilities that don't
    /// logically belong in a class.
    /// </remarks>
    public sealed class Curl
    {
        // for state management
        private static CURLcode sm_curlCode;

        /// <summary>
        /// Process-wide initialization -- call only once per process.
        /// </summary>
        /// <param name="flags">An or'd combination of
        /// <see cref="CURLinitFlag"/> members.</param>
        /// <returns>A <see cref="CURLcode"/>, hopefully
        /// <c>CURLcode.CURLE_OK</c>.</returns>
        public static CURLcode GlobalInit(int flags)
        {
            sm_curlCode = External.curl_global_init(flags);
            if (sm_curlCode == CURLcode.CURLE_OK)
                External.curl_shim_initialize();
            return sm_curlCode;
        }

        /// <summary>
        /// Process-wide cleanup -- call just before exiting process.
        /// </summary>
        /// <remarks>
        /// While it's not necessary that your program call this method
        /// before exiting, doing so will prevent leaks of native cURL resources.
        /// </remarks>
        public static void GlobalCleanup()
        {           
            if (sm_curlCode == CURLcode.CURLE_OK)
            {
                External.curl_shim_cleanup();
                External.curl_global_cleanup();
                sm_curlCode = CURLcode.CURLE_FAILED_INIT;
            }
        }

        /// <summary>
        /// URL encode a String.
        /// </summary>
        /// <param name="url">The string to URL encode.</param>
        /// <param name="length">Input string length;
        /// use 0 for cURL to determine.</param>
        /// <returns>A new URL encoded string.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if cURL isn't properly initialized.</exception>
        public static string Escape(string url, int length)
        {
            EnsureCurl();
            IntPtr p = External.curl_escape(url, length);
            String s = Marshal.PtrToStringAnsi(p);
            External.curl_free(p);
            return s;
        }

        /// <summary>
        /// URL decode a String.
        /// </summary>
        /// <param name="url">The string to URL decode.</param>
        /// <param name="length">Input string length;
        /// use 0 for cURL to determine.</param>
        /// <returns>A new URL decoded string.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if cURL isn't properly initialized.</exception>
        public static string Unescape(string url, int length)
        {
            EnsureCurl();
            IntPtr p = External.curl_unescape(url, length);
            String s = Marshal.PtrToStringAnsi(p);
            External.curl_free(p);
            return s;
        }

        /// <summary>
        /// Get the underlying cURL version as a string, example "7.12.2".
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if cURL isn't properly initialized.</exception>
        public static string Version
        {
            get
            {
                EnsureCurl();
                return Marshal.PtrToStringAnsi(External.curl_version());
            }
        }

        /// <summary>
        /// Get a <see cref="VersionInfoData"/> object.
        /// </summary>
        /// <param name="ver">Specify a <see cref="CURLversion"/>, such as
        /// <c>CURLversion.CURLVERSION_NOW</c>.</param>
        /// <returns>A <see cref="VersionInfoData"/> object.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if cURL isn't properly initialized.</exception>
        public static VersionInfoData GetVersionInfo(CURLversion ver)
        {
            EnsureCurl();
            return new VersionInfoData(ver);
        }

        /// <summary>
        /// Called by other classes to ensure valid cURL state.
        /// </summary>
        internal static void EnsureCurl()
        {
            if (sm_curlCode != CURLcode.CURLE_OK)
                throw new InvalidOperationException("cURL not initialized");
        }

        /// <summary>
        /// Class constructor - initialize global status.
        /// </summary>
        static Curl()
        {
            sm_curlCode = CURLcode.CURLE_FAILED_INIT;       
        }

        // hidden instance stuff
        private Curl() {}
        private new bool Equals(object obj) { return false; }
        private new int GetHashCode() { return 0; }
        private new Type GetType() { return null; }
        private new string ToString() { return null; }
    }
}
