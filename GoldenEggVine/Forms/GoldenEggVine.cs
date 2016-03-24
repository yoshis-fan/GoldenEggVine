using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

using GoldenEggVine.Labels;
using System.Diagnostics;
using GoldenEggVine.ROMRelated;
using GoldenEggVine.EditorRelated;
using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;

namespace GoldenEggVine.Forms
{
	/*
    public struct STSPal
    {
        public int _levelindex;
        public int _ts;
        public int _pal;

        public STSPal(int levelindex, int ts, int pal)
        {
            _levelindex = levelindex;
            _ts = ts;
            _pal = pal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool operator==(STSPal a, STSPal b)
        {
            return a._ts == b._ts && a._pal == b._pal;
        }

        public static bool operator!=(STSPal a, STSPal b)
        {
            return !(a == b);
        }

        public static bool operator>(STSPal a, STSPal b)
        {
            if (a._ts > b._ts)
            {
                return true;
            }
            else if(a._ts == b._ts)
            {
                return a._pal > b._pal;
            }
            //a._ts < b._ts
            else
            {
                return false;
            }
        }

        public static bool operator<(STSPal a, STSPal b)
        {
            if(a._ts < b._ts)
            {
                return true;
            }
            else if(a._ts == b._ts)
            {
                return a._pal < b._pal;
            }
            //a._ts > b._ts
            else
            {
                return false;
            }
        }

        public static List<STSPal> NaiveSort(List<STSPal> tspal)
        {
            List<STSPal> sorted = new List<STSPal>();
            List<STSPal> copy = new List<STSPal>(tspal);
            while(copy.Count > 0)
            {
                STSPal smallest = new STSPal(Int32.MaxValue, Int32.MaxValue, Int32.MaxValue);
                for(int i = 0; i < copy.Count; i++)
                {
                    if(copy[i] < smallest)
                    {
                        smallest = copy[i];
                    }
                }
                sorted.Add(smallest);
                copy.Remove(smallest);
            }
            return sorted;
        }

        public override string ToString()
        {
            return String.Format("{0:X2}",_levelindex) + "   " + String.Format("{0:X2}",_ts) + "   " + String.Format("{0:X2}",_pal);
        }
    }
	*/

	// TODO Replace all of the CYIROM references here with the data from the project files

	public partial class Mainform : Form
	{
		private enum EEntity { Objects = 0, Sprites = 1, ScreenExits = 2 }

		//The ROM-Class with all the ROMbytes in it
		//Class contains LEVELDATA, ENTRANCES, MIDWAYS
		internal CYIROM _yirom;

		// The currently opened project
		internal CProject _project;

		//Contains content such as Data from Asset files and Global Program Settings
		internal CLoadedContent _loadedcontent;

		//Forms with the Levels
		private List<LevelEditor> _openedlevels = new List<LevelEditor>();
		internal LevelEditor _currentlevel;

		//The OutOfBoundsSettings
		private ToolStripMenuItem[] _OutOfBoundsOffsets;


		//The Form for inserting Objects and Sprites
		internal ObjectSpriteSelector _objectspriteselector;
		//The Form for editing the Header
		internal HeaderEditor.HeaderEditor _headereditor;
		//the Form for editing the Screen Exits
		internal ScreenExitEditor _screenexiteditor;
		//The Form for editing the Palettes
		internal PaletteEditor _paletteeditor;



		public Mainform()
		{
			InitializeComponent();
			LoadContent();
			LoadEditingTools();
			CGlobalDrawing.GiveLoadedContent(_loadedcontent);
			ToggleEditorToolsEnabled(false);
		}

		private void LoadEditingTools()
		{
			_objectspriteselector = new ObjectSpriteSelector();
			_headereditor = new HeaderEditor.HeaderEditor(this);
			_screenexiteditor = new ScreenExitEditor(this);
			_paletteeditor = new PaletteEditor(this);
		}

