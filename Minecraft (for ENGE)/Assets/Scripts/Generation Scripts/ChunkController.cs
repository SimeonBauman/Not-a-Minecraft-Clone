using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public PlaceChunks controller;
    public GameObject player;
    public float time;
    public int index;

    [SerializeField]
    public GameObject[,,] blocks;
    bool exists = false;

    byte[,,] Voxels;
    private void Start()
    {
        controller.timers[index] = 0;
        blocks = new GameObject[18,124,18];
        generateChunk();
        check();
    }
    private void Update()
    {
        Vector3 pos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        float dist = Vector3.Distance(transform.position, pos);

        if(dist > 50)
        {
            time = (dist / 10) - 3;
            controller.timers[index] = Time.time + time;
            transform.gameObject.SetActive(false);
        }
    }
    public void check()
    {
        transform.gameObject.SetActive(true);
        Vector3 pos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        float dist = Vector3.Distance(transform.position, pos);

        if (dist > 50)
        {
            time = (dist / 10) - 3;
            controller.timers[index] = Time.time + time;
            transform.gameObject.SetActive(false);
        }
        else if(!exists)
        {
            Debug.Log("creatting");
            GenerateMesh();
            exists = true;
        }
        
    }

    void generateChunk()
    {
        float offX = (transform.position.x - 7.5f);
        float offZ = (transform.position.z - 7.5f) ;

        for (int y = 0; y < 124; y++) 
        {
            for (int x = 0; x < 18; x++)
            {
                for(int z = 0; z < 18; z++)
                {
                    Debug.Log((int)(Mathf.PerlinNoise(10 * (x + offX), 10 * (z + offZ))) + 1);
                    if(x == 0 || z== 0 || x == 17 || z == 17)
                    {
                        blocks[x, y, z] = null;
                    }
                    else if((int)(Mathf.PerlinNoise((x + offX)/16 , (z + offZ)/16 ) * 2) + 1 >= y)
                    {

                        blocks[x, y, z] = controller.blocks[1];

                    }
                    else
                    {
                        blocks[x, y, z] = controller.blocks[0];
                    }
                }
            }
        }
    }

  

    private void GenerateMesh()
    {
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int x = 1; x < 18 - 1; x++)
            for (int y = 1; y < 124 - 1; y++)
                for (int z = 1; z < 18 - 1; z++)
                {
                    Vector3[] VertPos = new Vector3[8]{
                        new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                        new Vector3(1, 1, 1), new Vector3(1, 1, -1),
                        new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),
                        new Vector3(1, -1, 1), new Vector3(1, -1, -1),
                    };

                    int[,] Faces = new int[6, 9]{
                        {0, 1, 2, 3, 0, 1, 0, 0, 0},     //top
                        {7, 6, 5, 4, 0, -1, 0, 1, 0},   //bottom
                        {2, 1, 5, 6, 0, 0, 1, 1, 1},     //right
                        {0, 3, 7, 4, 0, 0, -1,  1, 1},   //left
                        {3, 2, 6, 7, 1, 0, 0,  1, 1},    //front
                        {1, 0, 4, 5, -1, 0, 0,  1, 1}    //back
                    };

                    if (blocks[x, y, z] != controller.blocks[0])
                        for (int o = 0; o < 6; o++)
                            if (blocks[x + Faces[o, 4], y + Faces[o, 5], z + Faces[o, 6]] == controller.blocks[0])
                                AddQuad(o, Verticies.Count);

                    void AddQuad(int facenum, int v)
                    {
                        // Add Mesh
                        for (int i = 0; i < 4; i++) Verticies.Add(new Vector3(x, y, z) + VertPos[Faces[facenum, i]] / 2f);
                        Triangles.AddRange(new List<int>() { v, v + 1, v + 2, v, v + 2, v + 3 });

                        // Add uvs
                        Vector2 bottomleft = new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / 2f;

                        uv.AddRange(new List<Vector2>() { bottomleft + new Vector2(0, 0.5f), bottomleft + new Vector2(0.5f, 0.5f), bottomleft + new Vector2(0.5f, 0), bottomleft });
                    }
                }

        GetComponent<MeshFilter>().mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };
        GetComponent<MeshCollider>().sharedMesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };

    }
}
