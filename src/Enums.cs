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
 * $Id: Enums.cs,v 1.1 2005/02/17 22:47:25 jeffreyphillips Exp $
 **************************************************************************/

using System;

namespace SeasideResearch.LibCurlNet
{
    /// <summary>
    /// Your handler for the <see cref="Easy.IoctlFunction"/> delegate
    /// should return a member of this enumeration.
    /// </summary>
    public enum CURLIOERR
    {
        /// <summary>
        /// Indicate that the callback processed everything okay.
        /// </summary>
        CURLIOE_OK          = 0,
        /// <summary>
        /// Unknown command sent to callback. Right now, only
        /// <code>CURLIOCMD_RESTARTREAD</code> is supported.
        /// </summary>
        CURLIOE_UNKNOWNCMD  = 1,
        /// <summary>
        /// Indicate to libcurl that a restart failed.
        /// </summary>
        CURLIOE_FAILRESTART = 2,
        /// <summary>
        /// End of enumeration marker, don't use in a client application.
        /// </summary>
        CURLIOE_LAST        = 3
    }

    /// <summary>
    /// Your handler for the <see cref="Easy.IoctlFunction"/>
    /// delegate is passed one of these values as its first parameter.
    /// Right now, the only supported value is
    /// <code>CURLIOCMD_RESTARTREAD</code>.
    /// </summary>
    public enum CURLIOCMD
    {
        /// <summary>
        /// No IOCTL operation; we should never see this.
        /// </summary>
        CURLIOCMD_NOP           = 0,
        /// <summary>
        /// When this is sent, your callback may need to, for example,
        /// rewind a local file that is being sent via FTP.
        /// </summary>
        CURLIOCMD_RESTARTREAD   = 1,
        /// <summary>
        /// End of enumeration marker, don't use in a client application.
        /// </summary>
        CURLIOCMD_LAST          = 2
    }

    /// <summary>
    /// A member of this enumeration is passed as the first parameter to the
    /// <see cref="Easy.DebugFunction"/> delegate to which libcurl passes
    /// debug messages.
    /// </summary>
    public enum CURLINFOTYPE
    {
        /// <summary>
        /// The data is informational text.
        /// </summary>
        CURLINFO_TEXT         = 0,
        /// <summary>
        /// The data is header (or header-like) data received from the peer.
        /// </summary>
        CURLINFO_HEADER_IN    = 1,
        /// <summary>
        /// The data is header (or header-like) data sent to the peer.
        /// </summary>
        CURLINFO_HEADER_OUT   = 2,
        /// <summary>
        /// The data is protocol data received from the peer.
        /// </summary>
        CURLINFO_DATA_IN      = 3,
        /// <summary>
        /// The data is protocol data sent to the peer.
        /// </summary>
        CURLINFO_DATA_OUT     = 4,
        /// <summary>
        /// The data is SSL-related data sent to the peer.
        /// </summary>
        CURLINFO_SSL_DATA_IN  = 5,
        /// <summary>
        /// The data is SSL-related data received from the peer.
        /// </summary>
        CURLINFO_SSL_DATA_OUT = 6,
        /// <summary>
        /// End of enumeration marker, don't use in a client application.
        /// </summary>
        CURLINFO_END          = 7
    };

    /// <summary>
    /// Status code returned from <see cref="Easy"/> functions.
    /// </summary>
    public enum CURLcode
    {
        /// <summary>
        /// All fine. Proceed as usual.
        /// </summary>
        CURLE_OK                          = 0,
        /// <summary>
        /// Aborted by callback. An internal callback returned "abort"
        /// to libcurl. 
        /// </summary>
        CURLE_ABORTED_BY_CALLBACK         = 42,
        /// <summary>
        /// Internal error. A function was called in a bad order.
        /// </summary>
        CURLE_BAD_CALLING_ORDER           = 44,
        /// <summary>
        /// Unrecognized transfer encoding.
        /// </summary>
        CURLE_BAD_CONTENT_ENCODING        = 61,
        /// <summary>
        /// Attempting FTP resume beyond file size.
        /// </summary>
        CURLE_BAD_DOWNLOAD_RESUME         = 36,
        /// <summary>
        /// Internal error. A function was called with a bad parameter.
        /// </summary>
        CURLE_BAD_FUNCTION_ARGUMENT       = 43,
        /// <summary>
        /// Bad password entered. An error was signaled when the password was
        /// entered. This can also be the result of a "bad password" returned
        /// from a specified password callback. 
        /// </summary>
        CURLE_BAD_PASSWORD_ENTERED        = 46,
        /// <summary>
        /// Failed to connect to host or proxy. 
        /// </summary>
        CURLE_COULDNT_CONNECT             = 7,
        /// <summary>
        /// Couldn't resolve host. The given remote host was not resolved. 
        /// </summary>
        CURLE_COULDNT_RESOLVE_HOST        = 6,
        /// <summary>
        /// Couldn't resolve proxy. The given proxy host could not be resolved.
        /// </summary>
        CURLE_COULDNT_RESOLVE_PROXY       = 5,
        /// <summary>
        /// Very early initialization code failed. This is likely to be an
        /// internal error or problem. 
        /// </summary>
        CURLE_FAILED_INIT                 = 2,
        /// <summary>
        /// Maximum file size exceeded.
        /// </summary>
        CURLE_FILESIZE_EXCEEDED           = 63,
        /// <summary>
        /// A file given with FILE:// couldn't be opened. Most likely
        /// because the file path doesn't identify an existing file. Did
        /// you check file permissions? 
        /// </summary>
        CURLE_FILE_COULDNT_READ_FILE      = 37,
        /// <summary>
        /// We were denied access when trying to login to an FTP server or
        /// when trying to change working directory to the one given in the URL. 
        /// </summary>
        CURLE_FTP_ACCESS_DENIED           = 9,
        /// <summary>
        /// An internal failure to lookup the host used for the new
        /// connection.
        /// </summary>
        CURLE_FTP_CANT_GET_HOST           = 15,
        /// <summary>
        /// A bad return code on either PASV or EPSV was sent by the FTP
        /// server, preventing libcurl from being able to continue. 
        /// </summary>
        CURLE_FTP_CANT_RECONNECT          = 16,
        /// <summary>
        /// The FTP SIZE command returned error. SIZE is not a kosher FTP
        /// command, it is an extension and not all servers support it. This
        /// is not a surprising error. 
        /// </summary>
        CURLE_FTP_COULDNT_GET_SIZE        = 32,
        /// <summary>
        /// This was either a weird reply to a 'RETR' command or a zero byte
        /// transfer complete. 
        /// </summary>
        CURLE_FTP_COULDNT_RETR_FILE       = 19,
        /// <summary>
        /// libcurl failed to set ASCII transfer type (TYPE A).
        /// </summary>
        CURLE_FTP_COULDNT_SET_ASCII       = 29,
        /// <summary>
        /// Received an error when trying to set the transfer mode to binary.
        /// </summary>
        CURLE_FTP_COULDNT_SET_BINARY      = 17,
        /// <summary>
        /// FTP couldn't STOR file. The server denied the STOR operation.
        /// The error buffer usually contains the server's explanation to this. 
        /// </summary>
        CURLE_FTP_COULDNT_STOR_FILE       = 25,
        /// <summary>
        /// The FTP REST command returned error. This should never happen
        /// if the server is sane. 
        /// </summary>
        CURLE_FTP_COULDNT_USE_REST        = 31,
        /// <summary>
        /// The FTP PORT command returned error. This mostly happen when
        /// you haven't specified a good enough address for libcurl to use.
        /// See <see cref="CURLoption.CURLOPT_FTPPORT"/>. 
        /// </summary>
        CURLE_FTP_PORT_FAILED             = 30,
        /// <summary>
        /// When sending custom "QUOTE" commands to the remote server, one
        /// of the commands returned an error code that was 400 or higher. 
        /// </summary>
        CURLE_FTP_QUOTE_ERROR             = 21,
        /// <summary>
        /// Requested FTP SSL level failed.
        /// </summary>
        CURLE_FTP_SSL_FAILED              = 64,
        /// <summary>
        /// The FTP server rejected access to the server after the password
        /// was sent to it. It might be because the username and/or the
        /// password were incorrect or just that the server is not allowing
        /// you access for the moment etc. 
        /// </summary>
        CURLE_FTP_USER_PASSWORD_INCORRECT = 10,
        /// <summary>
        /// FTP servers return a 227-line as a response to a PASV command.
        /// If libcurl fails to parse that line, this return code is
        /// passed back. 
        /// </summary>
        CURLE_FTP_WEIRD_227_FORMAT        = 14,
        /// <summary>
        /// After having sent the FTP password to the server, libcurl expects
        /// a proper reply. This error code indicates that an unexpected code
        /// was returned. 
        /// </summary>
        CURLE_FTP_WEIRD_PASS_REPLY        = 11,
        /// <summary>
        /// libcurl failed to get a sensible result back from the server as
        /// a response to either a PASV or a EPSV command. The server is flawed. 
        /// </summary>
        CURLE_FTP_WEIRD_PASV_REPLY        = 13,
        /// <summary>
        /// After connecting to an FTP server, libcurl expects to get a
        /// certain reply back. This error code implies that it got a strange
        /// or bad reply. The given remote server is probably not an
        /// OK FTP server. 
        /// </summary>
        CURLE_FTP_WEIRD_SERVER_REPLY      = 8,
        /// <summary>
        /// After having sent user name to the FTP server, libcurl expects a
        /// proper reply. This error code indicates that an unexpected code
        /// was returned. 
        /// </summary>
        CURLE_FTP_WEIRD_USER_REPLY        = 12,
        /// <summary>
        /// After a completed file transfer, the FTP server did not respond a
        /// proper "transfer successful" code. 
        /// </summary>
        CURLE_FTP_WRITE_ERROR             = 20,
        /// <summary>
        /// Function not found. A required LDAP function was not found.
        /// </summary>
        CURLE_FUNCTION_NOT_FOUND          = 41,
        /// <summary>
        /// Nothing was returned from the server, and under the circumstances,
        /// getting nothing is considered an error.
        /// </summary>
        CURLE_GOT_NOTHING                 = 52,
        /// <summary>
        /// This is an odd error that mainly occurs due to internal confusion.
        /// </summary>
        CURLE_HTTP_POST_ERROR             = 34,
        /// <summary>
        /// The HTTP server does not support or accept range requests.
        /// </summary>
        CURLE_HTTP_RANGE_ERROR            = 33,
        /// <summary>
        /// This is returned if <see cref="CURLoption.CURLOPT_FAILONERROR"/>
        /// is set TRUE and the HTTP server returns an error code that
        /// is >= 400. 
        /// </summary>
        CURLE_HTTP_RETURNED_ERROR         = 22,
        /// <summary>
        /// Interface error. A specified outgoing interface could not be
        /// used. Set which interface to use for outgoing connections'
        /// source IP address with <see cref="CURLoption.CURLOPT_INTERFACE"/>. 
        /// </summary>
        CURLE_INTERFACE_FAILED            = 45,
        /// <summary>
        /// End-of-enumeration marker; do not use in client applications.
        /// </summary>
        CURLE_LAST                        = 67,
        /// <summary>
        /// LDAP cannot bind. LDAP bind operation failed.
        /// </summary>
        CURLE_LDAP_CANNOT_BIND            = 38,
        /// <summary>
        /// Invalid LDAP URL.
        /// </summary>
        CURLE_LDAP_INVALID_URL            = 62,
        /// <summary>
        /// LDAP search failed.
        /// </summary>
        CURLE_LDAP_SEARCH_FAILED          = 39,
        /// <summary>
        /// Library not found. The LDAP library was not found.
        /// </summary>
        CURLE_LIBRARY_NOT_FOUND           = 40,
        /// <summary>
        /// Malformat user. User name badly specified. *Not currently used*
        /// </summary>
        CURLE_MALFORMAT_USER              = 24,
        /// <summary>
        /// This is not an error. This used to be another error code in an
        /// old libcurl version and is currently unused. 
        /// </summary>
        CURLE_OBSOLETE                    = 50,
        /// <summary>
        /// Operation timeout. The specified time-out period was reached
        /// according to the conditions. 
        /// </summary>
        CURLE_OPERATION_TIMEOUTED         = 28,
        /// <summary>
        /// Out of memory. A memory allocation request failed. This is serious
        /// badness and things are severely messed up if this ever occurs. 
        /// </summary>
        CURLE_OUT_OF_MEMORY               = 27,
        /// <summary>
        /// A file transfer was shorter or larger than expected. This
        /// happens when the server first reports an expected transfer size,
        /// and then delivers data that doesn't match the previously
        /// given size. 
        /// </summary>
        CURLE_PARTIAL_FILE                = 18,
        /// <summary>
        /// There was a problem reading a local file or an error returned by
        /// the read callback. 
        /// </summary>
        CURLE_READ_ERROR                  = 26,
        /// <summary>
        /// Failure with receiving network data.
        /// </summary>
        CURLE_RECV_ERROR                  = 56,
        /// <summary>
        /// Failed sending network data.
        /// </summary>
        CURLE_SEND_ERROR                  = 55,
        /// <summary>
        /// Sending the data requires a rewind that failed.
        /// </summary>
        CURL_SEND_FAIL_REWIND             = 65,
        /// <summary>
        /// Share is in use.
        /// </summary>
        CURLE_SHARE_IN_USE                = 57,
        /// <summary>
        /// Problem with the CA cert (path? access rights?) 
        /// </summary>
        CURLE_SSL_CACERT                  = 60,
        /// <summary>
        /// There's a problem with the local client certificate. 
        /// </summary>
        CURLE_SSL_CERTPROBLEM             = 58,
        /// <summary>
        /// Couldn't use specified cipher. 
        /// </summary>
        CURLE_SSL_CIPHER                  = 59,
        /// <summary>
        /// A problem occurred somewhere in the SSL/TLS handshake. You really
        /// want to use the <see cref="Easy.DebugFunction"/> delegate and read
        /// the message there as it pinpoints the problem slightly more. It
        /// could be certificates (file formats, paths, permissions),
        /// passwords, and others. 
        /// </summary>
        CURLE_SSL_CONNECT_ERROR           = 35,
        /// <summary>
        /// Failed to initialize SSL engine.
        /// </summary>
        CURLE_SSL_ENGINE_INITFAILED       = 66,
        /// <summary>
        /// The specified crypto engine wasn't found. 
        /// </summary>
        CURLE_SSL_ENGINE_NOTFOUND         = 53,
        /// <summary>
        /// Failed setting the selected SSL crypto engine as default!
        /// </summary>
        CURLE_SSL_ENGINE_SETFAILED        = 54,
        /// <summary>
        /// The remote server's SSL certificate was deemed not OK.
        /// </summary>
        CURLE_SSL_PEER_CERTIFICATE        = 51,
        /// <summary>
        /// A telnet option string was improperly formatted.
        /// </summary>
        CURLE_TELNET_OPTION_SYNTAX        = 49,
        /// <summary>
        /// Too many redirects. When following redirects, libcurl hit the
        /// maximum amount. Set your limit with
        /// <see cref="CURLoption.CURLOPT_MAXREDIRS"/>. 
        /// </summary>
        CURLE_TOO_MANY_REDIRECTS          = 47,
        /// <summary>
        /// An option set with <see cref="CURLoption.CURLOPT_TELNETOPTIONS"/>
        /// was not recognized/known. Refer to the appropriate documentation. 
        /// </summary>
        CURLE_UNKNOWN_TELNET_OPTION       = 48,
        /// <summary>
        /// The URL you passed to libcurl used a protocol that this libcurl
        /// does not support. The support might be a compile-time option that
        /// wasn't used, it can be a misspelled protocol string or just a
        /// protocol libcurl has no code for. 
        /// </summary>
        CURLE_UNSUPPORTED_PROTOCOL        = 1,
        /// <summary>
        /// The URL was not properly formatted. 
        /// </summary>
        CURLE_URL_MALFORMAT               = 3,
        /// <summary>
        /// URL user malformatted. The user-part of the URL syntax was not
        /// correct. 
        /// </summary>
        CURLE_URL_MALFORMAT_USER          = 4,
        /// <summary>
        /// An error occurred when writing received data to a local file,
        /// or an error was returned to libcurl from a write callback. 
        /// </summary>
        CURLE_WRITE_ERROR                 = 23,
    };

