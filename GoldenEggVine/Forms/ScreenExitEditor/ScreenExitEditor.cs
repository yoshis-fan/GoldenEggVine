using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using GoldenEggVine.Labels;
using GoldenEggVine.ROMRelated;
using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;

namespace GoldenEggVine.Forms
{

    public partial class ScreenExitEditor : DockContent, IUpdatable
    {
		private enum EScreenExitType { LEVEL, MINIBATTLE };

        private Mainform _mainform;
		private TextBox[] _levelse;
		private TextBox[] _minibattlese;


        public ScreenExitEditor()
        {
            InitializeComponent();
			GroupValidatableTextBoxes();
        }

        public ScreenExitEditor(Mainform mainform) : this()
        {
			_mainform = mainform;
			RetrieveInformation();
            ScreenIndexCB.SelectedIndex = 0;
        }

		private void GroupValidatableTextBoxes()
		{
			_levelse = new TextBox[] { DestLevelTB, DestXTB, DestYTB };
			_minibattlese = new TextBox[] { ReturnLevelTB, ReturnXTB, ReturnYTB };
		}

		private void RetrieveInformation()
		{
            for (int i = 0; i < CYIROM.NumberOfScreensPerLevel(); i++)
            {
                ScreenIndexCB.Items.Add(String.Format("{0:X2}",i));
            }

            for (int i = 0; i < _mainform._loadedcontent._exitlist.Count(); i++)
            {
                ExittypeCB.Items.Add(String.Format("{0:X1}", _mainform._loadedcontent._exitlist.GetContent(i, EExitLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._exitlist.GetContent(i, EExitLabelsContent.DESCRIPTION));
            }
			for (int i = 0; i < _mainform._loadedcontent._minibattlelist.Count(); i++)
			{
				MiniBattletypeCB.Items.Add(String.Format("{0:X1}", _mainform._loadedcontent._minibattlelist.GetContent(i, EMiniBattleLabelsContent.INDEX)) + " - " + _mainform._loadedcontent._minibattlelist.GetContent(i, EMiniBattleLabelsContent.DESCRIPTION));
			}
		}

        public void UpdateStatus()
        {
			if (_mainform._currentlevel != null)
			{
				this.Enabled = true;
				CLevelScreenExit currentexit = CLevelScreenExit.GetScreenExitAtScreen(_mainform._currentlevel.LevelData().ScreenExits(), new SVector((byte)((ScreenIndexCB.SelectedIndex) & 0xF), (byte)(ScreenIndexCB.SelectedIndex >> 4)));
				if (currentexit == null)
				{
					ExitEnabledCkB.Checked = false;
					LevelTypeTC.Enabled = false;
					LevelTypeTC.SelectedTab = LevelTypeTC.TabPages[0];
					DestLevelTB.Text = "";
					DestXTB.Text = "";
					DestYTB.Text = "";
					ExittypeCB.SelectedIndex = -1;
					ReturnLevelTB.Text = "";
					ReturnXTB.Text = "";
					ReturnYTB.Text = "";
					MiniBattletypeCB.SelectedIndex = -1;
				}
				else
				{
					ExitEnabledCkB.Checked = true;
					LevelTypeTC.Enabled = true;
					//Ist ganz normales ScreenExit
					if (currentexit is CLevelLevelScreenExit)
					{
						LevelTypeTC.SelectedTab = LevelTypeTC.TabPages[0];
						DestLevelTB.Text = String.Format("{0:X2}", currentexit.GetDestinationLevel());
						DestXTB.Text = String.Format("{0:X2}", currentexit.GetDestinationX());
						DestYTB.Text = String.Format("{0:X2}", currentexit.GetDestinationY());
						ExittypeCB.SelectedIndex = (int)(((CLevelLevelScreenExit)currentexit).GetEntranceType());

						ReturnLevelTB.Text = "";
						ReturnXTB.Text = "";
						ReturnYTB.Text = "";
						MiniBattletypeCB.SelectedIndex = -1;
					}
					//Ist ScreenExit zu einem Minibattle
					else
					{
						LevelTypeTC.SelectedTab = LevelTypeTC.TabPages[1];
						ReturnLevelTB.Text = String.Format("{0:X2}", currentexit.GetDestinationLevel());
						ReturnXTB.Text = String.Format("{0:X2}", currentexit.GetDestinationX());
						ReturnYTB.Text = String.Format("{0:X2}", currentexit.GetDestinationY());
						MiniBattletypeCB.SelectedIndex = (((int)((((CLevelMinibattleScreenExit)currentexit).GetMinibattle())) - CYIROM.NumberOfLevels()));

						DestLevelTB.Text = "";
						DestXTB.Text = "";
						DestYTB.Text = "";
						ExittypeCB.SelectedIndex = -1;
					}
				}
			}
			else
			{
				this.Enabled = false;
			}
        }

        public void JumpToScreen(SVector position)
        {
            if (this.IsHidden)
            {
                _mainform.screenExitEditorTSB.PerformClick();
            }
            ScreenIndexCB.SelectedIndex = position._x + (position._y << 4);
        }

        private void ScreenExitEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _mainform.screenExitEditorTSB.PerformClick();
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

        private void ScreenIndexTB_TextChanged(object sender, EventArgs e)
        {
            ChooseComboBoxEntryFromTextBox((TextBox)sender, ScreenIndexCB);
        }

        private void ScreenIndexCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus();
			((ComboBox)sender).Focus();
        }

		private void ApplyB_Click(object sender, EventArgs e)
		{
			//Deletes old ScreenExit and uses the new one instead
			_mainform._currentlevel.DeleteLevelEntity(CLevelScreenExit.GetScreenExitAtScreen(_mainform._currentlevel._leveldata.ScreenExits(), new SVector((byte)(this.ScreenIndexCB.SelectedIndex & 0xF), (byte)(this.ScreenIndexCB.SelectedIndex >> 4))));
			//Level-ScreenExit
			if (this.LevelTypeTC.SelectedTab == this.LevelTypeTC.TabPages[0])
			{
				_mainform._currentlevel.AddNewLevelEntity
				(
					new CLevelLevelScreenExit
					(
						new SVector
						(
							(byte)(this.ScreenIndexCB.SelectedIndex & 0xF),
							(byte)(this.ScreenIndexCB.SelectedIndex >> 4)
						),
					Byte.Parse(DestLevelTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier),
					Byte.Parse(DestXTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier),
					Byte.Parse(DestYTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier),
					(EEntranceType)ExittypeCB.SelectedIndex
					)
				);
			}
			//MiniBattle-ScreenExit
			else if (this.LevelTypeTC.SelectedTab == this.LevelTypeTC.TabPages[1])
			{
				_mainform._currentlevel.AddNewLevelEntity
				(
					new CLevelMinibattleScreenExit
					(
						new SVector
						(
							(byte)(this.ScreenIndexCB.SelectedIndex & 0xF),
							(byte)(this.ScreenIndexCB.SelectedIndex >> 4)
						),
						(EMinibattle)MiniBattletypeCB.SelectedIndex,
						Byte.Parse(ReturnLevelTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier),
						Byte.Parse(ReturnXTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier),
						Byte.Parse(ReturnYTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier)
					)
				);
			}
			//Ungültig
			else
			{
				throw new GoldenEggVine.Exceptions.InvalidScreenExitTypeException("Screen Exits can only be of type \"Level\" or \"Minibattle\"!");
			}
		}

		private void DiscardB_Click(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		private bool ValidateTextbox(TextBox tb, int max)
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
			if (c < 0x00 || c > max || tb.Text.Equals(""))
			{
				tb.ForeColor = Color.Red;
				return false;
			}
			else
			{
				tb.ForeColor = Color.Black;
				return true;
			}
		}

		//Hardcoded, cause fuck you!
		private void CheckIfAllInputValid(EScreenExitType set)
		{
			switch(set)
			{
				case(EScreenExitType.LEVEL):
					ApplyB.Enabled = false;
					if(!ValidateTextbox(DestLevelTB, CYIROM.NumberOfLevels())){ return; }
					if(!ValidateTextbox(DestXTB, 0xFF)){ return; }
					if(!ValidateTextbox(DestYTB, 0x7F)){ return; }
					if (ExittypeCB.SelectedIndex < 0 || ExittypeCB.SelectedIndex >= ExittypeCB.Items.Count){ return; }
					ApplyB.Enabled = true;
					break;
				case(EScreenExitType.MINIBATTLE):
					ApplyB.Enabled = false;
					if (!ValidateTextbox(ReturnLevelTB, CYIROM.NumberOfLevels())){ return; }
					if (!ValidateTextbox(ReturnXTB, 0xFF)) { return; }
					if (!ValidateTextbox(ReturnYTB, 0x7F)) { return; }
					if (MiniBattletypeCB.SelectedIndex < 0 || MiniBattletypeCB.SelectedIndex >= MiniBattletypeCB.Items.Count) { return; }
					ApplyB.Enabled = true;
					break;
				default:
					throw new Exception("Arschlecken, du Sau!");
			}
		}

		private void DestLevelTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, CYIROM.NumberOfLevels() - 1);
			CheckIfAllInputValid(EScreenExitType.LEVEL);
		}