		private void LoadContent()
		{
			_OutOfBoundsOffsets = new ToolStripMenuItem[] { OOBTiles0, OOBTiles2, OOBTiles4, OOBTiles8, OOBTiles16 };

			_loadedcontent = new CLoadedContent();
			_loadedcontent._levellist = new CLevelLabels(Properties.Resources.LevelList.Split('\n'));

			_loadedcontent._bg1tslist = new CBG1TSLabels(Properties.Resources.BG1TSList.Split('\n'));
			_loadedcontent._bg2tslist = new CBG2TSLabels(Properties.Resources.BG2TSList.Split('\n'));
			_loadedcontent._bg3tslist = new CBG3TSLabels(Properties.Resources.BG3TSList.Split('\n'));

			_loadedcontent._bg1pallist = new CBG1PalLabels(Properties.Resources.BG1PalList.Split('\n'));
			_loadedcontent._bg2pallist = new CBG2PalLabels(Properties.Resources.BG2PalList.Split('\n'));
			_loadedcontent._bg3pallist = new CBG3PalLabels(Properties.Resources.BG3PalList.Split('\n'));
			_loadedcontent._sprpallist = new CSprPalLabels(Properties.Resources.SprPalList.Split('\n'));

			_loadedcontent._levelmodelist = new CLevelModeLabels(Properties.Resources.LevelModeList.Split('\n'));

			_loadedcontent._musiclist = new CMusicLabels(Properties.Resources.MusicList.Split('\n'));

			_loadedcontent._exitlist = new CExitLabels(Properties.Resources.ExitList.Split('\n'));
			_loadedcontent._minibattlelist = new CMiniBattleLabels(Properties.Resources.MinibattleList.Split('\n'));

			_loadedcontent._globalsettings = new CGlobalSettings();
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			AdjustLevelEditorToolsSize();
		}

		private void AdjustLevelEditorToolsSize()
		{
			this.mainPanel.Location = new Point(2, this.fileMenuStrip.Height + 2);
			this.mainPanel.Size = new Size(this.Width - 20, this.Height - 40 - this.fileMenuStrip.Height);

			this.dockTools.Location = new Point(0, HeightOfControls(this.romMenuStrip, this.romToolStrip));
			this.dockTools.Size = new Size(this.mainPanel.Width, this.mainPanel.Height - HeightOfControls(this.romMenuStrip, this.romToolStrip, this.infoStrip));
		}

		private void InititalizeLevelEditingTools()
		{
			if (_openedlevels != null)
			{
				foreach (LevelEditor le in _openedlevels)
				{
					this.dockTools.Controls.Remove(le);
				}
			}
			_openedlevels = new List<LevelEditor>();
			_screenexiteditor = new ScreenExitEditor(this);
			_headereditor = new HeaderEditor.HeaderEditor(this);

			_objectspriteselector.Show(this.dockTools, DockState.DockLeft);


			AdjustLevelEditorToolsSize();

			this.mainPanel.Visible = true;
		}

		private static int DockedWidth(DockState states, params DockContent[] controls)
		{
			int width = 0;
			foreach (DockContent dc in controls)
			{
				if (dc.DockState == DockState.DockLeft)
				{
					width += dc.Width;
				}
			}
			return width;
		}

		private static int HeightOfControls(params Control[] controls)
		{
			int height = 0;
			foreach (Control c in controls)
			{
				height += c.Height;
			}
			return height;
		}

		private void loadLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LevelSelector ls = new LevelSelector(_loadedcontent._levellist);
			ls.ShowDialog();
			LoadLevel(ls.GetLevelToLoad());
			ls.Dispose();
		}

		internal void LoadLevel(int levelindex)
		{
			if (levelindex > -1 && levelindex < 0xDE)
			{
				LevelEditor alreadyopen = GetOpenedLevelByIndex(levelindex);
				//If a Level has no open Tab, create a new one for the Level
				if (alreadyopen == null)
				{
					_openedlevels.Add(new LevelEditor(this, levelindex));
					_openedlevels.Last().Show(this.dockTools, DockState.Document);

					if (_objectspriteselector.IsDisposed)
					{
						this.toggleObjectSpriteSelectorTSB.PerformClick();
					}
					_currentlevel = _openedlevels.Last();
					//If no Levels were opened, Editor Tools must be enabled
					if (_openedlevels.Count > 0)
					{
						ToggleEditorToolsEnabled(true);
					}
				}
				//If a Level is already open, focus on the Tab it is opened
				else
				{
					alreadyopen.Show(this.dockTools, DockState.Document);
					_currentlevel = alreadyopen;
				}
				UpdateUtilities();
			}
		}