    /// <summary>
    /// This enumeration contains values used to specify the proxy type when
    /// using the <see cref="CURLoption.CURLOPT_PROXY"/> option when calling
    /// <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLproxyType
    {
        /// <summary>
        /// Ordinary HTTP proxy.
        /// </summary>
        CURLPROXY_HTTP   = 0,
        /// <summary>
        /// Use if the proxy supports SOCKS4 user authentication. If you're
        /// unfamiliar with this, consult your network administrator.
        /// </summary>
        CURLPROXY_SOCKS4 = 4,
        /// <summary>
        /// Use if the proxy supports SOCKS5 user authentication. If you're
        /// unfamiliar with this, consult your network administrator.
        /// </summary>
        CURLPROXY_SOCKS5 = 5
    };

    /// <summary>
    /// This enumeration contains values used to specify the HTTP authentication
    /// when using the <see cref="CURLoption.CURLOPT_HTTPAUTH"/> option when
    /// calling <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLhttpAuth
    {
        /// <summary>
        /// No authentication.
        /// </summary>
        CURLAUTH_NONE           = 0,
        /// <summary>
        /// HTTP Basic authentication. This is the default choice, and the
        /// only method that is in wide-spread use and supported virtually
        /// everywhere. This is sending the user name and password over the
        /// network in plain text, easily captured by others.
        /// </summary>
        CURLAUTH_BASIC          = 1,
        /// <summary>
        /// HTTP Digest authentication. Digest authentication is defined
        /// in RFC2617 and is a more secure way to do authentication over
        /// public networks than the regular old-fashioned Basic method.
        /// </summary>
        CURLAUTH_DIGEST         = 2,
        /// <summary>
        /// HTTP GSS-Negotiate authentication. The GSS-Negotiate (also known
        /// as plain "Negotiate") method was designed by Microsoft and is
        /// used in their web applications. It is primarily meant as a
        /// support for Kerberos5 authentication but may be also used along
        /// with another authentication methods. For more information see IETF
        /// draft draft-brezak-spnego-http-04.txt.
        /// <note>
        /// You need to use a version of libcurl.NET built with a suitable
        /// GSS-API library for this to work. This is not currently standard.
        /// </note>
        /// </summary>
        CURLAUTH_GSSNEGOTIATE   = 4,
        /// <summary>
        /// HTTP NTLM authentication. A proprietary protocol invented and
        /// used by Microsoft. It uses a challenge-response and hash concept
        /// similar to Digest, to prevent the password from being eavesdropped.
        /// </summary>
        CURLAUTH_NTLM           = 8,
        /// <summary>
        /// This is a convenience macro that sets all bits and thus makes
        /// libcurl pick any it finds suitable. libcurl will automatically
        /// select the one it finds most secure.
        /// </summary>
        CURLAUTH_ANY            = 15,   // ~0
        /// <summary>
        /// This is a convenience macro that sets all bits except Basic
        /// and thus makes libcurl pick any it finds suitable. libcurl
        /// will automatically select the one it finds most secure.
        /// </summary>
        CURLAUTH_ANYSAFE        = 14    // ~CURLAUTH_BASIC
    };

    /// <summary>
    /// This enumeration contains values used to specify the FTP SSL level
    /// using the <see cref="CURLoption.CURLOPT_FTP_SSL"/> option when calling
    /// <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLftpSSL
    {
        /// <summary>
        /// Don't attempt to use SSL.
        /// </summary>
        CURLFTPSSL_NONE     = 0,
        /// <summary>
        /// Try using SSL, proceed as normal otherwise.
        /// </summary>
        CURLFTPSSL_TRY      = 1,
        /// <summary>
        /// Require SSL for the control connection or fail with
        /// <see cref="CURLcode.CURLE_FTP_SSL_FAILED"/>. 
        /// </summary>
        CURLFTPSSL_CONTROL  = 2,
        /// <summary>
        /// Require SSL for all communication or fail with
        /// <see cref="CURLcode.CURLE_FTP_SSL_FAILED"/>.
        /// </summary>
        CURLFTPSSL_ALL      = 3,
        /// <summary>
        /// End-of-enumeration marker. Do not use in a client application.
        /// </summary>
        CURLFTPSSL_LAST     = 4
    };

    /// <summary>
    /// This enumeration contains values used to specify the FTP SSL
    /// authorization level using the
    /// <see cref="CURLoption.CURLOPT_FTPSSLAUTH"/> option when calling
    /// <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLftpAuth
    {
        /// <summary>
        /// Let <c>libcurl</c> decide on the authorization scheme.
        /// </summary>
        CURLFTPAUTH_DEFAULT     = 0,
        /// <summary>
        /// Use "AUTH SSL".
        /// </summary>
        CURLFTPAUTH_SSL         = 1,
        /// <summary>
        /// Use "AUTH TLS".
        /// </summary>
        CURLFTPAUTH_TLS         = 2,
        /// <summary>
        /// End-of-enumeration marker. Do not use in a client application.
        /// </summary>
        CURLFTPAUTH_LAST        = 3
    };

