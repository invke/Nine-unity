using UnityEngine;
using Assets.Core.Coordinates;

namespace Assets.Core
{
    public static class Terrain
    {
        ///<summary>
        /// Takes a world position and deduces the corresponding tile.
        ///<summary>
        public static TilePos GetTilePos(Vector3 worldPos)
        {
            TilePos tilePos = TilePos.FromWorldPos(worldPos);
            return tilePos;
        }


        public static TilePos GetTilePos(RaycastHit hit, bool adjacent = false)
        {
            Vector3 worldPos = new Vector3(
                FindTileXOrigin(hit.point.x, hit.normal.x, adjacent),
                FindTileYOrigin(hit.point.y, hit.normal.y, adjacent),
                FindTileZOrigin(hit.point.z, hit.normal.z, adjacent)
            );

            return GetTilePos(worldPos);
        }


        ///<summary>
        /// Takes an x component and shifts it to the nearest tile origin position with respect
        /// to a normal.
        ///<summary>
        private static float FindTileXOrigin(float pos, float normal, bool adjacent = false)
        {
            if (pos - (int) pos == 0.5f || pos - (int) pos == -0.5f)
            {
                if (adjacent)
                    pos += (normal / 2);
                else
                    pos -= (normal / 2);
            }

            return (float) pos;
        }


        ///<summary>
        /// Takes an z component and shifts it to the nearest tile origin position with respect
        /// to a normal.
        ///<summary>
        private static float FindTileYOrigin(float pos, float normal, bool adjacent = false)
        {
            if (pos - 0.25 * ((int) (pos / 0.25)) == 0.125f || 
                pos - 0.25 * ((int) (pos / 0.25)) == -0.125f) {
                if (adjacent)
                    pos += (normal / 8);
                else
                    pos -= (normal / 8);
            }

            return (float) pos;
        }


        ///<summary>
        /// Takes an y component and shifts it to the nearest tile origin position with respect
        /// to a normal.
        ///<summary>
        private static float FindTileZOrigin(float pos, float normal, bool adjacent = false)
        {
            if (pos - (int) pos == 0.5f || pos - (int) pos == -0.5f)
            {
                if (adjacent)
                    pos += (normal / 2);
                else
                    pos -= (normal / 2);
            }

            return (float) pos;
        }


        public static bool SetTile(RaycastHit hit, Tile tile, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null) return false;

            TilePos tilePos = GetTilePos(hit, adjacent);
            chunk.plane.SetTile(tilePos.x, tilePos.y, tilePos.z, tile);

            return true;
        }


        public static Tile GetTile(RaycastHit hit, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null) return null;

            TilePos tilePos = GetTilePos(hit, adjacent);
            return chunk.GetTile(tilePos.x, tilePos.y, tilePos.z);
        }
    }
}