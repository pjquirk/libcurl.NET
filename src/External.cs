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
 * $Id: External.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace SeasideResearch.LibCurlNet
{
	/// <summary>
	/// P/Invoke signatures.
	/// </summary>
	internal class External
	{
        // private members
        private const String m_libCurlBase = "libcurl.dll";
        private const String m_libCurlShim = "LibCurlShim.dll";

        // internal delegates from cURL
        internal delegate int CURL_WRITE_DELEGATE(IntPtr buf, int sz,
            int nmemb, IntPtr parm);
        internal delegate int CURL_READ_DELEGATE(IntPtr buf, int sz,
            int nmemb, IntPtr parm);
        internal delegate int CURL_PROGRESS_DELEGATE(IntPtr parm,
            double dlTotal, double dlNow, double ulTotal, double ulNow);
        internal delegate int CURL_DEBUG_DELEGATE(CURLINFOTYPE infoType,
            IntPtr msgBuf, int msgBufSize, IntPtr parm);
        internal delegate int CURL_HEADER_DELEGATE(IntPtr buf, int sz,
            int nmemb, IntPtr stream);
        internal delegate int CURL_SSL_CTX_DELEGATE(IntPtr ctx,
            IntPtr parm);
        internal delegate CURLIOERR CURL_IOCTL_DELEGATE(
            CURLIOCMD cmd, IntPtr parm);
        internal delegate void CURLSH_LOCK_DELEGATE(int data,
            int access, IntPtr userPtr);
        internal delegate void CURLSH_UNLOCK_DELEGATE(int data,
            IntPtr userPtr);

        // libcurl imports
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLcode curl_global_init(int flags);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_global_cleanup();
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl,
             CharSet=CharSet.Ansi)]
        internal static extern IntPtr curl_escape(String url, int length);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl,
             CharSet=CharSet.Ansi)]
        internal static extern IntPtr curl_unescape(String url, int length);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_free(IntPtr p);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_version();

        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_easy_init();
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_easy_cleanup(IntPtr pCurl);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLcode curl_easy_setopt(IntPtr pCurl,
            CURLoption opt, IntPtr parm);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl,
            EntryPoint="curl_easy_setopt")]
        internal static extern CURLcode curl_easy_setopt_64(IntPtr pCurl,
            CURLoption opt, long parm);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLcode curl_easy_perform(IntPtr pCurl);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_easy_duphandle(IntPtr pCurl);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_easy_strerror(CURLcode err);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLcode curl_easy_getinfo(IntPtr pCurl,
            CURLINFO info, ref IntPtr pInfo);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl,
            EntryPoint="curl_easy_getinfo")]
        internal static extern CURLcode curl_easy_getinfo_64(IntPtr pCurl,
            CURLINFO info, ref double dblVal);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_easy_reset(IntPtr pCurl);

        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_multi_init();
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLMcode curl_multi_cleanup(IntPtr pmulti);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLMcode curl_multi_add_handle(IntPtr pmulti, IntPtr peasy);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLMcode curl_multi_remove_handle(IntPtr pmulti, IntPtr peasy);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_multi_strerror(CURLMcode errorNum);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLMcode curl_multi_perform(IntPtr pmulti,
            ref int runningHandles);

        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_formfree(IntPtr pForm);

        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_share_init();
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLSHcode curl_share_cleanup(IntPtr pShare);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_share_strerror(CURLSHcode errorCode);
        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLSHcode curl_share_setopt(IntPtr pShare,
            CURLSHoption optCode, IntPtr option);

        [DllImport(m_libCurlBase, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_version_info(CURLversion ver);

        // libcurlshim imports
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_initialize();
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_cleanup();
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_shim_alloc_strings();
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl,
             CharSet=CharSet.Ansi)]
        internal static extern IntPtr curl_shim_add_string_to_slist(
            IntPtr pStrings, String str);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl,
             CharSet=CharSet.Ansi)]
        internal static extern IntPtr curl_shim_get_string_from_slist(
            IntPtr pSlist, ref IntPtr pStr);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl,
             CharSet=CharSet.Ansi)]
        internal static extern IntPtr curl_shim_add_string(IntPtr pStrings, String str);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_free_strings(IntPtr pStrings);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_install_delegates(IntPtr pCurl, IntPtr pThis,
            CURL_WRITE_DELEGATE pWrite, CURL_READ_DELEGATE pRead,
            CURL_PROGRESS_DELEGATE pProgress, CURL_DEBUG_DELEGATE pDebug,
            CURL_HEADER_DELEGATE pHeader, CURL_SSL_CTX_DELEGATE pCtx,
            CURL_IOCTL_DELEGATE pIoctl); 
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_cleanup_delegates(IntPtr pThis);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_get_file_time(int unixTime,
            ref int yy, ref int mm, ref int dd, ref int hh, ref int mn, ref int ss);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_free_slist(IntPtr p);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_shim_alloc_fd_sets();
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_free_fd_sets(IntPtr fdsets);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern CURLMcode curl_shim_multi_fdset(IntPtr multi,
            IntPtr fdsets, ref int maxFD);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_select(int maxFD, IntPtr fdsets,
            int timeoutMillis);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_shim_multi_info_read(IntPtr multi,
            ref int nMsgs);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_multi_info_free(IntPtr multiInfo);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_formadd(IntPtr[] ppForms,
            IntPtr[] pParams, int nParams);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_install_share_delegates(IntPtr pShare,
            IntPtr pThis, CURLSH_LOCK_DELEGATE pLock,
            CURLSH_UNLOCK_DELEGATE pUnlock);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern void curl_shim_cleanup_share_delegates(IntPtr pShare);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_get_version_int_value(IntPtr p, int offset);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_shim_get_version_char_ptr(IntPtr p, int offset);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern int curl_shim_get_number_of_protocols(IntPtr p, int offset);
        [DllImport(m_libCurlShim, CallingConvention=CallingConvention.Cdecl)]
        internal static extern IntPtr curl_shim_get_protocol_string(IntPtr p, int offset,
            int index);
    }
}
