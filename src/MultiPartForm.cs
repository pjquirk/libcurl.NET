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
 * $Id: MultiPartForm.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SeasideResearch.LibCurlNet
{
    /// <summary>
    /// This trivial class wraps the internal <c>curl_forms</c> struct.
    /// </summary>
    public sealed class CurlForms
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CurlForms() {}
        /// <summary>The <see cref="CURLformoption"/>.</summary>
        public CURLformoption opt;
        /// <summary>Value for the option.</summary>
        public Object         val;
    }

	/// <summary>
	/// Wraps a section of multipart form data to be submitted via the
	/// <see cref="CURLoption.CURLOPT_HTTPPOST"/> option in the
	/// <see cref="Easy.SetOpt"/> member of the <see cref="Easy"/> class.
	/// </summary>
	public class MultiPartForm
	{        
        // the two curlform pointers
        private IntPtr[] m_pItems;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This is thrown
        /// if <see cref="Curl"/> hasn't bee properly initialized.</exception>
        public MultiPartForm()
		{
            Curl.EnsureCurl();
            m_pItems = new IntPtr[2];
            m_pItems[0] = IntPtr.Zero;
            m_pItems[1] = IntPtr.Zero;
		}

        /// <summary>
        /// Destructor
        /// </summary>
        ~MultiPartForm()
        {
            Dispose(false);
        }

        // for Easy.SetOpt()
        internal IntPtr GetHandle() { return m_pItems[0]; }

        /// <summary>
        /// Add a multi-part form section.
        /// </summary>
        /// <param name="args">
        /// Argument list, as described in the remarks.
        /// </param>
        /// <returns>
        /// A <see cref="CURLFORMcode"/>, hopefully
        /// <c>CURLFORMcode.CURL_FORMADD_OK</c>.
        /// </returns>
        /// <remarks>
        /// This is definitely the workhorse method for this class. It
        /// should be called in roughly the same manner as
        /// <c>curl_formadd()</c>, except you would omit the first two
        /// <c>struct curl_httppost**</c> arguments (<c>firstitem</c> and
        /// <c>lastitem</c>), which are wrapped in this class. So you should
        /// pass arguments in the following sequence:
        /// <para>
        /// <c>MultiPartForm.AddSection(option1, value1, ..., optionX, valueX,
        /// CURLformoption.CURLFORM_END)</c>;
        /// </para>
        /// <para>
        /// For a complete list of possible options, see the documentation for
        /// the <see cref="CURLformoption"/> enumeration.
        /// </para>
        /// <note>
        /// The pointer options (<c>CURLFORM_PTRNAME</c>, etc.) make an
        /// internal copy of the passed <c>byte</c> array. Therefore, any
        /// changes you make to the client copy of this array AFTER calling
        /// this method, won't be reflected internally with <c>cURL</c>. The
        /// purpose of providing the pointer options is to support the
        /// posting of non-string binary data.
        /// </note>
        /// </remarks>
        public CURLFORMcode AddSection(params object[] args)
        {
            int nCount = args.Length;
            int nRealCount = nCount;
            CURLFORMcode retCode = CURLFORMcode.CURL_FORMADD_OK;
            CurlForms[] aForms = null;

            // one arg or even number of args is an error
            if ((nCount == 1) || (nCount % 2 == 0))
                return CURLFORMcode.CURL_FORMADD_INCOMPLETE;

            // ensure the last argument is CURLFORM_END
            CURLformoption iCode = (CURLformoption)
                Convert.ToInt32(args.GetValue(nCount - 1));
            if (iCode != CURLformoption.CURLFORM_END)
                return CURLFORMcode.CURL_FORMADD_INCOMPLETE;

            // walk through any passed arrays to get the true number of
            // items and ensure the child arrays are properly (and not
            // prematurely) terminated with CURLFORM_END
            for (int i = 0; i < nCount; i += 2)
            {
                iCode = (CURLformoption)Convert.ToInt32(args.GetValue(i));
                switch(iCode)
                {
                    case CURLformoption.CURLFORM_ARRAY:
                    {
                        aForms = args.GetValue(i + 1) as CurlForms[];
                        if (aForms == null)
                            return CURLFORMcode.CURL_FORMADD_INCOMPLETE;
                        int nFormsCount = aForms.Length;
                        for (int j = 0; j < nFormsCount; j++) 
                        {
                            CurlForms pcf = aForms.GetValue(j) as CurlForms;
                            if (pcf == null)
                                return CURLFORMcode.CURL_FORMADD_INCOMPLETE;
                            if (j == nFormsCount - 1)
                            {
                                if (pcf.opt != CURLformoption.CURLFORM_END)
                                    return CURLFORMcode.CURL_FORMADD_INCOMPLETE;
                            }
                            else
                            {
                                if (pcf.opt == CURLformoption.CURLFORM_END)
                                    return CURLFORMcode.CURL_FORMADD_INCOMPLETE;
                            }
                        }
                        // -2 accounts for the fact that we're a) not
                        // including the item with CURLFORM_END and b) not
                        // including CURLFORM_ARRAY in what we pass to cURL
                        nRealCount += 2 * (nFormsCount - 2);
                        break;
                    }

                    default:
                        break;
                }
            }

            // allocate the IntPtr array for the data
            IntPtr[] aPointers = new IntPtr[nRealCount];
            for (int i = 0; i < nRealCount - 1; i++)
                aPointers[i] = IntPtr.Zero;
            aPointers[nRealCount - 1] = (IntPtr)CURLformoption.CURLFORM_END;

            // now we go through the args
            aForms = null;
            int formArrayPos = 0;
            int argArrayPos = 0;
            int ptrArrayPos = 0;
            Object obj = null;

            while ((retCode == CURLFORMcode.CURL_FORMADD_OK) &&
                (ptrArrayPos < nRealCount))
            {
                if (aForms != null)
                {
                    CurlForms pcf = aForms.GetValue(formArrayPos++)
                        as CurlForms;
                    if (pcf == null)
                    {
                        retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        break;
                    }
                    iCode = pcf.opt;
                    obj = pcf.val;
                }
                else
                {
                    iCode = (CURLformoption)Convert.ToInt32(
                        args.GetValue(argArrayPos++));
                    obj = (iCode == CURLformoption.CURLFORM_END) ? null :
                        args.GetValue(argArrayPos++);
                }

                switch(iCode)
                {
                    // handle byte-array pointer-related items
                    case CURLformoption.CURLFORM_PTRNAME:
                    case CURLformoption.CURLFORM_PTRCONTENTS:
                    case CURLformoption.CURLFORM_BUFFERPTR:
                    {
                        byte[] bytes = obj as byte[];
                        if (bytes == null)
                            retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        else
                        {
                            int nLen = bytes.Length;
                            IntPtr ptr = Marshal.AllocHGlobal(nLen);
                            if (ptr != IntPtr.Zero)
                            {
                                aPointers[ptrArrayPos++] = (IntPtr)iCode;
                                // copy bytes to unmanaged buffer
                                for (int j = 0; j < nLen; j++)
                                    Marshal.WriteByte(ptr, bytes[j]);
                                aPointers[ptrArrayPos++] = ptr;
                            }
                            else
                                retCode = CURLFORMcode.CURL_FORMADD_MEMORY;
                        }
                        break;
                    }

                    // length values
                    case CURLformoption.CURLFORM_NAMELENGTH:
                    case CURLformoption.CURLFORM_CONTENTSLENGTH:
                    case CURLformoption.CURLFORM_BUFFERLENGTH:
                        aPointers[ptrArrayPos++] = (IntPtr)iCode;
                        aPointers[ptrArrayPos++] = (IntPtr)
                            Convert.ToInt32(obj);
                        break;

                    // strings
                    case CURLformoption.CURLFORM_COPYNAME:
                    case CURLformoption.CURLFORM_COPYCONTENTS:
                    case CURLformoption.CURLFORM_FILECONTENT:
                    case CURLformoption.CURLFORM_FILE:
                    case CURLformoption.CURLFORM_CONTENTTYPE:
                    case CURLformoption.CURLFORM_FILENAME:
                    case CURLformoption.CURLFORM_BUFFER:
                    {
                        aPointers[ptrArrayPos++] = (IntPtr)iCode;
                        string s = obj as String;
                        if (s == null)
                            retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        else
                        {
                            IntPtr p = Marshal.StringToHGlobalAnsi(s);
                            if (p != IntPtr.Zero)
                                aPointers[ptrArrayPos++] = p;
                            else
                                retCode = CURLFORMcode.CURL_FORMADD_MEMORY;
                        }
                        break;
                    }

                    // array case: already handled
                    case CURLformoption.CURLFORM_ARRAY:
                        if (aForms != null)
                            retCode = CURLFORMcode.CURL_FORMADD_ILLEGAL_ARRAY;
                        else
                        {
                            aForms = obj as CurlForms[];
                            if (aForms == null)
                                retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        }
                        break;

                    // slist
                    case CURLformoption.CURLFORM_CONTENTHEADER:
                    {
                        aPointers[ptrArrayPos++] = (IntPtr)iCode;
                        Slist s = obj as Slist;
                        if (s == null)
                            retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        else
                            aPointers[ptrArrayPos++] = s.GetHandle();
                        break;
                    }

                    // erroneous stuff
                    case CURLformoption.CURLFORM_NOTHING:
                        retCode = CURLFORMcode.CURL_FORMADD_INCOMPLETE;
                        break;

                    // end
                    case CURLformoption.CURLFORM_END:
                        if (aForms != null) // end of form
                        {
                            aForms = null;
                            formArrayPos = 0;
                        }
                        else
                            aPointers[ptrArrayPos++] = (IntPtr)iCode;
                        break;

                    // default is unknown
                    default:
                        retCode = CURLFORMcode.CURL_FORMADD_UNKNOWN_OPTION;
                        break;
                }
            }

            // ensure we didn't come up short on parameters
            if (ptrArrayPos != nRealCount)
                retCode = CURLFORMcode.CURL_FORMADD_INCOMPLETE;

            // if we're OK here, call into curl
            if (retCode == CURLFORMcode.CURL_FORMADD_OK)
            {
                retCode = (CURLFORMcode)External.curl_shim_formadd(
                    m_pItems, aPointers, nRealCount);
            }
       
            // unmarshal native allocations
            for (int i = 0; i < nRealCount - 1; i += 2)
            {
                iCode = (CURLformoption)(int)aPointers[i];
                switch(iCode)
                {
                    case CURLformoption.CURLFORM_COPYNAME:
                    case CURLformoption.CURLFORM_COPYCONTENTS:
                    case CURLformoption.CURLFORM_FILECONTENT:
                    case CURLformoption.CURLFORM_FILE:
                    case CURLformoption.CURLFORM_CONTENTTYPE:
                    case CURLformoption.CURLFORM_FILENAME:
                    case CURLformoption.CURLFORM_BUFFER:
                    // byte buffer cases
                    case CURLformoption.CURLFORM_PTRNAME:
                    case CURLformoption.CURLFORM_PTRCONTENTS:
                    case CURLformoption.CURLFORM_BUFFERPTR:
                    {
                        if (aPointers[i + 1] != IntPtr.Zero)
                            Marshal.FreeHGlobal(aPointers[i + 1]);
                        break;
                    }

                    default:
                        break;
                }
            }

            return retCode;
        }

        /// <summary>
        /// Free unmanaged resources.
        /// </summary>
        public void Free()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            lock(this) {
                // no if (disposing) pattern to clean up managed objects
                if (m_pItems[0] != IntPtr.Zero)
                    External.curl_formfree(m_pItems[0]);
                m_pItems[0] = IntPtr.Zero;
                m_pItems[1] = IntPtr.Zero;
            }
        }
	}
}
