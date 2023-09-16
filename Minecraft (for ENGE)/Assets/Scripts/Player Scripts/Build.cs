using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Build : MonoBehaviour
{

    float reach = 5f;
    Camera cam;
    public PlaceChunks controller;
    public GameObject highLighter;
    // Start is called before the first frame update

    private void Start()
    {
        highLighter = controller.highlighter;
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("chunk");
        if (Physics.Raycast(transform.position,transform.forward,out hit, reach))
        {
            GameObject g = hit.collider.transform.gameObject;
            Vector3 p = hit.point;
            drawBox(p);
            if (Input.GetButtonDown("Fire1"))
            {
                
                
                p += transform.forward * .01f;
                p -= g.transform.position;
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                
                ChunkController c = controller.GetChunkFromVector3(g.transform.position);

                c.blocks[(int)p.x, (int)p.y, (int)p.z] = new Block(0);
                c.GenerateMesh();

                checkForChunkEdge(p,c);


            }
            else if (Input.GetButtonDown("Fire2"))
            {
               
                
                p += transform.forward * -.01f;
                
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                ChunkController c = controller.GetChunkFromVector3(p);
                g = c.chunkObject;
                p -= g.transform.position;

                c.blocks[(int)p.x, (int)p.y, (int)p.z] = new Block(1);
                c.GenerateMesh();

            }
        }
        else
        {
            highLighter.SetActive(false);
        }
    }

    void drawBox(Vector3 p)
    {
        highLighter.SetActive(true);
        p += transform.forward * .01f;
        p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));

        highLighter.transform.position = p;
    }

    void checkForChunkEdge(Vector3 p, ChunkController n)
    {
       
        
        
        if (p.x == 0 || p.x == 15)
        {
            ChunkController c = controller.chunks[n.index[0] + ((int)p.x % 13)-1, n.index[1]];
           
            
            c.GenerateMesh();
            

        }
         if (p.z == 0 || p.z == 16)
        {
            ChunkController c = controller.chunks[n.index[0], n.index[1] + ((int)p.z % 13)-1];
           
            c.GenerateMesh();
            

        }

    }
}
