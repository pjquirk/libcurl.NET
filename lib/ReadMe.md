This is the README file for binaries. Included in the distribution are the following three files:

- `libcurl.dll`:   This was built with libcurl 7.13.0, openssl-0.9.7e, c-ares-1.2.1 and zlib-1.2.1
- `LibCurlNet.dll`: The DLL referenced by your program.
- `LibCurlShim.dll`: A DLL used internally by LibCurlNet.dll to interface with libcurl.dll.

Two more notes:

- If you have Visual Studio 2015 or higher, you can rebuild LibCurlNet.dll and LibCurlShim.dll by opening LibCurlNet.sln in the parent directory.
- Executables built from the samples directory should go here as well, since LibCurlNet.dll is now an unsigned, private assembly.
