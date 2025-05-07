# NativeAOTStatic

## Purpose
This is a Frankenstein (Fronkensteen for the cultured folk) project to show that it's technically possible to compile C# .NET down to a static binary without any OS dependencies (besides syscall's and file system paths).

This was most definitely a "can it be done" rather than a "should it be done" endeavor so YMMV.

Resultant binary size for the "Hello, World" project clocks in at `1108K` but it might be possible to reduce that. See [trimming options](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/trimming-options).

## Validation
### CentOS 6
```
PS C:\Users\ashurack\source\repos\NativeAOTStatic> docker run --rm -it -v ${PWD}:/app -w /app centos:6 sh
sh-4.1# cat /etc/redhat-release
CentOS release 6.10 (Final)
sh-4.1#
sh-4.1# file NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic: ELF 64-bit LSB executable, x86-64, version 1 (SYSV), statically linked, stripped
sh-4.1#
sh-4.1# NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
Hello, World!
```

### CentOS 5
```
PS C:\Users\ashurack\source\repos\NativeAOTStatic> docker run --rm -it -v ${PWD}:/app -w /app centos:5 sh
sh-3.2# cat /etc/redhat-release
CentOS release 5.11 (Final)
sh-3.2#
sh-3.2# ldd NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
        not a dynamic executable
sh-3.2#
sh-3.2# NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
Hello, World!
```

## Helpful Tools
- [MSBuild Binary and Structured Log Viewer](https://msbuildlog.com/)
- [sizoscope](https://github.com/MichalStrehovsky/sizoscope)