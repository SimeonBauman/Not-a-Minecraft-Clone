using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMen;
    public GameObject player = null;
    public GameObject controller;
    public GameObject graphicsSettings;
    public GameObject controllsSettings;
    public GameObject MainSettings;
    // Start is called before the first frame update
    void Start()
    {
        pauseMen.SetActive(false);
        MainSettings.SetActive(false);
        graphicsSettings.SetActive(false);
        controllsSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        openSettings();
    }
    public void openSettings(bool throughUI = false)
    {
        
        if ((Input.GetKeyDown(PlayerControlls.OpenSettings) && !controller.GetComponent<PlaceChunks>().invetoryUI.activeSelf) || throughUI) {
            if (player == null)
            {
                player = controller.GetComponent<PlaceChunks>().player;
            }

            if (!pauseMen.activeSelf)
            {
               if(graphicsSettings.activeSelf || controllsSettings.activeSelf)
                {
                    graphicsSettings.SetActive(false);
                    controllsSettings.SetActive(false);
                }
                player.GetComponentInChildren<PlayerLook>().canMove = false;



                pauseMen.SetActive(true);
                MainSettings.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                //player.GetComponent<PlayerInvetory>().canMove = true;
                player.GetComponentInChildren<PlayerLook>().canMove = true;
                pauseMen.SetActive(false);
                MainSettings.SetActive(false);
                graphicsSettings.SetActive(false);
                controllsSettings.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void exitWorld()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenGraphicsSettings()
    {
        if (graphicsSettings.activeSelf)
        {
            graphicsSettings.SetActive(false );
            pauseMen.SetActive(true);
        }
        else
        {
            graphicsSettings.SetActive(true);
            pauseMen.SetActive(false);
        }
    }

    public void OpenControllsSettings()
    {
        if (controllsSettings.activeSelf)
        {
            controllsSettings.SetActive(false);
            pauseMen.SetActive(true);
        }
        else
        {
            controllsSettings.SetActive(true);
            pauseMen.SetActive(false);
        }
    }

    public void saveWorld()
    {
        CreateWorldFiles.saveWorld("Assets/Worlds/" + NoiseVars.name +"/", controller.GetComponent<PlaceChunks>().createdChunks,player);
    }

}
