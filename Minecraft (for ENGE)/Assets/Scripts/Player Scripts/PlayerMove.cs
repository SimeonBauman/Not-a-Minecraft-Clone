using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    public int speed = 5;
    float vSpeed;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<CharacterController>();
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.L))
        {
            Settings.worldList();
            Settings.printName();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Settings.printName();
        }*/

        
        if (canMove)
        {
            move();
        }
        
    }

    void move()
    {
        
        
        float x = -Convert.ToInt32(Input.GetKey(PlayerControlls.Left)) + Convert.ToInt32(Input.GetKey(PlayerControlls.Right));
        float z = -Convert.ToInt32(Input.GetKey(PlayerControlls.Backword)) + Convert.ToInt32(Input.GetKey(PlayerControlls.Forword));

   


        Vector3 move = (transform.right * Mathf.Abs(Input.GetAxis("Horizontal")) * x + transform.forward * Mathf.Abs(Input.GetAxis("Vertical")) * z) * speed;

        if (controller.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKey(PlayerControlls.Jump))
            {
                vSpeed = 8;
            }
        }

        vSpeed -= 29.4f * Time.deltaTime;

        move.y = vSpeed;

        controller.Move(move * Time.deltaTime);
    }
}
