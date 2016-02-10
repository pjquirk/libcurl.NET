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
 * $Id: Easy.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SeasideResearch.LibCurlNet
{
	/// <summary>
	/// Implements the <c>curl_easy_xxx</c> API.
	/// </summary>
	/// <remarks>
	/// This is the most important class in <c>libcurl.NET</c>. It wraps a
	/// <c>CURL*</c> handle and provides delegates through which callbacks
	/// (such as <c>CURLOPT_WRITEFUNCTION</c> and <c>CURLOPT_READFUNCTION</c>)
	/// are implemented.
	/// </remarks>
	public class Easy
	{
        // constants (used internally only)
        private const int CURLOPTTYPE_OBJECTPOINT   = 10000;
        private const int CURLOPTTYPE_FUNCTIONPOINT = 20000;
        private const int CURLOPTTYPE_OFF_T         = 30000;
        private const int CURLINFO_STRING           = 0x100000;
        private const int CURLINFO_LONG             = 0x200000;
        private const int CURLINFO_DOUBLE           = 0x300000;
        private const int CURLINFO_SLIST            = 0x400000;

        /// <summary>
        /// Called when cURL has data for the client.
        /// </summary>
        /// <remarks>
        /// For usage, see the example <c>EasyGet.cs</c>.
        /// Arguments passed to the delegate implementation include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>buf</term>
        ///     <description>Data cURL is providing to the client.</description>
        /// </item>
        /// <item>
        ///     <term>size</term>
        ///     <description>Size of a character, usually 1.</description>
        /// </item>
        /// <item>
        ///     <term>nmemb</term>
        ///     <description>Number of characters.</description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return the number of bytes (not
        /// characters) processed. Return 0 to abort the transfer.
        /// </remarks>
        public delegate int WriteFunction(byte[] buf, int size, int nmemb,
            Object extraData);

        /// <summary>
        /// Called when cURL wants to read data from the client.
        /// </summary>
        /// <remarks>
        /// For usage, see the sample <c>Upload.cs</c>.
        /// Arguments passed to the recipient include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>buf</term>
        ///     <description>Buffer into which your client should write data
        ///     for cURL.</description>
        /// </item>
        /// <item>
        ///     <term>size</term>
        ///     <description>Size of a character, usually 1.</description>
        /// </item>
        /// <item>
        ///     <term>nmemb</term>
        ///     <description>Number of characters.</description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return the number of bytes (not
        /// characters) written to <c>buf</c>. Return 0 to abort the transfer.
        /// </remarks>
        public delegate int ReadFunction(byte[] buf, int size, int nmemb,
            Object extraData);

        /// <summary>
        /// Called when cURL wants to report progress.
        /// </summary>
        /// <remarks>
        /// For usage, see the sample <c>Upload.cs</c>.
        /// Arguments passed to the recipient include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// <item>
        ///     <term>dlTotal</term>
        ///     <description>Number of bytes to download.</description>
        /// </item>
        /// <item>
        ///     <term>dlNow</term>
        ///     <description>Number of bytes downloaded so far.</description>
        /// </item>
        /// <item>
        ///     <term>ulTotal</term>
        ///     <description>Number of bytes to upload.</description>
        /// </item>
        /// <item>
        ///     <term>ulNow</term>
        ///     <description>Number of bytes uploaded so far.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return 0 to continue, or a non-zero
        /// value to abort the transfer.
        /// </remarks>
        public delegate int ProgressFunction(Object extraData, double dlTotal,
            double dlNow, double ulTotal, double ulNow);

        /// <summary>
        /// Called when cURL has debug information for the client.
        /// </summary>
        /// <remarks>
        /// For usage, see the sample <c>Upload.cs</c>.
        /// Arguments passed to the recipient include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>infoType</term>
        ///     <description>Type of debug information, see
        ///     <see cref="CURLINFOTYPE"/>.</description>
        /// </item>
        /// <item>
        ///     <term>message</term>
        ///     <description>Debug information as a string.</description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public delegate void DebugFunction(CURLINFOTYPE infoType,
            String message, Object extraData);

        /// <summary>
        /// Called when cURL has header data for the client.
        /// </summary>
        /// <remarks>
        /// For usage, see the sample <c>Headers.cs</c>.
        /// Arguments passed to the recipient include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>buf</term>
        ///     <description>Header data from cURL to the client.</description>
        /// </item>
        /// <item>
        ///     <term>size</term>
        ///     <description>Size of a character, in bytes.</description>
        /// </item>
        /// <item>
        ///     <term>nmemb</term>
        ///     <description>Number of characters.</description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return the number of bytes (not
        /// characters) processed. Usually this is <c>size * nmemb</c>.
        /// Return -1 to abort the transfer.
        /// </remarks>
        public delegate int HeaderFunction(byte[] buf, int size, int nmemb,
            Object extraData);

        /// <summary>
        /// Called when cURL wants to report an SSL event.
        /// </summary>
        /// <remarks>
        /// For usage, see the sample <c>SSLGet.cs</c>.
        /// Arguments passed to the recipient include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>ctx</term>
        ///     <description>
        ///     An <see cref="SSLContext"/> object that wraps an
        ///     OpenSSL <c>SSL_CTX</c> pointer.
        ///     </description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return a <see cref="CURLcode"/>,
        /// which should be <see cref="CURLcode.CURLE_OK"/> if everything
        /// is okay.
        /// </remarks>
        public delegate CURLcode SSLContextFunction(SSLContext ctx,
            Object extraData);

        /// <summary>
        /// Called when cURL needs for the client to perform an
        /// IOCTL operation. An example might be when an FTP
        /// upload requires rewinding of the input file to deal
        /// with a resend occasioned by an error.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <listheader>
        ///     <term>Argument</term>
        ///     <description>Description</description>
        /// </listheader>
        /// <item>
        ///     <term>cmd</term>
        ///     <description>
        ///     A <see cref="CURLIOCMD"/>; for now, only
        ///     <c>CURLIOCMD_RESTARTREAD</c> should be passed.
        ///     </description>
        /// </item>
        /// <item>
        ///     <term>extraData</term>
        ///     <description>Client-provided extra data; in the
        ///     case of an FTP upload, it might be a
        ///     <c>FileStream</c> object.</description>
        /// </item>
        /// </list>
        /// 
        /// Your implementation should return a <see cref="CURLIOERR"/>,
        /// which should be <see cref="CURLIOERR.CURLIOE_OK"/> if everything
        /// is okay.
        /// </remarks>
        public delegate CURLIOERR IoctlFunction(CURLIOCMD cmd,
            Object extraData);

        // private members
        private IntPtr             m_pCURL;
        private IntPtr             m_pMyStrings;
        private Object             m_privateData;
        private WriteFunction      m_pfWrite;
        private Object             m_writeData;
        private ReadFunction       m_pfRead;
        private Object             m_readData;
        private ProgressFunction   m_pfProgress;
        private Object             m_progressData;
        private DebugFunction      m_pfDebug;
        private Object             m_debugData;
        private HeaderFunction     m_pfHeader;
        private Object             m_headerData;
        private SSLContextFunction m_pfSSLContext;
        private Object             m_sslContextData;
        private IoctlFunction      m_pfIoctl;
        private Object             m_ioctlData;
        private GCHandle           m_hThis;
        private IntPtr             m_ptrThis;

        private External.CURL_WRITE_DELEGATE    m_pDelWrite;
        private External.CURL_READ_DELEGATE     m_pDelRead;
        private External.CURL_PROGRESS_DELEGATE m_pDelProg;
        private External.CURL_DEBUG_DELEGATE    m_pDelDebug;
        private External.CURL_HEADER_DELEGATE   m_pDelHeader;
        private External.CURL_SSL_CTX_DELEGATE  m_pDelSSLCtx;
        private External.CURL_IOCTL_DELEGATE    m_pDelIoctl;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This is thrown
        /// if <see cref="Curl"/> hasn't bee properly initialized.</exception>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
		public Easy()
		{
            Curl.EnsureCurl();
            m_pCURL = External.curl_easy_init();
            EnsureHandle();
            External.curl_easy_setopt(m_pCURL, CURLoption.CURLOPT_NOPROGRESS,
                IntPtr.Zero);
            m_pMyStrings = External.curl_shim_alloc_strings();
            m_pfWrite = null;
            m_privateData = null;
            m_writeData = null;
            m_pfRead = null;
            m_readData = null;
            m_pfProgress = null;
            m_progressData = null;
            m_pfDebug = null;
            m_debugData = null;
            m_pfHeader = null;
            m_headerData = null;
            m_pfSSLContext = null;
            m_sslContextData = null;
            m_pfIoctl = null;
            m_ioctlData = null;
            InstallDelegates();
        }

        private Easy(Easy from)
        {
            m_pCURL = External.curl_easy_duphandle(from.m_pCURL);
            EnsureHandle();
            m_pMyStrings     = External.curl_shim_alloc_strings();
            m_pfWrite        = null;
            m_privateData    = null;
            m_writeData      = null;
            m_pfRead         = null;
            m_readData       = null;
            m_pfProgress     = null;
            m_progressData   = null;
            m_pfDebug        = null;
            m_debugData      = null;
            m_pfHeader       = null;
            m_headerData     = null;
            m_pfSSLContext   = null;
            m_sslContextData = null;
            m_pfIoctl        = null;
            m_ioctlData      = null;
            InstallDelegates();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Easy() 
        {
            Dispose(false);
        }

        /// <summary>
        /// Cleanup unmanaged resources.
        /// </summary>
        public void Cleanup()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            lock(this) {
                // if (disposing) cleanup managed resources
                // cleanup unmanaged resources
                if (m_pCURL != IntPtr.Zero)
                {
                    External.curl_shim_cleanup_delegates(m_ptrThis);
                    External.curl_easy_cleanup(m_pCURL);
                    External.curl_shim_free_strings(m_pMyStrings);
                    m_hThis.Free();
                    m_pCURL = IntPtr.Zero;
                }
            }
        }

        private void EnsureHandle()
        {
            if (m_pCURL == IntPtr.Zero)
                throw new NullReferenceException("No internal easy handle");
        }

        internal IntPtr GetHandle() { EnsureHandle(); return m_pCURL; }
 
        /// <summary>
        /// Reset the internal cURL handle.
        /// </summary>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public void Reset()
        {
            EnsureHandle();
            External.curl_easy_reset(m_pCURL);
        }

        /// <summary>
        /// Set options for this object. See the <c>EasyGet</c> sample for
        /// basic usage.
        /// </summary>
        /// <param name="option">This should be a valid <see cref="CURLoption"/>.</param>
        /// <param name="parameter">This should be a parameter of a varying
        /// type based on the value of the <c>option</c> parameter.</param>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        /// <returns>A <see cref="CURLcode"/>, typically obtained from
        /// <c>cURL</c> internally, but sometimes a
        /// <see cref="CURLcode.CURLE_BAD_FUNCTION_ARGUMENT"/>
        /// will be returned if the type of value of <c>parameter</c> is invalid.
        /// </returns>
        public CURLcode SetOpt(CURLoption option, Object parameter)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;

            // numeric cases
            if ((int)option < CURLOPTTYPE_OBJECTPOINT)
            {
                int i = 0;
                if (option == CURLoption.CURLOPT_DNS_USE_GLOBAL_CACHE ||
                    option == CURLoption.CURLOPT_SOURCE_PORT)
                {
                    return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                }
                else if (option == CURLoption.CURLOPT_TIMEVALUE)
                {
                    // unboxing may throw class cast exception
                    DateTime d = (DateTime)parameter;
                    DateTime startTime = new DateTime(1970, 1, 1);
                    TimeSpan currTime = new TimeSpan(DateTime.Now.Ticks -
                        startTime.Ticks);
                    i = Convert.ToInt32(currTime.TotalSeconds);
                }
                else
                    i = Convert.ToInt32(parameter);
                retCode = External.curl_easy_setopt(m_pCURL,
                    option, (IntPtr)i);
            }

            // object cases: the majority
            else if ((int)option < CURLOPTTYPE_FUNCTIONPOINT)
            {
                switch(option)
                {
                    // various data items
                    case CURLoption.CURLOPT_PRIVATE:
                        m_privateData = parameter; break;
                    case CURLoption.CURLOPT_WRITEDATA:
                        m_writeData = parameter; break;
                    case CURLoption.CURLOPT_READDATA:
                        m_readData = parameter; break;
                    case CURLoption.CURLOPT_PROGRESSDATA:
                        m_progressData = parameter; break;
                    case CURLoption.CURLOPT_DEBUGDATA:
                        m_debugData = parameter; break;
                    case CURLoption.CURLOPT_HEADERDATA:
                        m_headerData = parameter; break;
                    case CURLoption.CURLOPT_SSL_CTX_DATA:
                        m_sslContextData = parameter; break;
                    case CURLoption.CURLOPT_IOCTLDATA:
                        m_ioctlData = parameter; break;

                    // items that can't be set externally or
                    // obsolete items
                    case CURLoption.CURLOPT_ERRORBUFFER:
                    case CURLoption.CURLOPT_STDERR:
                    case CURLoption.CURLOPT_SOURCE_HOST:
                    case CURLoption.CURLOPT_SOURCE_PATH:
                    case CURLoption.CURLOPT_PASV_HOST:
                        return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;

                    // singular case for share
                    case CURLoption.CURLOPT_SHARE:
                    {
                        Share share = parameter as Share;
                        if (share == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        retCode = External.curl_easy_setopt(m_pCURL,
                            option, share.GetHandle());
                        break;
                    }

                    // multipart HTTP post
                    case CURLoption.CURLOPT_HTTPPOST:
                    {
                        MultiPartForm mf = parameter as MultiPartForm;
                        if (mf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        retCode = External.curl_easy_setopt(m_pCURL,
                            option, mf.GetHandle());
                        break;
                    }

                    // items curl wants as a curl_slist
                    case CURLoption.CURLOPT_HTTPHEADER:
                    case CURLoption.CURLOPT_PREQUOTE:
                    case CURLoption.CURLOPT_QUOTE:
                    case CURLoption.CURLOPT_POSTQUOTE:
                    case CURLoption.CURLOPT_SOURCE_QUOTE:
                    case CURLoption.CURLOPT_TELNETOPTIONS:
                    case CURLoption.CURLOPT_HTTP200ALIASES:
                    {
                        Slist slist = parameter as Slist;
                        if (slist == null) {
                            retCode = External.curl_easy_setopt(m_pCURL,
                                option, IntPtr.Zero);
                        }
                        else {
                            retCode = External.curl_easy_setopt(m_pCURL,
                                option, slist.GetHandle());
                        }
                        break;
                    }

                    // string items
                    default:
                    {
                        string s = parameter as string;
                        if (s == null) {
                            retCode = External.curl_easy_setopt(m_pCURL,
                                option, IntPtr.Zero);
                        }
                        else {
                            IntPtr pCurlStr = External.curl_shim_add_string(
                                m_pMyStrings, s);
                            if (pCurlStr != IntPtr.Zero)
                                retCode = External.curl_easy_setopt(m_pCURL,
                                    option, pCurlStr);
                        }
                        break;
                    }
                }                        
            }

            // FUNCTIONPOINT args, for delegates
            else if ((int)option < CURLOPTTYPE_OFF_T)
            {
                switch(option)
                {
                    case CURLoption.CURLOPT_WRITEFUNCTION:
                    {
                        WriteFunction wf = parameter as WriteFunction;
                        if (wf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfWrite = wf;
                        break;
                    }

                    case CURLoption.CURLOPT_READFUNCTION:
                    {
                        ReadFunction rf = parameter as ReadFunction;
                        if (rf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfRead = rf;
                        break;
                    }

                    case CURLoption.CURLOPT_PROGRESSFUNCTION:
                    {
                        ProgressFunction pf = parameter as ProgressFunction;
                        if (pf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfProgress = pf;
                        break;
                    }

                    case CURLoption.CURLOPT_DEBUGFUNCTION:
                    {
                        DebugFunction pd = parameter as DebugFunction;
                        if (pd == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfDebug = pd;
                        break;
                    }

                    case CURLoption.CURLOPT_HEADERFUNCTION:
                    {
                        HeaderFunction hf = parameter as HeaderFunction;
                        if (hf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfHeader = hf;
                        break;
                    }

                    case CURLoption.CURLOPT_SSL_CTX_FUNCTION:
                    {
                        SSLContextFunction sf = parameter as SSLContextFunction;
                        if (sf == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfSSLContext = sf;
                        break;
                    }

                    case CURLoption.CURLOPT_IOCTLFUNCTION:
                    {
                        IoctlFunction iof = parameter as IoctlFunction;
                        if (iof == null)
                            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                        m_pfIoctl = iof;
                        break;
                    }

                    default:
                        return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
                }
            }

            // otherwise, it's one of those 64-bit off_t guys!
            else
            {
                Int64 i = Convert.ToInt64(parameter);
                retCode = External.curl_easy_setopt_64(m_pCURL,
                    option, i);
            }

            return retCode;
        }

        /// <summary>
        /// Perform a transfer.
        /// </summary>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_perform()</c>.
        /// </returns>
        public CURLcode Perform()
        {
            EnsureHandle();
            return (CURLcode)External.curl_easy_perform(m_pCURL);
        }

        /// <summary>
        /// Clone an Easy object.
        /// </summary>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        /// <returns>A cloned <c>Easy</c> object.</returns>
        public Easy DupHandle()
        {
            return new Easy(this);
        }

        /// <summary>
        /// Get a string description of an error code.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <returns>String description of the error code.</returns>
        public String StrError(CURLcode code)
        {
            return Marshal.PtrToStringAnsi(
                External.curl_easy_strerror(code));
        }

        /// <summary>
        /// Extract information from a cURL handle.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration.</param>
        /// <param name="objInfo">Reference to an object into which the
        /// value specified by <c>info</c> is written.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref Object objInfo)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;
            IntPtr ptr = IntPtr.Zero;

            if ((int)info < CURLINFO_STRING)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;

            // trickery for filetime
            if (info == CURLINFO.CURLINFO_FILETIME)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;

            // private data
            if (info == CURLINFO.CURLINFO_PRIVATE)
            {
                objInfo = m_privateData;
                return retCode;
            }
     
            // string case
            if ((int)info < CURLINFO_LONG)
            {
                retCode = External.curl_easy_getinfo(m_pCURL, info, ref ptr);
                if (retCode == CURLcode.CURLE_OK)
                    objInfo = (Object)Marshal.PtrToStringAnsi(ptr);
                return retCode;
            }

            // int or double: return problem
            return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
        }

        /// <summary>
        /// Extract <c>Slist</c> information from an <c>Easy</c> object.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration. In this case, it must
        /// specifically be one of the members that obtains an <c>Slist</c>.
        /// </param>
        /// <param name="slist">Reference to an <c>Slist</c> value.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref Slist slist)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;
            IntPtr ptr = IntPtr.Zero, ptrs = IntPtr.Zero;

            if ((int)info < CURLINFO_SLIST)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
            retCode = External.curl_easy_getinfo(m_pCURL, info, ref ptr);
            if (retCode != CURLcode.CURLE_OK)
                return retCode;
            slist = new Slist();
            while (ptr != IntPtr.Zero)
            {
                ptr = External.curl_shim_get_string_from_slist(
                    ptr, ref ptrs);
                slist.Append(Marshal.PtrToStringAnsi(ptrs));
            }
            return retCode;
        }

        /// <summary>
        /// Extract <c>string</c> information from an <c>Easy</c> object.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration. In this case, it must
        /// specifically be one of the members that obtains a <c>string</c>.
        /// </param>
        /// <param name="strVal">Reference to an <c>string</c> value.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref string strVal)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;
            IntPtr ptr = IntPtr.Zero;

            if ((int)info < CURLINFO_STRING || (int)info >= CURLINFO_LONG)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
            retCode = External.curl_easy_getinfo(m_pCURL, info, ref ptr);
            if (retCode == CURLcode.CURLE_OK)
                strVal = Marshal.PtrToStringAnsi(ptr);
            return retCode;
        }

        /// <summary>
        /// Extract <c>int</c> information from an <c>Easy</c> object.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration. In this case, it must
        /// specifically be one of the members that obtains a <c>double</c>.
        /// </param>
        /// <param name="dblVal">Reference to an <c>double</c> value.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref double dblVal)
        {
            EnsureHandle();

            // ensure it's an integral type
            if ((int)info < CURLINFO_DOUBLE)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
            
            return External.curl_easy_getinfo_64(m_pCURL, info, ref dblVal);
        }

        /// <summary>
        /// Extract <c>int</c> information from an <c>Easy</c> object.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration. In this case, it must
        /// specifically be one of the members that obtains an <c>int</c>.
        /// </param>
        /// <param name="intVal">Reference to an <c>int</c> value.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref int intVal)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;
            IntPtr ptr = IntPtr.Zero;

            // ensure it's an integral type
            if ((int)info < CURLINFO_LONG || (int)info >= CURLINFO_DOUBLE)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;
            
            retCode = External.curl_easy_getinfo(m_pCURL, info, ref ptr);
            if (retCode == CURLcode.CURLE_OK)
                intVal = (int)ptr;
            return retCode;
        }

        /// <summary>
        /// Extract <c>DateTime</c> information from an <c>Easy</c> object.
        /// </summary>
        /// <param name="info">One of the values in the
        /// <see cref="CURLINFO"/> enumeration. In this case, it must
        /// specifically be <see cref="CURLINFO.CURLINFO_FILETIME"/>.
        /// </param>
        /// <param name="dt">Reference to a <c>DateTime</c> value.</param>
        /// <returns>The <see cref="CURLcode"/> obtained from the internal
        /// call to <c>curl_easy_getinfo()</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>CURL*</c> handle wasn't created successfully.</exception>
        public CURLcode GetInfo(CURLINFO info, ref DateTime dt)
        {
            EnsureHandle();
            CURLcode retCode = CURLcode.CURLE_OK;
            IntPtr ptr = IntPtr.Zero;

            if (info != CURLINFO.CURLINFO_FILETIME)
                return CURLcode.CURLE_BAD_FUNCTION_ARGUMENT;

            retCode = External.curl_easy_getinfo(m_pCURL, info, ref ptr);
            if (retCode == CURLcode.CURLE_OK)
            {
                if ((int)ptr < 0)
                    dt = new DateTime(0);
                else
                {
                    int yy = 0, mm = 0, dd = 0, hh = 0, mn = 0, ss = 0;
                    External.curl_shim_get_file_time((int)ptr, ref yy,
                        ref mm, ref dd, ref hh, ref mn, ref ss);
                    dt = new DateTime(yy, mm, dd, hh, mn, ss);
                }
            }
            return retCode;
        }

        // install the fuctions that will be called from libcurlshim
        private void InstallDelegates()
        {
            m_pDelWrite = new External.CURL_WRITE_DELEGATE(WriteDelegate);
            m_pDelRead = new External.CURL_READ_DELEGATE(ReadDelegate);
            m_pDelProg = new External.CURL_PROGRESS_DELEGATE(ProgressDelegate);
            m_pDelDebug = new External.CURL_DEBUG_DELEGATE(DebugDelegate);
            m_pDelHeader = new External.CURL_HEADER_DELEGATE(HeaderDelegate);
            m_pDelSSLCtx = new External.CURL_SSL_CTX_DELEGATE(SSLCtxDelegate);
            m_pDelIoctl = new External.CURL_IOCTL_DELEGATE(IoctlDelegate);
            m_hThis = GCHandle.Alloc(this);
            m_ptrThis = (IntPtr)m_hThis;
            External.curl_shim_install_delegates(m_pCURL, m_ptrThis,
                m_pDelWrite, m_pDelRead, m_pDelProg,
                m_pDelDebug, m_pDelHeader, m_pDelSSLCtx, m_pDelIoctl);
        }

        // called by libcurlshim
        private static int WriteDelegate(IntPtr buf, int sz, int nmemb,
            IntPtr parm)
        {
            int bytes = sz * nmemb;
            byte[] b = new byte[bytes];
            for (int i = 0; i < bytes; i++)
                b[i] = Marshal.ReadByte(buf, i);
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return 0;
            if (easy.m_pfWrite == null)
                return bytes;   // keep going
            return easy.m_pfWrite(b, sz, nmemb, easy.m_writeData);
        }

        // called by libcurlshim
        private static int ReadDelegate(IntPtr buf, int sz, int nmemb,
            IntPtr parm)
        {
            int bytes = sz * nmemb;
            byte[] b = new byte[bytes];
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return 0;
            if (easy.m_pfRead == null)
                return 0;
            int nRead = easy.m_pfRead(b, sz, nmemb, easy.m_readData);
            if (nRead > 0)
            {
                for (int i = 0; i < nRead; i++)
                    Marshal.WriteByte(buf, i, b[i]);
            }
            return nRead;
        }

        // called by libcurlshim
        private static int ProgressDelegate(IntPtr parm, double dlTotal,
            double dlNow, double ulTotal, double ulNow)
        {
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return 0;
            if (easy.m_pfProgress == null)
                return 0;
            int nprog = easy.m_pfProgress(easy.m_progressData, dlTotal,
                dlNow, ulTotal, ulNow);
            return nprog;
        }

        // called by libcurlshim
        private static int DebugDelegate(CURLINFOTYPE infoType,
            IntPtr msgBuf, int msgBufSize, IntPtr parm)
        {
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return 0;
            if (easy.m_pfDebug == null)
                return 0;
            String message = Marshal.PtrToStringAnsi(msgBuf, msgBufSize);
            easy.m_pfDebug(infoType, message, easy.m_debugData);
            return 0;
        }

        // called by libcurlshim
        private static int HeaderDelegate(IntPtr buf, int sz, int nmemb,
            IntPtr parm)
        {
            int bytes = sz * nmemb;
            byte[] b = new byte[bytes];
            for (int i = 0; i < bytes; i++)
                b[i] = Marshal.ReadByte(buf, i);
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return 0;
            if (easy.m_pfHeader == null)
                return bytes;   // keep going
            return easy.m_pfHeader(b, sz, nmemb, easy.m_headerData);
        }

        // called by libcurlshim
        private static int SSLCtxDelegate(IntPtr ctx, IntPtr parm)
        {
            int ok = (int)CURLcode.CURLE_OK;
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            if (easy == null)
                return ok;
            if (easy.m_pfSSLContext == null)
                return ok;  // keep going
            SSLContext context = new SSLContext(ctx);
            return (int)easy.m_pfSSLContext(context, easy.m_sslContextData);
        }

        // called by libcurlshim
        private static CURLIOERR IoctlDelegate(CURLIOCMD cmd,
            IntPtr parm)
        {
            GCHandle gch = (GCHandle)parm;
            Easy easy = (Easy)gch.Target;
            // let's require all of these to be non-null
            if (easy == null || easy.m_pfIoctl == null ||
                easy.m_ioctlData == null)
            {
                return CURLIOERR.CURLIOE_UNKNOWNCMD;
            }
            return easy.m_pfIoctl(cmd, easy.m_ioctlData);
        }
	}
}
