using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace spotifycontrol {
	static class ProcessExtender {
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

		/// <summary>
		/// Send a message to the process using Win32 SendMessage in user32.dll.
		/// </summary>
		public static int SendMessage(this Process process, int wMsg, int wParam, int lParam) {
			return SendMessage(process.Handle, wMsg, wParam, lParam);
		}
	}
}
