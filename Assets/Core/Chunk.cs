using Assets.Core.Coordinates;
using UnityEngine;


namespace Assets.Core
{


    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        public static int CHUNK_SIZE = 16;
        public static int CHUNK_WIDTH = CHUNK_SIZE;
        public static int CHUNK_DEPTH = CHUNK_SIZE;
        public static int CHUNK_HEIGHT = 4 * CHUNK_SIZE;


        // Note: Not readonly as rewritten post-construction when instantiation game objects from pre-fabs.
        public ChunkPos chunkPos;  

        // 3D array of tiles
        public Tile[,,] tiles = new Tile[CHUNK_WIDTH, CHUNK_HEIGHT, CHUNK_DEPTH];

        public Plane plane;

        public bool toUpdate = true;
        
        protected MeshFilter filter;

        protected new MeshCollider collider;


        public Chunk() 
        {
            this.chunkPos = new ChunkPos(0, 0, 0);
        }


        public Chunk(ChunkPos chunkPos)
        {
            this.chunkPos = chunkPos;
        }


        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start() 
        {
            filter = gameObject.GetComponent<MeshFilter>();
            collider = gameObject.GetComponent<MeshCollider>();
        }


        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update() 
        {
            if (toUpdate) {
                toUpdate = false;
                MeshData meshData = UpdateMeshData();
                UpdateMeshes(meshData);
            }
        }


        ///<summary>
        /// Iterates over the tiles of the chunk, assembling the vertices and triangles for the chunk-wide mesh.
        ///</summary>
        private MeshData UpdateMeshData() 
        {
            MeshData mesh = new MeshData();
            
            for (int xi = 0; xi < CHUNK_WIDTH; xi++) {
                for (int yi = 0; yi < CHUNK_HEIGHT; yi++) {
                    for (int zi = 0; zi < CHUNK_DEPTH; zi++) {
                        TilePos bswCorner = chunkPos.GetTilePos();
                        int x = bswCorner.x + xi;
                        int y = bswCorner.y + yi;
                        int z = bswCorner.z + zi;
                        Tile tile = tiles[xi, yi, zi];
                        mesh = tile.TileData(this, x, y, z, mesh);
                    }
                }
            }

            return mesh;
        }


        ///<summary>
        /// Sends the calculated mesh information to the filter mesh and collision component.
        ///</summary>
        private void UpdateMeshes(MeshData meshData) 
        {
            // For the render mesh (filter)
            filter.mesh.Clear();
            filter.mesh.vertices = meshData.vertices.ToArray();
            filter.mesh.triangles = meshData.triangles.ToArray();
            filter.mesh.colors = meshData.colors.ToArray();  // ..when using vertex shaders
            filter.mesh.RecalculateNormals();

            // and also for the collision mesh.
            collider.sharedMesh = null;
            Mesh mesh = new Mesh();
            mesh.vertices = meshData.colVertices.ToArray();
            mesh.triangles = meshData.colTriangles.ToArray();
            mesh.RecalculateNormals();
            collider.sharedMesh = mesh;
        }


        public Tile GetTile(int x, int y, int z) 
        {
            Tile tile;
            if ((InXRange(x)) && InYRange(y) && InZRange(z)) { 
                int xi = x - chunkPos.x * CHUNK_WIDTH;
                int yi = y - chunkPos.y * CHUNK_HEIGHT;
                int zi = z - chunkPos.z * CHUNK_DEPTH;
                tile = tiles[xi, yi, zi];
            } else
                return plane.GetTile(x, y, z);

            return tile;
        }


        public Tile[,,] Peek() {
            return tiles;
        }


        public void SetTile(int x, int y, int z, Tile tile)
        {
            if (InXRange(x) && InYRange(y) && InZRange(z)) {
                int xi = x - chunkPos.x * CHUNK_WIDTH;
                int yi = y - chunkPos.y * CHUNK_HEIGHT;
                int zi = z - chunkPos.z * CHUNK_DEPTH;
                tiles[xi, yi, zi] = tile;
                toUpdate = true;
            } else {
                plane.SetTile(x, y, z, tile);
            }
        }


        private bool InXRange(int x) 
        {
            int lowerBound = chunkPos.x * CHUNK_WIDTH;
            int upperBound = lowerBound + CHUNK_WIDTH;
            if (x >= lowerBound && x < upperBound) 
                return true;
            else
                return false;
        }


        private bool InYRange(int y)
        {
            int lowerBound = chunkPos.y * CHUNK_HEIGHT;
            int upperBound = lowerBound + CHUNK_HEIGHT;
            if (y >= lowerBound && y < upperBound) 
                return true;
            else
                return false;
        }


        private bool InZRange(int z) 
        {
            int lowerBound = chunkPos.z * CHUNK_DEPTH;
            int upperBound = lowerBound + CHUNK_DEPTH;
            if (z >= lowerBound && z < upperBound) 
                return true;
            else
                return false;
        }


        public void SetTilesUnmodified()
        {
            foreach (Tile tile in tiles) {
                tile.modified = false;
            }
        }
    }
}