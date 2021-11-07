using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Game.Control.Keyboard
{
	/// <summary>
	/// Description of InterceptKeys.
	/// </summary>
	class InterceptKeys
	{
		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x0100;
		const int WM_KEYUP = 0x0101;
		
		static IntPtr _hookID = IntPtr.Zero;
		static LowLevelKeyboardProc _proc = HookCallback;
		
		public static event OnKeyUpDelegate OnUp;
		public static event OnKeyDownDelegate OnDown;
		public delegate void OnKeyUpDelegate(Keys key);
		public delegate void OnKeyDownDelegate(Keys key);
		
		public static void OnProcessClose(){
			UnhookWindowsHookEx(_hookID);
		}
		public static void OnProcessStart(){
			using (Process curProcess = Process.GetCurrentProcess())
				using (ProcessModule curModule = curProcess.MainModule)
			{
				_hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc,
				                           GetModuleHandle(curModule.ModuleName), 0);
			}
		}
		
		delegate IntPtr LowLevelKeyboardProc(
			int nCode, IntPtr wParam, IntPtr lParam);
		
		static IntPtr HookCallback(
			int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0){
				int vkCode = Marshal.ReadInt32(lParam);
				Keys key = (Keys)vkCode;
				if (wParam == (IntPtr)WM_KEYUP){
					if (OnUp != null){
						try{
							OnUp(key);
						}catch{
						}
					}
				}
				else if (wParam == (IntPtr)WM_KEYDOWN){
					if (OnDown != null){
						try{
							OnDown(key);
						}
						catch{
						}
					}
				}
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr SetWindowsHookEx(int idHook,
		                                      LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool UnhookWindowsHookEx(IntPtr hhk);
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
		                                    IntPtr wParam, IntPtr lParam);
		
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr GetModuleHandle(string lpModuleName);
	}
}
