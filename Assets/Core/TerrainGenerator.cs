using UnityEngine;
using System.Collections;
using SimplexNoise;
using Assets.Core.Coordinates;
using Assets.Core.Tiles;

namespace Assets.Core 
{


    public class TerrainGenerator
    {
        public float stoneBaseHeight = -24;
        public float stoneBaseNoise = 0.05f;
        public float stoneBaseNoiseHeight = 4;
        public float stoneMountainHeight = 48;
        public float stoneMountainFrequency = 0.008f;
        public float stoneMinHeight = -12;
        public float soilBaseHeight = 1;
        public float soilNoise = 0.04f;
        public float soilNoiseHeight = 3;

        public int deletionTimer = 0;


        public static int GetNoise(int x, int y, int z, float scale, int max)
        {
            return Mathf.FloorToInt((Noise.Generate(x * scale, 4 * y * scale, z * scale) + 1f) * (max / 2f));
        }


        public Chunk GenerateChunk(Chunk chunk)
        {
            TilePos chunkOrigin = chunk.chunkPos.GetTilePos();
            for (int x = chunkOrigin.x; x < chunkOrigin.x + Chunk.CHUNK_WIDTH; x++) {
                // for (int y = chunkOrigin.y; y < chunkOrigin.y + Chunk.CHUNK_HEIGHT; y++) {
                for (int z = chunkOrigin.z; z < chunkOrigin.z + Chunk.CHUNK_DEPTH; z++) {
                    chunk = GenerateColumn(chunk, x, z);
                }
            }    
            // }
            return chunk;
        }


        public Chunk GenerateColumn(Chunk chunk, int x, int z)
        {
            // Generate the stone height
            int stoneHeight = Mathf.FloorToInt(stoneBaseHeight);
            stoneHeight += GetNoise(x, 0, z, stoneMountainFrequency, Mathf.FloorToInt(stoneMountainHeight));
            if (stoneHeight < stoneMinHeight)
                stoneHeight = Mathf.FloorToInt(stoneMinHeight);
            stoneHeight += GetNoise(x, 0, z, stoneBaseNoise, Mathf.FloorToInt(stoneBaseNoiseHeight));
            stoneHeight += 16;

            // Generate the dirt height
            int soilHeight = stoneHeight + Mathf.FloorToInt(soilBaseHeight);
            soilHeight += GetNoise(x, 100, z, soilNoise, Mathf.FloorToInt(soilNoiseHeight));
            soilHeight += 16;

            TilePos chunkOrigin = chunk.chunkPos.GetTilePos();
            for (int y = chunkOrigin.y; y < chunkOrigin.y + Chunk.CHUNK_HEIGHT; y++) {
                if (y <= stoneHeight)
                    chunk.SetTile(x, y, z, new StoneTile(x, y, z));
                else if (y <= soilHeight)
                    chunk.SetTile(x, y, z, new SoilTile(x, y, z));
                else    
                    chunk.SetTile(x, y, z, new AirTile(x, y, z));
            }

            return chunk;
        }

    }
}