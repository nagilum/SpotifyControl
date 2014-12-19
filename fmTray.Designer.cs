using System.Security.AccessControl;

namespace spotifycontrol
{
    partial class fmTray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmTray));
			this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
			this.ctTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.miStartAtWindowsStartup = new System.Windows.Forms.ToolStripMenuItem();
			this.ctTray.SuspendLayout();
			this.SuspendLayout();
			// 
			// niTray
			// 
			this.niTray.ContextMenuStrip = this.ctTray;
			this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
			this.niTray.Text = "SpotifyControl";
			this.niTray.Visible = true;
			// 
			// ctTray
			// 
			this.ctTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStartAtWindowsStartup,
            this.miExit});
			this.ctTray.Name = "ctTray";
			this.ctTray.Size = new System.Drawing.Size(205, 48);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(204, 22);
			this.miExit.Text = "Exit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// miStartAtWindowsStartup
			// 
			this.miStartAtWindowsStartup.Name = "miStartAtWindowsStartup";
			this.miStartAtWindowsStartup.Size = new System.Drawing.Size(204, 22);
			this.miStartAtWindowsStartup.Text = "Start at Windows Startup";
			this.miStartAtWindowsStartup.Click += new System.EventHandler(this.miStartAtWindowsStartup_Click);
			// 
			// fmTray
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(116, 0);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fmTray";
			this.Text = "fmTray";
			this.Load += new System.EventHandler(this.fmTray_Load);
	        this.Shown += new System.EventHandler(this.fmTray_Shown);
			this.ctTray.ResumeLayout(false);
			this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon niTray;
		private System.Windows.Forms.ContextMenuStrip ctTray;
        private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.ToolStripMenuItem miStartAtWindowsStartup;
    }
}