    /// <summary>
    /// One of these is passed as the first parameter to
    /// <see cref="Easy.SetOpt"/>. The <c>Description</c> column of
    /// the table describes the value that should be passed as the second parameter.
    /// </summary>
    public enum CURLoption
    {
        /// <summary>
        /// Pass a <c>true</c> parameter to enable this. When enabled, libcurl
        /// will automatically set the Referer: field in requests where it follows
        /// a Location: redirect. 
        /// </summary>
        CURLOPT_AUTOREFERER             = 58,
        /// <summary>
        /// Pass an <c>int</c> specifying your preferred size for the receive buffer
        /// in libcurl. The main point of this would be that the write callback gets
        /// called more often and with smaller chunks. This is just treated as a
        /// request, not an order. You cannot be guaranteed to actually get the
        /// requested size. (Added in 7.10) 
        /// </summary>
        CURLOPT_BUFFERSIZE              = 98,
        /// <summary>
        /// Pass a <c>string</c> naming a file holding one or more certificates
        /// to verify the peer with. This only makes sense when used in combination
        /// with the <c>CURLOPT_SSL_VERIFYPEER</c> option.
        /// </summary>
        CURLOPT_CAINFO                  = 10065,
        /// <summary>
        /// Pass a <c>string</c> naming a directory holding multiple CA certificates
        /// to verify the peer with. The certificate directory must be prepared
        /// using the openssl c_rehash utility. This only makes sense when used in
        /// combination with the <c>CURLOPT_SSL_VERIFYPEER</c> option. The
        /// <c>CURLOPT_CAPATH</c> function apparently does not work in Windows due
        /// to some limitation in openssl. (Added in 7.9.8) 
        /// </summary>
        CURLOPT_CAPATH                  = 10097,
        /// <summary>
        /// Pass an <c>int</c>. This option sets what policy libcurl should use when
        /// the connection cache is filled and one of the open connections has to be
        /// closed to make room for a new connection. This must be one of the
        /// <see cref="CURLclosePolicy"/> members. Use
        /// <see cref="CURLclosePolicy.CURLCLOSEPOLICY_LEAST_RECENTLY_USED"/> to make
        /// libcurl close the connection that was least recently used, that connection
        /// is also least likely to be capable of re-use. Use
        /// <see cref="CURLclosePolicy.CURLCLOSEPOLICY_OLDEST"/> to make libcurl close
        /// the oldest connection, the one that was created first among the ones in
        /// the connection cache. The other close policies are not supported yet.
        /// </summary>
        CURLOPT_CLOSEPOLICY             = 72,
        /// <summary>
        /// Time-out connect operations after this amount of seconds, if connects
        /// are OK within this time, then fine... This only aborts the connect
        /// phase. [Only works on unix-style/SIGALRM operating systems]
        /// </summary>
        CURLOPT_CONNECTTIMEOUT          = 78,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used to set a cookie
        /// in the http request. The format of the string should be NAME=CONTENTS,
        /// where NAME is the cookie name and CONTENTS is what the cookie should contain. 
        /// <para>
        /// If you need to set multiple cookies, you need to set them all using a
        /// single option and thus you need to concatenate them all in one single
        /// string. Set multiple cookies in one string like this:
        /// "name1=content1; name2=content2;" etc. 
        /// </para>
        /// <para>
        /// Using this option multiple times will only make the latest string override
        /// the previously ones.
        /// </para>
        /// </summary>
        CURLOPT_COOKIE                  = 10022,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It should contain the name of your
        /// file holding cookie data to read. The cookie data may be in Netscape /
        /// Mozilla cookie data format or just regular HTTP-style headers dumped
        /// to a file.
        /// <para>
        /// Given an empty or non-existing file, this option will enable cookies
        /// for this Easy object, making it understand and parse received cookies
        /// and then use matching cookies in future request. 
        /// </para> 
        /// </summary>
        CURLOPT_COOKIEFILE              = 10031,
        /// <summary>
        /// Pass a file name as <c>string</c>. This will make libcurl write all
        /// internally known cookies to the specified file when
        /// <see cref="Easy.Cleanup"/> is called. If no cookies are known, no file
        /// will be created. Using this option also enables cookies for this
        /// session, so if you for example follow a location it will make matching
        /// cookies get sent accordingly.
        /// <note>
        /// If the cookie jar file can't be created or written to
        /// (when <see cref="Easy.Cleanup"/> is called), libcurl will not and
        /// cannot report an error for this. Using <c>CURLOPT_VERBOSE</c> or
        /// <c>CURLOPT_DEBUGFUNCTION</c> will get a warning to display, but that
        /// is the only visible feedback you get about this possibly lethal situation.
        /// </note>
        /// </summary>
        CURLOPT_COOKIEJAR               = 10082,
        /// <summary>
        /// Pass a <c>bool</c> set to <c>true</c> to mark this as a new cookie
        /// "session". It will force libcurl to ignore all cookies it is about to
        /// load that are "session cookies" from the previous session. By default,
        /// libcurl always stores and loads all cookies, independent of whether they are
        /// session cookies. Session cookies are cookies without expiry date and they
        /// are meant to be alive and existing for this "session" only.
        /// </summary>
        CURLOPT_COOKIESESSION           = 96,
        /// <summary>
        /// Convert Unix newlines to CRLF newlines on transfers.
        /// </summary>
        CURLOPT_CRLF                    = 27,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used instead of GET or
        /// HEAD when doing an HTTP request, or instead of LIST or NLST when
        /// doing an ftp directory listing. This is useful for doing DELETE or
        /// other more or less obscure HTTP requests. Don't do this at will,
        /// make sure your server supports the command first. 
        /// <para>
        /// Restore to the internal default by setting this to <c>null</c>.
        /// </para>
        /// <note>
        /// Many people have wrongly used this option to replace the entire
        /// request with their own, including multiple headers and POST contents.
        /// While that might work in many cases, it will cause libcurl to send
        /// invalid requests and it could possibly confuse the remote server badly.
        /// Use <c>CURLOPT_POST</c> and <c>CURLOPT_POSTFIELDS</c> to set POST data.
        /// Use <c>CURLOPT_HTTPHEADER</c> to replace or extend the set of headers
        /// sent by libcurl. Use <c>CURLOPT_HTTP_VERSION</c> to change HTTP version.
        /// </note>
        /// </summary>
        CURLOPT_CUSTOMREQUEST           = 10036,
        /// <summary>
        /// Pass an <c>object</c> referene to whatever you want passed to your
        /// <see cref="Easy.DebugFunction"/> delegate's <c>extraData</c> argument.
        /// This reference is not used internally by libcurl, it is only passed to
        /// the delegate. 
        /// </summary>
        CURLOPT_DEBUGDATA               = 10095,
        /// <summary>
        /// Pass a reference to an <see cref="Easy.DebugFunction"/> delegate.
        /// <c>CURLOPT_VERBOSE</c> must be in effect. This delegate receives debug
        /// information, as specified with the <see cref="CURLINFOTYPE"/> argument.
        /// This function must return 0. 
        /// </summary>
        CURLOPT_DEBUGFUNCTION           = 20094,
        /// <summary>
        /// Pass an <c>int</c>, specifying the timeout in seconds. Name resolves
        /// will be kept in memory for this number of seconds. Set to zero (0)
        /// to completely disable caching, or set to -1 to make the cached
        /// entries remain forever. By default, libcurl caches this info for 60
        /// seconds.
        /// </summary>
        CURLOPT_DNS_CACHE_TIMEOUT       = 92,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_DNS_USE_GLOBAL_CACHE    = 91,
        /// <summary>
        /// Pass a <c>string</c> containing the path name to the Entropy Gathering
        /// Daemon socket. It will be used to seed the random engine for SSL.
        /// </summary>
        CURLOPT_EDGSOCKET               = 10077,
        /// <summary>
        /// Sets the contents of the Accept-Encoding: header sent in an HTTP request,
        /// and enables decoding of a response when a Content-Encoding: header is
        /// received. Three encodings are supported: <c>identity</c>, which does
        /// nothing, <c>deflate</c> which requests the server to compress its
        /// response using the zlib algorithm, and <c>gzip</c> which requests the
        /// gzip algorithm. If a zero-length string is set, then an Accept-Encoding:
        /// header containing all supported encodings is sent.
        /// </summary>
        CURLOPT_ENCODING                = 10102,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_ERRORBUFFER             = 10010,
        /// <summary>
        /// A <c>true</c> parameter tells the library to fail silently if the
        /// HTTP code returned is equal to or larger than 300. The default
        /// action would be to return the page normally, ignoring that code. 
        /// </summary>
        CURLOPT_FAILONERROR             = 45,
        /// <summary>
        /// Pass a <c>bool</c>. If it is <c>true</c>, libcurl will attempt to get
        /// the modification date of the remote document in this operation. This
        /// requires that the remote server sends the time or replies to a time
        /// querying command. The <see cref="Easy.GetInfo"/> function with the
        /// <see cref="CURLINFO.CURLINFO_FILETIME"/> argument can be used after a
        /// transfer to extract the received time (if any).
        /// </summary>
        CURLOPT_FILETIME                = 69,
        /// <summary>
        /// A <c>true</c> parameter tells the library to follow any Location:
        /// header that the server sends as part of an HTTP header.
        /// <note>
        /// this means that the library will re-send the same request on the
        /// new location and follow new Location: headers all the way until no
        /// more such headers are returned. <c>CURLOPT_MAXREDIRS</c> can be used
        /// to limit the number of redirects libcurl will follow.
        /// </note>
        /// </summary>
        CURLOPT_FOLLOWLOCATION          = 52,
        /// <summary>
        /// Pass a <c>bool</c>. Set to <c>true</c> to make the next transfer
        /// explicitly close the connection when done. Normally, libcurl keeps all
        /// connections alive when done with one transfer in case there comes a
        /// succeeding one that can re-use them. This option should be used with
        /// caution and only if you understand what it does. Set to <c>false</c>
        /// to have libcurl keep the connection open for possibly later re-use
        /// (default behavior). 
        /// </summary>
        CURLOPT_FORBID_REUSE            = 75,
        /// <summary>
        /// Pass a <c>bool</c>. Set to <c>true</c> to make the next transfer use a
        /// new (fresh) connection by force. If the connection cache is full before
        /// this connection, one of the existing connections will be closed as
        /// according to the selected or default policy. This option should be used
        /// with caution and only if you understand what it does. Set this to
        /// <c>false</c> to have libcurl attempt re-using an existing connection
        /// (default behavior). 
        /// </summary>
        CURLOPT_FRESH_CONNECT           = 74,
        /// <summary>
        /// String that will be passed to the FTP server when it requests
        /// account info.
        /// </summary>
        CURLOPT_FTPACCOUNT              = 10134,
        /// <summary>
        /// A <c>true</c> parameter tells the library to append to the remote
        /// file instead of overwrite it. This is only useful when uploading
        /// to an ftp site. 
        /// </summary>
        CURLOPT_FTPAPPEND               = 50,
        /// <summary>
        /// A <c>true</c> parameter tells the library to just list the names of
        /// an ftp directory, instead of doing a full directory listing that
        /// would include file sizes, dates etc. 
        /// <para>
        /// This causes an FTP NLST command to be sent. Beware that some FTP
        /// servers list only files in their response to NLST; they might not
        /// include subdirectories and symbolic links.
        /// </para>
        /// </summary>
        CURLOPT_FTPLISTONLY             = 48,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used to get the IP
        /// address to use for the ftp PORT instruction. The PORT instruction
        /// tells the remote server to connect to our specified IP address.
        /// The string may be a plain IP address, a host name, an network
        /// interface name (under Unix) or just a '-' letter to let the library
        /// use your systems default IP address. Default FTP operations are
        /// passive, and thus won't use PORT. 
        /// <para>
        /// You disable PORT again and go back to using the passive version
        /// by setting this option to NULL.
        /// </para>
        /// </summary>
        CURLOPT_FTPPORT                 = 10017,
        /// <summary>
        /// When FTP over SSL/TLS is selected (with <c>CURLOPT_FTP_SSL</c>),
        /// this option can be used to change libcurl's default action which
        /// is to first try "AUTH SSL" and then "AUTH TLS" in this order,
        /// and proceed when a OK response has been received.
        /// <para>
        /// Pass a member of the <see cref="CURLftpAuth"/> enumeration.
        /// </para>
        /// </summary>
        CURLOPT_FTPSSLAUTH              = 129,
        /// <summary>
        /// Pass a <c>bool</c>. If the value is <c>true</c>, cURL will attempt to
        /// create any remote directory that it fails to CWD into. CWD is the
        /// command that changes working directory. (Added in 7.10.7) 
        /// </summary>
        CURLOPT_FTP_CREATE_MISSING_DIRS = 110,
        /// <summary>
        /// Pass an <c>int</c>. Causes libcurl to set a timeout period (in seconds)
        /// on the amount of time that the server is allowed to take in order to
        /// generate a response message for a command before the session is
        /// considered hung. Note that while libcurl is waiting for a response, this
        /// value overrides <c>CURLOPT_TIMEOUT</c>. It is recommended that if used in
        /// conjunction with <c>CURLOPT_TIMEOUT</c>, you set
        /// <c>CURLOPT_FTP_RESPONSE_TIMEOUT</c> to a value smaller than
        /// <c>CURLOPT_TIMEOUT</c>. (Added in 7.10.8) 
        /// </summary>
        CURLOPT_FTP_RESPONSE_TIMEOUT    = 112,
        /// <summary>
        /// Pass a member of the <see cref="CURLftpSSL"/> enumeration.
        /// </summary>
        CURLOPT_FTP_SSL                 = 119,
        /// <summary>
        /// Pass a <c>bool</c>. If the value is <c>true</c>, it tells curl to use
        /// the EPRT (and LPRT) command when doing active FTP downloads (which is
        /// enabled by CURLOPT_FTPPORT). Using EPRT means that it will first attempt
        /// to use EPRT and then LPRT before using PORT, but if you pass <c>false</c>
        /// to this option, it will not try using EPRT or LPRT, only plain PORT.
        /// (Added in 7.10.5) 
        /// </summary>
        CURLOPT_FTP_USE_EPRT            = 106,
        /// <summary>
        /// Pass a <c>bool</c>. If the value is <c>true</c>, it tells curl to use
        /// the EPSV command when doing passive FTP downloads (which it always does
        /// by default). Using EPSV means that it will first attempt to use EPSV
        /// before using PASV, but if you pass <c>false</c> to this option, it will
        /// not try using EPSV, only plain PASV.
        /// </summary>
        CURLOPT_FTP_USE_EPSV            = 85,
        /// <summary>
        /// A <c>true</c> parameter tells the library to include the header in
        /// the body output. This is only relevant for protocols that actually
        /// have headers preceding the data (like HTTP).
        /// </summary>
        CURLOPT_HEADER                  = 42,
        /// <summary>
        /// Object reference to pass to the <see cref="Easy.HeaderFunction"/>
        /// delegate. Note that if you specify the <c>CURLOPT_HEADERFUNCTION</c>,
        /// this is the reference you'll get as the <c>extraData</c> parameter.
        /// </summary>
        CURLOPT_HEADERDATA              = 10029,
        /// <summary>
        /// Provide an <see cref="Easy.HeaderFunction"/> delegate reference.
        /// This delegate gets called by libcurl as soon as there is received
        /// header data that needs to be written down. The headers are guaranteed
        /// to be written one-by-one and only complete lines are written. Parsing
        /// headers should be easy enough using this. The size of the data contained
        /// in <c>buf</c> is <c>size</c> multiplied with <c>nmemb</c>.
        /// Return the number of bytes actually written or return -1 to signal
        /// error to the library (it will cause it to abort the transfer with a
        /// <see cref="CURLcode.CURLE_WRITE_ERROR"/> return code). 
        /// </summary>
        CURLOPT_HEADERFUNCTION          = 20079,
        /// <summary>
        /// Pass an <see cref="Slist"/> of aliases to be treated as valid HTTP
        /// 200 responses. Some servers respond with a custom header response line.
        /// For example, IceCast servers respond with "ICY 200 OK". By including
        /// this string in your list of aliases, the response will be treated as a
        /// valid HTTP header line such as "HTTP/1.0 200 OK". (Added in 7.10.3) 
        /// <note>
        /// The alias itself is not parsed for any version strings. So if your alias
        /// is "MYHTTP/9.9", libcurl will not treat the server as responding with
        /// HTTP version 9.9. Instead libcurl will use the value set by option
        /// <c>CURLOPT_HTTP_VERSION</c>. 
        /// </note>
        /// </summary>
        CURLOPT_HTTP200ALIASES          = 10104,
        /// <summary>
        /// Pass an <c>int</c> as parameter, which is set to a bitmask 
        /// of <see cref="CURLhttpAuth"/>, to tell libcurl what authentication
        /// method(s) you want it to use. If more than one bit is set, libcurl will
        /// first query the site to see what authentication methods it supports and
        /// then pick the best one you allow it to use. Note that for some methods,
        /// this will induce an extra network round-trip. Set the actual name and
        /// password with the <c>CURLOPT_USERPWD</c> option. (Added in 7.10.6) 
        /// </summary>
        CURLOPT_HTTPAUTH                = 107,
        /// <summary>
        /// Pass a <c>bool</c>. If it's <c>true</c>, this forces the HTTP request
        /// to get back to GET. Usable if a POST, HEAD, PUT or a custom request
        /// has been used previously using the same <see cref="Easy"/> object.
        /// </summary>
        CURLOPT_HTTPGET                 = 80,
        /// <summary>
        /// Pass an <see cref="Slist"/> reference containing HTTP headers to pass to
        /// the server in your HTTP request. If you add a header that is otherwise
        /// generated and used by libcurl internally, your added one will be used
        /// instead. If you add a header with no contents as in 'Accept:' (no data
        /// on the right side of the colon), the internally used header will get
        /// disabled. Thus, using this option you can add new headers, replace
        /// internal headers and remove internal headers. The headers included in the
        /// <c>Slist</c> must not be CRLF-terminated, because curl adds CRLF after
        /// each header item. Failure to comply with this will result in strange bugs
        /// because the server will most likely ignore part of the headers you specified. 
        /// <para>
        /// The first line in a request (usually containing a GET or POST) is not
        /// a header and cannot be replaced using this option. Only the lines
        /// following the request-line are headers. 
        /// </para>
        /// <para>
        /// Pass a <c>null</c> to this to reset back to no custom headers.
        /// </para>
        /// <note>
        /// The most commonly replaced headers have "shortcuts" in the options
        /// <c>CURLOPT_COOKIE</c>, <c>CURLOPT_USERAGENT</c> and <c>CURLOPT_REFERER</c>.
        /// </note>
        /// </summary>
        CURLOPT_HTTPHEADER              = 10023,
        /// <summary>
        /// Tells libcurl you want a multipart/formdata HTTP POST to be made and you
        /// instruct what data to pass on to the server. Pass a reference to a 
        /// <see cref="MultiPartForm"/> object as parameter.
        /// The best and most elegant way to do this, is to use 
        /// <see cref="MultiPartForm.AddSection"/> as documented.
        /// <para>
        /// Using POST with HTTP 1.1 implies the use of a "Expect: 100-continue"
        /// header. You can disable this header with <c>CURLOPT_HTTPHEADER</c> as usual.
        /// </para> 
        /// </summary>
        CURLOPT_HTTPPOST                = 10024,
        /// <summary>
        /// Set the parameter to <c>true</c> to get the library to tunnel all
        /// operations through a given HTTP proxy. Note that there is a big
        /// difference between using a proxy and tunneling through it. If you
        /// don't know what this means, you probably don't want this tunneling option. 
        /// </summary>
        CURLOPT_HTTPPROXYTUNNEL         = 61,
        /// <summary>
        /// Pass a member of the <see cref="CURLhttpVersion"/> enumeration. These
        /// values force libcurl to use the specific HTTP versions. This is not
        /// sensible to do unless you have a good reason. 
        /// </summary>
        CURLOPT_HTTP_VERSION            = 84,
        /// <summary>
        /// Provide an <see cref="Easy.IoctlFunction"/> delegate reference.
        /// This delegate gets called by libcurl when an IOCTL operation,
        /// such as a rewind of a file being sent via FTP, is required on
        /// the client side.
        /// </summary>
        CURLOPT_IOCTLFUNCTION           = 20130,
        /// <summary>
        /// Provide an object, such as a <c>FileStream</c>, upon which
        /// you may need to perform an IOCTL operation. Right now, only
        /// rewind is supported.
        /// </summary>
        CURLOPT_IOCTLDATA               = 10131,
        /// <summary>
        /// When uploading a file to a remote site, this option should be used to
        /// tell libcurl what the expected size of the infile is. This value should
        /// be passed as an <c>int</c>. 
        /// </summary>
        CURLOPT_INFILESIZE              = 14,
        /// <summary>
        /// When uploading a file to a remote site, this option should be used to
        /// tell libcurl what the expected size of the infile is. This value should
        /// be passed as a <c>long</c>. (Added in 7.11.0) 
        /// </summary>
        CURLOPT_INFILESIZE_LARGE        = 30115,
        /// <summary>
        /// Pass a <c>string</c> as parameter. This sets the interface name to use
        /// as the outgoing network interface. The name can be an interface name,
        /// an IP address or a host name.
        /// </summary>
        CURLOPT_INTERFACE               = 10062,
        /// <summary>
        /// Pass one of the members of the <see cref="CURLipResolve"/> enumeration.
        /// </summary>
        CURLOPT_IPRESOLVE               = 113,
        /// <summary>
        /// Pass a <c>string</c> as parameter. Set the kerberos4 security level;
        /// this also enables kerberos4 awareness. This is a string, 'clear', 'safe',
        /// 'confidential' or 'private'. If the string is set but doesn't match
        /// one of these, 'private' will be used. Set the string to <c>null</c>
        /// to disable kerberos4. The kerberos support only works for FTP.
        /// </summary>
        CURLOPT_KRB4LEVEL               = 10063,
        /// <summary>
        /// Last numeric entry in the enumeration. Don't use this in your
        /// application code.
        /// </summary>
        CURLOPT_LASTENTRY               = 135,
        /// <summary>
        /// Pass an <c>int</c> as parameter. It contains the transfer speed in bytes
        /// per second that the transfer should be below during
        /// <c>CURLOPT_LOW_SPEED_TIME</c> seconds for the library to consider it
        /// too slow and abort.
        /// </summary>
        CURLOPT_LOW_SPEED_LIMIT         = 19,
        /// <summary>
        /// Pass an <c>int</c> as parameter. It contains the time in seconds that
        /// the transfer should be below the <c>CURLOPT_LOW_SPEED_LIMIT</c> for the
        /// library to consider it too slow and abort.
        /// </summary>
        CURLOPT_LOW_SPEED_TIME          = 20,
        /// <summary>
        /// Pass an <c>int</c>. The set number will be the persistent connection
        /// cache size. The set amount will be the maximum amount of simultaneously
        /// open connections that libcurl may cache. Default is 5, and there isn't
        /// much point in changing this value unless you are perfectly aware of how
        /// this works and changes libcurl's behaviour. This concerns connections
        /// using any of the protocols that support persistent connections. 
        /// <para>
        /// When reaching the maximum limit, cURL uses the <c>CURLOPT_CLOSEPOLICY</c>
        /// to figure out which of the existing connections to close to prevent the
        /// number of open connections to increase. 
        /// </para>
        /// <note>
        /// if you already have performed transfers with this Easy object, setting a
        /// smaller <c>CURLOPT_MAXCONNECTS</c> than before may cause open connections
        /// to get closed unnecessarily.
        /// </note>
        /// </summary>
        CURLOPT_MAXCONNECTS             = 71,
        /// <summary>
        /// Pass an <c>int</c> as parameter. This allows you to specify the maximum
        /// size (in bytes) of a file to download. If the file requested is larger
        /// than this value, the transfer will not start and
        /// <see cref="CURLcode.CURLE_FILESIZE_EXCEEDED"/> will be returned.
        /// <note>
        /// The file size is not always known prior to download, and for such files
        /// this option has no effect even if the file transfer ends up being larger
        /// than this given limit. This concerns both FTP and HTTP transfers. 
        /// </note> 
        /// </summary>
        CURLOPT_MAXFILESIZE             = 114,
        /// <summary>
        /// Pass a <c>long</c> as parameter. This allows you to specify the
        /// maximum size (in bytes) of a file to download. If the file requested
        /// is larger than this value, the transfer will not start and
        /// <see cref="CURLcode.CURLE_FILESIZE_EXCEEDED"/> will be returned.
        /// (Added in 7.11.0) 
        /// <note>
        /// The file size is not always known prior to download, and for such files
        /// this option has no effect even if the file transfer ends up being larger
        /// than this given limit. This concerns both FTP and HTTP transfers. 
        /// </note>
        /// </summary>
        CURLOPT_MAXFILESIZE_LARGE       = 30117,
        /// <summary>
        /// Pass an <c>int</c>. The set number will be the redirection limit. If
        /// that many redirections have been followed, the next redirect will cause
        /// an error (<c>CURLE_TOO_MANY_REDIRECTS</c>). This option only makes sense
        /// if the <c>CURLOPT_FOLLOWLOCATION</c> is used at the same time.
        /// </summary>
        CURLOPT_MAXREDIRS               = 68,
        /// <summary>
        /// This parameter controls the preference of libcurl between using
        /// user names and passwords from your <c>~/.netrc</c> file, relative to
        /// user names and passwords in the URL supplied with <c>CURLOPT_URL</c>. 
        /// <note>
        /// libcurl uses a user name (and supplied or prompted password)
        /// supplied with <c>CURLOPT_USERPWD</c> in preference to any of the
        /// options controlled by this parameter.
        /// </note>
        /// <para>
        /// Pass a member of the <see cref="CURLnetrcOption"/> enumeration.
        /// </para>
        /// <para>
        /// Only machine name, user name and password are taken into account
        /// (init macros and similar things aren't supported).
        /// </para>
        /// <note>
        /// libcurl does not verify that the file has the correct properties
        /// set (as the standard Unix ftp client does). It should only be
        /// readable by user.
        /// </note>
        /// </summary>
        CURLOPT_NETRC                   = 51,
        /// <summary>
        /// Pass a <c>string</c> as parameter, containing the full path name to the
        /// file you want libcurl to use as .netrc file. If this option is omitted,
        /// and <c>CURLOPT_NETRC</c> is set, libcurl will attempt to find the a
        /// .netrc file in the current user's home directory. (Added in 7.10.9) 
        /// </summary>
        CURLOPT_NETRC_FILE              = 10118,
        /// <summary>
        /// A <c>true</c> parameter tells the library to not include the
        /// body-part in the output. This is only relevant for protocols that
        /// have separate header and body parts. On HTTP(S) servers, this
        /// will make libcurl do a HEAD request. 
        /// <para>
        /// To change back to GET, you should use <c>CURLOPT_HTTPGET</c>. To
        /// change back to POST, you should use <c>CURLOPT_POST</c>. Setting
        /// <c>CURLOPT_NOBODY</c> to <c>false</c> has no effect.
        /// </para>
        /// </summary>
        CURLOPT_NOBODY                  = 44,
        /// <summary>
        /// A <c>true</c> parameter tells the library to shut off progress
        /// reporting.
        /// </summary>
        CURLOPT_NOPROGRESS              = 43,
        /// <summary>
        /// Pass a <c>bool</c>. If it is <c>true</c>, libcurl will not use any
        /// functions that install signal handlers or any functions that cause
        /// signals to be sent to the process. This option is mainly here to allow
        /// multi-threaded unix applications to still set/use all timeout options
        /// etc, without risking getting signals. (Added in 7.10)
        /// <para>
        /// Consider using libcurl with ares built-in to enable asynchronous DNS
        /// lookups. It enables nice timeouts for name resolves without signals.
        /// </para> 
        /// </summary>
        CURLOPT_NOSIGNAL                = 99,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_PASV_HOST               = 126,
        /// <summary>
        /// Pass an <c>int</c> specifying what remote port number to connect to,
        /// instead of the one specified in the URL or the default port for the
        /// used protocol. 
        /// </summary>
        CURLOPT_PORT                    = 3,
        /// <summary>
        /// A <c>true</c> parameter tells the library to do a regular HTTP post.
        /// This will also make the library use the a "Content-Type:
        /// application/x-www-form-urlencoded" header. (This is by far the most
        /// commonly used POST method).
        /// <para>
        /// Use the <c>CURLOPT_POSTFIELDS</c> option to specify what data to post
        /// and <c>CURLOPT_POSTFIELDSIZE</c> to set the data size. Optionally,
        /// you can provide data to POST using the <c>CURLOPT_READFUNCTION</c> and
        /// <c>CURLOPT_READDATA</c> options.
        /// </para>
        /// <para>
        /// You can override the default POST Content-Type: header by setting
        /// your own with <c>CURLOPT_HTTPHEADER</c>. 
        /// </para>
        /// <para>
        /// Using POST with HTTP 1.1 implies the use of a "Expect: 100-continue"
        /// header. You can disable this header with <c>CURLOPT_HTTPHEADER</c> as usual.
        /// </para> 
        /// <para>
        /// If you use POST to a HTTP 1.1 server, you can send data without knowing
        /// the size before starting the POST if you use chunked encoding. You
        /// enable this by adding a header like "Transfer-Encoding: chunked" with
        /// <c>CURLOPT_HTTPHEADER</c>. With HTTP 1.0 or without chunked transfer,
        /// you must specify the size in the request. 
        /// </para>
        /// <note>
        /// if you have issued a POST request and want to make a HEAD or GET instead,
        /// you must explictly pick the new request type using <c>CURLOPT_NOBODY</c>
        /// or <c>CURLOPT_HTTPGET</c> or similar. 
        /// </note>
        /// </summary>
        CURLOPT_POST                    = 47,
        /// <summary>
        /// Pass a <c>string</c> as parameter, which should be the full data to post
        /// in an HTTP POST operation. You must make sure that the data is formatted
        /// the way you want the server to receive it. libcurl will not convert or
        /// encode it for you. Most web servers will assume this data to be
        /// url-encoded. Take note. 
        /// <para>
        /// This POST is a normal application/x-www-form-urlencoded kind (and
        /// libcurl will set that Content-Type by default when this option is used),
        /// which is the most commonly used one by HTML forms. See also the
        /// <c>CURLOPT_POST</c>. Using <c>CURLOPT_POSTFIELDS</c> implies
        /// <c>CURLOPT_POST</c>. 
        /// </para>
        /// <para>
        /// Using POST with HTTP 1.1 implies the use of a "Expect: 100-continue"
        /// header. You can disable this header with <c>CURLOPT_HTTPHEADER</c> as usual. 
        /// </para>
        /// <note>
        /// to make multipart/formdata posts (aka rfc1867-posts), check out the
        /// <c>CURLOPT_HTTPPOST</c> option.
        /// </note>
        /// </summary>
        CURLOPT_POSTFIELDS              = 10015,
        /// <summary>
        /// If you want to post data to the server without letting libcurl do a
        /// <c>strlen()</c> to measure the data size, this option must be used. When
        /// this option is used you can post fully binary data, which otherwise
        /// is likely to fail. If this size is set to zero, the library will use
        /// <c>strlen()</c> to get the size.
        /// </summary>
        CURLOPT_POSTFIELDSIZE           = 60,
        /// <summary>
        /// Pass a <c>long</c> as parameter. Use this to set the size of the
        /// <c>CURLOPT_POSTFIELDS</c> data to prevent libcurl from doing
        /// <c>strlen()</c> on the data to figure out the size. This is the large
        /// file version of the <c>CURLOPT_POSTFIELDSIZE</c> option. (Added in 7.11.1) 
        /// </summary>
        CURLOPT_POSTFIELDSIZE_LARGE     = 30120,
        /// <summary>
        /// Pass an <see cref="Slist"/> of FTP commands to pass to the server after
        /// your ftp transfer request. Disable this operation again by setting this
        /// option to <c>null</c>. 
        /// </summary>
        CURLOPT_POSTQUOTE               = 10039,
        /// <summary>
        /// Pass an <see cref="Slist"/> containing the FTP commands to pass to
        /// the server after the transfer type is set. Disable this operation
        /// again by setting a <c>null</c> to this option.
        /// </summary>
        CURLOPT_PREQUOTE                = 10093,
        /// <summary>
        /// Pass an <c>object</c> as parameter, referencing data that should be
        /// associated with this <see cref="Easy"/> object. The object can
        /// subsequently be retrieved using <see cref="Easy.GetInfo"/> with the
        /// <see cref="CURLINFO.CURLINFO_PRIVATE"/> option. libcurl itself does
        /// nothing with this data. (Added in 7.10.3) 
        /// </summary>
        CURLOPT_PRIVATE                 = 10103,
        /// <summary>
        /// Pass an <c>object</c> reference that will be untouched by libcurl
        /// and passed as the first argument in the progress delegate set with
        /// <c>CURLOPT_PROGRESSFUNCTION</c>.
        /// </summary>
        CURLOPT_PROGRESSDATA            = 10057,
        /// <summary>
        /// Pass an <see cref="Easy.ProgressFunction"/> delegate reference. This
        /// delegate gets called by libcurl at a frequent interval during data
        /// transfer. Unknown/unused argument values will be set to zero (like if
        /// you only download data, the upload size will remain 0). Returning a
        /// non-zero value from this delegate will cause libcurl to abort the
        /// transfer and return <see cref="CURLcode.CURLE_ABORTED_BY_CALLBACK"/>.
        /// <note>
        /// <c>CURLOPT_NOPROGRESS</c> must be set to <c>false</c> to make this
        /// function actually get called. 
        /// </note>
        /// </summary>
        CURLOPT_PROGRESSFUNCTION        = 20056,
        /// <summary>
        /// Set HTTP proxy to use. The parameter should be a <c>string</c> holding
        /// the host name or dotted IP address. To specify port number in this
        /// string, append <c>:[port]</c> to the end of the host name. The proxy
        /// string may be prefixed with <c>[protocol]://</c> since any such prefix
        /// will be ignored. The proxy's port number may optionally be specified
        /// with the separate option <c>CURLOPT_PROXYPORT</c>. 
        /// <para>
        /// NOTE: when you tell the library to use an HTTP proxy, libcurl will
        /// transparently convert operations to HTTP even if you specify an FTP
        /// URL etc. This may have an impact on what other features of the library
        /// you can use, such as <c>CURLOPT_QUOTE</c> and similar FTP specifics
        /// that don't work unless you tunnel through the HTTP proxy. Such tunneling
        /// is activated with <c>CURLOPT_HTTPPROXYTUNNEL</c>. 
        /// </para>
        /// </summary>
        CURLOPT_PROXY                   = 10004,
        /// <summary>
        /// Pass a bitmask of <see cref="CURLhttpAuth"/> as the paramter, to tell
        /// libcurl what authentication method(s) you want it to use for your proxy
        /// authentication. If more than one bit is set, libcurl will first query the
        /// site to see what authentication methods it supports and then pick the best
        /// one you allow it to use. Note that for some methods, this will induce an
        /// extra network round-trip. Set the actual name and password with the
        /// <c>CURLOPT_PROXYUSERPWD</c> option. The bitmask can be constructed by
        /// or'ing together the <see cref="CURLhttpAuth"/> bits. As of this writing,
        /// only <see cref="CURLhttpAuth.CURLAUTH_BASIC"/> and
        /// <see cref="CURLhttpAuth.CURLAUTH_NTLM"/> work. (Added in 7.10.7) 
        /// </summary>
        CURLOPT_PROXYAUTH               = 111,
        /// <summary>
        /// Pass an <c>int</c> with this option to set the proxy port to connect
        /// to unless it is specified in the proxy string <c>CURLOPT_PROXY</c>.
        /// </summary>
        CURLOPT_PROXYPORT               = 59,
        /// <summary>
        /// Pass a <see cref="CURLproxyType"/> to set type of the proxy.
        /// </summary>
        CURLOPT_PROXYTYPE               = 101,
        /// <summary>
        /// Pass a <c>string</c> as parameter, which should be
        /// <c>[user name]:[password]</c> to use for the connection to the
        /// HTTP proxy. Use <c>CURLOPT_PROXYAUTH</c> to decide authentication method. 
        /// </summary>
        CURLOPT_PROXYUSERPWD            = 10006,
        /// <summary>
        /// A <c>true</c> parameter tells the library to use HTTP PUT to transfer
        /// data. The data should be set with <c>CURLOPT_READDATA</c> and
        /// <c>CURLOPT_INFILESIZE</c>. 
        /// <para>
        /// This option is deprecated and starting with version 7.12.1 you should
        /// instead use <c>CURLOPT_UPLOAD</c>. 
        /// </para>
        /// </summary>
        CURLOPT_PUT                     = 54,
        /// <summary>
        /// Pass a reference to an <see cref="Slist"/> containing FTP commands to
        /// pass to the server prior to your ftp request. This will be done before
        /// any other FTP commands are issued (even before the CWD command).
        /// Disable this operation again by setting a null to this option.
        /// </summary>
        CURLOPT_QUOTE                   = 10028,
        /// <summary>
        /// Pass a <c>string</c> containing the file name. The file will be used
        /// to read from to seed the random engine for SSL. The more random the
        /// specified file is, the more secure the SSL connection will become.
        /// </summary>
        CURLOPT_RANDOM_FILE             = 10076,
        /// <summary>
        /// Pass a <c>string</c> as parameter, which should contain the
        /// specified range you want. It should be in the format <c>X-Y</c>, where X
        /// or Y may be left out. HTTP transfers also support several intervals,
        /// separated with commas as in <c>X-Y,N-M</c>. Using this kind of multiple
        /// intervals will cause the HTTP server to send the response document
        /// in pieces (using standard MIME separation techniques). Pass a
        /// <c>null</c> to this option to disable the use of ranges. 
        /// </summary>
        CURLOPT_RANGE                   = 10007,
        /// <summary>
        /// Object reference to pass to the <see cref="Easy.ReadFunction"/>
        /// delegate. Note that if you specify the <c>CURLOPT_READFUNCTION</c>,
        /// this is the reference you'll get as input.
        /// </summary>
        CURLOPT_READDATA                = 10009,
        /// <summary>
        /// Pass a reference to an <see cref="Easy.ReadFunction"/> delegate.
        /// This delegate gets called by libcurl as soon as it needs to read data
        /// in order to send it to the peer. The data area referenced by the
        /// <c>buf</c> may be filled with at most <c>size</c> multiplied with
        /// <c>nmemb</c> number of bytes. Your function must return the actual
        /// number of bytes that you stored in that byte array. Returning 0 will
        /// signal end-of-file to the library and cause it to stop the current transfer. 
        /// <para>
        /// If you stop the current transfer by returning 0 "pre-maturely"
        /// (i.e before the server expected it, like when you've told you will
        /// upload N bytes and you upload less than N bytes), you may experience that
        /// the server "hangs" waiting for the rest of the data that won't come. 
        /// </para>
        /// </summary>
        CURLOPT_READFUNCTION            = 20012,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used to set the Referer:
        /// header in the http request sent to the remote server. This can be used
        /// to fool servers or scripts. You can also set any custom header with
        /// <c>CURLOPT_HTTPHEADER</c>. 
        /// </summary>
        CURLOPT_REFERER                 = 10016,
        /// <summary>
        /// Pass an <c>int</c> as parameter. It contains the offset in number of
        /// bytes that you want the transfer to start from. Set this option to 0
        /// to make the transfer start from the beginning (effectively disabling resume). 
        /// </summary>
        CURLOPT_RESUME_FROM             = 21,
        /// <summary>
        /// Pass a <c>long</c> as parameter. It contains the offset in number of
        /// bytes that you want the transfer to start from. (Added in 7.11.0) 
        /// </summary>
        CURLOPT_RESUME_FROM_LARGE       = 30116,
        /// <summary>
        /// Pass an initialized <see cref="Share"/> reference as a parameter.
        /// Setting this option will make this <see cref="Easy"/> object use the
        /// data from the Share object instead of keeping the data to itself. This
        /// enables several Easy objects to share data. If the Easy objects are used
        /// simultaneously, you MUST use the Share object's locking methods.
        /// See <see cref="Share.SetOpt"/> for details.
        /// </summary>
        CURLOPT_SHARE                   = 10100,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_SOURCE_HOST             = 10122,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_SOURCE_PATH             = 10124,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_SOURCE_PORT             = 125,
        /// <summary>
        /// When doing a third-party transfer, set the source post-quote list,
        /// as an <see cref="Slist"/>.
        /// </summary>
        CURLOPT_SOURCE_POSTQUOTE        = 10128,
        /// <summary>
        /// When doing a third-party transfer, set the source pre-quote list,
        /// as an <see cref="Slist"/>.
        /// </summary>
        CURLOPT_SOURCE_PREQUOTE         = 10127,
        /// <summary>
        /// When doing a third-party transfer, set a quote list,
        /// as an <see cref="Slist"/>.
        /// </summary>
        CURLOPT_SOURCE_QUOTE            = 10133,
        /// <summary>
        /// Set the source URL for a third-party transfer.
        /// </summary>
        CURLOPT_SOURCE_URL              = 10132,
        /// <summary>
        /// When doing 3rd party transfer, set the source user and password, as
        /// a <c>string</c> with format <c>user:password</c>.
        /// </summary>
        CURLOPT_SOURCE_USERPWD          = 10123,
        /// <summary>
        /// Pass a <c>string</c> as parameter. The string should be the file name
        /// of your certificate. The default format is "PEM" and can be changed
        /// with <c>CURLOPT_SSLCERTTYPE</c>.
        /// </summary>
        CURLOPT_SSLCERT                 = 10025,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used as the password
        /// required to use the <c>CURLOPT_SSLCERT</c> certificate. 
        /// <para>
        /// This option is replaced by <c>CURLOPT_SSLKEYPASSWD</c> and should only
        /// be used for backward compatibility. You never needed a pass phrase to
        /// load a certificate but you need one to load your private key.
        /// </para>
        /// </summary>
        CURLOPT_SSLCERTPASSWD           = 10026,
        /// <summary>
        /// Pass a <c>string</c> as parameter. The string should be the format of
        /// your certificate. Supported formats are "PEM" and "DER". (Added in 7.9.3) 
        /// </summary>
        CURLOPT_SSLCERTTYPE             = 10086,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used as the identifier
        /// for the crypto engine you want to use for your private key.
        /// <note>
        /// If the crypto device cannot be loaded, 
        /// <see cref="CURLcode.CURLE_SSL_ENGINE_NOTFOUND"/> is returned.
        /// </note>
        /// </summary>
        CURLOPT_SSLENGINE               = 10089,
        /// <summary>
        /// Sets the actual crypto engine as the default for (asymmetric)
        /// crypto operations.
        /// <note>
        /// If the crypto device cannot be set,
        /// <see cref="CURLcode.CURLE_SSL_ENGINE_SETFAILED"/> is returned. 
        /// </note>
        /// </summary>
        CURLOPT_SSLENGINE_DEFAULT       = 90,
        /// <summary>
        /// Pass a <c>string</c> as parameter. The string should be the file name
        /// of your private key. The default format is "PEM" and can be changed
        /// with <c>CURLOPT_SSLKEYTYPE</c>. 
        /// </summary>
        CURLOPT_SSLKEY                  = 10087,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used as the password
        /// required to use the <c>CURLOPT_SSLKEY</c> private key.
        /// </summary>
        CURLOPT_SSLKEYPASSWD            = 10026,
        /// <summary>
        /// Pass a <c>string</c> as parameter. The string should be the format of
        /// your private key. Supported formats are "PEM", "DER" and "ENG". 
        /// <note>
        /// The format "ENG" enables you to load the private key from a crypto
        /// engine. In this case <c>CURLOPT_SSLKEY</c> is used as an identifier
        /// passed to the engine. You have to set the crypto engine with
        /// <c>CURLOPT_SSLENGINE</c>. "DER" format key file currently does not
        /// work because of a bug in OpenSSL. 
        /// </note>
        /// </summary>
        CURLOPT_SSLKEYTYPE              = 10088,
        /// <summary>
        /// Pass a member of the <see cref="CURLsslVersion"/> enumeration as the
        /// parameter to set the SSL version to use. By default
        /// the SSL library will try to solve this by itself although some servers
        /// servers make this difficult why you at times may have to use this
        /// option.
        /// </summary>
        CURLOPT_SSLVERSION              = 32,
        /// <summary>
        /// Pass a <c>string</c> holding the list of ciphers to use for the SSL
        /// connection. The list must be syntactically correct, it consists of
        /// one or more cipher strings separated by colons. Commas or spaces are
        /// also acceptable separators but colons are normally used, !, - and +
        /// can be used as operators. Valid examples of cipher lists include
        /// 'RC4-SHA', ´SHA1+DES´, 'TLSv1' and 'DEFAULT'. The default list is
        /// normally set when you compile OpenSSL.
        /// <para>
        /// You'll find more details about cipher lists on this URL:
        /// http://www.openssl.org/docs/apps/ciphers.html 
        /// </para>
        /// </summary>
        CURLOPT_SSL_CIPHER_LIST         = 10083,
        /// <summary>
        /// Object reference to pass to the ssl context delegate set by the option
        /// <c>CURLOPT_SSL_CTX_FUNCTION</c>, this is the pointer you'll get as the
        /// second parameter, otherwise <c>null</c>. (Added in 7.11.0) 
        /// </summary>
        CURLOPT_SSL_CTX_DATA            = 10109,
        /// <summary>
        /// Reference to an <see cref="Easy.SSLContextFunction"/> delegate.
        /// This delegate gets called by libcurl just before the initialization of
        /// an SSL connection after having processed all other SSL related options
        /// to give a last chance to an application to modify the behaviour of
        /// openssl's ssl initialization. The <see cref="SSLContext"/> parameter
        /// wraps a pointer to an openssl SSL_CTX. If an error is returned no attempt
        /// to establish a connection is made and the perform operation will return
        /// the error code from this callback function. Set the parm argument with
        /// the <c>CURLOPT_SSL_CTX_DATA</c> option. This option was introduced
        /// in 7.11.0.
        /// <note>
        /// To use this properly, a non-trivial amount of knowledge of the openssl
        /// libraries is necessary. Using this function allows for example to use
        /// openssl callbacks to add additional validation code for certificates,
        /// and even to change the actual URI of an HTTPS request.
        /// </note>
        /// </summary>
        CURLOPT_SSL_CTX_FUNCTION        = 20108,
        /// <summary>
        /// Pass an <c>int</c>. Set if we should verify the common name from the
        /// peer certificate in the SSL handshake, set 1 to check existence, 2 to
        /// ensure that it matches the provided hostname. This is by default set
        /// to 2. (default changed in 7.10) 
        /// </summary>
        CURLOPT_SSL_VERIFYHOST          = 81,
        /// <summary>
        /// Pass a <c>bool</c> that is set to <c>false</c> to stop curl from
        /// verifying the peer's certificate (7.10 starting setting this option
        /// to non-zero by default). Alternate certificates to verify against
        /// can be specified with the <c>CURLOPT_CAINFO</c> option or a
        /// certificate directory can be specified with the <c>CURLOPT_CAPATH</c>
        /// option. As of 7.10, curl installs a default bundle.
        /// <c>CURLOPT_SSL_VERIFYHOST</c> may also need to be set to 1
        /// or 0 if <c>CURLOPT_SSL_VERIFYPEER</c> is disabled (it defaults to 2). 
        /// </summary>
        CURLOPT_SSL_VERIFYPEER          = 64,
        /// <summary>
        /// Not supported.
        /// </summary>
        CURLOPT_STDERR                  = 10037,
        /// <summary>
        /// Pass a <c>bool</c> specifying whether the TCP_NODELAY option should be
        /// set or cleared (<c>true</c> = set, <c>false</c> = clear). The option is
        /// cleared by default. This will have no effect after the connection has
        /// been established.
        /// <para>
        /// Setting this option will disable TCP's Nagle algorithm. The purpose of
        /// this algorithm is to try to minimize the number of small packets on the
        /// network (where "small packets" means TCP segments less than the Maximum
        /// Segment Size (MSS) for the network). 
        /// </para>
        /// <para>
        /// Maximizing the amount of data sent per TCP segment is good because it
        /// amortizes the overhead of the send. However, in some cases (most notably
        /// telnet or rlogin) small segments may need to be sent without delay. This
        /// is less efficient than sending larger amounts of data at a time, and can
        /// contribute to congestion on the network if overdone. 
        /// </para> 
        /// </summary>
        CURLOPT_TCP_NODELAY             = 121,
        /// <summary>
        /// Provide an <see cref="Slist"/> with variables to pass to the telnet
        /// negotiations. The variables should be in the format "option=value".
        /// libcurl supports the options 'TTYPE', 'XDISPLOC' and 'NEW_ENV'. See
        /// the TELNET standard for details. 
        /// </summary>
        CURLOPT_TELNETOPTIONS           = 10070,
        /// <summary>
        /// Pass a member of the <see cref="CURLtimeCond"/> enumeration as
        /// parameter. This defines how the <c>CURLOPT_TIMEVALUE</c> time
        /// value is treated. This feature applies to HTTP and FTP. 
        /// <note>
        /// The last modification time of a file is not always known and in such
        /// instances this feature will have no effect even if the given time
        /// condition would have not been met.
        /// </note>
        /// </summary>
        CURLOPT_TIMECONDITION           = 33,
        /// <summary>
        /// Pass a <c>int</c> as parameter containing the maximum time in seconds
        /// that you allow the libcurl transfer operation to take. Normally, name
        /// lookups can take a considerable time and limiting operations to less
        /// than a few minutes risk aborting perfectly normal operations. This
        /// option will cause curl to use the SIGALRM to enable time-outing
        /// system calls. 
        /// <note>
        /// this is not recommended to use in unix multi-threaded programs,
        /// as it uses signals unless <c>CURLOPT_NOSIGNAL</c> (see above) is set.
        /// </note>
        /// </summary>
        CURLOPT_TIMEOUT                 = 13,
        /// <summary>
        /// Pass a <see cref="System.DateTime"/> as parameter. This time will be
        /// used in a condition as specified with <c>CURLOPT_TIMECONDITION</c>. 
        /// </summary>
        CURLOPT_TIMEVALUE               = 34,
        /// <summary>
        /// A <c>true</c> parameter tells the library to use ASCII mode for ftp
        /// transfers, instead of the default binary transfer. For LDAP transfers
        /// it gets the data in plain text instead of HTML and for win32 systems
        /// it does not set the stdout to binary mode. This option can be usable
        /// when transferring text data between systems with different views on
        /// certain characters, such as newlines or similar.
        /// </summary>
        CURLOPT_TRANSFERTEXT            = 53,
        /// <summary>
        /// A <c>true</c> parameter tells the library it can continue to send
        /// authentication (user+password) when following locations, even when
        /// hostname changed. Note that this is meaningful only when setting
        /// <c>CURLOPT_FOLLOWLOCATION</c>.
        /// </summary>
        CURLOPT_UNRESTRICTED_AUTH       = 105,
        /// <summary>
        /// A <c>true</c> parameter tells the library to prepare for an
        /// upload. The <c>CURLOPT_READDATA</c> and <c>CURLOPT_INFILESIZE</c>
        /// or <c>CURLOPT_INFILESIZE_LARGE</c> are also interesting for uploads.
        /// If the protocol is HTTP, uploading means using the PUT request
        /// unless you tell libcurl otherwise. 
        /// <para>
        /// Using PUT with HTTP 1.1 implies the use of a "Expect: 100-continue"
        /// header. You can disable this header with <c>CURLOPT_HTTPHEADER</c> as usual. 
        /// </para>
        /// <para>
        /// If you use PUT to a HTTP 1.1 server, you can upload data without
        /// knowing the size before starting the transfer if you use chunked
        /// encoding. You enable this by adding a header like
        /// "Transfer-Encoding: chunked" with <c>CURLOPT_HTTPHEADER</c>. With
        /// HTTP 1.0 or without chunked transfer, you must specify the size.
        /// </para>
        /// </summary>
        CURLOPT_UPLOAD                  = 46,
        /// <summary>
        /// The actual URL to deal with. The parameter should be a <c>string</c>.
        /// If the given URL lacks the protocol part ("http://" or "ftp://" etc), it
        /// will attempt to guess which protocol to use based on the given host name.
        /// <para>If the given protocol of the set URL is not supported, libcurl will return
        /// an error <c>CURLcode.</c>(<see cref="CURLcode.CURLE_UNSUPPORTED_PROTOCOL"/>)
        /// when you call Easy's <see cref="Easy.Perform"/> or
        /// Multi's <see cref="Multi.Perform"/>.</para>
        /// <para>Use <see cref="Curl.GetVersionInfo"/> for detailed info
        /// on which protocols that are supported.</para>
        /// </summary>
        CURLOPT_URL                     = 10002,
        /// <summary>
        /// Pass a <c>string</c> as parameter. It will be used to set the
        /// User-Agent: header in the http request sent to the remote server.
        /// This can be used to fool servers or scripts. You can also set any
        /// custom header with <c>CURLOPT_HTTPHEADER</c>.
        /// </summary>
        CURLOPT_USERAGENT               = 10018,
        /// <summary>
        /// Pass a <c>string</c> as parameter, which should be
        /// <c>[user name]:[password]</c> to use for the connection. Use
        /// <c>CURLOPT_HTTPAUTH</c> to decide authentication method. 
        /// <para>
        /// When using HTTP and <c>CURLOPT_FOLLOWLOCATION</c>, libcurl might
        /// perform several requests to possibly different hosts. libcurl will
        /// only send this user and password information to hosts using the
        /// initial host name (unless <c>CURLOPT_UNRESTRICTED_AUTH</c> is set),
        /// so if libcurl follows locations to other hosts it will not send the
        /// user and password to those. This is enforced to prevent accidental
        /// information leakage. 
        /// </para>
        /// </summary>
        CURLOPT_USERPWD                 = 10005,
        /// <summary>
        /// Set the parameter to <c>true</c> to get the library to display a lot
        /// of verbose information about its operations. Very useful for libcurl
        /// and/or protocol debugging and understanding. The verbose information
        /// will be sent to the <see cref="Easy.DebugFunction"/> delegate, if it's
        /// implemented. You hardly ever want this set in production use, you will
        /// almost always want this when you debug/report problems. 
        /// </summary>
        CURLOPT_VERBOSE                 = 41,
        /// <summary>
        /// Object reference to pass to the <see cref="Easy.WriteFunction"/>
        /// delegate. Note that if you specify the <c>CURLOPT_WRITEFUNCTION</c>,
        /// this is the object you'll get as input. 
        /// </summary>
        CURLOPT_WRITEDATA               = 10001,
        /// <summary>
        /// Pass a reference to an <see cref="Easy.WriteFunction"/> delegate.
        /// The delegate gets called by libcurl as soon as there is data received
        /// that needs to be saved. The size of the data referenced by <c>buf</c>
        /// is <c>size</c> multiplied with <c>nmemb</c>, it will not be zero
        /// terminated. Return the number of bytes actually taken care of. If
        /// that amount differs from the amount passed to your function, it'll
        /// signal an error to the library and it will abort the transfer and
        /// return <c>CURLcode.</c><see cref="CURLcode.CURLE_WRITE_ERROR"/>. 
        /// <note>This function may be called with zero bytes data if the
        /// transfered file is empty.</note>
        /// </summary>
        CURLOPT_WRITEFUNCTION           = 20011,
        /// <summary>
        /// Pass a <c>string</c> of the output using full variable-replacement
        /// as described elsewhere.
        /// </summary>
        CURLOPT_WRITEINFO               = 10040,
    };

