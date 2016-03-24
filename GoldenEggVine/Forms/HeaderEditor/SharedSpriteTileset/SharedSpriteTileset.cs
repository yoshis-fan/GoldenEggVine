using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoldenEggVine.Forms.HeaderEditor.ShareSpriteTileset
{
	public partial class SharedSpriteTileset : Form
	{
		private HeaderEditor _headereditor;
		private int _spritetilesetindex;
		private Timer _timer;

		private SharedSpriteTileset()
		{
			InitializeComponent();
			_timer = new Timer()
			{
				Interval = 250,
			};
			_timer.Tick += new EventHandler(t_Tick);
			_timer.Start();
		}

		public SharedSpriteTileset(HeaderEditor headereditor, int spritetilesetindex) : this()
		{
			WarningL.Text = "Warning!\nAll of the Levels mentioned on the right will be affected by changing Sprite Tileset Index 0x" + String.Format("{0:X2}", spritetilesetindex) + "! You can open one or more Levels by double-clicking them in the list.";
			_headereditor = headereditor;
			_spritetilesetindex = spritetilesetindex;
			//Iterates over each Level and gets Levels, which share 'spritetilesetindex'
			foreach (GoldenEggVine.ROMRelated.LevelRelated.CLevelData ld in headereditor._mainform._yirom.GetAllLevelData())
			{
				if (ld.LevelHeader().GetHeaderValue(ROMRelated.LevelRelated.EHeaderValue.SprTS) == spritetilesetindex)
				{
					this.SharedSpriteTilesetsLB.Items.Add(String.Format("{0:X2}", ld.GetLevelIndex()));
				}
			}
		}

		/// <summary>
		/// Loads Level from List of crossovers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SharedSpriteTilesetsLB_DoubleClick(object sender, EventArgs e)
		{
			if (this.SharedSpriteTilesetsLB.Items.Count > 0)
			{
				_headereditor._mainform.LoadLevel(Int32.Parse((string)this.SharedSpriteTilesetsLB.SelectedItem, System.Globalization.NumberStyles.AllowHexSpecifier));
			}
		}

		/// <summary>
		/// Validates the Cursor being inside the Form, if not, close
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void t_Tick(object sender, EventArgs e)
		{
			if(((Cursor.Position.X < this.DesktopLocation.X - 5) || (Cursor.Position.X > this.DesktopLocation.X + this.Width + 5)) || ((Cursor.Position.Y < this.DesktopLocation.Y - 5) || (Cursor.Position.Y > this.DesktopLocation.Y + this.Height + 5)))
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
		private void SharedSpriteTileset_Load(object sender, EventArgs e)
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
			_headereditor._spritetileseteditor = new SpriteTilesetEditor.SpriteTilesetEditor(_spritetilesetindex);
			_headereditor._spritetileseteditor.ShowDialog();
			this.Close();
		}
	}
}