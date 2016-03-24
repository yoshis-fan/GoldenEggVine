namespace GoldenEggVine.Forms
{
    partial class ScreenExitEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenExitEditor));
			this.LevelTypeTC = new System.Windows.Forms.TabControl();
			this.LevelTP = new System.Windows.Forms.TabPage();
			this.ExittypeCB = new System.Windows.Forms.ComboBox();
			this.DestYTB = new System.Windows.Forms.TextBox();
			this.DestXTB = new System.Windows.Forms.TextBox();
			this.DestLevelTB = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.MiniBattleTP = new System.Windows.Forms.TabPage();
			this.MiniBattletypeCB = new System.Windows.Forms.ComboBox();
			this.ReturnYTB = new System.Windows.Forms.TextBox();
			this.ReturnXTB = new System.Windows.Forms.TextBox();
			this.ReturnLevelTB = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.ScreenIndexTB = new System.Windows.Forms.TextBox();
			this.ScreenIndexCB = new System.Windows.Forms.ComboBox();
			this.ExitEnabledCkB = new System.Windows.Forms.CheckBox();
			this.ApplyB = new System.Windows.Forms.Button();
			this.DiscardB = new System.Windows.Forms.Button();
			this.LevelTypeTC.SuspendLayout();
			this.LevelTP.SuspendLayout();
			this.MiniBattleTP.SuspendLayout();
			this.SuspendLayout();
			// 
			// LevelTypeTC
			// 
			this.LevelTypeTC.Controls.Add(this.LevelTP);
			this.LevelTypeTC.Controls.Add(this.MiniBattleTP);
			this.LevelTypeTC.Location = new System.Drawing.Point(12, 36);
			this.LevelTypeTC.Name = "LevelTypeTC";
			this.LevelTypeTC.SelectedIndex = 0;
			this.LevelTypeTC.Size = new System.Drawing.Size(239, 201);
			this.LevelTypeTC.TabIndex = 0;
			// 
			// LevelTP
			// 
			this.LevelTP.Controls.Add(this.ExittypeCB);
			this.LevelTP.Controls.Add(this.DestYTB);
			this.LevelTP.Controls.Add(this.DestXTB);
			this.LevelTP.Controls.Add(this.DestLevelTB);
			this.LevelTP.Controls.Add(this.label4);
			this.LevelTP.Controls.Add(this.label3);
			this.LevelTP.Controls.Add(this.label2);
			this.LevelTP.Controls.Add(this.label1);
			this.LevelTP.Location = new System.Drawing.Point(4, 22);
			this.LevelTP.Name = "LevelTP";
			this.LevelTP.Padding = new System.Windows.Forms.Padding(3);
			this.LevelTP.Size = new System.Drawing.Size(231, 175);
			this.LevelTP.TabIndex = 0;
			this.LevelTP.Text = "Level";
			this.LevelTP.UseVisualStyleBackColor = true;
			// 
			// ExittypeCB
			// 
			this.ExittypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ExittypeCB.FormattingEnabled = true;
			this.ExittypeCB.Location = new System.Drawing.Point(9, 133);
			this.ExittypeCB.Name = "ExittypeCB";
			this.ExittypeCB.Size = new System.Drawing.Size(207, 21);
			this.ExittypeCB.TabIndex = 7;
			// 
			// DestYTB
			// 
			this.DestYTB.Location = new System.Drawing.Point(179, 81);
			this.DestYTB.MaxLength = 2;
			this.DestYTB.Name = "DestYTB";
			this.DestYTB.Size = new System.Drawing.Size(37, 20);
			this.DestYTB.TabIndex = 6;
			this.DestYTB.TextChanged += new System.EventHandler(this.DestYTB_TextChanged);
			// 
			// DestXTB
			// 
			this.DestXTB.Location = new System.Drawing.Point(179, 55);
			this.DestXTB.MaxLength = 2;
			this.DestXTB.Name = "DestXTB";
			this.DestXTB.Size = new System.Drawing.Size(37, 20);
			this.DestXTB.TabIndex = 5;
			this.DestXTB.TextChanged += new System.EventHandler(this.DestXTB_TextChanged);
			// 
			// DestLevelTB
			// 
			this.DestLevelTB.Location = new System.Drawing.Point(179, 29);
			this.DestLevelTB.MaxLength = 2;
			this.DestLevelTB.Name = "DestLevelTB";
			this.DestLevelTB.Size = new System.Drawing.Size(37, 20);
			this.DestLevelTB.TabIndex = 4;
			this.DestLevelTB.TextChanged += new System.EventHandler(this.DestLevelTB_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Destination Exittype";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Destination Y-Coordinate";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Destination X-Coordinate";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Destination Level";
			// 
			// MiniBattleTP
			// 
			this.MiniBattleTP.Controls.Add(this.MiniBattletypeCB);
			this.MiniBattleTP.Controls.Add(this.ReturnYTB);
			this.MiniBattleTP.Controls.Add(this.ReturnXTB);
			this.MiniBattleTP.Controls.Add(this.ReturnLevelTB);
			this.MiniBattleTP.Controls.Add(this.label5);
			this.MiniBattleTP.Controls.Add(this.label6);
			this.MiniBattleTP.Controls.Add(this.label7);
			this.MiniBattleTP.Controls.Add(this.label8);
			this.MiniBattleTP.Location = new System.Drawing.Point(4, 22);
			this.MiniBattleTP.Name = "MiniBattleTP";
			this.MiniBattleTP.Padding = new System.Windows.Forms.Padding(3);
			this.MiniBattleTP.Size = new System.Drawing.Size(231, 175);
			this.MiniBattleTP.TabIndex = 1;
			this.MiniBattleTP.Text = "Mini Battle";
			this.MiniBattleTP.UseVisualStyleBackColor = true;
			// 
			// MiniBattletypeCB
			// 
			this.MiniBattletypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MiniBattletypeCB.FormattingEnabled = true;
			this.MiniBattletypeCB.Location = new System.Drawing.Point(9, 133);
			this.MiniBattletypeCB.Name = "MiniBattletypeCB";
			this.MiniBattletypeCB.Size = new System.Drawing.Size(207, 21);
			this.MiniBattletypeCB.TabIndex = 15;
			// 
			// ReturnYTB
			// 
			this.ReturnYTB.Location = new System.Drawing.Point(179, 81);
			this.ReturnYTB.MaxLength = 2;
			this.ReturnYTB.Name = "ReturnYTB";
			this.ReturnYTB.Size = new System.Drawing.Size(37, 20);
			this.ReturnYTB.TabIndex = 14;
			this.ReturnYTB.TextChanged += new System.EventHandler(this.ReturnYTB_TextChanged);
			// 
			// ReturnXTB
			// 
			this.ReturnXTB.Location = new System.Drawing.Point(179, 55);
			this.ReturnXTB.MaxLength = 2;
			this.ReturnXTB.Name = "ReturnXTB";
			this.ReturnXTB.Size = new System.Drawing.Size(37, 20);
			this.ReturnXTB.TabIndex = 13;
			this.ReturnXTB.TextChanged += new System.EventHandler(this.ReturnXTB_TextChanged);
			// 
			// ReturnLevelTB
			// 
			this.ReturnLevelTB.Location = new System.Drawing.Point(179, 29);
			this.ReturnLevelTB.MaxLength = 2;
			this.ReturnLevelTB.Name = "ReturnLevelTB";
			this.ReturnLevelTB.Size = new System.Drawing.Size(37, 20);
			this.ReturnLevelTB.TabIndex = 12;
			this.ReturnLevelTB.TextChanged += new System.EventHandler(this.ReturnLevelTB_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Destination Mini Battle";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 84);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(103, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Return Y-Coordinate";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 58);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(103, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Return X-Coordinate";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 32);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 13);
			this.label8.TabIndex = 8;
			this.label8.Text = "Return Level";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(16, 13);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(70, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Screen Index";
			// 
			// ScreenIndexTB
			// 
			this.ScreenIndexTB.Location = new System.Drawing.Point(150, 10);
			this.ScreenIndexTB.MaxLength = 2;
			this.ScreenIndexTB.Name = "ScreenIndexTB";
			this.ScreenIndexTB.Size = new System.Drawing.Size(35, 20);
			this.ScreenIndexTB.TabIndex = 8;
			this.ScreenIndexTB.TextChanged += new System.EventHandler(this.ScreenIndexTB_TextChanged);
			// 
			// ScreenIndexCB
			// 
			this.ScreenIndexCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScreenIndexCB.FormattingEnabled = true;
			this.ScreenIndexCB.Location = new System.Drawing.Point(191, 9);
			this.ScreenIndexCB.Name = "ScreenIndexCB";
			this.ScreenIndexCB.Size = new System.Drawing.Size(56, 21);
			this.ScreenIndexCB.TabIndex = 7;
			this.ScreenIndexCB.SelectedIndexChanged += new System.EventHandler(this.ScreenIndexCB_SelectedIndexChanged);
			// 
			// ExitEnabledCkB
			// 
			this.ExitEnabledCkB.AutoSize = true;
			this.ExitEnabledCkB.Location = new System.Drawing.Point(131, 36);
			this.ExitEnabledCkB.Name = "ExitEnabledCkB";
			this.ExitEnabledCkB.Size = new System.Drawing.Size(116, 17);
			this.ExitEnabledCkB.TabIndex = 9;
			this.ExitEnabledCkB.Text = "Enable Screen Exit";
			this.ExitEnabledCkB.UseVisualStyleBackColor = true;
			this.ExitEnabledCkB.Click += new System.EventHandler(this.ExitEnabledCkB_Click);
			// 
			// ApplyB
			// 
			this.ApplyB.Enabled = false;
			this.ApplyB.Location = new System.Drawing.Point(13, 240);
			this.ApplyB.Name = "ApplyB";
			this.ApplyB.Size = new System.Drawing.Size(124, 23);
			this.ApplyB.TabIndex = 10;
			this.ApplyB.Text = "Apply";
			this.ApplyB.UseVisualStyleBackColor = true;
			this.ApplyB.Click += new System.EventHandler(this.ApplyB_Click);
			// 
			// DiscardB
			// 
			this.DiscardB.Enabled = false;
			this.DiscardB.Location = new System.Drawing.Point(143, 239);
			this.DiscardB.Name = "DiscardB";
			this.DiscardB.Size = new System.Drawing.Size(104, 23);
			this.DiscardB.TabIndex = 11;
			this.DiscardB.Text = "Discard";
			this.DiscardB.UseVisualStyleBackColor = true;
			this.DiscardB.Click += new System.EventHandler(this.DiscardB_Click);
			// 
			// ScreenExitEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(260, 422);
			this.Controls.Add(this.DiscardB);
			this.Controls.Add(this.ApplyB);
			this.Controls.Add(this.ExitEnabledCkB);
			this.Controls.Add(this.ScreenIndexTB);
			this.Controls.Add(this.ScreenIndexCB);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.LevelTypeTC);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScreenExitEditor";
			this.Text = "Screen Exit Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenExitEditor_FormClosing);
			this.LevelTypeTC.ResumeLayout(false);
			this.LevelTP.ResumeLayout(false);
			this.LevelTP.PerformLayout();
			this.MiniBattleTP.ResumeLayout(false);
			this.MiniBattleTP.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TabControl LevelTypeTC;
		private System.Windows.Forms.TabPage LevelTP;
		private System.Windows.Forms.ComboBox ExittypeCB;
		private System.Windows.Forms.TextBox DestYTB;
		private System.Windows.Forms.TextBox DestXTB;
		private System.Windows.Forms.TextBox DestLevelTB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage MiniBattleTP;
		private System.Windows.Forms.ComboBox MiniBattletypeCB;
		private System.Windows.Forms.TextBox ReturnYTB;
		private System.Windows.Forms.TextBox ReturnXTB;
		private System.Windows.Forms.TextBox ReturnLevelTB;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ScreenIndexTB;
        private System.Windows.Forms.ComboBox ScreenIndexCB;
        private System.Windows.Forms.CheckBox ExitEnabledCkB;
		private System.Windows.Forms.Button ApplyB;
		private System.Windows.Forms.Button DiscardB;
    }
}