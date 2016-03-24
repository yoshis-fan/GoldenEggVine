using GoldenEggVine.ROMRelated.LevelRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GoldenEggVine.Forms.HeaderEditor
{
	public enum EBGScrollValue { Error=-4, Invalid=-3, None=-2, Constant=-1 };

    public partial class HeaderEditor : DockContent, IUpdatable
    {
		internal Mainform _mainform;

		internal ShareSpriteTileset.SharedSpriteTileset _sharedspritetileset;
		internal SpriteTilesetEditor.SpriteTilesetEditor _spritetileseteditor;

        public HeaderEditor()
        {
            InitializeComponent();
			this.hardwareandgameparameters = new Label[] { NMIandIRGModeVL, IRQModeVL, SCBRVL, SBMRVL, InvalidatePal00VL, BGMODEVL, BG1SCVL, BG2SCVL, BG3SCVL, BG1NBAVL, BG34NBAVL, W12SELVL, W34SELVL, WOBJSELVL, TMVL, TSVL, TMWVL, TSWVL, CGSWSELVL, CGADSUBVL };
        }

        public HeaderEditor(Mainform mainform) : this()
        {
			_mainform = mainform;
			PopulateComboBoxes();
        }

		private void PopulateComboBoxes()
		{
			for(int i = 0; i < _mainform._loadedcontent._bg1tslist.Count(); i++)
			{
				this.BG1TSCB.Items.Add(String.Format("{0:X1}",_mainform._loadedcontent._bg1tslist.GetContent(i, Labels.EBG1TSLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._bg1tslist.GetContent(i, Labels.EBG1TSLabelsContent.HINT));
			}
			for (int i = 0; i < _mainform._loadedcontent._bg2tslist.Count(); i++)
			{
				this.BG2TSCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._bg2tslist.GetContent(i, Labels.EBG2TSLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._bg2tslist.GetContent(i, Labels.EBG2TSLabelsContent.HINT));
			}
			for (int i = 0; i < _mainform._loadedcontent._bg3tslist.Count(); i++)
			{
				this.BG3TSCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._bg3tslist.GetContent(i, Labels.EBG3TSLabelsContent.INDEX)) + " - "  + _mainform._loadedcontent._bg3tslist.GetContent(i, Labels.EBG3TSLabelsContent.HINT));
			}
			for (int i = 0; i < CLevelHeader.GetHeaderValueRange(EHeaderValue.SprTS); i++)
			{
				this.SprTSCB.Items.Add(String.Format("{0:X2}", i));
			}

			for(int i = 0; i < _mainform._loadedcontent._bg1pallist.Count(); i++)
			{
				this.BG1PalCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._bg1pallist.GetContent(i, Labels.EBG1PalLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._bg1pallist.GetContent(i, Labels.EBG1PalLabelsContent.HINT));
			}
			for (int i = 0; i < _mainform._loadedcontent._bg2pallist.Count(); i++)
			{
				this.BG2PalCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._bg2pallist.GetContent(i, Labels.EBG2PalLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._bg2pallist.GetContent(i, Labels.EBG2PalLabelsContent.HINT));
			}
			for (int i = 0; i < _mainform._loadedcontent._bg3pallist.Count(); i++)
			{
				this.BG3PalCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._bg3pallist.GetContent(i, Labels.EBG3PalLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._bg3pallist.GetContent(i, Labels.EBG3PalLabelsContent.HINT));
			}
			for (int i = 0; i < _mainform._loadedcontent._sprpallist.Count(); i++)
			{
				this.SprPalCB.Items.Add(String.Format("{0:X1}", _mainform._loadedcontent._sprpallist.GetContent(i, Labels.ESprPalLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._sprpallist.GetContent(i, Labels.ESprPalLabelsContent.HINT));
			}

			for (int i = 0; i < _mainform._loadedcontent._levelmodelist.Count(); i++)
			{
				this.LevelModeCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._levelmodelist.GetContent(i, Labels.ELevelModeLabelsContent.INDEX)));
			}

			for (int i = 0; i < CLevelHeader.GetHeaderValueRange(EHeaderValue.BGScrolling); i++)
			{
				this.BGScrollingCB.Items.Add(String.Format("{0:X2}",i));
			}

			for (int i = 0; i < _mainform._loadedcontent._musiclist.Count(); i++)
			{
				this.MusicCB.Items.Add(String.Format("{0:X2}", _mainform._loadedcontent._musiclist.GetContent(i, Labels.EMusicLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._musiclist.GetContent(i, Labels.EMusicLabelsContent.HINT));
			}

			for (int i = 0; i < CLevelHeader.GetHeaderValueRange(EHeaderValue.ItemMemory); i++)
			{
				this.ItemMemoryCB.Items.Add(String.Format("{0:X1}",i));
			}

		}

		private void ReadCurrentLevelHeaderAndFillForm()
		{
            if (_mainform._currentlevel != null)
            {
                this.Enabled = true;
                BG1TSCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG1TS);
                BG2TSCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG2TS);
                BG3TSCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG3TS);
				SprTSCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.SprTS);

				BG1PalCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG1Pal);
				BG2PalCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG2Pal);
				BG3PalCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BG3Pal);
				SprPalCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.SprPal);

				LevelModeCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.LevelMode);
				BGScrollingCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.BGScrolling);
                MusicCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.Music);

				ItemMemoryCB.SelectedIndex = _mainform._currentlevel._leveldata.LevelHeader().GetHeaderValue(EHeaderValue.ItemMemory);
            }
            else
            {
                this.Enabled = false;
            }
		}

		private void SetGameParametersAndHardwareParameters(int levelmodeindex)
		{
			byte[] levelmodeparams = _mainform._yirom.GetLevelModeParams(levelmodeindex);
			for (int i = 0; i < this.hardwareandgameparameters.Length; i++)
			{
				this.hardwareandgameparameters[i].Text = ": " + String.Format("{0:X2}",levelmodeparams[i]);
			}
		}

		private void HeaderEditor_Resize(object sender, EventArgs e)
        {
			//Resize Container for Editor Tabs
            this.LevelHeaderTC.Width = this.Width - 20;
        }

		private void BG1TB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG1TSCB);
		}

		private void BG2TB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG2TSCB);
		}

		private static void ChooseComboBoxEntryFromTextBox(TextBox tb, ComboBox cb)
		{
			//Transforms into upper case
			int sel = tb.SelectionStart;
			tb.Text = tb.Text.ToUpper();
			tb.SelectionStart = sel;
			int c = -1;
			try
			{
				c = Int32.Parse(tb.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
			}
			catch (Exception)
			{ }

			//If the number is invalid, paint it red, else black
			if (c < 0x00 || c >= cb.Items.Count || tb.Text.Equals(""))
			{
				tb.ForeColor = Color.Red;
			}
			else
			{
				tb.ForeColor = Color.Black;
				cb.SelectedIndex = c;
			}
		}

		private void BG3TSTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG3TSCB);
		}

		private void SprTSTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, SprTSCB);
		}

		private void SprTSCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			byte[] gfxfiles = _mainform._yirom.GetSpriteGFXFiles(((ComboBox)sender).SelectedIndex);
			StringBuilder sb = new StringBuilder();
			foreach(byte b in gfxfiles)
			{
				sb.Append(String.Format("{0:X2}",b) + "  ");
			}
			GFXFilesL.Text = sb.ToString();
		}

		private void MusicCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			AllowItemsCkB.Checked = _mainform._yirom.GetItemsAllowed(((ComboBox)sender).SelectedIndex);
		}

        private void HeaderEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _mainform.headerEditorTSB.PerformClick();
        }

        public void UpdateStatus()
        {
            ReadCurrentLevelHeaderAndFillForm();
        }

		private void BG1PalTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG1PalCB);
		}

		private void BG2PalTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG2PalCB);
		}

		private void BG3PalTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BG3PalCB);
		}

		private void SprPalTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, SprPalCB);
		}

		private void ItemMemoryTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, ItemMemoryCB);
		}

		private void LevelModeCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			LevelModeDescriptionL.Text = _mainform._loadedcontent._levelmodelist.GetContent(((ComboBox)sender).SelectedIndex, Labels.ELevelModeLabelsContent.DESCRIPTION);
			SetGameParametersAndHardwareParameters(((ComboBox)sender).SelectedIndex);
		}

		private void LevelModeTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, LevelModeCB);
		}

        private void MusicTB_TextChanged(object sender, EventArgs e)
        {
            ChooseComboBoxEntryFromTextBox((TextBox)sender, MusicCB);
        }

		private void EditSprTSB_Click(object sender, EventArgs e)
		{
			_sharedspritetileset = new ShareSpriteTileset.SharedSpriteTileset(this, SprTSCB.SelectedIndex);
			_sharedspritetileset.ShowDialog();
			_sharedspritetileset.Dispose();
		}

		private void BGScrollingCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateBGScrollingTableEntries();
		}

		private void BGScrollingTB_TextChanged(object sender, EventArgs e)
		{
			ChooseComboBoxEntryFromTextBox((TextBox)sender, BGScrollingCB);
		}

		private void UpdateBGScrollingTableEntries()
		{
			short[] bgscrollingparams = _mainform._yirom.GetBGScrollingParams(BGScrollingCB.SelectedIndex);
			BG2HorizontalScrollL.Text = EvaluateHorizontalBGScrollingParam(bgscrollingparams[0]);
			BG2VerticalScrollL.Text = EvaluateVerticalBGScrollingParam(bgscrollingparams[1]);
			BG3HorizontalScrollL.Text = EvaluateHorizontalBGScrollingParam(bgscrollingparams[2]);
			BG3VerticalScrollL.Text = EvaluateVerticalBGScrollingParam(bgscrollingparams[3]);
			BG4HorizontalScrollL.Text = EvaluateHorizontalBGScrollingParam(bgscrollingparams[4]);
			BG4VerticalScrollL.Text = EvaluateVerticalBGScrollingParam(bgscrollingparams[5]);
		}

		private static string EvaluateHorizontalBGScrollingParam(short value)
		{
			EBGScrollValue scroll = EBGScrollValue.Error;
			if (value < 0)
			{
				scroll = EBGScrollValue.Invalid;
			}
			else
			{	//Is positive, since value is positive!
				scroll = (EBGScrollValue)(Math.Round((float)value/(float)0x100, 4) * 10000);
			}
			switch (scroll)
			{
				case EBGScrollValue.Error:
					throw new Exception("An Error occurred while a Horizontal BG Scrolling Parameter was processed");
				case EBGScrollValue.Invalid:
					return "Invalid";
				default:
					return (((float)scroll/100)).ToString("0.00") + "%";
			}

		}

		private static string EvaluateVerticalBGScrollingParam(short value)
		{
			EBGScrollValue scroll = EBGScrollValue.Error;
			if (value < 0)
			{
				scroll = EBGScrollValue.Constant;
			}
			else if (value == 0)
			{
				scroll = EBGScrollValue.None;
			}
			else
			{	//Is positive, since value is positive!
				scroll = (EBGScrollValue)(Math.Round((float)value/(float)0x100, 4) * 10000);
			}
			switch (scroll)
			{
				case EBGScrollValue.Error:
					throw new Exception("An Error occurred while a Vertical BG Scrolling Parameter was processed");
				case EBGScrollValue.None:
					return "Constant/Gone";
				case EBGScrollValue.Constant:
					return "Constant";
				default:
					return (((float)scroll/100)).ToString("0.00") + "%";
			}
		}

		private void EditBGScrollB_Click(object sender, EventArgs e)
		{

		}

		private void AllowItemsCkB_Click(object sender, EventArgs e)
		{
			SharedMusic.SharedMusic sm = new SharedMusic.SharedMusic(this, MusicCB.SelectedIndex);
			sm.ShowDialog();
			if (SharedMusic.SharedMusic._changevalue)
			{
				((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
				SharedMusic.SharedMusic._changevalue = false;
			}
		}

		private void BG1TSCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			BG1TSDescriptionL.Text = _mainform._loadedcontent._bg1tslist.GetContent(((ComboBox)sender).SelectedIndex, Labels.EBG1TSLabelsContent.DESCRIPTION);
		}

		private void BG2TSCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			BG2TSDescriptionL.Text = _mainform._loadedcontent._bg2tslist.GetContent(((ComboBox)sender).SelectedIndex, Labels.EBG2TSLabelsContent.DESCRIPTION);
		}

		private void BG3TSCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			BG3TSDescriptionL.Text = _mainform._loadedcontent._bg3tslist.GetContent(((ComboBox)sender).SelectedIndex, Labels.EBG3TSLabelsContent.DESCRIPTION);
		}

		private void tilesets_Resize(object sender, EventArgs e)
		{
			this.ObjectLayersTSGB.Width = ((TabPage)sender).Width - 13;
			this.SpriteLayerTSGB.Width = ((TabPage)sender).Width - 13;

			this.BG1TSCB.Width = ((TabPage)sender).Width - 105;
			this.BG2TSCB.Width = ((TabPage)sender).Width - 105;
			this.BG3TSCB.Width = ((TabPage)sender).Width - 105;

			this.BG1TSDescriptionL.MaximumSize = new Size(this.BG1TSCB.Width, 26);
			this.BG2TSDescriptionL.MaximumSize = new Size(this.BG2TSCB.Width, 26);
			this.BG3TSDescriptionL.MaximumSize = new Size(this.BG3TSCB.Width, 26);

			this.EditSprTSB.Width = ((TabPage)sender).Width - 25;
		}

		private void levelMode_Resize(object sender, EventArgs e)
		{
			this.LevelModeGB.Width = ((TabPage)sender).Width - 13;
			this.HardwareParametersGB.Width = ((TabPage)sender).Width - 13;
			this.GameParametersGB.Width = ((TabPage)sender).Width - 13;
		}

		private void music_Resize(object sender, EventArgs e)
		{
			this.MusicGB.Width = ((TabPage)sender).Width - 13;
			this.ItemCardsGB.Width = ((TabPage)sender).Width - 13;

			this.MusicCB.Width = ((TabPage)sender).Width - 105;
		}

		private void bgScrollingRate_Resize(object sender, EventArgs e)
		{
			this.BGScrollingRateGB.Width = ((TabPage)sender).Width - 13;
			this.ObjectLayerScrollingGB.Width = ((TabPage)sender).Width - 13;

			this.EditBGScrollB.Width = ((TabPage)sender).Width - 25;
		}

		private void itemMemory_Resize(object sender, EventArgs e)
		{
			this.ItemMemoryGB.Width = ((TabPage)sender).Width - 13;
		}

		private void palettes_Resize(object sender, EventArgs e)
		{
			this.ObjectLayersPalGB.Width = ((TabPage)sender).Width - 13;
			this.SpriteLayerPalGB.Width = ((TabPage)sender).Width - 13;

			this.BG1PalCB.Width = ((TabPage)sender).Width - 105;
			this.BG2PalCB.Width = ((TabPage)sender).Width - 105;
			this.BG3PalCB.Width = ((TabPage)sender).Width - 105;
			this.SprPalCB.Width = ((TabPage)sender).Width - 105;

			this.BG1PalDescriptionL.MaximumSize = new Size(this.BG1PalCB.Width, 26);
			this.BG2PalDescriptionL.MaximumSize = new Size(this.BG2PalCB.Width, 26);
			this.BG3PalDescriptionL.MaximumSize = new Size(this.BG3PalCB.Width, 26);
			this.SprPalDescriptionL.MaximumSize = new Size(this.SprPalCB.Width, 26);
		}

		private void SprPalCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SprPalDescriptionL.Text = _mainform._loadedcontent._sprpallist.GetContent(((ComboBox)sender).SelectedIndex, Labels.ESprPalLabelsContent.DESCRIPTION);
		}
    }
}
