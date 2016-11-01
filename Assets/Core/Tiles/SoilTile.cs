using System;


namespace Assets.Core.Tiles
{


    [Serializable]
    public class SoilTile : Core.Tile
    {   
        public override float R {
            // get { return 210 / 255; }
            get { return 0.82f; }
        }
        public override float G {
            // get { return 180 / 255; }
            get { return 0.70f; }
        }
        public override float B {
            // get { return 140 / 255; }
            get { return 0.54f; }
        }


        public SoilTile() : 
        base() {}


        public SoilTile(int x, int y, int z) : 
        base() {}

        public override bool IsSolid(Direction direction) 
        {
            return true;
        }
        
    }
}