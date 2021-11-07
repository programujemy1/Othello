using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

namespace Game.Control
{
	/// <summary>
	/// Description of Mouse.
	/// </summary>
	public class Mouse
	{
		const int MOUSEEVENTF_LEFTDOWN = 0x02;
		const int MOUSEEVENTF_LEFTUP = 0x04;
		const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		const int MOUSEEVENTF_RIGHTUP = 0x10;
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
		
		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int X, int Y);
		
		public static void LeftUp()
		{
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}
		
		public static void LeftDown()
		{
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
		}
		
		public static void RightUp()
		{
			mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
		}
		
		public static void RightDown()
		{
			mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
		}
		
		public static void Drag(int sx, int sy, int ex, int ey)
		{
			SetCursorPos(sx, sy);
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			SetCursorPos(ex, ey);
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}
		
		public static void LeftClick(int x, int y, int delay = 0)
		{
			SetCursorPos(x, y);
			if (delay <= 0){
				mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
				return;
			}
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			Thread.Sleep(delay);
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}
		
		public static void RightClick(int x, int y, int delay = 0)
		{
			SetCursorPos(x, y);
			if (delay <= 0){
				mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
				return;
			}
			mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
			Thread.Sleep(delay);
			mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
		}
	}
}