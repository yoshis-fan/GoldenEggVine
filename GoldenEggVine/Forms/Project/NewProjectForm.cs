using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoldenEggVine.Forms.Project
{
	public partial class NewProjectForm : Form
	{
		public string BaseRom { get { return baseRomPathTextBox.Text; } }
		public string ProjectParentDir { get { return projectParentDirTextBox.Text; } }
		public string ProjectName { get { return projectNameTextBox.Text; } }

		public NewProjectForm()
		{
			InitializeComponent();
		}

		private void browseBaseRomPathButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog() {
				Filter = "SNES ROMs (*.smc/*.sfc)|*.smc;*.sfc",
				Title = "Select a headered Yoshi's Island (U) (1.0) ROM"
			};
			if(ofd.ShowDialog() == DialogResult.OK)
			{
				baseRomPathTextBox.Text = ofd.FileName;
			}
		}

		private void browseProjectParentDirButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog() {
				ShowNewFolderButton = true
			};
			if(fbd.ShowDialog() == DialogResult.OK)
			{
				projectParentDirTextBox.Text = fbd.SelectedPath;
			}
		}

		private void ValidateFormData(object sender, EventArgs e)
		{
			createProjectButton.Enabled = false;
			// Project can be created if the ROM file exists, it's a valid ROM, the directory exists and the project name can be converted into a folder name
			if(File.Exists(baseRomPathTextBox.Text) && Directory.Exists(projectParentDirTextBox.Text) && projectNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
			{
				try
				{
					new ROMRelated.CYIROM(baseRomPathTextBox.Text);
					createProjectButton.Enabled = true;
				}
				catch(Exception)
				{
					baseRomPathTextBox.Text = "";
					MessageBox.Show("Please select a valid Yoshi's Island ROM.\nIt must be headered (filesize 2,097,664 bytes) and the North-American 1.0 version");
				}
			}
		}

		private void createProjectButton_Click(object sender, EventArgs e)
		{
			var targetDir = Path.Combine(projectParentDirTextBox.Text, projectNameTextBox.Text);
			if(Directory.Exists(targetDir))
			{
				var result = MessageBox.Show("The directory you selected already exists. You can choose to delete it and its contents and create the project or choose a different directory. Do you want to cancel and choose a different directory?", "WARNING!! DANGER!!!", MessageBoxButtons.YesNo);
				if(result == DialogResult.Yes)
				{
					return;
				}
				else
				{
					Directory.Delete(targetDir, true);
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
