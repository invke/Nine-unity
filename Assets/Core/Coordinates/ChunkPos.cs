using System;
using UnityEngine;


namespace Assets.Core.Coordinates
{

    
    [Serializable]
    public class ChunkPos
    {
        public readonly int x;
        public readonly int y;
        public readonly int z;


        public ChunkPos(int x, int y, int z) 
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public static ChunkPos FromWorldPos(Vector3 worldPos)
        {
            int x = Mathf.FloorToInt(Mathf.FloorToInt(worldPos.x) / Chunk.CHUNK_WIDTH);
            int y = Mathf.FloorToInt(Mathf.FloorToInt(worldPos.y / 0.25f) / Chunk.CHUNK_HEIGHT);
            int z = Mathf.FloorToInt(Mathf.FloorToInt(worldPos.z) / Chunk.CHUNK_DEPTH);
            if (x < 0) x--;
            if (y < 0) y--;
            if (z < 0) z--;
            return new ChunkPos(x, y, z);
        }


        public TilePos GetTilePos()
        {
            return new TilePos(
                x * Chunk.CHUNK_WIDTH,
                y * Chunk.CHUNK_HEIGHT,
                z * Chunk.CHUNK_DEPTH);
        }


        // override object.Equals
        public override bool Equals(object otherObj)
        {
            if (otherObj == null || !(otherObj is ChunkPos))
                return false;
            
            ChunkPos otherPos = (ChunkPos) otherObj;
            return x == otherPos.x && y == otherPos.y && z == otherPos.z;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return  x + 
                    y * Plane.PLANE_WIDTH +
                    z * Plane.PLANE_WIDTH * Plane.PLANE_HEIGHT;
        }
    }
}