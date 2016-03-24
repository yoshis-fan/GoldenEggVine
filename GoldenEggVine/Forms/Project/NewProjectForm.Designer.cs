namespace GoldenEggVine.Forms.Project
{
	partial class NewProjectForm
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
			this.baseRomPathTextBox = new System.Windows.Forms.TextBox();
			this.projectParentDirTextBox = new System.Windows.Forms.TextBox();
			this.projectNameTextBox = new System.Windows.Forms.TextBox();
			this.browseBaseRomPathButton = new System.Windows.Forms.Button();
			this.baseRomPathLabel = new System.Windows.Forms.Label();
			this.projectParentDirLabel = new System.Windows.Forms.Label();
			this.projectNameLabel = new System.Windows.Forms.Label();
			this.browseProjectParentDirButton = new System.Windows.Forms.Button();
			this.createProjectButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// baseRomPathTextBox
			// 
			this.baseRomPathTextBox.Location = new System.Drawing.Point(12, 135);
			this.baseRomPathTextBox.Name = "baseRomPathTextBox";
			this.baseRomPathTextBox.ReadOnly = true;
			this.baseRomPathTextBox.Size = new System.Drawing.Size(315, 20);
			this.baseRomPathTextBox.TabIndex = 0;
			this.baseRomPathTextBox.TabStop = false;
			this.baseRomPathTextBox.Click += new System.EventHandler(this.browseBaseRomPathButton_Click);
			this.baseRomPathTextBox.TextChanged += new System.EventHandler(this.ValidateFormData);
			// 
			// projectParentDirTextBox
			// 
			this.projectParentDirTextBox.Location = new System.Drawing.Point(12, 77);
			this.projectParentDirTextBox.Name = "projectParentDirTextBox";
			this.projectParentDirTextBox.ReadOnly = true;
			this.projectParentDirTextBox.Size = new System.Drawing.Size(315, 20);
			this.projectParentDirTextBox.TabIndex = 0;
			this.projectParentDirTextBox.TabStop = false;
			this.projectParentDirTextBox.Click += new System.EventHandler(this.browseProjectParentDirButton_Click);
			// 
			// projectNameTextBox
			// 
			this.projectNameTextBox.Location = new System.Drawing.Point(12, 25);
			this.projectNameTextBox.Name = "projectNameTextBox";
			this.projectNameTextBox.Size = new System.Drawing.Size(191, 20);
			this.projectNameTextBox.TabIndex = 1;
			this.projectNameTextBox.Text = "Yoshi\'s Island Hacking Project";
			this.projectNameTextBox.TextChanged += new System.EventHandler(this.ValidateFormData);
			// 
			// browseBaseRomPathButton
			// 
			this.browseBaseRomPathButton.Location = new System.Drawing.Point(333, 135);
			this.browseBaseRomPathButton.Name = "browseBaseRomPathButton";
			this.browseBaseRomPathButton.Size = new System.Drawing.Size(75, 23);
			this.browseBaseRomPathButton.TabIndex = 3;
			this.browseBaseRomPathButton.Text = "Browse...";
			this.browseBaseRomPathButton.UseVisualStyleBackColor = true;
			this.browseBaseRomPathButton.Click += new System.EventHandler(this.browseBaseRomPathButton_Click);
			// 
			// baseRomPathLabel
			// 
			this.baseRomPathLabel.AutoSize = true;
			this.baseRomPathLabel.Location = new System.Drawing.Point(12, 119);
			this.baseRomPathLabel.Name = "baseRomPathLabel";
			this.baseRomPathLabel.Size = new System.Drawing.Size(95, 13);
			this.baseRomPathLabel.TabIndex = 4;
			this.baseRomPathLabel.Text = "Path to base ROM";
			// 
			// projectParentDirLabel
			// 
			this.projectParentDirLabel.AutoSize = true;
			this.projectParentDirLabel.Location = new System.Drawing.Point(12, 61);
			this.projectParentDirLabel.Name = "projectParentDirLabel";
			this.projectParentDirLabel.Size = new System.Drawing.Size(191, 13);
			this.projectParentDirLabel.TabIndex = 5;
			this.projectParentDirLabel.Text = "Directory to create the Project Folder in";
			// 
			// projectNameLabel
			// 
			this.projectNameLabel.AutoSize = true;
			this.projectNameLabel.Location = new System.Drawing.Point(12, 9);
			this.projectNameLabel.Name = "projectNameLabel";
			this.projectNameLabel.Size = new System.Drawing.Size(71, 13);
			this.projectNameLabel.TabIndex = 6;
			this.projectNameLabel.Text = "Project Name";
			// 
			// browseProjectParentDirButton
			// 
			this.browseProjectParentDirButton.Location = new System.Drawing.Point(333, 74);
			this.browseProjectParentDirButton.Name = "browseProjectParentDirButton";
			this.browseProjectParentDirButton.Size = new System.Drawing.Size(75, 23);
			this.browseProjectParentDirButton.TabIndex = 2;
			this.browseProjectParentDirButton.Text = "Browse...";
			this.browseProjectParentDirButton.UseVisualStyleBackColor = true;
			this.browseProjectParentDirButton.Click += new System.EventHandler(this.browseProjectParentDirButton_Click);
			// 
			// createProjectButton
			// 
			this.createProjectButton.Enabled = false;
			this.createProjectButton.Location = new System.Drawing.Point(12, 184);
			this.createProjectButton.Name = "createProjectButton";
			this.createProjectButton.Size = new System.Drawing.Size(396, 42);
			this.createProjectButton.TabIndex = 4;
			this.createProjectButton.Text = "Create Project";
			this.createProjectButton.UseVisualStyleBackColor = true;
			this.createProjectButton.Click += new System.EventHandler(this.createProjectButton_Click);
			// 
			// ProjectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 238);
			this.Controls.Add(this.createProjectButton);
			this.Controls.Add(this.browseProjectParentDirButton);
			this.Controls.Add(this.projectNameLabel);
			this.Controls.Add(this.projectParentDirLabel);
			this.Controls.Add(this.baseRomPathLabel);
			this.Controls.Add(this.browseBaseRomPathButton);
			this.Controls.Add(this.projectNameTextBox);
			this.Controls.Add(this.projectParentDirTextBox);
			this.Controls.Add(this.baseRomPathTextBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Create a new Project";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox baseRomPathTextBox;
		private System.Windows.Forms.TextBox projectParentDirTextBox;
		private System.Windows.Forms.TextBox projectNameTextBox;
		private System.Windows.Forms.Button browseBaseRomPathButton;
		private System.Windows.Forms.Label baseRomPathLabel;
		private System.Windows.Forms.Label projectParentDirLabel;
		private System.Windows.Forms.Label projectNameLabel;
		private System.Windows.Forms.Button browseProjectParentDirButton;
		private System.Windows.Forms.Button createProjectButton;
	}
}