    /// <summary>
    /// This enumeration contains values used to specify the IP resolution
    /// method when using the <see cref="CURLoption.CURLOPT_IPRESOLVE"/>
    /// option in a call to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLipResolve
    {
        /// <summary>
        /// Default, resolves addresses to all IP versions that your system
        /// allows.
        /// </summary>
        CURL_IPRESOLVE_WHATEVER = 0,
        /// <summary>
        /// Resolve to ipv4 addresses.
        /// </summary>
        CURL_IPRESOLVE_V4       = 1,
        /// <summary>
        /// Resolve to ipv6 addresses.
        /// </summary>
        CURL_IPRESOLVE_V6       = 2
    };

    /// <summary>
    /// Contains values used to specify the HTTP version level when using
    /// the <see cref="CURLoption.CURLOPT_HTTP_VERSION"/> option in a call
    /// to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLhttpVersion
    {
        /// <summary>
        /// We don't care about what version the library uses. libcurl will
        /// use whatever it thinks fit.
        /// </summary>
        CURL_HTTP_VERSION_NONE  = 0,
        /// <summary>
        /// Enforce HTTP 1.0 requests.
        /// </summary>
        CURL_HTTP_VERSION_1_0   = 1,
        /// <summary>
        /// Enforce HTTP 1.1 requests.
        /// </summary>
        CURL_HTTP_VERSION_1_1   = 2,
        /// <summary>
        /// Last entry in enumeration; do not use in application code.
        /// </summary>
        CURL_HTTP_VERSION_LAST  = 3
    };

