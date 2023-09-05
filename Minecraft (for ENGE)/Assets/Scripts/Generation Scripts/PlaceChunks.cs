using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChunks : MonoBehaviour
{
    private int chunkNum = 2;

 

    public GameObject player;

    public ChunkController[,] chunks;

    public float[] timers;

    public GameObject[] blocks;

    public Material texturePack;

    public GameObject highlighter;

    void Start()
    {
        
        player = new Player(new Vector3(10, 50, 10),this).p;
        chunks = new ChunkController[chunkNum,chunkNum];
        timers= new float[chunkNum * chunkNum];
        
        place();
        
    }

    

    private void place()
    {
        int startx = 0;
        int startz = 0 ;

        int num = 0;

        for(int i = 0; i < chunkNum; i++)
        {
            for(int j = 0; j < chunkNum; j++)
            {
                ChunkController c = new ChunkController(new Vector3(startx + (i*16),0,startz + (j*16)),this);
                chunks[i,j] = c;
                c.controller = this;
                c.player = player;
                c.index = num;
                
                num++;
            }
        }

    }

    public ChunkController GetChunkFromVector3(Vector3 pos)
    {

        int x = Mathf.FloorToInt(pos.x / 16);
        int z = Mathf.FloorToInt(pos.z / 16);
        return chunks[x, z];

    }
}
