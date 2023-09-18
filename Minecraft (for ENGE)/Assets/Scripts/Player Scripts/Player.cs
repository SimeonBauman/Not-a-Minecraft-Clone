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
     
        
       
        p.AddComponent<PlayerMove>();
        p.AddComponent<CharacterController>().radius = .45f;
        cam = new GameObject("eyes");
        cam.AddComponent<PlayerLook>().player = p.transform;
        cam.AddComponent<Build>().controller = controller;
        cam.AddComponent<Camera>();
        p.transform.position = pos;
        pos.y = pos.y + .5f;
        cam.transform.position = pos;
        cam.transform.SetParent(p.transform);

        
        
    }
}
