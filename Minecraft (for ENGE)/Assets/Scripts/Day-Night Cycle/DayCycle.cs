using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float seconds;
    public GameObject sun;
    public GameObject controller;
    public GameObject player;
    public GameObject tempPlayer;
    public int dayLength = 900;
    public Color32 cameraBackGround = new Color32(168, 193, 233, 0);
    // Start is called before the first frame update
    private void Start()
    {
        seconds = dayLength / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || player == tempPlayer)
        {
            player = controller.GetComponent<PlaceChunks>().player;
            transform.position = new Vector3(player.transform.position.x, 200, player.transform.position.z);
        }
        else
        {
            seconds += Time.deltaTime;
            transform.RotateAround(player.transform.position, transform.right, 360 / dayLength * Time.deltaTime);
            if (seconds / dayLength > .65)
            {
                player.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(cameraBackGround, Color.black,((seconds/dayLength)-.65f)*5);
            }
            else if(seconds/dayLength > .15)
            {
                player.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(Color.black,cameraBackGround, ((seconds / dayLength) - .15f) * 5);
            }
            else
            {
                //player.GetComponentInChildren<Camera>().backgroundColor = cameraBackGround;
            }
            if (seconds > dayLength)
            {
                seconds = 0;
            }
        }
    }
}
