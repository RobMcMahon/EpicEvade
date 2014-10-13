EpicEvade
=========

Epic AV Evasion

This application takes in base64encoded shellcode, encrypts the shellcode then outputs an exe file that will run the shellcode.
The shellcode is encrypted with AES and not decrypted until just before it's run. 

EpicEvade.exe "base64EncodedShellCodeHere"

This outputs a cs file and an exe file. The exe file is the built cs file.

Currently this only builds as 32bit. Soon I'll include an option to go between 32 and 64 bit.
The salt is hardcoded I will make it randomly generated as soon as I have time.
