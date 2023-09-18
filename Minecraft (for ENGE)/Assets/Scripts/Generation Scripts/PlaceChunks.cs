using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaceChunks : MonoBehaviour
{
    public int chunkNum = 100;

 

    public GameObject player;

    public ChunkController[,] chunks;

    public float[] timers;

    public GameObject[] blocks;

    public Material texturePack;

    public GameObject highlighter;

    ChunkController lastPlayerChunk;


    public bool onStart = true;

    float renderDist = 12;

    float sTime;

    public bool Done = true;

    
     List<ChunkController> chunksToGen = new List<ChunkController>();

    public int listLenth;
    Vector3 spawnPoint = new Vector3(800, 100, 800);
    void Start()
    {
       

        
        
        chunks = new ChunkController[chunkNum,chunkNum];
        timers= new float[chunkNum * chunkNum];
        spawnPoint = new Vector3(Random.Range(1, chunkNum * 16), 100, Random.Range(1, chunkNum * 16));
        player.transform.position = spawnPoint;

        place(new Vector2(spawnPoint.x,spawnPoint.z));
        
        
        

        refreshRenderDist();
        sTime= Time.time;

    }

    private void Update()
    {
        listLenth = chunksToGen.Count;
        if(Time.frameCount > (renderDist + 1.5f) * (renderDist + 1.5f) * 17 && onStart && listLenth ==0)
        {
            onStart = false;
            player = new Player(spawnPoint, this).p;
        }
        if(lastPlayerChunk != null)
        {
            if (lastPlayerChunk != GetChunkFromVector3(player.transform.position))
            {
                refreshRenderDist();
                
            }
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
                float d = Vector2.Distance(playerPos, pos) / 16;
                if (d < renderDist + 2 && chunks[i, j] == null)
                {
                    createChunk(i, j);


                }
                else if (d < renderDist && !chunks[i, j].created)
                {

                    //chunks[i, j].GenerateMesh(true);
                    if (chunksToGen.Count == 0)
                    {

                        chunksToGen.Add(chunks[i, j]);
                        StartCoroutine(startGen());
                    }
                    else
                    {

                        chunksToGen.Add(chunks[i, j]);
                    }


                }
                if(d < renderDist)
                {
                    chunks[i, j].chunkObject.SetActive(true);
                }
                else
                {
                    if (chunks[i,j] != null && chunks[i, j].chunkObject.activeSelf) chunks[i, j].chunkObject.SetActive(false);
                }
               
            }
        }
        lastPlayerChunk = GetChunkFromVector3(player.transform.position);
    }

    IEnumerator startGen()
    {
        while (chunksToGen.Count > 0)
        {
            if (!chunksToGen[0].created)
            {

                Done = false;
                StartCoroutine(chunksToGen[0].createMesh());
                

            }

            while (!Done)
            {
                
                yield return null;
            }
            //chunksToGen[0].chunkObject.SetActive(false) ;
            chunksToGen.RemoveAt(0);
            
        }
    }

    private void place(Vector2 dist)
    {
        int startx = 0;
        int startz = 0 ;

     

        for(int i = 0; i < chunkNum; i++)
        {
            for(int j = 0; j < chunkNum; j++)
            {
                if (Vector2.Distance(dist, new Vector2(startx + (i * 16), startz + (j * 16))) / 16 <= renderDist)
                {


                    createChunk(i,j);

                   
                }
            }
        }
        
        
    }

   void createChunk(int i, int j)
    {
        ChunkController c = new ChunkController(new Vector3((i * 16), 0,(j * 16)), this);
        chunks[i, j] = c;
        c.controller = this;
        c.player = player;
        c.index = new int[] {i, j };
        c.chunkObject.transform.parent = this.transform;
        StartCoroutine( c.generateChunk(onStart));
    }

    public ChunkController GetChunkFromVector3(Vector3 pos)
    {

        int x = Mathf.FloorToInt(pos.x / 16);
        int z = Mathf.FloorToInt(pos.z / 16);
        return chunks[x,z];

    }
}
