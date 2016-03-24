namespace GoldenEggVine.Forms.HeaderEditor.ShareSpriteTileset
{
	partial class SharedSpriteTileset
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
			this.SharedSpriteTilesetsLB = new System.Windows.Forms.ListBox();
			this.WarningL = new System.Windows.Forms.Label();
			this.ProceedB = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SharedSpriteTilesetsLB
			// 
			this.SharedSpriteTilesetsLB.FormattingEnabled = true;
			this.SharedSpriteTilesetsLB.Location = new System.Drawing.Point(191, 9);
			this.SharedSpriteTilesetsLB.Margin = new System.Windows.Forms.Padding(0);
			this.SharedSpriteTilesetsLB.Name = "SharedSpriteTilesetsLB";
			this.SharedSpriteTilesetsLB.Size = new System.Drawing.Size(60, 82);
			this.SharedSpriteTilesetsLB.TabIndex = 0;
			this.SharedSpriteTilesetsLB.DoubleClick += new System.EventHandler(this.SharedSpriteTilesetsLB_DoubleClick);
			// 
			// WarningL
			// 
			this.WarningL.AutoSize = true;
			this.WarningL.ForeColor = System.Drawing.Color.Red;
			this.WarningL.Location = new System.Drawing.Point(12, 9);
			this.WarningL.MaximumSize = new System.Drawing.Size(174, 0);
			this.WarningL.Name = "WarningL";
			this.WarningL.Size = new System.Drawing.Size(10, 13);
			this.WarningL.TabIndex = 1;
			this.WarningL.Text = "-";
			// 
			// ProceedB
			// 
			this.ProceedB.Location = new System.Drawing.Point(12, 103);
			this.ProceedB.Name = "ProceedB";
			this.ProceedB.Size = new System.Drawing.Size(239, 23);
			this.ProceedB.TabIndex = 2;
			this.ProceedB.Text = "Proceed";
			this.ProceedB.UseVisualStyleBackColor = true;
			this.ProceedB.Click += new System.EventHandler(this.ProceedB_Click);
			// 
			// SharedSpriteTileset
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(260, 139);
			this.Controls.Add(this.ProceedB);
			this.Controls.Add(this.WarningL);
			this.Controls.Add(this.SharedSpriteTilesetsLB);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SharedSpriteTileset";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Shared Sprite Tilesets";
			this.Load += new System.EventHandler(this.SharedSpriteTileset_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox SharedSpriteTilesetsLB;
		private System.Windows.Forms.Label WarningL;
		private System.Windows.Forms.Button ProceedB;
	}
}