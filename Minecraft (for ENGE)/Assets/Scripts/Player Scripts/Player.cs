using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player 
{
    public GameObject p;
    GameObject cam;
    
    public Player(Vector3 pos, PlaceChunks controller )
    {

        p = new GameObject("player");

        p.transform.position = pos;

        p.AddComponent<PlayerMove>();
        p.tag = "Player";
        PlayerInvetory i = p.AddComponent <PlayerInvetory>();
        i.hand = controller.hand;
        i.controller = controller;
        
        p.AddComponent<CharacterController>().radius = .4f;
        p.GetComponent<CharacterController>().height = 1.8f;
        
        PlayerStats ps = p.AddComponent<PlayerStats>();

        ps.hearts = controller.hearts;

        p.GetComponent<PlayerMove>().playerStats = ps;


        cam = new GameObject("eyes");

        
        cam.AddComponent<PlayerLook>().player = p.transform;
        cam.AddComponent<Build>().controller = controller;
        cam.AddComponent<Camera>();
        cam.GetComponent<Camera>().backgroundColor = new Color32(168, 193, 233, 0);
        //controller.UICanvas.worldCamera = cam.GetComponent<Camera>();
        pos.y +=.5f;
        cam.transform.position = pos;
        cam.transform.SetParent(p.transform);
        i.hand.transform.SetParent(cam.transform);
        i.hand.transform.localPosition = new Vector3(.25f, -.25f, .5f);



    }
}
