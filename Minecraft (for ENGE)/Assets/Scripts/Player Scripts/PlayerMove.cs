using System.Collections;
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
    {
        

        
        if (canMove)
        {
            move();
        }
        
    }

    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        if (controller.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKey("space"))
            {
                vSpeed = 8;
            }
        }

        vSpeed -= 29.4f * Time.deltaTime;

        move.y = vSpeed;

        controller.Move(move * Time.deltaTime);
    }
}
