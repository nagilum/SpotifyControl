using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace spotifycontrol {
    public partial class fmTray : Form {
        public fmTray() {
            InitializeComponent();
        }

	    readonly KeyboardHook keyboardHook = new KeyboardHook();
	    readonly SpotifyHandler spotifyHandler = new SpotifyHandler();

        // default modifiers
	    private const ModifierKeys modifierKeys = spotifycontrol.ModifierKeys.Control | spotifycontrol.ModifierKeys.Alt;

	    // default main-keys
	    private const Keys keysNext = Keys.PageDown;
	    private const Keys keysPlayPause = Keys.Home;
	    private const Keys keysPrevious = Keys.PageUp;
	    private const Keys keysStop = Keys.End;
	    private const Keys keysVolumeDown = Keys.Down;
	    private const Keys keysVolumeUp = Keys.Up;

		/// <summary>
		/// App start, perform various startup tasks.
		/// </summary>
	    private void fmTray_Load(object sender, EventArgs e) {
            // Assign hotkey-handler.
            this.keyboardHook.KeyPressed += this.keyboardHook_KeyPressed;

            // Assign hotkeys.
			this.keyboardHook.RegisterHotKey(modifierKeys, keysNext);
			this.keyboardHook.RegisterHotKey(modifierKeys, keysPlayPause);
			this.keyboardHook.RegisterHotKey(modifierKeys, keysPrevious);
			this.keyboardHook.RegisterHotKey(modifierKeys, keysStop);
			this.keyboardHook.RegisterHotKey(modifierKeys, keysVolumeDown);
			this.keyboardHook.RegisterHotKey(modifierKeys, keysVolumeUp);

            // Hide windows from view + alt+tab.
            this.ShowInTaskbar = false;
             this.Hide();

			// Check if the program is part of the Windows startup.
		    this.miStartAtWindowsStartup.Checked = this.checkIfProgramIsInStartup();
	    }

		/// <summary>
		/// Triggers after the form is shown.
		/// </summary>
	    private void fmTray_Shown(object sender, EventArgs e) {
			// Display current playing song.
			var currentSong = this.getCurrentPlayingSong();
			if (currentSong != null)
				this.niTray.ShowBalloonTip(
					3,
					"Spotify Control",
					"Playing " + currentSong,
					ToolTipIcon.Info);
	    }

		/// <summary>
		/// Determine which global hotkey was triggered and perform associated command.
		/// </summary>
        private void keyboardHook_KeyPressed(object sender, KeyPressedEventArgs e) {
	        if (e.Modifier == modifierKeys && e.Key == keysNext) // next
		        this.performSpotifyCommand("next");
	        else if (e.Modifier == modifierKeys && e.Key == keysPlayPause) // play/pause
				this.performSpotifyCommand("play/pause");
	        else if (e.Modifier == modifierKeys && e.Key == keysPrevious) // previous
				this.performSpotifyCommand("previous");
	        else if (e.Modifier == modifierKeys && e.Key == keysStop) // stop
				this.performSpotifyCommand("stop");
	        else if (e.Modifier == modifierKeys && e.Key == keysVolumeDown) // volume down
				this.performSpotifyCommand("volume down");
	        else if (e.Modifier == modifierKeys && e.Key == keysVolumeUp) // volume up
				this.performSpotifyCommand("volume up");
        }

		/// <summary>
		/// Close and exit the application.
		/// </summary>
        private void miExit_Click(object sender, EventArgs e) {
            this.Close();
        }

		/// <summary>
		/// Set/unset SpotifyControl to start at Windows startup.
		/// </summary>
		private void miStartAtWindowsStartup_Click(object sender, EventArgs e) {
			var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

			if (key == null)
				return;

			if (this.miStartAtWindowsStartup.Checked)
				key.DeleteValue(Application.ProductName);
			else
				key.SetValue(Application.ProductName, Application.ExecutablePath);

			this.miStartAtWindowsStartup.Checked = !this.miStartAtWindowsStartup.Checked;
		}

		/// <summary>
		/// Check if the SpotifyControl app is set to start at Windows startup.
		/// </summary>
	    private bool checkIfProgramIsInStartup() {
		    var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

			if (key == null)
				return false;

			var value = key.GetValue(Application.ProductName);

			if (value == null)
				return false;

			var value_ins = value.ToString();

			return value_ins != "0";
		}

		/// <summary>
		/// Perform a specific command inside Spotify and show a balloon tip indicating what.
		/// </summary>
		/// <param name="command">Command to send.</param>
	    private void performSpotifyCommand(string command) {
		    if (string.IsNullOrWhiteSpace(command))
			    return;

		    string balloonTipText = null;

		    switch (command) {
			    case "next":
					this.spotifyHandler.Next();
				    balloonTipText = "Next Track";
				    break;

				case "previous":
					this.spotifyHandler.Previous();
					balloonTipText = "Previous Track";
				    break;

				case "play/pause":
					this.spotifyHandler.PlayPause();
					balloonTipText = "Play/Pause";
				    break;

				case "stop":
					this.spotifyHandler.Stop();
					balloonTipText = "Stop";
				    break;

				case "volume up":
					this.spotifyHandler.VolumeUp();
					balloonTipText = "Volume Up";
				    break;

				case "volume down":
					this.spotifyHandler.VolumeDown();
					balloonTipText = "Volume Down";
				    break;
		    }

			// We need to sleep for a while to let the Spotify UI update itself with the new title.
			Thread.Sleep(100);

		    if (string.IsNullOrWhiteSpace(balloonTipText))
			    return;

			var currentSong = this.getCurrentPlayingSong();
			if (currentSong != null)
				balloonTipText += "\r\n" + currentSong;

			this.niTray.ShowBalloonTip(
				1,
				"Spotify Control",
				balloonTipText,
				ToolTipIcon.Info);
	    }

		/// <summary>
		/// Get the current playing song on Spotify.
		/// </summary>
		/// <returns>Artist and song.</returns>
	    private string getCurrentPlayingSong() {
			var title = this.spotifyHandler.GetWindowTitle();

		    if (title.StartsWith("Spotify - "))
			    title = title.Substring("Spotify - ".Length).Trim();

			if (title == "Spotify")
				return null;

		    return string.IsNullOrWhiteSpace(title) ? null : title;
	    }
    }
}