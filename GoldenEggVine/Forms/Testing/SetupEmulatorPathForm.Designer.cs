namespace GoldenEggVine.Forms.Testing
{
	partial class SetupEmulatorPathForm
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
			this.EmulatorPathLabel = new System.Windows.Forms.Label();
			this.EmulatorPathTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.ConfirmButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// EmulatorPathLabel
			// 
			this.EmulatorPathLabel.AutoSize = true;
			this.EmulatorPathLabel.Location = new System.Drawing.Point(13, 13);
			this.EmulatorPathLabel.Name = "EmulatorPathLabel";
			this.EmulatorPathLabel.Size = new System.Drawing.Size(117, 13);
			this.EmulatorPathLabel.TabIndex = 0;
			this.EmulatorPathLabel.Text = "Path to SNES Emulator";
			// 
			// EmulatorPathTextBox
			// 
			this.EmulatorPathTextBox.Location = new System.Drawing.Point(13, 30);
			this.EmulatorPathTextBox.Name = "EmulatorPathTextBox";
			this.EmulatorPathTextBox.ReadOnly = true;
			this.EmulatorPathTextBox.Size = new System.Drawing.Size(254, 20);
			this.EmulatorPathTextBox.TabIndex = 3;
			this.EmulatorPathTextBox.Click += new System.EventHandler(this.BrowseButton_Click);
			this.EmulatorPathTextBox.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(277, 27);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseButton.TabIndex = 1;
			this.BrowseButton.Text = "Browse...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// ConfirmButton
			// 
			this.ConfirmButton.Enabled = false;
			this.ConfirmButton.Location = new System.Drawing.Point(13, 85);
			this.ConfirmButton.Name = "ConfirmButton";
			this.ConfirmButton.Size = new System.Drawing.Size(339, 38);
			this.ConfirmButton.TabIndex = 2;
			this.ConfirmButton.Text = "Use this one";
			this.ConfirmButton.UseVisualStyleBackColor = true;
			this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
			// 
			// SetupEmulatorPathForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 135);
			this.Controls.Add(this.ConfirmButton);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.EmulatorPathTextBox);
			this.Controls.Add(this.EmulatorPathLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetupEmulatorPathForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Setup the path for your testing Emulator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label EmulatorPathLabel;
		private System.Windows.Forms.TextBox EmulatorPathTextBox;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.Button ConfirmButton;
	}
}