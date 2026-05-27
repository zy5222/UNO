namespace UNO
{
    partial class frmMain
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
            this.pnlComputer = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnUno = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblComputerCount = new System.Windows.Forms.Label();
            this.UnoTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pnlComputer
            // 
            this.pnlComputer.BackColor = System.Drawing.Color.Transparent;
            this.pnlComputer.Location = new System.Drawing.Point(10, 10);
            this.pnlComputer.Name = "pnlComputer";
            this.pnlComputer.Size = new System.Drawing.Size(1023, 184);
            this.pnlComputer.TabIndex = 0;
            this.pnlComputer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlComputer_Paint);
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.Location = new System.Drawing.Point(10, 200);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(1023, 268);
            this.pnlCenter.TabIndex = 1;
            this.pnlCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCenter_Paint);
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayer.Location = new System.Drawing.Point(10, 466);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(1020, 193);
            this.pnlPlayer.TabIndex = 2;
            this.pnlPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPlayer_Paint);
            this.pnlPlayer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlPlayer_MouseClick);
            // 
            // btnDraw
            // 
            this.btnDraw.BackColor = System.Drawing.Color.Orange;
            this.btnDraw.ForeColor = System.Drawing.Color.White;
            this.btnDraw.Location = new System.Drawing.Point(1117, 147);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(120, 80);
            this.btnDraw.TabIndex = 3;
            this.btnDraw.Text = "抽牌";
            this.btnDraw.UseVisualStyleBackColor = false;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click_1);
            // 
            // btnUno
            // 
            this.btnUno.BackColor = System.Drawing.Color.Red;
            this.btnUno.ForeColor = System.Drawing.Color.White;
            this.btnUno.Location = new System.Drawing.Point(1117, 251);
            this.btnUno.Name = "btnUno";
            this.btnUno.Size = new System.Drawing.Size(120, 80);
            this.btnUno.TabIndex = 4;
            this.btnUno.Text = "UNO!";
            this.btnUno.UseVisualStyleBackColor = false;
            this.btnUno.Click += new System.EventHandler(this.btnUno_Click_1);
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.SteelBlue;
            this.btnNewGame.ForeColor = System.Drawing.Color.White;
            this.btnNewGame.Location = new System.Drawing.Point(1117, 357);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(120, 80);
            this.btnNewGame.TabIndex = 5;
            this.btnNewGame.Text = "新遊戲";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click_1);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.Location = new System.Drawing.Point(1117, 466);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 80);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "離開";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(2, 645);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1023, 75);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblComputerCount
            // 
            this.lblComputerCount.Font = new System.Drawing.Font("Microsoft JhengHei", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblComputerCount.ForeColor = System.Drawing.Color.White;
            this.lblComputerCount.Location = new System.Drawing.Point(1097, 24);
            this.lblComputerCount.Name = "lblComputerCount";
            this.lblComputerCount.Size = new System.Drawing.Size(154, 120);
            this.lblComputerCount.TabIndex = 8;
            this.lblComputerCount.Text = "電腦:   7張";
            this.lblComputerCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UnoTimer
            // 
            this.UnoTimer.Tick += new System.EventHandler(this.UnoTimer_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1274, 729);
            this.Controls.Add(this.lblComputerCount);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnUno);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pnlPlayer);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlComputer);
            this.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(1300, 800);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNO遊戲";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlComputer;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlPlayer;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnUno;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblComputerCount;
        private System.Windows.Forms.Timer UnoTimer;
    }
}