		internal void LoadLevelAndJump(int levelindex, SVector position)
		{
			LoadLevel(levelindex);
			_currentlevel._jump = true;
			_currentlevel._jumpscroll = position;
			_currentlevel.Panel().Invalidate();
		}

		private LevelEditor GetOpenedLevelByIndex(int levelindex)
		{
			foreach (LevelEditor le in _openedlevels)
			{
				if (le._leveldata.GetLevelIndex() == levelindex)
				{
					return le;
				}
			}
			return null;
		}

		private void objectsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_loadedcontent._globalsettings.SetShowObjects(((ToolStripMenuItem)sender).Checked);
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		private void spritesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_loadedcontent._globalsettings.SetShowSprites(((ToolStripMenuItem)sender).Checked);
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		private void screenGridToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_loadedcontent._globalsettings.SetShowScreenGrid(((ToolStripMenuItem)sender).Checked);
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		private void tileGridToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_loadedcontent._globalsettings.SetShowTileGrid(((ToolStripMenuItem)sender).Checked);
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		private void checkeredBackgroundToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_loadedcontent._globalsettings.SetCheckeredBackground(((ToolStripMenuItem)sender).Checked);
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		internal void UpdateMousePosition(int x, int y)
		{
			if (x < 0 || y < 0)
			{
				XPosTSSL.Text = "X: --";
				YPosTSSL.Text = "Y: --";
			}
			else
			{
				XPosTSSL.Text = "X: " + String.Format("{0:X2}", x);
				YPosTSSL.Text = "Y: " + String.Format("{0:X2}", y);
			}
		}

		internal void LevelEditorWindowClosed(LevelEditor le)
		{
			_openedlevels.Remove(le);
			_currentlevel = null;
			UpdateUtilities();
			//If closing an Editor Window causes the Editor to have no Levels loaded, disable editing tools
			if (_openedlevels.Count == 0)
			{
				ToggleEditorToolsEnabled(false);
			}
		}

		private void loadLevelTSB_Click(object sender, EventArgs e)
		{
			loadLevelToolStripMenuItem.PerformClick();
		}

		private void saveLevelTSB_Click(object sender, EventArgs e)
		{
			saveLevelToolStripMenuItem.PerformClick();
		}

		private void saveAllLevelsTSB_Click(object sender, EventArgs e)
		{
			saveAllOpenedLevelsToolStripMenuItem.PerformClick();
		}

