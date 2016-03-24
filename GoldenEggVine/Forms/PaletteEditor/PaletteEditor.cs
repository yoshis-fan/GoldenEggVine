using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoldenEggVine.Forms
{
    public partial class PaletteEditor : Form
    {
        private Mainform _mainform;

        public PaletteEditor()
        {
            InitializeComponent();
        }

        public PaletteEditor(Mainform mainform) : this()
        {
            _mainform = mainform;
        }
    }
}
