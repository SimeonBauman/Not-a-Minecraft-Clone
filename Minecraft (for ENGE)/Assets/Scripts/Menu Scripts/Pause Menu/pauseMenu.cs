using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMen;
    public GameObject player = null;
    public GameObject controller;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        openSettings();
    }
    void openSettings()
    {
        
        if ((Input.GetKeyDown(KeyCode.Escape) && !controller.GetComponent<PlaceChunks>().invetoryUI.activeSelf)) {
            if (player == null)
            {
                player = controller.GetComponent<PlaceChunks>().player;
            }

            if (!pauseMen.activeSelf)
            {
               
                player.GetComponentInChildren<PlayerLook>().canMove = false;



                pauseMen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                //player.GetComponent<PlayerInvetory>().canMove = true;
                player.GetComponentInChildren<PlayerLook>().canMove = true;
                pauseMen.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
