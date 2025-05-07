# NativeAOTStatic

## Purpose
This is a Frankenstein (Fronkensteen for the cultured folk) project to show that it's technically possible to compile C# .NET down to a static binary without any OS dependencies (besides syscall's and file system paths).

This was most definitely a "can it be done" rather than a "should it be done" endeavor so YMMV.

## Validation
```
docker run --rm -it -v ${PWD}:/app -w /app centos:6 sh
sh-4.1# file NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic: ELF 64-bit LSB executable, x86-64, version 1 (SYSV), statically linked, stripped
sh-4.1#
sh-4.1# NativeAOTStatic/bin/Release/net9.0/linux-musl-x64/native/NativeAOTStatic
Hello, World!
```
