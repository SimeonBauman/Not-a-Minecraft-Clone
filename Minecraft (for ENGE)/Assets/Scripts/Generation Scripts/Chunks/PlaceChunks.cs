using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlaceChunks : MonoBehaviour
{
    public int chunkNum = 10000;

    public GameObject[] hotBarSlots;

    public GameObject playerUI;

    public GameObject invetoryUI;

    public GameObject pauseMenu;

    public GameObject player;

    public ChunkController[,] chunks;

    public float[] timers;

    public GameObject[] blocks;

    public Material texturePack;

    public GameObject highlighter;

    public GameObject hand;

    ChunkController lastPlayerChunk;

    public Canvas UICanvas;

    public bool onStart = true;

    float renderDist = 2;

    float sTime;

    public bool Done = true;


    public int seed = 1;

 
     List<ChunkController> chunksToGen = new List<ChunkController>();

    public int listLenth;

    int lastTime;
    
    void Start()
    {
        //writeBiomes();
        renderDist = Settings.renderDist;
        NoiseVars.chunkNum = chunkNum;
        NoiseVars.recalc(NoiseVars.Seed);
        
        chunks = new ChunkController[chunkNum,chunkNum];
        
        
        player.transform.position = NoiseVars.spawnPoint;

        place(new Vector2(NoiseVars.spawnPoint.x,NoiseVars.spawnPoint.z));
        
        
        

        refreshRenderDist();
        sTime= Time.time;

    }

    private void Update()
    {
        listLenth = chunksToGen.Count;
        if(Time.realtimeSinceStartup > 10 && onStart && listLenth ==0)
        {
            onStart = false;
            player = new Player(NoiseVars.spawnPoint, this).p;
            playerUI.SetActive(false);
            playerUI.SetActive(true);
        }
        if(lastPlayerChunk != null)
        {
            
            if (lastPlayerChunk != GetChunkFromVector3(player.transform.position) ||Time.time > lastTime + 4)
            {
                lastTime = (int)Time.time;
                refreshRenderDist();

            }
        }
        
    }

    void writeBiomes()
    {
        string s = "";
        string b = "public static Biome[] biomes = new Biome[]{";
        for (int i = 10;i< 90; i++)
        {
            s += "public static Biome Chunk_" + i + " = new Biome(\"Chunk_" + i + "\" "+ ", " + (90 - i)+","+ i + " , new Block[] { new Block(2), new Block(1) });\n";
            b += "Chunk_" + i + ", ";
        }
        b += "};";
        Debug.Log(s);
        Debug.Log(b);
    }

    void refreshRenderDist()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z)/16;
        for (int i = (int)(playerPos.x - (renderDist +3)); i < (int)(playerPos.x + (renderDist + 3)); i++)
        {
            for (int j = (int)(playerPos.y - (renderDist + 3)); j < (int)(playerPos.y + (renderDist + 3)); j++)
            {
                if (i < 0 || i >= chunkNum) break;
                if (j >= 0 && j <chunkNum-1)
                {
                    Vector2 pos = new Vector2(i, j);
                    float d = Vector2.Distance(playerPos, pos);
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
                    else if (d < renderDist)
                    {
                        chunks[i, j].chunkObject.SetActive(true);
                    }
                    else
                    {
                        if (chunks[i, j] != null && chunks[i,j].chunkObject != null && chunks[i, j].chunkObject.activeSelf) chunks[i, j].chunkObject.SetActive(false);
                    }
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

        int r = biomeIndex(i,j);
        
        ChunkController c = new ChunkController(new Vector3((i * 16), 0,(j * 16)), this,Biome.biomes[r]);
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
        if (x < 0 || z < 0 || x>= chunkNum || z>= chunkNum-1)
            return chunks[1,1];

        return chunks[x,z];

        

    }

    public int biomeIndex(int i,int j)
    {
        float x = NoiseVars.bXOff + i;
        float z = NoiseVars.bZOff + j;
        int r;
        r = Mathf.RoundToInt((Mathf.PerlinNoise(x /(Biome.biomes.Length * 3) , z / (Biome.biomes.Length * 3)) *Biome.biomes.Length));
        
        return r;
    }

    public void changeRenderDist(float r)
    {
        this.renderDist = r;
    }
}
