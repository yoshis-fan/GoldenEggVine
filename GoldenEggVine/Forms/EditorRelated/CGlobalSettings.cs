using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.EditorRelated
{
    public class CGlobalSettings
    {
        private bool _showobjects;
        private bool _showsprites;
        private bool _showscreengrid;
        private bool _showtilegrid;
        private bool _checkeredbackground;
        private int _tilesoutofboundaries;

        public CGlobalSettings()
        {
            _showobjects = true;
            _showsprites = true;
            _showscreengrid = true;
            _showtilegrid = true;
            _checkeredbackground = true;
            _tilesoutofboundaries = 0;
        }

        public bool ShowObjects() { return _showobjects; }

        public void SetShowObjects(bool showobjects) { _showobjects = showobjects; }

        public bool ShowSprites() { return _showsprites; }

        public void SetShowSprites(bool showsprites) { _showsprites = showsprites; }

        public bool ShowScreenGrid() { return _showscreengrid; }

        public void SetShowScreenGrid(bool showscreengrid) { _showscreengrid = showscreengrid; }

        public bool ShowTileGrid() { return _showtilegrid; }

        public void SetShowTileGrid(bool showtilegrid) { _showtilegrid = showtilegrid; }

        public bool CheckeredBackground() { return _checkeredbackground; }

        public void SetCheckeredBackground(bool checkeredbackground) { _checkeredbackground = checkeredbackground; }

        public int TilesOutOfBoundaries() { return _tilesoutofboundaries; }

        public void SetTilesOutOfBoundaries(int tilesoutofboundaries) { _tilesoutofboundaries = tilesoutofboundaries; }
    }
}
