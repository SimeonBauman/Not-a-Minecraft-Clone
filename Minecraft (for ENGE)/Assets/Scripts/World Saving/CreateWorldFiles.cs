using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CreateWorldFiles : MonoBehaviour
{
    public static string worldsPath = "Assets/Worlds";
    private void Start()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(worldsPath);
        int count = dir.GetFiles().Length;
        Debug.Log(count);
    }
    

    public static void createWorld(string name, string seed)
    {
        string worldPath = worldsPath + "/" + name;
        System.IO.Directory.CreateDirectory(worldPath);
        worldPath += "/";
        using (StreamWriter sw = File.CreateText(worldPath + "worldData.txt"))
        {
            sw.WriteLine(name);
            sw.WriteLine(seed);
           
        }
        System.IO.Directory.CreateDirectory(worldPath + "Chunks");
        
    }
    public static void saveWorld(string path,ChunkController[,] chunks)
    {
        path += "Chunks/";
        string[,,] blocks = new string[16,124,16];
        for(int i  = 0; i < NoiseVars.chunkNum; i++)
        {
            for(int j = 0; j < NoiseVars.chunkNum; j++)
            {
                if (chunks[i,j] != null)
                {
                    if (!File.Exists(path + chunks[i, j].chunkObject.name + ".txt"))
                    {
                        using (StreamWriter sw = File.CreateText(path + chunks[i, j].chunkObject.name + ".txt"))
                        {
                        }
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 124; y++)
                                {
                                    for (int z = 0; z < 16; z++)
                                    {
                                        File.AppendAllText(path + chunks[i, j].chunkObject.name + ".txt", chunks[i, j].blocks[x, y, z].textIndex + ",");
                                        
                                    }
                                }
                            }
                        
                    }
                    
                   
                    
                    
                }
                Debug.Log("saved");
            }
        }
    }
}