    /// <summary>
    /// Contains values used to specify the preference of libcurl between
    /// using user names and passwords from your ~/.netrc file, relative to
    /// user names and passwords in the URL supplied with
    /// <see cref="CURLoption.CURLOPT_URL"/>. This is passed when using
    /// the <see cref="CURLoption.CURLOPT_NETRC"/> option in a call
    /// to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLnetrcOption
    {
        /// <summary>
        /// The library will ignore the file and use only the information
        /// in the URL. This is the default. 
        /// </summary>
        CURL_NETRC_IGNORED  = 0,
        /// <summary>
        /// The use of your ~/.netrc file is optional, and information in the
        /// URL is to be preferred. The file will be scanned with the host
        /// and user name (to find the password only) or with the host only,
        /// to find the first user name and password after that machine,
        /// which ever information is not specified in the URL. 
        /// <para>
        /// Undefined values of the option will have this effect.
        /// </para>
        /// </summary>
        CURL_NETRC_OPTIONAL = 1,
        /// <summary>
        /// This value tells the library that use of the file is required,
        /// to ignore the information in the URL, and to search the file
        /// with the host only.
        /// </summary>
        CURL_NETRC_REQUIRED = 2,
        /// <summary>
        /// Last entry in enumeration; do not use in application code.
        /// </summary>
        CURL_NETRC_LAST     = 3
    };

