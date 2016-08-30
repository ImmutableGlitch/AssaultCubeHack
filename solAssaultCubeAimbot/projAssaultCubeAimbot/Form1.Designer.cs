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
            this.lblZposEn = new System.Windows.Forms.Label();
            this.lblYposEn = new System.Windows.Forms.Label();
            this.lblXposEn = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHealth = new System.Windows.Forms.Label();
            this.lblHealthEn = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.btnAttach = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrProcess
            // 
            this.tmrProcess.Enabled = true;
            this.tmrProcess.Interval = 10;
            this.tmrProcess.Tick += new System.EventHandler(this.tmrProcess_Tick);
            // 
            // lblXpos
            // 
            this.lblXpos.AutoSize = true;
            this.lblXpos.Location = new System.Drawing.Point(52, 81);
            this.lblXpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXpos.Name = "lblXpos";
            this.lblXpos.Size = new System.Drawing.Size(16, 17);
            this.lblXpos.TabIndex = 1;
            this.lblXpos.Text = "0";
            // 
            // lblYpos
            // 
            this.lblYpos.AutoSize = true;
            this.lblYpos.Location = new System.Drawing.Point(52, 110);
            this.lblYpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYpos.Name = "lblYpos";
            this.lblYpos.Size = new System.Drawing.Size(16, 17);
            this.lblYpos.TabIndex = 2;
            this.lblYpos.Text = "0";
            // 
            // lblZpos
            // 
            this.lblZpos.AutoSize = true;
            this.lblZpos.Location = new System.Drawing.Point(52, 139);
            this.lblZpos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZpos.Name = "lblZpos";
            this.lblZpos.Size = new System.Drawing.Size(16, 17);
            this.lblZpos.TabIndex = 3;
            this.lblZpos.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "x";
            // 
            // lblZposEn
            // 
            this.lblZposEn.AutoSize = true;
            this.lblZposEn.Location = new System.Drawing.Point(288, 139);
            this.lblZposEn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZposEn.Name = "lblZposEn";
            this.lblZposEn.Size = new System.Drawing.Size(20, 17);
            this.lblZposEn.TabIndex = 9;
            this.lblZposEn.Text = " 0";
            // 
            // lblYposEn
            // 
            this.lblYposEn.AutoSize = true;
            this.lblYposEn.Location = new System.Drawing.Point(288, 110);
            this.lblYposEn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYposEn.Name = "lblYposEn";
            this.lblYposEn.Size = new System.Drawing.Size(20, 17);
            this.lblYposEn.TabIndex = 8;
            this.lblYposEn.Text = " 0";
            // 
            // lblXposEn
            // 
            this.lblXposEn.AutoSize = true;
            this.lblXposEn.Location = new System.Drawing.Point(288, 81);
            this.lblXposEn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXposEn.Name = "lblXposEn";
            this.lblXposEn.Size = new System.Drawing.Size(20, 17);
            this.lblXposEn.TabIndex = 7;
            this.lblXposEn.Text = " 0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 168);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "HP";
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(52, 168);
            this.lblHealth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(16, 17);
            this.lblHealth.TabIndex = 13;
            this.lblHealth.Text = "0";
            // 
            // lblHealthEn
            // 
            this.lblHealthEn.AutoSize = true;
            this.lblHealthEn.Location = new System.Drawing.Point(288, 168);
            this.lblHealthEn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHealthEn.Name = "lblHealthEn";
            this.lblHealthEn.Size = new System.Drawing.Size(20, 17);
            this.lblHealthEn.TabIndex = 15;
            this.lblHealthEn.Text = " 0";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(155, 110);
            this.lblDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(16, 17);
            this.lblDistance.TabIndex = 17;
            this.lblDistance.Text = "d";
            // 
            // btnAttach
            // 
            this.btnAttach.BackColor = System.Drawing.Color.Red;
            this.btnAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttach.Location = new System.Drawing.Point(118, 13);
            this.btnAttach.Margin = new System.Windows.Forms.Padding(4);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(100, 28);
            this.btnAttach.TabIndex = 18;
            this.btnAttach.Text = "Attach";
            this.btnAttach.UseVisualStyleBackColor = false;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Player";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(253, 58);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "Enemy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 168);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "HP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 139);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(259, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(259, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "x";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 201);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAttach);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblHealthEn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.lblZposEn);
            this.Controls.Add(this.lblYposEn);
            this.Controls.Add(this.lblXposEn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblZpos);
            this.Controls.Add(this.lblYpos);
            this.Controls.Add(this.lblXpos);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AssaultCube v1.2 - Aimbot";
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
        private System.Windows.Forms.Label lblZposEn;
        private System.Windows.Forms.Label lblYposEn;
        private System.Windows.Forms.Label lblXposEn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Label lblHealthEn;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}