		private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_currentlevel != null)
			{
				_currentlevel.SaveLevel();
			}
			else
			{
				MessageBox.Show("Currently no Level is opened!");
			}
		}

		private void headerEditorTSB_Click(object sender, EventArgs e)
		{
			if (_currentlevel != null && _headereditor.IsHidden)
			{
				_headereditor.Show(this.dockTools, DockState.DockRight);
				headerEditorTSB.Checked = true;
			}
			else if (!_headereditor.IsHidden)
			{
				_headereditor.Hide();
				headerEditorTSB.Checked = false;
			}
		}

		private void toggleObjectSpriteSelectorTSB_Click(object sender, EventArgs e)
		{
			if (!_objectspriteselector.IsDisposed)
			{
				_objectspriteselector.Hide();
				_objectspriteselector.Dispose();
				toggleObjectSpriteSelectorTSB.Checked = false;
			}
			else
			{
				if (_currentlevel != null)
				{
					_objectspriteselector = new ObjectSpriteSelector();
					_objectspriteselector.Show(this.dockTools, DockState.DockLeft);
					toggleObjectSpriteSelectorTSB.Checked = true;
				}
			}
		}

		private void screenExitEditorTSB_Click(object sender, EventArgs e)
		{
			if (_currentlevel != null && _screenexiteditor.IsHidden)
			{
				_screenexiteditor.Show(this.dockTools, DockState.DockRight);
				screenExitEditorTSB.Checked = true;
			}
			else if (!_screenexiteditor.IsHidden)
			{
				_screenexiteditor.Hide();
				screenExitEditorTSB.Checked = false;
			}
		}

		private void OOBTiles_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in _OutOfBoundsOffsets)
			{
				tsmi.Checked = false;
			}
			((ToolStripMenuItem)sender).Checked = true;
			_loadedcontent._globalsettings.SetTilesOutOfBoundaries((int)(((ToolStripMenuItem)sender).Tag));
			areaOutsideOfBoundariesToolStripMenuItem.Checked = _loadedcontent._globalsettings.TilesOutOfBoundaries() != 0;
			foreach (LevelEditor le in _openedlevels)
			{
				le.Panel().Invalidate();
			}
		}

		private void spielwieseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			unchecked
			{
				//MessageBox.Show(((short)0xFFFF).ToString());
			}


			//Erhalte alle Levels mit bestimmten Header-Settings
			StringBuilder sb = new StringBuilder();
			foreach (GoldenEggVine.ROMRelated.LevelRelated.CLevelData ld in _yirom.GetAllLevelData())
			{
				if (ld.LevelHeader().GetHeaderValue(ROMRelated.LevelRelated.EHeaderValue.SprPal) == 0x8)
				{
					sb.AppendLine(String.Format("{0:X2}", ld.GetLevelIndex()));
				}
			}
			MessageBox.Show(sb.ToString());
		}

		internal void UpdateUtilities()
		{
			_screenexiteditor.UpdateStatus();
			_headereditor.UpdateStatus();
		}

		private void headerEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			headerEditorTSB.PerformClick();
		}

		private void paletteEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			paletteEditorTSB.PerformClick();
		}

		private void screenExitEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			screenExitEditorTSB.PerformClick();
		}

		private void viewLevelStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ToggleEditorToolsEnabled(bool state)
		{
			//"Level"
			saveLevelToolStripMenuItem.Enabled = state;
			closeLevelToolStripMenuItem.Enabled = state;
			saveAndCloseLevelToolStripMenuItem.Enabled = state;
			saveAllOpenedLevelsToolStripMenuItem.Enabled = state;
			minimapToolStripMenuItem.Enabled = state;
			//"Edit"
			objectEditModeToolStripMenuItem.Enabled = state;
			spriteEditModeToolStripMenuItem.Enabled = state;
			headerEditorToolStripMenuItem.Enabled = state;
			paletteEditorToolStripMenuItem.Enabled = state;
			screenExitEditorToolStripMenuItem.Enabled = state;
			increaseZOrderToolStripMenuItem.Enabled = state;
			decreaseZOrderToolStripMenuItem.Enabled = state;
			undoToolStripMenuItem.Enabled = state;
			redoToolStripMenuItem.Enabled = state;
			//"View"
			objectsToolStripMenuItem.Enabled = state;
			spritesToolStripMenuItem.Enabled = state;
			screenGridToolStripMenuItem.Enabled = state;
			tileGridToolStripMenuItem.Enabled = state;
			checkeredBackgroundToolStripMenuItem.Enabled = state;
			areaOutsideOfBoundariesToolStripMenuItem.Enabled = state;
			viewLevelStatisticsToolStripMenuItem.Enabled = state;
			//Toolstrip-Icons
			saveLevelTSB.Enabled = state;
			saveAllLevelsTSB.Enabled = state;
			objectEditModeTSB.Enabled = state;
			spriteEditModeTSB.Enabled = state;
			toggleObjectSpriteSelectorTSB.Enabled = state;
			undoTSB.Enabled = state;
			redoTSB.Enabled = state;
			headerEditorTSB.Enabled = state;
			paletteEditorTSB.Enabled = state;
			screenExitEditorTSB.Enabled = state;
			miniMapTSB.Enabled = state;
		}

		private void createProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Project.NewProjectForm pf = new Project.NewProjectForm();
			if (pf.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			_project = new CProject(pf.ProjectName, pf.ProjectParentDir);
			_project.InitialSetup(pf.BaseRom);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void emulatorPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Testing.SetupEmulatorPathForm sepf = new Testing.SetupEmulatorPathForm();
			if (sepf.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllText(_project.EmulatorPath, sepf.EmulatorPath);
			}
		}
	}
}