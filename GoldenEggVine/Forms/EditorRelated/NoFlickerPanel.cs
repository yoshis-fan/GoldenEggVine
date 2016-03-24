using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.EditorRelated
{
    public class NoFlickerPanel : System.Windows.Forms.Panel
    {
        public NoFlickerPanel()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint | 
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | 
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, 
                true);
        }
    }
}
