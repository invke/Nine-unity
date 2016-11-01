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


        public TilePos GetTilePos()
        {
            return new TilePos(
                x * Chunk.CHUNK_WIDTH,
                y * Chunk.CHUNK_HEIGHT,
                z * Chunk.CHUNK_DEPTH);
        }


        public Vector3 WorldPos() 
        {
            return new Vector3(x, y, z);
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