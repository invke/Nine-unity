using System;


namespace Assets.Core.Tiles
{

    
    [Serializable]
    public class AirTile : Tile
    {
        public override float R {
            get { return 1; }
        }
        public override float G {
            get { return 1; }
        }
        public override float B {
            get { return 1; }
        }

        public AirTile() : 
        base() {}


        public AirTile(int x, int y, int z) : 
        base(x, y, z) {}


        public override bool IsSolid(Direction direction) 
        {
            return false;
        }


        public override MeshData TileData(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            return meshData;
        }
        
    }
}