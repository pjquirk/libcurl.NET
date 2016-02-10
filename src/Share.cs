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
 * $Id: Share.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SeasideResearch.LibCurlNet
{
	/// <summary>
	/// This class provides an infrastructure for serializing access to data
	/// shared by multiple <see cref="Easy"/> objects, including cookie data
	/// and DNS hosts. It implements the <c>curl_share_xxx</c> API.
	/// </summary>
	public class Share
	{        
        /// <summary>
        /// Called when <c>cURL</c> wants to lock a shared resource.
        /// </summary>
        /// <remarks>
        /// For a usage example, refer to the <c>ShareDemo.cs</c> sample.
        /// Arguments passed to your delegate implementation include:
        /// <list type="table">
        /// <listheader>
        /// <term>Argument</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item>
        /// <term>data</term>
        /// <term>
        /// Type of data to lock; one of the values in the
        /// <see cref="CURLlockData"/> enumeration.
        /// </term>
        /// </item>
        /// <item>
        /// <term>access</term>
        /// <term>
        /// Lock access requested; one of the values in the
        /// <see cref="CURLlockAccess"/> enumeration.
        /// </term>
        /// </item>
        /// <item>
        /// <term>userData</term>
        /// <term>
        /// Client-provided data that is not touched internally by
        /// <c>cURL</c>. This is set via
        /// <see cref="CURLSHoption.CURLSHOPT_USERDATA"/> when calling the
        /// <see cref="Share.SetOpt"/> member of the <see cref="Share"/>
        /// class.
        /// </term>
        /// </item>
        /// </list> 
        /// </remarks>
        public delegate void LockFunction(CURLlockData data,
            CURLlockAccess access, Object userData);

        /// <summary>
        /// Called when <c>cURL</c> wants to unlock a shared resource.
        /// </summary>
        /// <remarks>
        /// For a usage example, refer to the <c>ShareDemo.cs</c> sample.
        /// Arguments passed to your delegate implementation include:
        /// <list type="table">
        /// <listheader>
        /// <term>Argument</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item>
        /// <term>data</term>
        /// <term>
        /// Type of data to unlock; one of the values in the
        /// <see cref="CURLlockData"/> enumeration.
        /// </term>
        /// </item>
        /// <item>
        /// <term>userData</term>
        /// <term>
        /// Client-provided data that is not touched internally by
        /// <c>cURL</c>. This is set via
        /// <see cref="CURLSHoption.CURLSHOPT_USERDATA"/> when calling the
        /// <see cref="Share.SetOpt"/> member of the <see cref="Share"/>
        /// class.
        /// </term>
        /// </item>
        /// </list> 
        /// </remarks>
        public delegate void UnlockFunction(CURLlockData data,
            Object userData);

        // private members
        private IntPtr         m_pShare;       // share handle
        private GCHandle       m_hThis;        // for handle extraction
        private IntPtr         m_ptrThis;      // numeric handle
        private LockFunction   m_pfLock;       // client lock delegate
        private UnlockFunction m_pfUnlock;     // client unlock delegate
        private Object         m_userData;     // user data for delegates
        private External.CURLSH_LOCK_DELEGATE    m_pDelLock;     // lock delegate
        private External.CURLSH_UNLOCK_DELEGATE  m_pDelUnlock;   // unlock delegate

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This is thrown
        /// if <see cref="Curl"/> hasn't bee properly initialized.</exception>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>share</c> handle wasn't created successfully.</exception>
        public Share()
		{
            Curl.EnsureCurl();
            m_pShare = External.curl_share_init();
            EnsureHandle();
            m_pfLock = null;
            m_pfUnlock = null;
            m_userData = null;
            InstallDelegates();
		}

        /// <summary>
        /// Destructor
        /// </summary>
        ~Share()
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

        /// <summary>
        /// Set options for this object.
        /// </summary>
        /// <param name="option">
        /// One of the values in the <see cref="CURLSHoption"/>
        /// enumeration.
        /// </param>
        /// <param name="parameter">
        /// An appropriate object based on the value passed in the
        /// <c>option</c> argument. See <see cref="CURLSHoption"/>
        /// for more information about the appropriate parameter type.
        /// </param>
        /// <returns>
        /// A <see cref="CURLSHcode"/>, hopefully
        /// <c>CURLSHcode.CURLSHE_OK</c>.
        /// </returns>
        /// <exception cref="System.NullReferenceException">This is thrown if
        /// the native <c>share</c> handle wasn't created successfully.</exception>
        public CURLSHcode SetOpt(CURLSHoption option, Object parameter)
        {
            EnsureHandle();
            CURLSHcode retCode = CURLSHcode.CURLSHE_OK;

            switch(option)
            {
                case CURLSHoption.CURLSHOPT_LOCKFUNC:
                    LockFunction lf = parameter as LockFunction;
                    if (lf == null)
                        return CURLSHcode.CURLSHE_BAD_OPTION;
                    m_pfLock = lf;
                    break;

                case CURLSHoption.CURLSHOPT_UNLOCKFUNC:
                    UnlockFunction ulf = parameter as UnlockFunction;
                    if (ulf == null)
                        return CURLSHcode.CURLSHE_BAD_OPTION;
                    m_pfUnlock = ulf;
                    break;

                case CURLSHoption.CURLSHOPT_SHARE:
                case CURLSHoption.CURLSHOPT_UNSHARE:
                {
                    CURLlockData opt = (CURLlockData)
                        Convert.ToInt32(parameter);
                    if ((opt != CURLlockData.CURL_LOCK_DATA_COOKIE) &&
                        (opt != CURLlockData.CURL_LOCK_DATA_DNS))
                        return CURLSHcode.CURLSHE_BAD_OPTION;
                    retCode = External.curl_share_setopt(m_pShare,
                        option, (IntPtr)opt);
                    break;
                }

                case CURLSHoption.CURLSHOPT_USERDATA:
                    m_userData = parameter;
                    break;

                default:
                    retCode = CURLSHcode.CURLSHE_BAD_OPTION;
                    break;
            }
            return retCode;
        }

        /// <summary>
        /// Return a String description of an error code.
        /// </summary>
        /// <param name="errorNum">
        /// The <see cref="CURLSHcode"/> for which to obtain the error
        /// string description.
        /// </param>
        /// <returns>The string description.</returns>
        public String StrError(CURLSHcode errorNum)
        {
            return Marshal.PtrToStringAnsi(External.curl_share_strerror(
                errorNum));
        }

        private void Dispose(bool disposing)
        {
            lock(this) {
                // if (disposing) cleanup managed objects
                if (m_pShare != IntPtr.Zero)
                {
                    External.curl_shim_cleanup_share_delegates(m_pShare);
                    External.curl_share_cleanup(m_pShare);
                    m_hThis.Free();
                    m_ptrThis = IntPtr.Zero;
                    m_pShare = IntPtr.Zero;
                }
            }
        }

        internal IntPtr GetHandle() { return m_pShare; }

        private void EnsureHandle()
        {
            if (m_pShare == IntPtr.Zero)
                throw new NullReferenceException("No internal share handle");
        }

        private void InstallDelegates()
        {
            m_pDelLock = new External.CURLSH_LOCK_DELEGATE(LockDelegate);
            m_pDelUnlock = new External.CURLSH_UNLOCK_DELEGATE(UnlockDelegate);
            m_hThis = GCHandle.Alloc(this);
            m_ptrThis = (IntPtr)m_hThis;
            External.curl_shim_install_share_delegates(m_pShare,
                m_ptrThis, m_pDelLock, m_pDelUnlock);
        }

        internal static void LockDelegate(int data, int access,
            IntPtr userPtr)
        {
            GCHandle gch = (GCHandle)userPtr;
            Share share = (Share)gch.Target;
            if (share == null)
                return;
            if (share.m_pfLock == null)
                return;
            share.m_pfLock((CURLlockData)data, (CURLlockAccess)access,
                share.m_userData);
        }

        internal static void UnlockDelegate(int data, IntPtr userPtr)
        {
            GCHandle gch = (GCHandle)userPtr;
            Share share = (Share)gch.Target;
            if (share == null)
                return;
            if (share.m_pfUnlock == null)
                return;
            share.m_pfUnlock((CURLlockData)data, share.m_userData);
        }
    }
}
