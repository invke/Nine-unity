using UnityEngine;
using Assets.Core.Coordinates;
using System;


namespace Assets.Core
{


    public enum Direction 
    {
        north,
        east,
        south,
        west,
        up,
        down
    };


    [Serializable]
    public abstract class Tile 
    {
        public const float TILE_SIZE = 0.125f;
        public abstract float R { get; }
        public abstract float G { get; }
        public abstract float B { get; }
        
        [NonSerializedAttribute]
        private Color _color;
        public Color color {
             get { return new Color(R, G, B); }
        }

        public bool modified = true;

        public readonly TilePos tilePos;


        public Tile() {}


        public Tile(int x, int y, int z) 
        {
            tilePos = new TilePos(x, y, z);
        }


        public virtual bool IsSolid(Direction direction) 
        {
            switch(direction) {
                case Direction.north:
                    return true;
                case Direction.east:
                    return true;
                case Direction.south:
                    return true;
                case Direction.west:
                    return true;
                case Direction.up:
                    return true;
                case Direction.down:
                    return true;
            }
            return false;
        }


        public virtual MeshData TileData(Chunk chunk, int x, int y, int z, MeshData meshData) 
        {
            // By default use the same mesh for collision as rendering.
            meshData.useRenderDataForCol = true;

            // Check above.
            if (!chunk.GetTile(x, y + 1, z).IsSolid(Direction.down))
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            // Check below.
            if (!chunk.GetTile(x, y - 1, z).IsSolid(Direction.up))
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            // Check north.
            if (!chunk.GetTile(x, y, z + 1).IsSolid(Direction.south))
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            // Check south.
            if (!chunk.GetTile(x, y, z - 1).IsSolid(Direction.north))
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            // Check east.
            if (!chunk.GetTile(x + 1, y, z).IsSolid(Direction.west))
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            // Check west.
            if (!chunk.GetTile(x - 1, y, z).IsSolid(Direction.east))
                meshData = FaceDataWest(chunk, x, y, z, meshData);

            return meshData;
        }
    

        protected virtual MeshData FaceDataUp(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }


        protected virtual MeshData FaceDataDown(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }


        protected virtual MeshData FaceDataNorth(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }


        protected virtual MeshData FaceDataEast(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }


        protected virtual MeshData FaceDataSouth(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x + 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }


        protected virtual MeshData FaceDataWest(
            Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z + 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f + 0.125f, z - 0.5f), color);
            meshData.AddVertex(new Vector3(x - 0.5f, y / 4f - 0.125f, z - 0.5f), color);
            meshData.AddQuadTriangles();

            return meshData;
        }
    }
}