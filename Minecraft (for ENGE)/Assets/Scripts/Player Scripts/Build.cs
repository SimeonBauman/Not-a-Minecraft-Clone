using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{

    float reach = 5f;
    Camera cam;
    public PlaceChunks controller;
    public GameObject highLighter;
    Image breakStatus;
    float breakTime = 0;
    BlockEntity targetedBlock;
    Vector3 OldP;
    // Start is called before the first frame update

    private void Start()
    {
        breakStatus = controller.UICanvas.GetComponentInChildren<Image>();
        highLighter = controller.highlighter;
        breakStatus.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("chunk");
        if (Physics.Raycast(transform.position,transform.forward,out hit, reach,layer_mask))
        {
            GameObject g = hit.collider.transform.gameObject;
            Vector3 p = hit.point;
            
            drawBox(p);
            if (Input.GetButton("Fire1"))
            {
                ChunkController c = controller.GetChunkFromVector3(g.transform.position);
                p += transform.forward * .01f;
                p -= g.transform.position;
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));

                if (breakStatus.fillAmount == 0)
                {
                    OldP = p;
                    
                    targetedBlock = controller.blocks[(int)c.blocks[(int)p.x, (int)p.y, (int)p.z].textIndex].GetComponent<BlockEntity>();
                    breakTime = Time.time;  
                }
                if(Time.time - breakTime >= targetedBlock.durability)
                {

                    

                    GameObject temp = (targetedBlock.drop);
                    if (temp != null)
                    {
                        Instantiate(temp, hit.point, Quaternion.identity);
                    }
                    
                    c.blocks[(int)p.x, (int)p.y, (int)p.z] = new Block(0);
                    c.GenerateMesh();

                    checkForChunkEdge(p, c);
                    breakStatus.fillAmount = 0;
                    breakTime = 0;
                }
                else if(OldP != p)
                {
                    
                    breakStatus.fillAmount = 0;
                    breakTime = 0;
                }
                else
                {
                    
                    breakStatus.fillAmount = ((Time.time - breakTime)/targetedBlock.durability )+ .1f;
                }
               


            }
            else if (Input.GetButtonDown("Fire2"))
            {
               
                
                p += transform.forward * -.01f;
                
                p = new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                ChunkController c = controller.GetChunkFromVector3(p);
                g = c.chunkObject;
                p -= g.transform.position;
                PlayerInvetory Pi = transform.parent.GetComponent<PlayerInvetory>();
                c.blocks[(int)p.x, (int)p.y, (int)p.z] = new Block(Pi.placeBlock());
                c.GenerateMesh();

            }
            else
            {
                breakStatus.fillAmount = 0;
                breakTime = 0;
            }
        }
        else
        {
            breakStatus.fillAmount = 0;
            breakTime = 0;
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
         if (p.z == 0 || p.z == 15)
        {
            ChunkController c = controller.chunks[n.index[0], n.index[1] + ((int)p.z % 13)-1];
           
            c.GenerateMesh();
            

        }

    }
}
