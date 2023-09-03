using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{

    float reach = 2.5f;
    Camera cam;
    public GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("chunk");
        if (Physics.Raycast(transform.position,transform.forward,out hit, reach))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject g = hit.collider.transform.gameObject;
                Vector3 p = hit.point;
                p += transform.forward * .01f;
                p -= g.transform.position;
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                ChunkController c = g.GetComponent<ChunkController>();
                c.blocks[(int)p.x, (int)p.y, (int)p.z] = controller.GetComponent<PlaceChunks>().blocks[0];
                c.GenerateMesh();
                
                
                
                
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                GameObject g = hit.collider.transform.gameObject;
                Vector3 p = hit.point;
                p += transform.forward * -.01f;
                p -= g.transform.position;
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                ChunkController c = g.GetComponent<ChunkController>();
                c.blocks[(int)p.x, (int)p.y, (int)p.z] = controller.GetComponent<PlaceChunks>().blocks[1];
                c.GenerateMesh();

            }
        }
    }

   
}
