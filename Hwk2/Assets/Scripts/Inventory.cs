using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Setting global_setting;
    public bool HasKey = false;

    void Start(){
        //assign global setting
        global_setting = GameObject.Find("Global").GetComponent<Setting>();
    }

    private void Update()
    {
//        if(Input.GetKeyDown(KeyCode.Q)) HasKey = !HasKey;
        if(Input.GetKeyDown(KeyCode.Q)){ // for testing purposes because i dont know where the key logic is
            HasKey = !HasKey;
         //   global_setting.enableRoomChange = HasKey;
            //Debug.Log("Got Key");
            HasKey = !HasKey; // this is just for debugging.
        }
    }
}
