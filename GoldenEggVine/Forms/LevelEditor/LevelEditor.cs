using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using GoldenEggVine.ROMRelated.LevelRelated;
using GoldenEggVine.ROMRelated.TranslevelRelated;
using GoldenEggVine.EditorRelated;
using GoldenEggVine.Helpers;
using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;

namespace GoldenEggVine.Forms
{
    public partial class LevelEditor : DockContent
    {
        private Mainform _mainform;
        internal CLevelData _leveldata;
		internal CTranslevelLevelConnection _levelconnections;
        internal CEventHistory _eventhistory;

        private NoFlickerPanel _levelpanel;

		internal bool _jump = true;
		internal SVector _jumpscroll;



        private LevelEditor()
        {
            InitializeComponent();
        }

        public LevelEditor(Mainform mainform, int levelindex) : this()
        {
            _mainform = mainform;
            _leveldata = _mainform._yirom.GetLevelData(levelindex);
			

            this.Text = "Level " + String.Format("{0:X2}", levelindex);

			_levelpanel = this.levelPanel;

			_jumpscroll = _leveldata.Objects().Count > 0 ? _leveldata.Objects().First().GetPosition() : new SVector(0, 0);
        }

		public LevelEditor(Mainform mainform, int levelindex, SVector jumpscroll) : this(mainform, levelindex)
		{
			_jumpscroll = jumpscroll;
		}

        public void SaveLevel()
        {
            CByteStream objectbytes = new CByteStream();
            //Header
            objectbytes.Append(_leveldata.LevelHeader().GetByteArray());

            //Objects
            foreach(CLevelEntity le in _leveldata.Objects())
            {
                if (le.Exists())
                {
                    objectbytes.Append(le.ToByteArray());
                }
            }

            objectbytes.Append(0xFF);

            //Screen Exits
            foreach(CLevelEntity le in _leveldata.ScreenExits())
            {
                if(le.Exists())
                {
                    objectbytes.Append(le.ToByteArray());
                }
            }

            objectbytes.Append(0xFF);

            CByteStream spritebytes = new CByteStream();
            //Sprites
            foreach(CLevelEntity le in _leveldata.Sprites())
            {
                if(le.Exists())
                {
                    spritebytes.Append(le.ToByteArray());
                }
            }

            spritebytes.Append(new byte[] { 0xFF, 0xFF });

			StringBuilder sb = new StringBuilder();
			EHeaderValue[] vals = new EHeaderValue[] { EHeaderValue.BGColor, EHeaderValue.BG1TS, EHeaderValue.BG1Pal, EHeaderValue.BG2TS, EHeaderValue.BG2Pal, EHeaderValue.BG3TS, EHeaderValue.BG3Pal, EHeaderValue.SprTS, EHeaderValue.SprPal, EHeaderValue.LevelMode, EHeaderValue.AnimationTS, EHeaderValue.AnimationPal, EHeaderValue.BGScrolling, EHeaderValue.Music, EHeaderValue.ItemMemory, EHeaderValue.Unused };
			foreach (EHeaderValue v in vals)
			{
				sb.Append(String.Format("{0:X2}",_leveldata.LevelHeader().GetHeaderValue(v)) + ", ");
			}
			MessageBox.Show(sb.ToString());
        }

        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            //TODO: Schließung bei Änderungen abfangen
            e.Cancel = false;
            _mainform.LevelEditorWindowClosed(this);
        }

        private Point GetLevelCoordinatesFromCursor()
        {
            return new Point((_levelpanel.PointToClient(Cursor.Position).X >> 4) - _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries(), (_levelpanel.PointToClient(Cursor.Position).Y >> 4) - _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
        }

        private void levelPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentpos = GetLevelCoordinatesFromCursor();
            _mainform.UpdateMousePosition(currentpos.X, currentpos.Y);
        }

        private void FollowScreenExit()
        {
            Point currentpos = GetLevelCoordinatesFromCursor();
            CLevelScreenExit atcurrentpos = CLevelScreenExit.GetScreenExitAtScreen(_leveldata.ScreenExits(), new SVector((byte)(currentpos.X >> 4), (byte)(currentpos.Y >> 4)));
            if (atcurrentpos != null)
            {
                _mainform.LoadLevelAndJump(atcurrentpos.GetDestinationLevel(), atcurrentpos.GetDestinationVector());
            }
        }

