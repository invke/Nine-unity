using System;


namespace Assets.Core.Tiles
{


    [Serializable]
    public class StoneTile : Core.Tile
    {   
        public override float R {
            // get { return 210 / 255; }
            get { return 0.4f; }
        }
        public override float G {
            // get { return 180 / 255; }
            get { return 0.4f; }
        }
        public override float B {
            // get { return 140 / 255; }
            get { return 0.4f; }
        }


        public StoneTile() : 
        base() {}


        public StoneTile(int x, int y, int z) : 
        base() {}

        public override bool IsSolid(Direction direction) 
        {
            return true;
        }
        
    }
}