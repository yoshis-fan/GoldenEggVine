namespace GoldenEggVine.Forms.HeaderEditor.SharedMusic
{
	partial class SharedMusic
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
			this.ProceedB = new System.Windows.Forms.Button();
			this.WarningL = new System.Windows.Forms.Label();
			this.SharedMusicLB = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// ProceedB
			// 
			this.ProceedB.Location = new System.Drawing.Point(12, 103);
			this.ProceedB.Name = "ProceedB";
			this.ProceedB.Size = new System.Drawing.Size(239, 23);
			this.ProceedB.TabIndex = 5;
			this.ProceedB.Text = "Proceed";
			this.ProceedB.UseVisualStyleBackColor = true;
			this.ProceedB.Click += new System.EventHandler(this.ProceedB_Click);
			// 
			// WarningL
			// 
			this.WarningL.AutoSize = true;
			this.WarningL.ForeColor = System.Drawing.Color.Red;
			this.WarningL.Location = new System.Drawing.Point(12, 9);
			this.WarningL.MaximumSize = new System.Drawing.Size(174, 0);
			this.WarningL.Name = "WarningL";
			this.WarningL.Size = new System.Drawing.Size(10, 13);
			this.WarningL.TabIndex = 4;
			this.WarningL.Text = "-";
			// 
			// SharedMusicLB
			// 
			this.SharedMusicLB.FormattingEnabled = true;
			this.SharedMusicLB.Location = new System.Drawing.Point(191, 9);
			this.SharedMusicLB.Margin = new System.Windows.Forms.Padding(0);
			this.SharedMusicLB.Name = "SharedMusicLB";
			this.SharedMusicLB.Size = new System.Drawing.Size(60, 82);
			this.SharedMusicLB.TabIndex = 3;
			this.SharedMusicLB.DoubleClick += new System.EventHandler(this.SharedMusicLB_DoubleClick);
			// 
			// SharedMusic
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(260, 139);
			this.Controls.Add(this.ProceedB);
			this.Controls.Add(this.WarningL);
			this.Controls.Add(this.SharedMusicLB);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximumSize = new System.Drawing.Size(276, 519);
			this.MinimumSize = new System.Drawing.Size(276, 173);
			this.Name = "SharedMusic";
			this.Text = "Shared Music";
			this.Load += new System.EventHandler(this.SharedMusic_Load);
			this.Resize += new System.EventHandler(this.SharedMusic_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ProceedB;
		private System.Windows.Forms.Label WarningL;
		private System.Windows.Forms.ListBox SharedMusicLB;
	}
}