        private void CreateOrFocusScreenExit()
        {
            Point currentpos = GetLevelCoordinatesFromCursor();
            CLevelScreenExit atcurrentpos = CLevelScreenExit.GetScreenExitAtScreen(_leveldata.ScreenExits(), new SVector((byte)(currentpos.X >> 4), (byte)(currentpos.Y >> 4)));
            if(atcurrentpos == null)
            {
                AddNewLevelEntity(CLevelScreenExit.CreateScreenExitFromData(new SVector((byte)(currentpos.X >> 4), (byte)(currentpos.Y >> 4)), 0, 0, 0, EEntranceType.Door));
                _mainform._screenexiteditor.UpdateStatus();
            }
            else
            {
                _mainform._screenexiteditor.JumpToScreen(new SVector((byte)(currentpos.X >> 4), (byte)(currentpos.Y >> 4)));
                _mainform._screenexiteditor.UpdateStatus();
            }
        }

        private void DeleteScreenExit()
        {
            Point currentpos = GetLevelCoordinatesFromCursor();
            CLevelScreenExit atcurrentpos = CLevelScreenExit.GetScreenExitAtScreen(_leveldata.ScreenExits(), new SVector((byte)(currentpos.X >> 4), (byte)(currentpos.Y >> 4)));
            if (atcurrentpos != null)
            {
                DeleteLevelEntity(atcurrentpos);
                _mainform._screenexiteditor.UpdateStatus();
            }
        }

		/// <summary>
		/// Used to scroll to a certain Tile. The destination Tile is centered
		/// </summary>
		/// <param name="position"></param>
		internal void ScrollToTilePosition(SVector position)
		{
			//Number of Tiles on the X coordinate on screen; divided by 2 for center
			int xoffset = (int)(this.Width >> 5);
			int yoffset = (int)(this.Height >> 5);

			//MessageBox.Show((xoffset << 1) + " XTiles\n" + (yoffset << 1) + " YTiles");

			//Get the value in ratio to one Tile on screen
			double xonetile = (((double)this.HorizontalScroll.Maximum) / ((double)(0x100 + (_mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() << 1))));
			double yonetile = (((double)this.VerticalScroll.Maximum) / ((double)(0x80 + (_mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() << 1))));

			//MessageBox.Show("Center Tile: " + String.Format("{0:X2}",position._x) + "/" + String.Format("{0:X2}",position._y) + "\nOffset: " + String.Format("{0:X2}",xoffset) + "/" + String.Format("{0:X2}",yoffset) + "\nFocus Top Left: " + String.Format("{0:X2}",(position._x + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - xoffset)) + "/" + String.Format("{0:X2}",(position._y + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - yoffset)));
			//MessageBox.Show("Scroll per Tile: " + xonetile + "/" + yonetile);

			//Multiplicate the value for one tile with the Tile indexes
			int xvalue = (position._x + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - xoffset) * xonetile < this.HorizontalScroll.Minimum ? this.HorizontalScroll.Minimum : (int)((position._x + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - xoffset) * xonetile);
			int yvalue = (position._y + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - yoffset) * yonetile < this.VerticalScroll.Minimum ? this.VerticalScroll.Minimum : (int)((position._y + _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() - yoffset) * yonetile);

			//Double set each value, or else it won't work
			this.HorizontalScroll.Value = xvalue;
			this.VerticalScroll.Value = yvalue;
            PerformLayout();
		}

