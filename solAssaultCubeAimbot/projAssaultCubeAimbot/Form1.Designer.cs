namespace projAssaultCubeAimbot
{
    partial class Form1
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
            this.tmrProcess = new System.Windows.Forms.Timer(this.components);
            this.lblXpos = new System.Windows.Forms.Label();
            this.lblYpos = new System.Windows.Forms.Label();
            this.lblZpos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHealth = new System.Windows.Forms.Label();
            this.btnAttach = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrProcess
            // 
            this.tmrProcess.Enabled = true;
            this.tmrProcess.Interval = 10;
            this.tmrProcess.Tick += new System.EventHandler(this.TmrProcess_Tick);
            // 
            // lblXpos
            // 
            this.lblXpos.AutoSize = true;
            this.lblXpos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblXpos.Location = new System.Drawing.Point(380, 9);
            this.lblXpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXpos.Name = "lblXpos";
            this.lblXpos.Size = new System.Drawing.Size(16, 17);
            this.lblXpos.TabIndex = 1;
            this.lblXpos.Text = "0";
            this.lblXpos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYpos
            // 
            this.lblYpos.AutoSize = true;
            this.lblYpos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblYpos.Location = new System.Drawing.Point(380, 38);
            this.lblYpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYpos.Name = "lblYpos";
            this.lblYpos.Size = new System.Drawing.Size(16, 17);
            this.lblYpos.TabIndex = 2;
            this.lblYpos.Text = "0";
            this.lblYpos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZpos
            // 
            this.lblZpos.AutoSize = true;
            this.lblZpos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblZpos.Location = new System.Drawing.Point(380, 67);
            this.lblZpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZpos.Name = "lblZpos";
            this.lblZpos.Size = new System.Drawing.Size(16, 17);
            this.lblZpos.TabIndex = 3;
            this.lblZpos.Text = "0";
            this.lblZpos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(298, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(298, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(298, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(282, 96);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Health";
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHealth.Location = new System.Drawing.Point(380, 96);
            this.lblHealth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(16, 17);
            this.lblHealth.TabIndex = 13;
            this.lblHealth.Text = "0";
            this.lblHealth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAttach
            // 
            this.btnAttach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(61)))), ((int)(((byte)(65)))));
            this.btnAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttach.Location = new System.Drawing.Point(296, 134);
            this.btnAttach.Margin = new System.Windows.Forms.Padding(4);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(100, 28);
            this.btnAttach.TabIndex = 18;
            this.btnAttach.Text = "Enabled";
            this.btnAttach.UseVisualStyleBackColor = false;
            this.btnAttach.Click += new System.EventHandler(this.BtnAttach_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(13, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 153);
            this.label4.TabIndex = 21;
            this.label4.Text = "F1 - Save coordinates A\r\nF2 - Save coordinates B\r\n\r\nF3 - Load coordinates A\r\nF4 -" +
    " Load coordinates B\r\n\r\nF5 - 999 Health\r\n\r\nRight Mouse(Hold) - Aimbot";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(452, 179);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAttach);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblZpos);
            this.Controls.Add(this.lblYpos);
            this.Controls.Add(this.lblXpos);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AssaultCube v1.2 - Teleport & Aimbot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrProcess;
        private System.Windows.Forms.Label lblXpos;
        private System.Windows.Forms.Label lblYpos;
        private System.Windows.Forms.Label lblZpos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Label label4;
    }
}

