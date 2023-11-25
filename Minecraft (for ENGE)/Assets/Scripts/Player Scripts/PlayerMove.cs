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
    float lastyPos = 0f;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<CharacterController>();
        canMove = true;
        lastyPos = transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {

        
        if (canMove)
        {
            move();
            fallDamage();
        }
        
    }

    void move()
    {
        
        
        float x = -Convert.ToInt32(Input.GetKey(PlayerControlls.Left)) + Convert.ToInt32(Input.GetKey(PlayerControlls.Right));
        float z = -Convert.ToInt32(Input.GetKey(PlayerControlls.Backword)) + Convert.ToInt32(Input.GetKey(PlayerControlls.Forword));

   


        Vector3 move = (transform.right  * x + transform.forward * z) * speed;

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

    void fallDamage()
    {
        if(controller.isGrounded)
        {
            if(lastyPos - transform.position.y  > 4)
            {
                int damage = Mathf.RoundToInt(lastyPos - transform.position.y - 4);
                playerStats.takeDamage(damage);
                Debug.Log("did " + damage);
            }
            lastyPos = transform.position.y;

        }
    }
}