    /// <summary>
    /// Contains values used to specify the SSL version level when using
    /// the <see cref="CURLoption.CURLOPT_SSLVERSION"/> option in a call
    /// to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLsslVersion
    {
        /// <summary>
        /// Use whatever version the SSL library selects.
        /// </summary>
        CURL_SSLVERSION_DEFAULT = 0,
        /// <summary>
        /// Use TLS version 1.
        /// </summary>
        CURL_SSLVERSION_TLSv1   = 1,
        /// <summary>
        /// Use SSL version 2. This is not a good option unless it's the
        /// only version supported by the remote server.
        /// </summary>
        CURL_SSLVERSION_SSLv2   = 2,
        /// <summary>
        /// Use SSL version 3. This is a preferred option.
        /// </summary>
        CURL_SSLVERSION_SSLv3   = 3,
        /// <summary>
        /// Last entry in enumeration; do not use in application code.
        /// </summary>
        CURL_SSLVERSION_LAST    = 4
    };

    /// <summary>
    /// Contains values used to specify the time condition when using
    /// the <see cref="CURLoption.CURLOPT_TIMECONDITION"/> option in a call
    /// to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLtimeCond
    {
        /// <summary>
        /// Use no time condition.
        /// </summary>
        CURL_TIMECOND_NONE          = 0,
        /// <summary>
        /// The time condition is true if the resource has been modified
        /// since the date/time passed in
        /// <see cref="CURLoption.CURLOPT_TIMEVALUE"/>.
        /// </summary>
        CURL_TIMECOND_IFMODSINCE    = 1,
        /// <summary>
        /// True if the resource has not been modified since the date/time
        /// passed in <see cref="CURLoption.CURLOPT_TIMEVALUE"/>.
        /// </summary>
        CURL_TIMECOND_IFUNMODSINCE  = 2,
        /// <summary>
        /// True if the resource's last modification date/time equals that
        /// passed in <see cref="CURLoption.CURLOPT_TIMEVALUE"/>.
        /// </summary>
        CURL_TIMECOND_LASTMOD       = 3,
        /// <summary>
        /// Last entry in enumeration; do not use in application code.
        /// </summary>
        CURL_TIMECOND_LAST          = 4
    };

