using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    public float sensitivity = .5f;


    public bool canMove;


    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;   
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            look();
        }
    }

    void look()
    {
        float mouseX = Input.GetAxis("Mouse X") * PlayerControlls.sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * PlayerControlls.sensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
