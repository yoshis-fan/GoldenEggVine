using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GoldenEggVine.Forms.Testing
{
	public partial class SetupEmulatorPathForm : Form
	{
		public string EmulatorPath { get { return EmulatorPathTextBox.Text; } }

		public SetupEmulatorPathForm()
		{
			InitializeComponent();
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog()
				{
					Filter = "Executables (*.exe)|*.exe",
					Title = "Choose an SNES Emulator"
				};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				EmulatorPathTextBox.Text = ofd.FileName;
			}
		}

		private void ValidateForm(object sender, EventArgs e)
		{
			ConfirmButton.Enabled = File.Exists(EmulatorPath);
		}

		private void ConfirmButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