    /// <summary>
    /// These are options available to build a multi-part form section
    /// in a call to <see cref="MultiPartForm.AddSection"/>
    /// </summary>
    public enum CURLformoption
    {
        /// <summary>
        /// Another possibility to send options to
        /// <see cref="MultiPartForm.AddSection"/> is this option, that
        /// passes a <see cref="CurlForms"/> array reference as its value.
        /// Each <see cref="CurlForms"/> array element has a
        /// <see cref="CURLformoption"/> and a <c>string</c>. All available
        /// options can be used in an array, except the <c>CURLFORM_ARRAY</c>
        /// option itself! The last argument in such an array must always be
        /// <c>CURLFORM_END</c>. 
        /// </summary>
        CURLFORM_ARRAY            = 8,
        /// <summary>
        /// Followed by a <c>string</c>, tells libcurl that a buffer is to be
        /// used to upload data instead of using a file.
        /// </summary>
        CURLFORM_BUFFER           = 11,
        /// <summary>
        /// Followed by an <c>int</c> with the size of the
        /// <c>CURLFORM_BUFFERPTR</c> byte array, tells libcurl the length of
        /// the data to upload. 
        /// </summary>
        CURLFORM_BUFFERLENGTH     = 13,
        /// <summary>
        /// Followed by a <c>byte[]</c> array, tells libcurl the address of
        /// the buffer containing data to upload (as indicated with
        /// <c>CURLFORM_BUFFER</c>). You must also use
        /// <c>CURLFORM_BUFFERLENGTH</c> to set the length of the buffer area. 
        /// </summary>
        CURLFORM_BUFFERPTR        = 12,
        /// <summary>
        /// Specifies extra headers for the form POST section. This takes an
        /// <see cref="Slist"/> prepared in the usual way using
        /// <see cref="Slist.Append"/> and appends the list of headers to
        /// those libcurl automatically generates.
        /// </summary>
        CURLFORM_CONTENTHEADER    = 15,
        /// <summary>
        /// Followed by an <c>int</c> setting the length of the contents. 
        /// </summary>
        CURLFORM_CONTENTSLENGTH   = 6,
        /// <summary>
        /// Followed by a <c>string</c> with a content-type will make cURL
        /// use this given content-type for this file upload part, possibly
        /// instead of an internally chosen one. 
        /// </summary>
        CURLFORM_CONTENTTYPE      = 14,
        /// <summary>
        /// Followed by a <c>string</c> is used for the contents of this part, the
        /// actual data to send away. If you'd like it to contain zero bytes,
        /// you need to set the length of the name with
        /// <c>CURLFORM_CONTENTSLENGTH</c>. 
        /// </summary>
        CURLFORM_COPYCONTENTS     = 4,
        /// <summary>
        /// Followed by a <c>string</c> used to set the name of this part.
        /// If you'd like it to contain zero bytes, you need to set the
        /// length of the name with <c>CURLFORM_NAMELENGTH</c>. 
        /// </summary>
        CURLFORM_COPYNAME         = 1,
        /// <summary>
        /// This should be the last argument to a call to
        /// <see cref="MultiPartForm.AddSection"/>.
        /// </summary>
        CURLFORM_END              = 17,
        /// <summary>
        /// Followed by a file name, makes this part a file upload part. It
        /// sets the file name field to the actual file name used here,
        /// it gets the contents of the file and passes as data and sets the
        /// content-type if the given file match one of the new internally
        /// known file extension. For <c>CURLFORM_FILE</c> the user may send
        /// one or more files in one part by providing multiple <c>CURLFORM_FILE</c>
        /// arguments each followed by the filename (and each <c>CURLFORM_FILE</c>
        /// is allowed to have a <c>CURLFORM_CONTENTTYPE</c>). 
        /// </summary>
        CURLFORM_FILE             = 10,
        /// <summary>
        /// Followed by a file name, and does the file read: the contents
        /// will be used in as data in this part. 
        /// </summary>
        CURLFORM_FILECONTENT      = 7,
        /// <summary>
        /// Followed by a <c>string</c> file name, will make libcurl use the
        /// given name in the file upload part, instead of the actual file
        /// name given to <c>CURLFORM_FILE</c>. 
        /// </summary>
        CURLFORM_FILENAME         = 16,
        /// <summary>
        /// Followed by an <c>int</c> setting the length of the name. 
        /// </summary>
        CURLFORM_NAMELENGTH       = 3,
        /// <summary>
        /// Not used.
        /// </summary>
        CURLFORM_NOTHING          = 0,
        /// <summary>
        /// No longer used.
        /// </summary>
        CURLFORM_OBSOLETE         = 9,
        /// <summary>
        /// No longer used.
        /// </summary>
        CURLFORM_OBSOLETE2        = 18,
        /// <summary>
        /// Followed by a <c>byte[]</c> used for the contents of this part.
        /// If you'd like it to contain zero bytes, you need to set the
        /// length of the name with <c>CURLFORM_CONTENTSLENGTH</c>. 
        /// </summary>
        CURLFORM_PTRCONTENTS      = 5,
        /// <summary>
        /// Followed by a <c>byte[]</c> used for the name of this part.
        /// If you'd like it to contain zero bytes, you need to set the
        /// length of the name with <c>CURLFORM_NAMELENGTH</c>. 
        /// </summary>
        CURLFORM_PTRNAME          = 2
    };

    /// <summary>
    /// One of these is returned by <see cref="MultiPartForm.AddSection"/>.
    /// </summary>
    public enum CURLFORMcode
    {
        /// <summary>
        /// The section was added properly.
        /// </summary>
        CURL_FORMADD_OK               = 0,
        /// <summary>
        /// Out-of-memory when adding the section.
        /// </summary>
        CURL_FORMADD_MEMORY           = 1,
        /// <summary>
        /// Invalid attempt to add the same option more than once to a
        /// section.
        /// </summary>
        CURL_FORMADD_OPTION_TWICE     = 2,
        /// <summary>
        /// Invalid attempt to pass a <c>null</c> string or byte array in
        /// one of the arguments.
        /// </summary>
        CURL_FORMADD_NULL             = 3,
        /// <summary>
        /// Invalid attempt to pass an unrecognized option in one of the
        /// arguments.
        /// </summary>
        CURL_FORMADD_UNKNOWN_OPTION   = 4,
        /// <summary>
        /// Incomplete argument lists.
        /// </summary>
        CURL_FORMADD_INCOMPLETE       = 5,
        /// <summary>
        /// Invalid attempt to provide a nested <c>CURLFORM_ARRAY</c>.
        /// </summary>
        CURL_FORMADD_ILLEGAL_ARRAY    = 6,
        /// <summary>
        /// This will not be returned so long as HTTP is enabled, which
        /// it always is in libcurl.NET.
        /// </summary>
        CURL_FORMADD_DISABLED         = 7,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURL_FORMADD_LAST             = 8
    };

    /// <summary>
    /// This enumeration is used to extract information associated with an
    /// <see cref="Easy"/> transfer. Specifically, a member of this
    /// enumeration is passed as the first argument to
    /// <see cref="Easy.GetInfo"/> specifying the item to retrieve in the
    /// second argument, which is a reference to an <c>int</c>, a
    /// <c>double</c>, a <c>string</c>, a <c>DateTime</c> or an <c>object</c>.
    /// </summary>
    public enum CURLINFO
    {
        /// <summary>
        /// The second argument receives the elapsed time, as a <c>double</c>,
        /// in seconds, from the start until the connect to the remote host
        /// (or proxy) was completed. 
        /// </summary>
        CURLINFO_CONNECT_TIME             = 0x300005,
        /// <summary>
        /// The second argument receives, as a <c>double</c>, the content-length
        /// of the download. This is the value read from the Content-Length: field. 
        /// </summary>
        CURLINFO_CONTENT_LENGTH_DOWNLOAD  = 0x30000F,
        /// <summary>
        /// The second argument receives, as a <c>double</c>, the specified size
        /// of the upload. 
        /// </summary>
        CURLINFO_CONTENT_LENGTH_UPLOAD    = 0x300010,
        /// <summary>
        /// The second argument receives, as a <c>string</c>, the content-type of
        /// the downloaded object. This is the value read from the Content-Type:
        /// field. If you get <c>null</c>, it means that the server didn't
        /// send a valid Content-Type header or that the protocol used
        /// doesn't support this. 
        /// </summary>
        CURLINFO_CONTENT_TYPE             = 0x100012,
        /// <summary>
        /// The second argument receives, as a <c>string</c>, the last
        /// used effective URL. 
        /// </summary>
        CURLINFO_EFFECTIVE_URL            = 0x100001,
        /// <summary>
        /// The second argument receives, as a <c>long</c>, the remote time
        /// of the retrieved document. You should construct a <c>DateTime</c>
        /// from this value, as shown in the <c>InfoDemo</c> sample. If you
        /// get a date in the distant
        /// past, it can be because of many reasons (unknown, the server
        /// hides it or the server doesn't support the command that tells
        /// document time etc) and the time of the document is unknown. Note
        /// that you must tell the server to collect this information before
        /// the transfer is made, by using the 
        /// <see cref="CURLoption.CURLOPT_FILETIME"/> option to
        /// <see cref="Easy.SetOpt"/>. (Added in 7.5) 
        /// </summary>
        CURLINFO_FILETIME                 = 0x20000E,
        /// <summary>
        /// The second argument receives an <c>int</c> specifying the total size
        /// of all the headers received. 
        /// </summary>
        CURLINFO_HEADER_SIZE              = 0x20000B,
        /// <summary>
        /// The second argument receives, as an <c>int</c>, a bitmask indicating
        /// the authentication method(s) available. The meaning of the bits is
        /// explained in the documentation of
        /// <see cref="CURLoption.CURLOPT_HTTPAUTH"/>. (Added in 7.10.8) 
        /// </summary>
        CURLINFO_HTTPAUTH_AVAIL           = 0x200017,
        /// <summary>
        /// The second argument receives an <c>int</c> indicating the numeric
        /// connect code for the HTTP request.
        /// </summary>
        CURLINFO_HTTP_CONNECTCODE         = 0x200016,
        /// <summary>
        /// End-of-enumeration marker; do not use in client applications.
        /// </summary>
        CURLINFO_LASTONE                  = 0x1C,
        /// <summary>
        /// The second argument receives, as a <c>double</c>, the time, in
        /// seconds it took from the start until the name resolving was
        /// completed. 
        /// </summary>
        CURLINFO_NAMELOOKUP_TIME          = 0x300004,
        /// <summary>
        /// Never used.
        /// </summary>
        CURLINFO_NONE                     = 0x0,
        /// <summary>
        /// The second argument receives an <c>int</c> indicating the
        /// number of current connections. (Added in 7.13.0)
        /// </summary>
        CURLINFO_NUM_CONNECTS             = 0x20001A,
        /// <summary>
        /// The second argument receives an <c>int</c> indicating the operating
        /// system error number: <c>_errro</c> or <c>GetLastError()</c>,
        /// depending on the platform. (Added in 7.12.2)
        /// </summary>
        CURLINFO_OS_ERRNO                 = 0x200019,
        /// <summary>
        /// The second argument receives, as a <c>double</c>, the time, in
        /// seconds, it took from the start until the file transfer is just about
        /// to begin. This includes all pre-transfer commands and negotiations
        /// that are specific to the particular protocol(s) involved. 
        /// </summary>
        CURLINFO_PRETRANSFER_TIME         = 0x300006,
        /// <summary>
        /// The second argument receives a reference to the private data
        /// associated with the <see cref="Easy"/> object (set with the
        /// <see cref="CURLoption.CURLOPT_PRIVATE"/> option to
        /// <see cref="Easy.SetOpt"/>. (Added in 7.10.3) 
        /// </summary>
        CURLINFO_PRIVATE                  = 0x100015,
        /// <summary>
        /// The second argument receives, as an <c>int</c>, a bitmask
        /// indicating the authentication method(s) available for your
        /// proxy authentication. This will be a bitmask of
        /// <see cref="CURLhttpAuth"/> enumeration constants.
        /// (Added in 7.10.8) 
        /// </summary>
        CURLINFO_PROXYAUTH_AVAIL          = 0x200018,
        /// <summary>
        /// The second argument receives an <c>int</c> indicating the total
        /// number of redirections that were actually followed. (Added in 7.9.7) 
        /// </summary>
        CURLINFO_REDIRECT_COUNT           = 0x200014,
        /// <summary>
        /// The second argument receives, as a <c>double</c>, the total time, in
        /// seconds, for all redirection steps include name lookup, connect,
        /// pretransfer and transfer before final transaction was started.
        /// <c>CURLINFO_REDIRECT_TIME</c> contains the complete execution
        /// time for multiple redirections. (Added in 7.9.7) 
        /// </summary>
        CURLINFO_REDIRECT_TIME            = 0x300013,
        /// <summary>
        /// The second argument receives an <c>int</c> containing the total size
        /// of the issued requests. This is so far only for HTTP requests. Note
        /// that this may be more than one request if
        /// <see cref="CURLoption.CURLOPT_FOLLOWLOCATION"/> is <c>true</c>.
        /// </summary>
        CURLINFO_REQUEST_SIZE             = 0x20000C,
        /// <summary>
        /// The second argument receives an <c>int</c> with the last received HTTP
        /// or FTP code. This option was known as <c>CURLINFO_HTTP_CODE</c> in
        /// libcurl 7.10.7 and earlier. 
        /// </summary>
        CURLINFO_RESPONSE_CODE            = 0x200002,
        /// <summary>
        /// The second argument receives a <c>double</c> with the total amount of
        /// bytes that were downloaded. The amount is only for the latest transfer
        /// and will be reset again for each new transfer. 
        /// </summary>
        CURLINFO_SIZE_DOWNLOAD            = 0x300008,
        /// <summary>
        /// The second argument receives a <c>double</c> with the total amount
        /// of bytes that were uploaded. 
        /// </summary>
        CURLINFO_SIZE_UPLOAD              = 0x300007,
        /// <summary>
        /// The second argument receives a <c>double</c> with the average
        /// download speed that cURL measured for the complete download. 
        /// </summary>
        CURLINFO_SPEED_DOWNLOAD           = 0x300009,
        /// <summary>
        /// The second argument receives a <c>double</c> with the average
        /// upload speed that libcurl measured for the complete upload. 
        /// </summary>
        CURLINFO_SPEED_UPLOAD             = 0x30000A,
        /// <summary>
        /// The second argument receives an <see cref="Slist"/> containing
        /// the names of the available SSL engines.
        /// </summary>
        CURLINFO_SSL_ENGINES              = 0x40001B,
        /// <summary>
        /// The second argument receives an <c>int</c> with the result of
        /// the certificate verification that was requested (using the
        /// <see cref="CURLoption.CURLOPT_SSL_VERIFYPEER"/> option in
        /// <see cref="Easy.SetOpt"/>. 
        /// </summary>
        CURLINFO_SSL_VERIFYRESULT         = 0x20000D,
        /// <summary>
        /// The second argument receives a <c>double</c> specifying the time,
        /// in seconds, from the start until the first byte is just about to be
        /// transferred. This includes <c>CURLINFO_PRETRANSFER_TIME</c> and
        /// also the time the server needs to calculate the result. 
        /// </summary>
        CURLINFO_STARTTRANSFER_TIME       = 0x300011,
        /// <summary>
        /// The second argument receives a <c>double</c> indicating the total transaction
        /// time in seconds for the previous transfer. This time does not include
        /// the connect time, so if you want the complete operation time,
        /// you should add the <c>CURLINFO_CONNECT_TIME</c>. 
        /// </summary>
        CURLINFO_TOTAL_TIME               = 0x300003,
    };

