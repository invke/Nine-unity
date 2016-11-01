using System;


namespace Assets.Core.Coordinates
{

    
    [Serializable]
    public class TilePos
    {
        public readonly int x;
        public readonly int y;
        public readonly int z;


        public TilePos(int x, int y, int z) 
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public ChunkPos GetChunkPos() 
        {
            // Careful - negative int division results in negative quotient (must phase by -1) 
            int cx = x / Chunk.CHUNK_WIDTH;
            if (x < 0) cx--;
            int cy = y / Chunk.CHUNK_HEIGHT;
            if (y < 0) cy--;
            int cz = z / Chunk.CHUNK_DEPTH;
            if (z < 0) cz--;
            return new ChunkPos(cx, cy, cz);
        }
    }
}