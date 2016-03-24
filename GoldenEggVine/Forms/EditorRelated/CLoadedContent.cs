using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.EditorRelated
{
    public class CLoadedContent
    {
        internal Labels.CLevelLabels _levellist;
        //internal Labels.CExitLabels _exitlist;
        //internal Labels.CObjectLabels _objectlist;
        //internal Labels.CXObjectLabels _xobjectlist;
        //internal Labels.CSpriteLabels _spritelist;

		internal Labels.CBG1TSLabels _bg1tslist;
		internal Labels.CBG2TSLabels _bg2tslist;
		internal Labels.CBG3TSLabels _bg3tslist;

		internal Labels.CBG1PalLabels _bg1pallist;
		internal Labels.CBG2PalLabels _bg2pallist;
		internal Labels.CBG3PalLabels _bg3pallist;
		internal Labels.CSprPalLabels _sprpallist;

		internal Labels.CLevelModeLabels _levelmodelist;

		internal Labels.CMusicLabels _musiclist;

		internal Labels.CExitLabels _exitlist;
		internal Labels.CMiniBattleLabels _minibattlelist;

        internal CGlobalSettings _globalsettings;
    }
}
