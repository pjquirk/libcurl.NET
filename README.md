# libcurl.NET
This is a fork/update of the 2005 package of the same name by Jeff Phillips, hosted [here](https://sourceforge.net/projects/libcurl-net/).  

## What I've Changed
- Updated the native cURL libraries to version 7.40.0
  - The native cURL libraries were built using [Curl for Windows](https://github.com/peters/curl-for-windows) and statically linked into a single DLL, `libcurl.dll`.  The native libraries are linked against the VS 2012 toolset. 
- Added an x64 configuration
  - Doing this required breaking [`curl_shim_formadd`](https://github.com/pjquirk/libcurl.NET/blob/9dc19b98f941e122a477e3fc767b6bddbdf59777/shim/LibCurlShim.c#L412) since the [`__asm` keyword is not supported by the compiler in x64 builds](http://mariusbancila.ro/blog/2010/10/17/no-more-inline-asm-in-vc-on-x64/) (and it wouldn't be correct for x64 architecture anyways).

## TODO
- Upload to Nuget
- Create a different package distribution that uses [Fody Costura](https://github.com/Fody/Costura) to embed the native cURL and shim libraries in the C# assembly, making distribution easier.
- Update the API to be friendlier, and expose any functionality added between 7.13.0 and 7.40.0 (roughly a decade).
- Fix `curl_shim_formadd` to not use inline assembly.
- Statically-link libcurl into the shim so I don't need both, and so I can get rid of the `GetProcAddress` calls that it performs.
- Add more/better tests.  Now I just have a few smoke tests to make sure things aren't horribly broken.
- Make an easier process for updating the cURL native libraries.


_Original README from Jeff Phillips is below:_

--------
$Id: README,v 1.1.1.1 2005/02/17 22:20:29 jeffreyphillips Exp $

libcurl.NET 1.3 (c) 2004, 2005 Jeff Phillips jeff@jeffp.net

This is the initial public source code release of this product on sourceforge.net.

This release contains all of the binaries necessary to run the sample applications in the samples subdirectory, but ensure that you read the file ReadMe.samples in that directory for more information on building and running the samples. You should be able to build the samples with only the csc or vbc command-line compilers.

If you want to rebuild LibCurlNet.dll and LibCurlShim.dll in the bin subdirectory, you can do so by opening up the LibCurlNet.sln file in this directory with Microsoft Visual Studio 7.1 or higher. These files will be rebuilt in the bin subdirectory.

The libcurl.dll file in the bin subdirectory was built with SSL, c-ares (asynchronous DNS) and zlib (for compression) linked in. I will try to update this file as libcurl evolves.

API documentation is provided in the file LibCurlNet.chm in the doc subdirectory. You should be able to view it in Microsoft's HTML Help viewer (hh.exe). If you change the API and wish to rebuild the documentation, you'll need to use the NDOC tool (minimum version 1.3) available at ndoc.sourceforge.net.

The CURLOPT_SSL_CTX_FUNCTION issue remains: The Easy class has a delegate for this function. But the delegate is called with a reference to an SSLContext object which is just a thin wrapper of a native OpenSSL SSL_CTX pointer. I didn't provide a managed wrapper for an SSL_CTX pointer and don't expect to do so in the future. So if you want to work with the SSL_CTX pointer (SSLContext.Context), you'll have to do so using your own native methods.

