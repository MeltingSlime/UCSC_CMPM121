using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSetLogic : MonoBehaviour
{
    int currRoom = 0;


    private GameObject global;
    private Initialization global_init;


    [HideInInspector]
    public void NextRoomCheck(){ // Won't be called due to destroying

        Debug.Log("run run run");
        global = GameObject.Find("Global");
        global_init = global.GetComponent<Initialization>();

        if (global_init.keyList.Find(x => x.name == "Blox").isInteracted)
        {
            currRoom = 1;
        }
        if(global_init.keyList.Find(x => x.name == "Box").isInteracted && global_init.keyList.Find(x => x.name == "Notepad").isInteracted)
        {
            currRoom = 2;
        }
        if(global_init.keyList.Find(x => x.name == "Trophy").isInteracted)
        {
            currRoom = -1;
        }
        updateSet(currRoom);
    }

    [HideInInspector]
    public void updateSet(int room){

        if(currRoom != -1)
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                //if (i == room)
                //{
                //    transform.GetChild(i).gameObject.SetActive(true);
                //}
                //else
                //{
                //    transform.GetChild(i).gameObject.SetActive(false);
                //}
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }




        ////Debug.Log("PPP" + room.ToString());
        //int i = 0;
        //foreach(Transform child in transform){
        //    if (i == room)
        //        child.gameObject.SetActive(true);
        //    else
        //        child.gameObject.SetActive(false);
        //    i++;
        //}
    }
}
