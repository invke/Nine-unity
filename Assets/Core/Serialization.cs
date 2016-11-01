using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Assets.Core.Coordinates;

namespace Assets.Core 
{
    public static class Serialization 
    {
        public static string saveFolderName = "NineSaves";


        public static string SaveLocation(string worldName)
        {
            string saveLocation = saveFolderName + "/" + worldName + "/";
            if (!Directory.Exists(saveLocation))
                Directory.CreateDirectory(saveLocation);
            
            return saveLocation;
        }


        public static string FileName(ChunkPos location) 
        {
            string fileName = location.x + "," + location.y + "," + location.z + ".bin";
            return fileName; 
        }


        public static void SaveChunk(Chunk chunk)
        {
            Save save = new Save(chunk);
            if (save.tiles.Count == 0) return;

            string saveFile = SaveLocation(chunk.plane.planeName);
            saveFile += FileName(chunk.chunkPos);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
            
            formatter.Serialize(stream, save);
            stream.Close();
        }


        public static bool Load(Chunk chunk)
        {
            string saveFile = SaveLocation(chunk.plane.planeName);
            saveFile += FileName(chunk.chunkPos);

            if (!File.Exists(saveFile)) return false;

            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveFile, FileMode.Open);

            Save save = (Save) formatter.Deserialize(stream);
            foreach (var pair in save.tiles) {
                chunk.tiles[pair.Key.x, pair.Key.y, pair.Key.z] = pair.Value;
            }
            stream.Close();
            return true;
        }
    }
}