        private void levelPanel_Paint(object sender, PaintEventArgs e)
        {
            this._levelpanel.Size = new Size((0x100 << 4) + (_mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() << 5), (0x80 << 4) + (_mainform._loadedcontent._globalsettings.TilesOutOfBoundaries() << 5));
            
            //Checkered BG
            if (_mainform._loadedcontent._globalsettings.CheckeredBackground())
            {
                CreateCheckeredBackground(e.Graphics, sender, _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
            }
            //TileGrid
            if (_mainform._loadedcontent._globalsettings.ShowTileGrid())
            {
                CreateTileGrid(e.Graphics, sender, _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
            }
            //Objects
            if (_mainform._loadedcontent._globalsettings.ShowObjects())
            {
                DrawLevelEntities(e.Graphics, _leveldata.Objects(), _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
            }
            //Sprites
            if (_mainform._loadedcontent._globalsettings.ShowSprites())
            {
                DrawLevelEntities(e.Graphics, _leveldata.Sprites(), _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
            }
            //ScreenExits first, then Grid
            if (_mainform._loadedcontent._globalsettings.ShowScreenGrid())
            {
                CreateScreenGrid(e.Graphics, sender, _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
                DrawLevelEntities(e.Graphics, _leveldata.ScreenExits(), _mainform._loadedcontent._globalsettings.TilesOutOfBoundaries());
            }

			if (_jump)
			{
				ScrollToTilePosition(_jumpscroll);
				_jump = false;
			}
        }

        private void DrawLevelEntities(Graphics g, List<CLevelEntity> entitylist, int offset)
        {
            foreach (CLevelEntity le in entitylist)
            {
                if (le.Exists())
                {
                    le.DrawEntity(g, offset);
                }
            }
        }

        private void CreateScreenGrid(Graphics g, object sender, int offset)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    g.DrawRectangle(CGlobalDrawing._bleuouterline, (i << 8) + (offset << 4), (j << 8) + (offset << 4), 0xFF, 0xFF);
                    g.DrawRectangle(CGlobalDrawing._blueinnerline, (i << 8) + 0x2 + (offset << 4), (j << 8) + 0x2 + (offset << 4), 0xFB, 0xFB);
                    //Draw Label only if no ScreenExit exists
                    if(NoScreenExitInScreen(new SVector((byte)i,(byte)j)))
                    {
                        g.FillRectangle(CGlobalDrawing._blueindexrect, (i << 8) + 0x3 + (offset << 4), (j << 8) + 0x3 + (offset << 4), 0xD, 0xD);
                        g.DrawString(String.Format("{0:X2}", (j << 4) + i), CGlobalDrawing._font, CGlobalDrawing._fontbrush, (i << 8) + 0x1 + (offset << 4), (j << 8) + 0x2 + (offset << 4));
                    }
                }
            }
        }

        private bool NoScreenExitInScreen(SVector s)
        {
            return CLevelScreenExit.GetScreenExitAtScreen(_leveldata.ScreenExits(), s) == null;
        }

        private void CreateTileGrid(Graphics g, object sender, int offset)
        {
            for (int i = 1; i < 0x100 + (offset << 1); i++)
            {
                g.DrawLine(CGlobalDrawing._gridlines, (i << 4), 0, (i << 4), ((Panel)sender).Height);
            }
            for (int i = 1; i < 0x80 + (offset << 1); i++)
            {
                g.DrawLine(CGlobalDrawing._gridlines, 0, (i << 4), ((Panel)sender).Width, (i << 4));
            }
        }

        private void CreateCheckeredBackground(Graphics g, object sender, int offset)
        {
            g.FillRectangle(CGlobalDrawing._darktiles, 0, 0, ((NoFlickerPanel)sender).Width, ((NoFlickerPanel)sender).Height);
            for (int i = 0; i < 0x100 + (offset << 2); i++)
            {
                for (int j = 0; j < 0x100 + (offset << 2); j++)
                {
                    g.FillRectangle(CGlobalDrawing._lighttiles, ((i & 1) == 0 ? 8 : 0) + (j << 4), (i << 3), 8, 8);
                }
            }
        }

        public NoFlickerPanel Panel()
        {
            return _levelpanel;
        }

        public CLevelData LevelData()
        {
            return _leveldata;
        }

        private void LevelEditor_Enter(object sender, EventArgs e)
        {
            _mainform._currentlevel = this;
            _mainform.UpdateUtilities();
        }

        private void LevelEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case('e'):
                    CreateOrFocusScreenExit();
                    break;
                case('x'):
                    //Remove selected object...???
                    DeleteScreenExit();
                    break;
                case('g'):
                    FollowScreenExit();
                    break;
				case('0'):
					break;
				case('1'):
					break;
				case('2'):
					break;
				case('3'):
					break;
				case('4'):
					break;
            }
        }

        internal void AddNewLevelEntity(CLevelEntity entity)
        {
            _leveldata.AddNewEntity(entity);
            _levelpanel.Invalidate();
        }

        internal void DeleteLevelEntity(CLevelEntity entity)
        {
            entity.Delete();
            _levelpanel.Invalidate();
        }
    }
}