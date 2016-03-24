using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoldenEggVine.Forms.HeaderEditor.SharedMusic
{
	public partial class SharedMusic : Form
	{
		internal static bool _changevalue = false;

		private HeaderEditor _headereditor;
		private int _trackindex;
		private Timer _timer;

		private SharedMusic()
		{
			InitializeComponent();
			_timer = new Timer()
			{
				Interval = 250,
			};
			_timer.Tick += new EventHandler(t_Tick);
			_timer.Start();
		}

		public SharedMusic(HeaderEditor headereditor, int trackindex) : this()
		{
			WarningL.Text = "Warning!\nAll of the Levels mentioned on the right will be affected by changing Item Card Settings of Track Index 0x" + String.Format("{0:X1}", trackindex) + "! You can open one or more Levels by double-clicking them in the list.";
			_headereditor = headereditor;
			_trackindex = trackindex;
			//Iterates over each Level and gets Levels, which share 'trackindex'
			foreach (GoldenEggVine.ROMRelated.LevelRelated.CLevelData ld in headereditor._mainform._yirom.GetAllLevelData())
			{
				if (ld.LevelHeader().GetHeaderValue(ROMRelated.LevelRelated.EHeaderValue.Music) == trackindex)
				{
					this.SharedMusicLB.Items.Add(String.Format("{0:X2}", ld.GetLevelIndex()));
				}
			}
		}

		/// <summary>
		/// Loads Level from List of crossovers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SharedMusicLB_DoubleClick(object sender, EventArgs e)
		{
			if (this.SharedMusicLB.Items.Count > 0)
			{
				_headereditor._mainform.LoadLevel(Int32.Parse((string)this.SharedMusicLB.SelectedItem, System.Globalization.NumberStyles.AllowHexSpecifier));
			}
		}

		/// <summary>
		/// Validates the Cursor being inside the Form, if not, close
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void t_Tick(object sender, EventArgs e)
		{
			if (((Cursor.Position.X < this.DesktopLocation.X - 80) || (Cursor.Position.X > this.DesktopLocation.X + this.Width + 80)) || ((Cursor.Position.Y < this.DesktopLocation.Y - 80) || (Cursor.Position.Y > this.DesktopLocation.Y + this.Height + 80)))
			{
				_timer.Stop();
				_timer.Dispose();
				this.Close();
			}
		}

		/// <summary>
		/// Makes Form appear near the Cursor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SharedMusic_Load(object sender, EventArgs e)
		{
			this.DesktopLocation = new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10);
		}

		/// <summary>
		/// Loads Sprite Tileset Editor with specified Tileset to edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProceedB_Click(object sender, EventArgs e)
		{
			_changevalue = true;
			this.Close();
		}

		private void SharedMusic_Resize(object sender, EventArgs e)
		{
			ProceedB.Top = this.Height - 70;
			SharedMusicLB.Height = this.Height - 91;
		}
	}
}