    /// <summary>
    /// Contains values used to specify the order in which cached connections
    /// are closed. One of these is passed as the
    /// <see cref="CURLoption.CURLOPT_CLOSEPOLICY"/> option in a call
    /// to <see cref="Easy.SetOpt"/>
    /// </summary>
    public enum CURLclosePolicy
    {
        /// <summary>
        /// No close policy. Never use this.
        /// </summary>
        CURLCLOSEPOLICY_NONE                = 0,
        /// <summary>
        /// Close the oldest cached connections first.
        /// </summary>
        CURLCLOSEPOLICY_OLDEST              = 1,
        /// <summary>
        /// Close the least recently used connections first.
        /// </summary>
        CURLCLOSEPOLICY_LEAST_RECENTLY_USED = 2,
        /// <summary>
        /// Close the connections with the least traffic first.
        /// </summary>
        CURLCLOSEPOLICY_LEAST_TRAFFIC       = 3,
        /// <summary>
        /// Close the slowest connections first.
        /// </summary>
        CURLCLOSEPOLICY_SLOWEST             = 4,
        /// <summary>
        /// Currently unimplemented.
        /// </summary>
        CURLCLOSEPOLICY_CALLBACK            = 5,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURLCLOSEPOLICY_LAST                = 6
    };

    /// <summary>
    /// Contains values used to initialize libcurl internally. One of
    /// these is passed in the call to <see cref="Curl.GlobalInit"/>.
    /// </summary>
    public enum CURLinitFlag
    {
        /// <summary>
        /// Initialise nothing extra. This sets no bit.
        /// </summary>
        CURL_GLOBAL_NOTHING    = 0,
        /// <summary>
        /// Initialize SSL.
        /// </summary>
        CURL_GLOBAL_SSL        = 1,
        /// <summary>
        /// Initialize the Win32 socket libraries.
        /// </summary>
        CURL_GLOBAL_WIN32      = 2,
        /// <summary>
        /// Initialize everything possible. This sets all known bits.
        /// </summary>
        CURL_GLOBAL_ALL        = 3,
        /// <summary>
        /// Equivalent to <c>CURL_GLOBAL_ALL</c>.
        /// </summary>
        CURL_GLOBAL_DEFAULT    = CURL_GLOBAL_ALL
    };

    /// <summary>
    /// Members of this enumeration should be passed to
    /// <see cref="Share.SetOpt"/> when it is called with the
    /// <c>CURLSHOPT_SHARE</c> or <c>CURLSHOPT_UNSHARE</c> options
    /// provided in the <see cref="CURLSHoption"/> enumeration.
    /// </summary>
    public enum CURLlockData
    {
        /// <summary>
        /// Not used.
        /// </summary>
        CURL_LOCK_DATA_NONE         = 0,
        /// <summary>
        /// Used internally by libcurl.
        /// </summary>
        CURL_LOCK_DATA_SHARE        = 1,
        /// <summary>
        /// Cookie data will be shared across the <see cref="Easy"/> objects
        /// using this shared object.
        /// </summary>
        CURL_LOCK_DATA_COOKIE       = 2,
        /// <summary>
        /// Cached DNS hosts will be shared across the <see cref="Easy"/>
        /// objects using this shared object. 
        /// </summary>
        CURL_LOCK_DATA_DNS          = 3,
        /// <summary>
        /// Not supported yet.
        /// </summary>
        CURL_LOCK_DATA_SSL_SESSION  = 4,
        /// <summary>
        /// Not supported yet.
        /// </summary>
        CURL_LOCK_DATA_CONNECT      = 5,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURL_LOCK_DATA_LAST         = 6
    };

    /// <summary>
    /// Values containing the type of shared access requested when libcurl
    /// calls the <see cref="Share.LockFunction"/> delegate.
    /// </summary>
    public enum CURLlockAccess
    {
        /// <summary>
        /// Unspecified action; the delegate should never receive this.
        /// </summary>
        CURL_LOCK_ACCESS_NONE   = 0,
        /// <summary>
        /// The delegate receives this call when libcurl is requesting
        /// read access to the shared resource.
        /// </summary>
        CURL_LOCK_ACCESS_SHARED = 1,
        /// <summary>
        /// The delegate receives this call when libcurl is requesting
        /// write access to the shared resource.
        /// </summary>
        CURL_LOCK_ACCESS_SINGLE = 2,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURL_LOCK_ACCESS_LAST   = 3
    };

    /// <summary>
    /// Contains return codes from many of the functions in the
    /// <see cref="Share"/> class.
    /// </summary>
    public enum CURLSHcode
    {
        /// <summary>
        /// The function succeeded.
        /// </summary>
        CURLSHE_OK          = 0,
        /// <summary>
        /// A bad option was passed to <see cref="Share.SetOpt"/>.
        /// </summary>
        CURLSHE_BAD_OPTION  = 1,
        /// <summary>
        /// An attempt was made to pass an option to
        /// <see cref="Share.SetOpt"/> while the Share object is in use.
        /// </summary>
        CURLSHE_IN_USE      = 2,
        /// <summary>
        /// The <see cref="Share"/> object's internal handle is invalid.
        /// </summary>
        CURLSHE_INVALID     = 3,
        /// <summary>
        /// Out of memory. This is a severe problem.
        /// </summary>
        CURLSHE_NOMEM       = 4,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURLSHE_LAST        = 5
    };

    /// <summary>
    /// A member of this enumeration is passed to the function
    /// <see cref="Share.SetOpt"/> to configure a <see cref="Share"/>
    /// transfer. 
    /// </summary>
    public enum CURLSHoption
    {
        /// <summary>
        /// Start-of-enumeration; do not use in application code.
        /// </summary>
        CURLSHOPT_NONE          = 0,
        /// <summary>
        /// The parameter, which should be a member of the
        /// <see cref="CURLlockData"/> enumeration, specifies a type of
        /// data that should be shared.
        /// </summary>
        CURLSHOPT_SHARE         = 1,
        /// <summary>
        /// The parameter, which should be a member of the
        /// <see cref="CURLlockData"/> enumeration, specifies a type of
        /// data that should be unshared.
        /// </summary>
        CURLSHOPT_UNSHARE       = 2,
        /// <summary>
        /// The parameter should be a reference to a
        /// <see cref="Share.LockFunction"/> delegate. 
        /// </summary>
        CURLSHOPT_LOCKFUNC      = 3,
        /// <summary>
        /// The parameter should be a reference to a
        /// <see cref="Share.UnlockFunction"/> delegate. 
        /// </summary>
        CURLSHOPT_UNLOCKFUNC    = 4,
        /// <summary>
        /// The parameter allows you to specify an object reference that
        /// will passed to the <see cref="Share.LockFunction"/> delegate and
        /// the <see cref="Share.UnlockFunction"/> delegate. 
        /// </summary>
        CURLSHOPT_USERDATA      = 5,
        /// <summary>
        /// End-of-enumeration; do not use in application code.
        /// </summary>
        CURLSHOPT_LAST          = 6
    };

    /// <summary>
    /// A member of this enumeration is passed to the function
    /// <see cref="Curl.GetVersionInfo"/> 
    /// </summary>
    public enum CURLversion
    {
        /// <summary>
        /// Capabilities associated with the initial version of libcurl.
        /// </summary>
        CURLVERSION_FIRST  = 0,
        /// <summary>
        /// Capabilities associated with the second version of libcurl.
        /// </summary>
        CURLVERSION_SECOND = 1,
        /// <summary>
        /// Capabilities associated with the third version of libcurl.
        /// </summary>
        CURLVERSION_THIRD  = 2,
        /// <summary>
        /// Same as <c>CURLVERSION_THIRD</c>.
        /// </summary>
        CURLVERSION_NOW    = CURLVERSION_THIRD,
        /// <summary>
        /// End-of-enumeration marker; do not use in application code.
        /// </summary>
        CURLVERSION_LAST   = 3
    };

    /// <summary>
    /// A bitmask of libcurl features OR'd together as the value of the
    /// property <see cref="VersionInfoData.Features"/>. The feature
    /// bits are summarized in the table below.
    /// </summary>
    public enum CURLversionFeatureBitmask
    {
        /// <summary>
        /// Supports IPv6.
        /// </summary>
        CURL_VERSION_IPV6         = 0x01,
        /// <summary>
        /// Supports kerberos4 (when using FTP).
        /// </summary>
        CURL_VERSION_KERBEROS4    = 0x02,
        /// <summary>
        /// Supports SSL (HTTPS/FTPS).
        /// </summary>
        CURL_VERSION_SSL          = 0x04,
        /// <summary>
        /// Supports HTTP deflate using libz.
        /// </summary>
        CURL_VERSION_LIBZ         = 0x08,
        /// <summary>
        /// Supports HTTP NTLM (added in 7.10.6).
        /// </summary>
        CURL_VERSION_NTLM         = 0x10,
        /// <summary>
        /// Supports HTTP GSS-Negotiate (added in 7.10.6).
        /// </summary>
        CURL_VERSION_GSSNEGOTIATE = 0x20,
        /// <summary>
        /// libcurl was built with extra debug capabilities built-in. This
        /// is mainly of interest for libcurl hackers. (added in 7.10.6) 
        /// </summary>
        CURL_VERSION_DEBUG        = 0x40,
        /// <summary>
        /// libcurl was built with support for asynchronous name lookups,
        /// which allows more exact timeouts (even on Windows) and less
        /// blocking when using the multi interface. (added in 7.10.7) 
        /// </summary>
        CURL_VERSION_ASYNCHDNS    = 0x80,
        /// <summary>
        /// libcurl was built with support for SPNEGO authentication
        /// (Simple and Protected GSS-API Negotiation Mechanism, defined
        /// in RFC 2478.) (added in 7.10.8) 
        /// </summary>
        CURL_VERSION_SPNEGO       = 0x100,
        /// <summary>
        /// libcurl was built with support for large files.
        /// </summary>
        CURL_VERSION_LARGEFILE    = 0x200,
        /// <summary>
        /// libcurl was built with support for IDNA, domain names with
        /// international letters. 
        /// </summary>
        CURL_VERSION_IDN          = 0x400
    };

    /// <summary>
    /// The status code associated with an <see cref="Easy"/> object in a
    /// <see cref="Multi"/> operation. One of these is returned in response
    /// to reading the <see cref="MultiInfo.Msg"/> property.
    /// </summary>
    public enum CURLMSG
    {
        /// <summary>
        /// First entry in the enumeration, not used.
        /// </summary>
        CURLMSG_NONE = 0,
        /// <summary>
        /// The associated <see cref="Easy"/> object completed.
        /// </summary>
        CURLMSG_DONE = 1,
        /// <summary>
        /// End-of-enumeration marker, not used.
        /// </summary>
        CURLMSG_LAST = 2
    };

    /// <summary>
    /// Contains return codes for many of the functions in the
    /// <see cref="Multi"/> class.
    /// </summary>
    public enum CURLMcode
    {
        /// <summary>
        /// You should call <see cref="Multi.Perform"/> again before calling
        /// <see cref="Multi.Select"/>.
        /// </summary>
        CURLM_CALL_MULTI_PERFORM  = -1,
        /// <summary>
        /// The function succeded.
        /// </summary>
        CURLM_OK                  = 0,
        /// <summary>
        /// The internal <see cref="Multi"/> is bad.
        /// </summary>
        CURLM_BAD_HANDLE          = 1,
        /// <summary>
        /// One of the <see cref="Easy"/> handles associated with the
        /// <see cref="Multi"/> object is bad.
        /// </summary>
        CURLM_BAD_EASY_HANDLE     = 2,
        /// <summary>
        /// Out of memory. This is a severe problem.
        /// </summary>
        CURLM_OUT_OF_MEMORY       = 3,
        /// <summary>
        /// Internal error deep within the libcurl library.
        /// </summary>
        CURLM_INTERNAL_ERROR      = 4,
        /// <summary>
        /// End-of-enumeration marker, not used.
        /// </summary>
        CURLM_LAST                = 5
    };
}
