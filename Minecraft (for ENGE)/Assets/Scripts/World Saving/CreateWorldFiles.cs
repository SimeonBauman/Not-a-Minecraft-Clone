using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CreateWorldFiles : MonoBehaviour
{
    public static string worldsPath = "Assets/Worlds";
    public static string currentName = "";
    private void Start()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(worldsPath);
        int count = dir.GetFiles().Length;
        Debug.Log(count);
    }
    
    public static string[] getWorlds()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("Assets/Worlds");
        int count = dir.GetFiles().Length;
        string[] returnString = new string[count];
        for (int i = 0; i < count; i++)
        {
            string name = dir.GetFiles()[i].Name;
            returnString[i] = name.Remove(name.Length - 5);
        }
        return returnString;
    }
    public static void createWorld(string name, string seed)
    {
        string worldPath = worldsPath + "/" + name;
        currentName = name;
        System.IO.Directory.CreateDirectory(worldPath);
        worldPath += "/";
        NoiseVars.Seed = int.Parse(seed);
        NoiseVars.name = name;
        using (StreamWriter sw = File.CreateText(worldPath + "worldData.txt"))
        {
            sw.WriteLine(name);
            sw.WriteLine(seed);
           
        }
        System.IO.Directory.CreateDirectory(worldPath + "Chunks");
        
    }

    public static void loadWorld(string path)
    {
        string[] data = File.ReadAllLines(path + "worldData.txt");
        NoiseVars.Seed = int.Parse(data[1]);
        NoiseVars.name = data[0];
        NoiseVars.spawnPoint.x = float.Parse(data[2]);
        NoiseVars.spawnPoint.y = float.Parse(data[3]);
        NoiseVars.spawnPoint.z = float.Parse(data[4]);
    }
    public static void saveWorld(string path,List<ChunkController> chunks,GameObject player)
    {
        string[] data = File.ReadAllLines(path + "worldData.txt");
        data[2] = player.transform.position.x.ToString();
        data[3] = player.transform.position.y.ToString();
        data[4] = player.transform.position.z.ToString();
        File.WriteAllLines(path + "worldData.txt", data);
        path += "Chunks/";
        string[,,] blocks = new string[16,124,16];
        string[] text = {""};
        for(int i  = 0; i < chunks.Count; i++)
        {
            if (chunks[i].hasChanged)
            {

                if (!File.Exists(path + chunks[i].chunkObject.name + ".txt"))
                {
                    using (StreamWriter sw = File.CreateText(path + chunks[i].chunkObject.name + ".txt"))
                    {
                    }
                }
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 124; y++)
                        {
                            for (int z = 0; z < 16; z++)
                            {
                                text[0] += chunks[i].blocks[x, y, z].textIndex + ",";

                            }
                        }
                    }

                


                File.WriteAllLines(path + chunks[i].chunkObject.name + ".txt", text);

                text[0] = "";
                Debug.Log("saved");
            }
        }
    }

    public static bool chunkIsSaved(ChunkController chunk)
    {
        return File.Exists("Assets/Worlds/test1/Chunks/" + chunk.chunkObject.name + ".txt");
    }

    public static string returnData(ChunkController chunk)
    {
        
        return File.ReadAllText("Assets/Worlds/test1/Chunks/" + chunk.chunkObject.name + ".txt");
    }



}
