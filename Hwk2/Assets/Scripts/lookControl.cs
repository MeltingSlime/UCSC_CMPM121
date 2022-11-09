using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool enableCursor;

    float mouseX ;
    float mouseY ;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //change cursor visibility
        if(enableCursor == false)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }



        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        //mouse X will rotate player
        playerBody.Rotate(Vector3.up * mouseX);

        //mouse Y will NOT rotate player but rotate camera
        xRotation -= mouseY;
        //limit the angle, so it won't look behind player
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
