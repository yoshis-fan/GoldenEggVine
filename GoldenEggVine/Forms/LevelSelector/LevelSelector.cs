using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GoldenEggVine.Labels;

namespace GoldenEggVine.Forms
{
    public partial class LevelSelector : Form
    {
        private enum SelectionMethod { TEXTBOX, LIST }

        private string _lastfilter = "";
        private int _leveltoload = -1;
        private bool _validtextboxinput = false;
        private SelectionMethod _selectionmethod = SelectionMethod.TEXTBOX;

        private CLevelLabels _selection;




        private LevelSelector()
        {
            InitializeComponent();
        }

        public LevelSelector(CLevelLabels ls) : this()
        {
            this.LevelChoiceTB.SelectionStart = this.LevelChoiceTB.Text.Length;
            _selection = ls;
			for (int i = 0; i < _selection.Count(); i++)
			{
				this.LevelsListLV.Items.Add(new ListViewItem(_selection.GetRow(i)));
			}
            this.LevelsListLV.Columns[1].Width = this.Width - this.LevelsListLV.Columns[0].Width - 62;
        }

        public int GetLevelToLoad()
        {
            return _leveltoload;
        }

        private void LevelSelector_SizeChanged(object sender, EventArgs e)
        {
            this.LevelsListLV.Width = this.Width - 40;
            this.LevelsListLV.Height = this.Height - 113;
            this.LevelsListLV.Columns[1].Width = this.Width - this.LevelsListLV.Columns[0].Width - 62;

            this.LevelChoiceTB.Width = this.Width - 272;
            this.DescriptionFilterTB.Width = this.Width - 130;
        }

        private void LevelsListLV_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show(this.LevelsListLV.Items[this.LevelsListLV.SelectedIndices[0]].Text);
            }
        }

        private void LevelChoiceTB_TextChanged(object sender, EventArgs e)
        {
            //Transforms into upper case
            int sel = LevelChoiceTB.SelectionStart;
            this.LevelChoiceTB.Text = this.LevelChoiceTB.Text.ToUpper();
            this.LevelChoiceTB.SelectionStart = sel;
            int c = -1;
            try
            {
                c = Int32.Parse(this.LevelChoiceTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch (Exception)
            {  }

            //If the number is invalid, paint it red, else black
            if(c < 0x00 || c >= GoldenEggVine.ROMRelated.CYIROM.NumLevelIndices || this.LevelChoiceTB.Text.Equals(""))
            {
                this.LevelChoiceTB.ForeColor = Color.Red;
                _validtextboxinput = false;
            }
            else
            {
                this.LevelChoiceTB.ForeColor = Color.Black;
                _validtextboxinput = true;
            }
        }

        private void SubmitB_Click(object sender, EventArgs e)
        {
            if(_selectionmethod == SelectionMethod.LIST)
            {
                _leveltoload = Int32.Parse(this.LevelsListLV.Items[this.LevelsListLV.SelectedIndices[0]].Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else if(_selectionmethod == SelectionMethod.TEXTBOX)
            {
                if(_validtextboxinput)
                {
                    _leveltoload = Int32.Parse(this.LevelChoiceTB.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
            this.Close();
        }

        private void LevelsListLV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _selectionmethod = SelectionMethod.LIST;
            button1.PerformClick();
        }

        private void DescriptionFilterTB_TextChanged(object sender, EventArgs e)
        {
            if(_lastfilter.Length > DescriptionFilterTB.Text.Length)
            {
                this.LevelsListLV.Items.Clear();
                for (int i = 0; i < _selection.Count(); i++)
                {
                    this.LevelsListLV.Items.Add(new ListViewItem(_selection.GetRow(i)));
                }
            }
            string cmp = DescriptionFilterTB.Text.ToLower();
            for(int i = 0; i < this.LevelsListLV.Items.Count; ++i)
            {
                if(!this.LevelsListLV.Items[i].SubItems[1].Text.ToLower().Contains(cmp))
                {
                    this.LevelsListLV.Items.RemoveAt(i);
                    --i;
                }
            }
            _lastfilter = DescriptionFilterTB.Text;
        }
    }
}
