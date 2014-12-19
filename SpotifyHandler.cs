using System.Diagnostics;
using System.IO;
using System.Linq;

namespace spotifycontrol {
    class SpotifyHandler {
		private const int WM_APPCOMMAND = 0x0319;

		private const int SPOTIFY_APPCOMMAND_NEXT = 720896;
		private const int SPOTIFY_APPCOMMAND_PLAY_PAUSE = 917504;
		private const int SPOTIFY_APPCOMMAND_PREVIOUS = 786432;
		private const int SPOTIFY_APPCOMMAND_STOP = 851968;
		private const int SPOTIFY_APPCOMMAND_VOLUMEDOWN = 589824;
		private const int SPOTIFY_APPCOMMAND_VOLUMEUP = 655360;

		/// <summary>
		/// Stored process, for handling.
		/// </summary>
	    private Process process;

		/// <summary>
		/// Attempt to fetch the current Spotify process, or start it.
		/// </summary>
	    private Process Process {
		    get {
				// Attempt to fetch the process from active processes.
			    this.process = Process.GetProcessesByName("spotify").FirstOrDefault();

			    if (this.process != null)
					return this.process;

				// If it fails, try to find and start Spotify.
			    var path = findSpotifyExec();

			    if (!File.Exists(path))
				    return null;

				var startInfo = new ProcessStartInfo(path);

				this.process = new Process { StartInfo = startInfo };
				this.process.Start();

				return this.process;
		    }
	    }

		/// <summary>
		/// Scans the local drives for an install of Spotify.
		/// </summary>
	    private static string findSpotifyExec() {
		    var folders = (from drive in DriveInfo.GetDrives() where drive.IsReady select drive.RootDirectory.FullName).ToList();
		    var index = 0;

		    while (true) {
			    try {
				    var temp = Directory.GetDirectories(
					    folders[index],
					    "*",
					    SearchOption.TopDirectoryOnly);

				    folders.AddRange(temp);
			    }
			    catch {}

			    try {
				    var files = Directory.GetFiles(
					    folders[index],
					    "spotify.exe",
					    SearchOption.TopDirectoryOnly);

				    if (files.Length > 0)
					    return files[0];
			    }
				catch {}

			    index++;
			    if (index == folders.Count)
				    break;
		    }

		    return null;
	    }

		/// <summary>
		/// Send the "next track" command to the open Spotify instance.
		/// </summary>
        public void Next() {
            this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_NEXT);
        }

		/// <summary>
		/// Send the "play/pause track" command to the open Spotify instance.
		/// </summary>
        public void PlayPause() {
			this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_PLAY_PAUSE);
        }

		/// <summary>
		/// Send the "previous track" command to the open Spotify instance.
		/// </summary>
        public void Previous() {
			this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_PREVIOUS);
        }

		/// <summary>
		/// Send the "stop track" command to the open Spotify instance.
		/// </summary>
        public void Stop() {
			this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_STOP);
        }

		/// <summary>
		/// Send the "decrease volume" command to the open Spotify instance.
		/// </summary>
        public void VolumeDown() {
			this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_VOLUMEDOWN);
        }

		/// <summary>
		/// Send the "increase volume" command to the open Spotify instance.
		/// </summary>
        public void VolumeUp() {
			this.Process.SendMessage(WM_APPCOMMAND, 0x00000000, SPOTIFY_APPCOMMAND_VOLUMEUP);
        }

		/// <summary>
		/// Get the window title of the Spotify window.
		/// </summary>
	    public string GetWindowTitle() {
			var title = "";

			if (this.Process != null)
				title = this.Process.MainWindowTitle;

			return title;
		}
    }
}