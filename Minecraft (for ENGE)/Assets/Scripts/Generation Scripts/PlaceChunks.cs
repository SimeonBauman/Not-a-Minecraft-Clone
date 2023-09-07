using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChunks : MonoBehaviour
{
    private int chunkNum = 10;

 

    public GameObject player;

    public ChunkController[,] chunks;

    public float[] timers;

    public GameObject[] blocks;

    public Material texturePack;

    public GameObject highlighter;

    ChunkController lastPlayerChunk;
   

    void Start()
    {
       

        
        
        chunks = new ChunkController[chunkNum,chunkNum];
        timers= new float[chunkNum * chunkNum];
        place();
        lastPlayerChunk = GetChunkFromVector3(new Vector3(80, 50, 80));
        
        player = new Player(new Vector3(80, 50, 80), this).p;
        refreshRenderDist();

    }

    private void Update()
    {
        if(lastPlayerChunk != GetChunkFromVector3(player.transform.position)){
            refreshRenderDist();
            lastPlayerChunk = GetChunkFromVector3(player.transform.position);
        }
    }

    void refreshRenderDist()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        for (int i = 0; i < chunkNum; i++)
        {
            for (int j = 0; j < chunkNum; j++)
            {
                Vector2 pos = new Vector2(i * 16, j * 16);

                if (Vector2.Distance(playerPos, pos) / 16 < 3)
                {
                    if (!chunks[i, j].chunkObject.activeSelf) chunks[i, j].chunkObject.SetActive(true);
                    if (!chunks[i, j].created) StartCoroutine( chunks[i, j].generateChunk());
                }
                else
                {
                    if (chunks[i, j].chunkObject.activeSelf) chunks[i, j].chunkObject.SetActive(false);
                }
            }
        }
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
