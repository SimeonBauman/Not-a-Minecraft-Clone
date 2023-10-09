using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkController
{
    public PlaceChunks controller;
    public GameObject player;
    public float time;
    public int[] index;


    public Block[,,] blocks;
    bool exists = false;

    byte[,,] Voxels;

    public bool created = false;

    public GameObject chunkObject;
    MeshCollider meshCollider;
    MeshFilter meshFilter;

   bool small = false;

    Vector3 size = new Vector3(16, 124, 16);

    Biome biome;

    Vector3[] VertPos = new Vector3[8]{
                        new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                        new Vector3(1, 1, 1), new Vector3(1, 1, -1),
                        new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),
                        new Vector3(1, -1, 1), new Vector3(1, -1, -1),
                    };

    int[,] Faces = new int[6, 9]{
                        {0, 1, 2, 3, 0, 1, 0, 0, 0},     //top
                        {7, 6, 5, 4, 0, -1, 0, 0, 0},   //bottom
                        {2, 1, 5, 6, 0, 0, 1, 1, 1},     //right
                        {0, 3, 7, 4, 0, 0, -1,  1, 1},   //left
                        {3, 2, 6, 7, 1, 0, 0,  1, 1},    //front
                        {1, 0, 4, 5, -1, 0, 0,  1, 1}    //back
                    };


    public ChunkController(Vector3 pos, PlaceChunks controller, Biome b)
    {

        this.controller = controller;
        chunkObject = new GameObject().gameObject;
        chunkObject.name = b.name;
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        this.biome = b;
        meshCollider = chunkObject.AddComponent<MeshCollider>();
        chunkObject.AddComponent<MeshRenderer>().material = controller.texturePack;
        chunkObject.transform.position = pos;
        chunkObject.layer = 6;
        blocks = new Block[(int)size.x, (int)size.y, (int)size.z];


    }



    public IEnumerator generateChunk(bool onCreate = false)
    {

        Random.seed = controller.seed;
        float offX = (chunkObject.transform.position.x - 7.5f + NoiseVars.xOff);
        float offZ = (chunkObject.transform.position.z - 7.5f + NoiseVars.zOff);

        int airBlocks = 0;
        bool finish = false;
        for (int y = 0; y < size.y - 0; y++)
        {
            for (int x = 0; x < size.x - 0; x++)
            {
                for (int z = 0; z < size.z - 0; z++)
                {

                    if (finish)
                    {
                        blocks[x, y, z] = new Block(0);
                    }
                    else
                    {
                        float pX = ((x + offX));
                        float pZ = ((z  + offZ)) ;
                        float pNoise = (Mathf.PerlinNoise(pX / 50, pZ / 50) * biome.biomeStepth) + 25;
                        float pNoise2 = (Mathf.PerlinNoise(pX / 75, pZ / 75) * biome.biomeStepth / 2) + 25;


                        if (y <= 50 && pNoise2 >= y)
                        {

                            blocks[x, y, z] = new Block(layers(y,pNoise2,x));




                        }
                        else if (pNoise >= y)
                        {


                            blocks[x, y, z] = new Block(layers(y,pNoise,x));


                        }
                        
                        else
                        {
                            blocks[x, y, z] = new Block(0);
                            airBlocks++;
                        }
                    }

                }


            }
            if (!onCreate)
                yield return null;

            if (airBlocks == 256) {

               finish = true;

            }
            else airBlocks = 0;

            
        }
        placeTree();

       

    }

    public void GenerateMesh(bool onCreate = false)
    {
        
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int x = 0; x < size.x; x++) { 
            for (int y = 1; y < size.y - 1; y++) { 
                for (int z = 0; z < size.z; z++)
                {

                    if (blocks[x, y, z] != null && blocks[x, y, z].textIndex != 0)
                    {
                        
                        for (int o = 0; o < 6; o++)
                        {
                            int nX = x + Faces[o, 4];
                            int nZ = z + Faces[o, 6];



                            if ((nX == -1 || nX == 16) && (nZ == -1 || nZ == 16))
                            {
                                ChunkController c = controller.chunks[index[0] + (nX % 15), index[1] + (nZ % 15)];
                                nX += 16;
                                nX %= 16;
                                nZ += 16;
                                nZ %= 16;
                                if (c != null && c.blocks[nX, y, nZ] != null)
                                    if (c.blocks[nX, y, nZ].textIndex == 0)
                                        AddQuad(o, Verticies.Count);

                            }
                            else if (nX == -1 || nX == 16)
                            {
                                ChunkController c = controller.chunks[index[0] + (nX % 15), index[1]];
                                nX += 16;
                                nX %= 16;
                                if (c != null && c.blocks[nX, y, z] != null)
                                    if (c.blocks[nX, y, z].textIndex == 0)

                                        AddQuad(o, Verticies.Count);


                            }
                            else if (nZ == -1 || nZ == 16)
                            {
                                ChunkController c = controller.chunks[index[0], index[1] + (nZ % 15)];
                                nZ += 16;
                                nZ %= 16;
                                if (c != null && c.blocks[x, y, nZ] != null)
                                    if (c.blocks[x, y, nZ].textIndex == 0)

                                        AddQuad(o, Verticies.Count);


                            }
                            else if (blocks[nX, y + Faces[o, 5], nZ] == null || blocks[nX, y + Faces[o, 5], nZ].textIndex == 0)
                            {
                                AddQuad(o, Verticies.Count);
                            }
                        }
                    }

                    void AddQuad(int facenum, int v)
                    {
                        // Add Mesh
                        for (int i = 0; i < 4; i++) Verticies.Add(new Vector3(x, y, z) + VertPos[Faces[facenum, i]] / 2f);
                        Triangles.AddRange(new List<int>() { v, v + 1, v + 2, v, v + 2, v + 3 });

                        // Add uvs
                        Vector2 bottomleft = new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / (2f * (controller.blocks.Length - 1));
                        bottomleft.x += (blocks[x, y, z].textIndex-1) / (controller.blocks.Length - 1);
                        uv.AddRange(new List<Vector2>() { bottomleft + new Vector2(0, 0.5f / (controller.blocks.Length - 1)), bottomleft + new Vector2(0.5f / (controller.blocks.Length - 1), 0.5f / (controller.blocks.Length - 1)), bottomleft + new Vector2(0.5f / (controller.blocks.Length - 1), 0), bottomleft });
                    }

                    
                    
                }
                
                
            }
            

        }
        

        meshFilter.mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };
        meshCollider.sharedMesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };

      
    }

    public IEnumerator createMesh()
    {
        created = true;
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 1; y < size.y - 1; y++)
            {
                for (int z = 0; z < size.z; z++)
                {

                    if (blocks[x, y, z] != null && blocks[x, y, z].textIndex != 0)
                    {
                        checkBlockUpdate(new int[] { x, y, z }, y + blocks[x, y + 1, z].textIndex);
                        for (int o = 0; o < 6; o++)
                        {
                            int nX = x + Faces[o, 4];
                            int nZ = z + Faces[o, 6];

                            

                            Vector3 b = new Vector3(x + Faces[o, 4] - 7.5f + NoiseVars.xOff, y + Faces[o,5], z + Faces[o, 6] - 7.5f + NoiseVars.zOff);
                            

                            ChunkController c = null;

                            if ((nX == -1 || nX == 16 || nZ == -1 || nZ == 16))
                            {
                                if ((nX == -1 || nX == 16) && (nZ == -1 || nZ == 16))
                                {
                                    c = controller.chunks[index[0] + (nX % 15), index[1] + (nZ % 15)];
      

                                }
                                else if (nX == -1 || nX == 16)
                                {
                                    c = controller.chunks[index[0] + (nX % 15), index[1]];
  


                                }
                                else if (nZ == -1 || nZ == 16)
                                {
                                    c = controller.chunks[index[0], index[1] + (nZ % 15)];
   

                                }

                                b += chunkObject.transform.position;
                                
                                if ((Mathf.PerlinNoise((b.x / 50), (b.z / 50)) * c.biome.biomeStepth) + 25 < b.y)
                                {

                                    AddQuad(o, Verticies.Count);
                                }
                               


                            }

                            else if (blocks[nX, y + Faces[o, 5], nZ] == null || blocks[nX, y + Faces[o, 5], nZ].textIndex == 0 )
                            {
                                AddQuad(o, Verticies.Count);
                            }
                        }
                    }
                    void AddQuad(int facenum, int v)
                    {
                        // Add Mesh
                        for (int i = 0; i < 4; i++) Verticies.Add(new Vector3(x, y, z) + VertPos[Faces[facenum, i]] / 2f);
                        Triangles.AddRange(new List<int>() { v, v + 1, v + 2, v, v + 2, v + 3 });

                        // Add uvs
                        Vector2 bottomleft = new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / (2f * (controller.blocks.Length - 1));
                        bottomleft.x += (blocks[x, y, z].textIndex-1) / (controller.blocks.Length - 1);
                        uv.AddRange(new List<Vector2>() { bottomleft + new Vector2(0, 0.5f / (controller.blocks.Length - 1)), bottomleft + new Vector2(0.5f / (controller.blocks.Length - 1), 0.5f / (controller.blocks.Length - 1)), bottomleft + new Vector2(0.5f / (controller.blocks.Length - 1), 0), bottomleft });
                    }



                }
                

            }
            yield return null;

        }


        meshFilter.mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };
        meshCollider.sharedMesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };

        controller.Done = true;
    }


    void checkBlockUpdate(int[] b, float y)
        
    {
        //Debug.Log(b[1] + " , " + y);
        if(b[1] == y && blocks[b[0], b[1], b[2]].textIndex == 1 )
        {
            blocks[b[0], b[1], b[2]] = new Block(2);
        }
        else if(b[1] < y && blocks[b[0], b[1], b[2]].textIndex == 2)
        {
            blocks[b[0], b[1], b[2]] = new Block(1);
        }
    }

    void placeTree()
    {
        int seed = Random.seed;
        int pos = 0;
        Random.seed = NoiseVars.Seed;
        for (int i = 0; i < 5; i++)
        {

           
            
            
            float r = Random.Range(0, 100);
         
            if ( r < biome.treeOdds)
            {

                int xPos = Random.Range(2, 14); ;
                
                int zPos = Random.Range(2, 14);
               
               
                for (int y = 1; y < 123; y++)
                {
                    if (blocks[xPos, y, zPos] != null && blocks[xPos, y, zPos].textIndex == 0 && (blocks[xPos, y-1, zPos].textIndex == 1 || blocks[xPos, y-1, zPos].textIndex == 2))
                    {
                        pos = y;
                        
                        break;
                    }
                }

                if (pos != 0)
                {

                    for (int y = 0; y < Tree.treeTemp.GetLength(0); y++)
                    {
                        for (int x = -2 + xPos; x <= 2 + xPos; x++)
                        {
                            for (int z = -2 + zPos; z <= 2 + zPos; z++)
                            {
                                if (blocks[x, y + pos, z].textIndex == 0)
                                {
                                    this.blocks[x, y + pos, z] = new Block(Tree.treeTemp[Tree.treeTemp.GetLength(0) - 1 - y, x + 2 - xPos, z + 2 - zPos]);
                                }
                            }
                        }
                    }
                }
                Random.seed = Mathf.RoundToInt((pos * Mathf.PerlinNoise(chunkObject.transform.position.x / 50, chunkObject.transform.position.z / 50) * biome.biomeStepth) + 25);
                
            }
           
            pos = 0;
        }
        Random.seed = seed;
    }
    

    int layers(float y, float height, int xPos)
    {
        float depth = height - y;
        Random.seed = Mathf.RoundToInt((depth * xPos *  Mathf.PerlinNoise(chunkObject.transform.position.x / 50, chunkObject.transform.position.z / 50) * biome.biomeStepth) + 25);
        int returnVal;
        float randRang = Random.Range(0, 1000);
       // Debug.Log(randRang);
        if (depth > 3)
        {
            if ((height - 8) < depth && randRang < 1)
            {
                returnVal = 6;
                
            }
            else if ((height - 20) < depth && randRang < 2)
            {
                returnVal = 8;

            }
            else if((height - 45) < depth && randRang < 10 )
            {
                returnVal = 7;

            }
            

            else
            {
                returnVal = 5;
            }

        }
        else
        {
            returnVal =  2;
        }
        Random.seed = NoiseVars.Seed;
        return returnVal;
        
    }
}