		private void DestXTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, 0xFF);
			CheckIfAllInputValid(EScreenExitType.LEVEL);
		}

		private void DestYTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, 0x7F);
			CheckIfAllInputValid(EScreenExitType.LEVEL);
		}

		private void ReturnLevelTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, CYIROM.NumberOfLevels() - 1);
			CheckIfAllInputValid(EScreenExitType.MINIBATTLE);
		}

		private void ReturnXTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, 0xFF);
			CheckIfAllInputValid(EScreenExitType.MINIBATTLE);
		}

		private void ReturnYTB_TextChanged(object sender, EventArgs e)
		{
			ValidateTextbox((TextBox)sender, 0x7F);
			CheckIfAllInputValid(EScreenExitType.MINIBATTLE);
		}

		private void ExitEnabledCkB_Click(object sender, EventArgs e)
		{
			//When checked, create new ScreenExit
			if (((CheckBox)sender).Checked)
			{
				_mainform._currentlevel.AddNewLevelEntity(new CLevelLevelScreenExit(new SVector((byte)(this.ScreenIndexCB.SelectedIndex & 0xF), (byte)(this.ScreenIndexCB.SelectedIndex >> 4)), 0, 0, 0, EEntranceType.Door));
			}
			//When unchecked, it was checked before, so a ScreenExit exists; remove
			else
			{
				_mainform._currentlevel.DeleteLevelEntity(CLevelScreenExit.GetScreenExitAtScreen(_mainform._currentlevel._leveldata.ScreenExits(), new SVector((byte)(this.ScreenIndexCB.SelectedIndex & 0xF), (byte)(this.ScreenIndexCB.SelectedIndex >> 4))));
			}
			//Refresh afterwards
			UpdateStatus();
		}
    }
}