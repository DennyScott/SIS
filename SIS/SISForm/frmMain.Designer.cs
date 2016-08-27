namespace SIS
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

         protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.   
        //Do not modify it using the code editor.
        private System.Windows.Forms.MainMenu mnuMain;

        private System.Windows.Forms.MenuItem mnuFile;

        private System.Windows.Forms.MenuItem mnuExit;

        private System.Windows.Forms.MenuItem mnuHelp;

        private System.Windows.Forms.MenuItem mnuAbout;

        private System.Windows.Forms.ListBox lstStatus;

        private System.Windows.Forms.Button btnBroadcast;

        private System.Windows.Forms.TextBox txtBroadcast;

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.ListBox lstPlayers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPM;
        private System.Windows.Forms.Button btnKick;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label Label1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.txtBroadcast = new System.Windows.Forms.TextBox();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstPlayers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPM = new System.Windows.Forms.Button();
            this.btnKick = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnuMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // mnuFile
            // 
            this.mnuFile.Enabled = false;
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuExit});
            this.mnuFile.ShowShortcut = false;
            this.mnuFile.Text = "";
            this.mnuFile.Visible = false;
            // 
            // mnuExit
            // 
            this.mnuExit.Enabled = false;
            this.mnuExit.Index = 0;
            this.mnuExit.ShowShortcut = false;
            this.mnuExit.Text = "";
            this.mnuExit.Visible = false;
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Enabled = false;
            this.mnuHelp.Index = 1;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            this.mnuHelp.ShowShortcut = false;
            this.mnuHelp.Text = "";
            this.mnuHelp.Visible = false;
            // 
            // mnuAbout
            // 
            this.mnuAbout.Enabled = false;
            this.mnuAbout.Index = 0;
            this.mnuAbout.ShowShortcut = false;
            this.mnuAbout.Text = "";
            this.mnuAbout.Visible = false;
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstStatus.Enabled = false;
            this.lstStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstStatus.IntegralHeight = false;
            this.lstStatus.ItemHeight = 20;
            this.lstStatus.Location = new System.Drawing.Point(493, 187);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstStatus.Size = new System.Drawing.Size(0, 0);
            this.lstStatus.TabIndex = 0;
            this.lstStatus.Visible = false;
            // 
            // txtBroadcast
            // 
            this.txtBroadcast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBroadcast.Enabled = false;
            this.txtBroadcast.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBroadcast.Location = new System.Drawing.Point(493, 187);
            this.txtBroadcast.MaxLength = 0;
            this.txtBroadcast.Name = "txtBroadcast";
            this.txtBroadcast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBroadcast.Size = new System.Drawing.Size(0, 26);
            this.txtBroadcast.TabIndex = 0;
            this.txtBroadcast.Visible = false;
            this.txtBroadcast.WordWrap = false;
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBroadcast.Enabled = false;
            this.btnBroadcast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBroadcast.ImageIndex = 0;
            this.btnBroadcast.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBroadcast.Location = new System.Drawing.Point(493, 187);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBroadcast.Size = new System.Drawing.Size(0, 0);
            this.btnBroadcast.TabIndex = 0;
            this.btnBroadcast.Visible = false;
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblInstructions.Enabled = false;
            this.lblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblInstructions.ImageIndex = 0;
            this.lblInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInstructions.Location = new System.Drawing.Point(493, 187);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblInstructions.Size = new System.Drawing.Size(0, 0);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Visible = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label1.Enabled = false;
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label1.ImageIndex = 0;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(493, 187);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(0, 0);
            this.Label1.TabIndex = 0;
            this.Label1.Visible = false;
            // 
            // lstPlayers
            // 
            this.lstPlayers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstPlayers.Enabled = false;
            this.lstPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstPlayers.IntegralHeight = false;
            this.lstPlayers.ItemHeight = 20;
            this.lstPlayers.Location = new System.Drawing.Point(493, 187);
            this.lstPlayers.Name = "lstPlayers";
            this.lstPlayers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstPlayers.Size = new System.Drawing.Size(0, 0);
            this.lstPlayers.TabIndex = 0;
            this.lstPlayers.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Enabled = false;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.ImageIndex = 0;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(493, 187);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(0, 0);
            this.label2.TabIndex = 0;
            this.label2.Visible = false;
            // 
            // btnPM
            // 
            this.btnPM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPM.Enabled = false;
            this.btnPM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPM.ImageIndex = 0;
            this.btnPM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPM.Location = new System.Drawing.Point(493, 187);
            this.btnPM.Name = "btnPM";
            this.btnPM.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPM.Size = new System.Drawing.Size(0, 0);
            this.btnPM.TabIndex = 0;
            this.btnPM.Visible = false;
            // 
            // btnKick
            // 
            this.btnKick.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKick.Enabled = false;
            this.btnKick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKick.ImageIndex = 0;
            this.btnKick.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnKick.Location = new System.Drawing.Point(493, 187);
            this.btnKick.Name = "btnKick";
            this.btnKick.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnKick.Size = new System.Drawing.Size(0, 0);
            this.btnKick.TabIndex = 0;
            this.btnKick.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Enabled = false;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox1.Location = new System.Drawing.Point(493, 187);
            this.textBox1.MaxLength = 0;
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(0, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            this.textBox1.WordWrap = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Enabled = false;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.ImageIndex = 0;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(493, 187);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(0, 0);
            this.label3.TabIndex = 0;
            this.label3.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(986, 374);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnKick);
            this.Controls.Add(this.btnPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstPlayers);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.txtBroadcast);
            this.Controls.Add(this.lstStatus);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

