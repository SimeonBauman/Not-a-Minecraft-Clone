using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInvetory : MonoBehaviour
{

    public Vector2[] invetory = new Vector2[50];
    public  int currentIndex;
    public GameObject hand;
    public PlaceChunks controller;
    public GameObject[] hotBar = new GameObject[10];
    PlayerMove pm;
    PlayerLook pl;
    public bool canMove;
    public GameObject invetoryVisuals;
    public GameObject pauseMenu;

    private void Start()
    {
        canMove = true;
        hotBar = controller.hotBarSlots;
        pm = GetComponent<PlayerMove>();
        pl = GetComponentInChildren<PlayerLook>();
        invetoryVisuals = controller.invetoryUI;
        pauseMenu = controller.pauseMenu;

        for(int i =0; i < hotBar.Length; i++)
        {
            updateHotBar(i);
        }
    }

    void Update()
    {
        if (canMove)
        {
            actions();
        }
       // openInvetory();
        
    }

    void actions()
    {
        currentIndex -= (int)(Input.GetAxis("Mouse ScrollWheel") * 10) - 10;
        currentIndex %= 10;
        currentIndex = Mathf.Abs(currentIndex);
        hand.GetComponent<MeshRenderer>().material = controller.blocks[(int)invetory[currentIndex].x].GetComponent<MeshRenderer>().sharedMaterial;
    }


    void openInvetory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!invetoryVisuals.activeSelf)
            {
                pl.canMove = false;
                pm.canMove = false;
                canMove = false;
                invetoryVisuals.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pl.canMove = true;
                pm.canMove = true;
                canMove=true;
                invetoryVisuals.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

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
            updateHotBar(currentIndex);
            return temp;
        }

        return 0;
    }

    public void pickUp(int textIndex)
    {
        int t = returnIndex(textIndex);
        if(t == -1)
        {
            t = returnIndex(0);
            invetory[t].x = textIndex;
            invetory[t].y = 1;

        }
        else
        {
            invetory[t].y++;
        }
        updateHotBar(t);
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

    void updateHotBar(int i)
    {

        hotBar[i].GetComponentInChildren<MeshRenderer>().material = controller.blocks[(int)invetory[i].x].GetComponent<MeshRenderer>().sharedMaterial;
        
        if(invetory[i].y >= 1)
        {
            hotBar[i].GetComponentInChildren<TMP_Text>().text = ((int)invetory[i].y).ToString();
            hotBar[i].SetActive(true);
        }
        else
        {
            hotBar[i].SetActive(false);
        }
       
        
    }
}
