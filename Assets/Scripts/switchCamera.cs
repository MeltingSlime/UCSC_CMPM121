using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{

    public List<Camera> Cameras;
    public GameObject player;

    private int currentCam;
    private PlayerMovement PM_Script;




    // Start is called before the first frame update
    void Start()
    {
        currentCam = 0;
        PM_Script = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        //disable other camera and enable the select one
        for (int i = 0; i < Cameras.Count; i++)
        {
            if(i == currentCam)
            {
                Cameras[i].enabled = true;

            }
            else
            {
                Cameras[i].enabled = false;
            }
        }//for end

        //press G will allow player to switch camera type
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentCam++;
            if(currentCam >= Cameras.Count)
            {
                currentCam = 0;
            }//if end

            if(currentCam == 0)
            {
                PM_Script.firstPersonEnabled = true;
                PM_Script.thirdPersonEnabled = false;

            }
            else if (currentCam == 1)
            {
                PM_Script.firstPersonEnabled = false;
                PM_Script.thirdPersonEnabled = true;
            }
            else
            {
                Debug.Log("Error. Incorrect Camera Type");
            }//if end


            //player.transform.localScale = new Vector3(0, 0, 0);

        }
    }
}
