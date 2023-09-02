using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChunks : MonoBehaviour
{
    private int chunkNum = 2;

    public GameObject chunk;

    public GameObject player;

    public ChunkController[] chunks;

    public float[] timers;

    public GameObject[] blocks;

    void Start()
    {
        chunks = new ChunkController[chunkNum * chunkNum];
        timers= new float[chunkNum * chunkNum];
        
        place();
    }

    

    private void place()
    {
        int startx = 0 - ((chunkNum / 2) * 16);
        int startz = 0 - ((chunkNum / 2) * 16);

        int num = 0;

        for(int i = 0; i < chunkNum; i++)
        {
            for(int j = 0; j < chunkNum; j++)
            {
                ChunkController c = Instantiate(chunk, new Vector3((i * 16) + startx, 0, (j * 16) + startz), transform.rotation).GetComponent<ChunkController>(); ;
                chunks[num] = c;
                c.controller = this;
                c.player = player;
                c.index = num;
                num++;
            }
        }

    }

    private void Update()
    {
        for(int i = 0; i < timers.Length; i++)
        {
            if (timers[i] <= Time.time)
            {
                chunks[i].check();
            }
        }
    }
}
