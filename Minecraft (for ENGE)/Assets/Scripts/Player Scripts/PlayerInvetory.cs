using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetory : MonoBehaviour
{

    public Vector2[] invetory = new Vector2[10];
    public  int currentIndex;
    public GameObject hand;
    public PlaceChunks controller;
    
    void Update()
    {
      
        currentIndex -= (int)(Input.GetAxis("Mouse ScrollWheel") * 10) -10;
        currentIndex %= 10;
        currentIndex = Mathf.Abs(currentIndex);
        hand.GetComponent<MeshRenderer>().material = controller.blocks[(int)invetory[currentIndex].x].GetComponent<MeshRenderer>().sharedMaterial;
    }
    public int placeBlock()
    {
        if(invetory[currentIndex].x != 0)
        {
            int temp = (int)invetory[currentIndex].x;
            invetory[currentIndex].y--;

            if(invetory[currentIndex].y == 0)
            {
                invetory[currentIndex].x = 0;
            }

            return temp;
        }

        return 0;
    }

    public void pickUp(int textIndex)
    {
        int t = returnIndex(textIndex);
        if(t == -1)
        {
            int i = returnIndex(0);
            invetory[i].x = textIndex;
            invetory[i].y = 1;

        }
        else
        {
            invetory[t].y++;
        }
    }

    int returnIndex(int textIndex)
    {
        for(int i = 0; i < invetory.Length; i++)
        {
            if ((int)invetory[i].x == textIndex && invetory[i].y < 64)
            {
                return i;
            }
        }

        return -1;
    }
}
