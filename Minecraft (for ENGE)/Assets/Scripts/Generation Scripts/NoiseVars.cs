using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVars
{
    
    public static int xOff = Random.Range(0, 1000000);
    public static int zOff = Random.Range(0, 1000000);
    public static float bXOff = Random.Range(0, 1000000);
    public static float bZOff = Random.Range(0, 1000000);
    public static Vector3 spawnPoint;
    public static int chunkNum;
    public static int Seed = 0;
    public static void recalc(int seed)
    {
        Random.seed = seed;
        Seed = seed;
        xOff = Random.Range(0, 1000000);
        zOff = Random.Range(0, 1000000);
        bXOff = (float)Random.Range(0, 100000) / 10;
        bZOff =(float) Random.Range(0, 100000) /10;
   
        spawnPoint = new Vector3( chunkNum * 8, 100, chunkNum * 8);

    }

    

}
    
