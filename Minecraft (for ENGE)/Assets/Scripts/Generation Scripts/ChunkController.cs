using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkController
{
    public PlaceChunks controller;
    public GameObject player;
    public float time;
    public int IPOS;
    public int JPOS;

    [SerializeField]
    public Block[,,] blocks;
    bool exists = false;

    byte[,,] Voxels;

    public bool created = false;

    public GameObject chunkObject;
    MeshCollider meshCollider;
    MeshFilter meshFilter;

    public int biomeStepth = 60;//the lower the Stepth the steeper the terrain is
    public int biomeHeight = 20;//the higher the height the higher the mountains will peak at

    Vector3 size = new Vector3(16, 124, 16);

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


    public ChunkController(Vector3 pos, PlaceChunks controller)
    {
        this.controller = controller;
        chunkObject = new GameObject();
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshCollider = chunkObject.AddComponent<MeshCollider>();
        chunkObject.AddComponent<MeshRenderer>().material = controller.texturePack;
        chunkObject.transform.position = pos;

        blocks = new Block[(int)size.x, (int)size.y, (int)size.z];


    }



    public IEnumerator generateChunk()
    {
        created = true;
        float offX = (chunkObject.transform.position.x - 7.5f);
        float offZ = (chunkObject.transform.position.z - 7.5f);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int z = 0; z < size.z; z++)
                {


                    if ((Mathf.PerlinNoise((x + offX) / biomeStepth, (z + offZ) / biomeStepth) * biomeHeight) + 50 >= y)
                    {
                        int r = Random.Range(1, 3);
                        blocks[x, y, z] = new Block(r);



                    }
                    else
                    {
                        blocks[x, y, z] = new Block(0);
                    }
                }
                yield return null;

            }


        }
        GenerateMesh(true);
    }



    public void GenerateMesh(bool onCreate = false, bool onBuild = false)
    {

        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        ChunkController chunktoRegen = null;

        

        for (int x = 0; x < size.x; x++)
            for (int y = 1; y < size.y - 1; y++)
                for (int z = 0; z < size.z; z++)
                {

                    if (blocks[x, y, z].textIndex != 0)
                    {
                        for (int o = 0; o < 6; o++)
                        {

                            Vector3 b = new Vector3(x + Faces[o, 4] - 7.5f, y + Faces[o, 5], z + Faces[o, 6] - 7.5f);
                            Vector3 b2 = new Vector3(x + Faces[o, 4] , y, z + Faces[o, 6] );
                            
                            b += chunkObject.transform.position;


                            if (x + Faces[o, 4] == -1 || x + Faces[o, 4] == 16 || z + Faces[o, 6] == -1 || z + Faces[o, 6] == 16 )
                            {
                                AddQuad(o, Verticies.Count);

                            }
                            else if (blocks[x + Faces[o, 4], y + Faces[o, 5], z + Faces[o, 6]] == null || blocks[x + Faces[o, 4], y + Faces[o, 5], z + Faces[o, 6]].textIndex == 0)
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

        /*float inde()
        {

            for(int i = 0; i < controller.blocks.Length; i++) 
            {
                if (controller.blocks[i].Equals( blocks[x, y, z]))
                {
                    Debug.Log("Matched" + i);
                    return i - 1 ;
                }
            }
            return 0;
        }*/
    


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
}
