using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GoldenEggVine.EditorRelated
{
    public abstract class CGlobalDrawing
    {
        private static CLoadedContent _loadedcontent;

        //Labelling font for SCREEN related stuff
        internal static Font _font;
        internal static Brush _fontbrush;


        //TILE GRID
        internal static Pen _gridlines;

        //SCREEN GRID
        internal static Pen _bleuouterline;
        internal static Pen _blueinnerline;
        internal static Brush _blueindexrect;

        //SCREEN EXIT
        internal static Pen _redouterline;
        internal static Pen _redinnerline;
        internal static Brush _datarect;

        //CHECKERED BG
        internal static Brush _darktiles;
        internal static Brush _lighttiles;
        



        //This one updates all the Objects as well
        internal static void GiveLoadedContent(CLoadedContent loadedcontent)
        {
            _loadedcontent = loadedcontent;
            update();
        }

        //Actually initializing the values and updating them
        internal static void update()
        {
            if(_loadedcontent == null)
            {
                throw new Exception("Cannot update without any Contents loaded!");
            }
            else
            {
                _font = new Font("Arial", 7.5f);
                _fontbrush = new SolidBrush(Color.FromArgb(255,255,255));

                _gridlines = new Pen(Color.FromArgb(192, 192, 192), 1);

                _bleuouterline = new Pen(Color.FromArgb(0,0,255), 1);
                _blueinnerline = new Pen(Color.FromArgb(0,0,255), 1);
                _blueindexrect = new SolidBrush(Color.FromArgb(96,0,0,255));

                _redouterline = new Pen(Color.FromArgb(255,0,0), 1);
                _redinnerline = new Pen(Color.FromArgb(255,0,0), 2);
                _datarect = new SolidBrush(Color.FromArgb(96,255,0,0));

                _darktiles = new SolidBrush(Color.FromArgb(144,144,144));
                _lighttiles = new SolidBrush(Color.FromArgb(160,160,160));
            }
        }

    }
}
