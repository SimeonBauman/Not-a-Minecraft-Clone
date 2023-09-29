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
                        if ((Mathf.PerlinNoise(pX/50,pZ /50) *biome.biomeStepth)  +25>= y || y < 50)
                        {
                            int r = Random.Range(1, 3);
                            blocks[x, y, z] = new Block((int)biome.blockLayers[0].textIndex);




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

        float avgStepths()
        {
            return (Biome.biomes[controller.biomeIndex(index[0] - 1, index[1])].biomeStepth + Biome.biomes[controller.biomeIndex(index[0] + 1, index[1])].biomeStepth + Biome.biomes[controller.biomeIndex(index[0], index[1] - 1)].biomeStepth + Biome.biomes[controller.biomeIndex(index[0], index[1] + 1)].biomeStepth + biome.biomeStepth) / 5;
        }
        float avgHeights()
        {
            return (Biome.biomes[controller.biomeIndex(index[0] - 1, index[1])].biomeHeight + Biome.biomes[controller.biomeIndex(index[0] + 1, index[1])].biomeHeight + Biome.biomes[controller.biomeIndex(index[0], index[1] - 1)].biomeHeight + Biome.biomes[controller.biomeIndex(index[0], index[1] + 1)].biomeHeight + biome.biomeHeight) / 5;
        }

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
                        checkBlockUpdate(new int[] { x, y, z }, y + blocks[x, y + 1, z].textIndex);
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
                        bottomleft.x += blocks[x, y, z].textIndex / (controller.blocks.Length - 1);
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
                        //checkBlockUpdate(new int[] { x, y, z }, y + blocks[x, y + 1, z].textIndex);
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

                                if ((Mathf.PerlinNoise((b.x/50) , (b.z/50) )*c.biome.biomeStepth)  +25 < b.y && b.y>=50)
                                {
                                   
                                    AddQuad(o, Verticies.Count);
                                }

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
                        bottomleft.x += blocks[x, y, z].textIndex / (controller.blocks.Length - 1);
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
        if(b[1] == y && blocks[b[0], b[1], b[2]].textIndex == 2 )
        {
            blocks[b[0], b[1], b[2]] = new Block(1);
        }
        else if(b[1] < y && blocks[b[0], b[1], b[2]].textIndex == 1)
        {
            blocks[b[0], b[1], b[2]] = new Block(2);
        }
    }

    
}