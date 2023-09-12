using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    public int speed = 10;
    float vSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;
        
        if (controller.isGrounded)
        {
            vSpeed = 0; 
            if (Input.GetKey("space"))
            { 
                vSpeed = 4;
            }
        }

        vSpeed -= 9.8f * Time.deltaTime;

        move.y = vSpeed;

        controller.Move(move  * Time.deltaTime);

        

        
    }
}
