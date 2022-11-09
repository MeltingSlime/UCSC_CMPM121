 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Spawner : MonoBehaviour
{
    //grab global setting
    private Setting global_setting;
    


    //create a list of all prefabs
    private List<GameObject> prefabs_rooms;
    private List<GameObject> prefabs_triggers;
    private List<GameObject> prefabs_keys;

    


    //create a list of rooms
    private List<GameObject> rooms = new List<GameObject>();
    private GameObject defaultRoom;

    //create a list of stairs
    private List<GameObject> stairs = new List<GameObject>();
    private List<GameObject> stairsUp = new List<GameObject>();
    private GameObject defaultStairs;

    //other variable for generate rooms
    [HideInInspector] public bool enableGenerate;
    [HideInInspector] public bool furnitureSetGenerate = false;
    [HideInInspector] public bool generateUp = false;
    [HideInInspector] public bool generateDown = false;

    //create a list of trigger
    private List<GameObject> triggers = new List<GameObject>();
    private List<GameObject> triggersUp = new List<GameObject>();
    private GameObject defaultTrigger;
    private GameObject TriggerUp;
    private List<GameObject> triggersSet = new List<GameObject>();
    private List<GameObject> triggersSetUp = new List<GameObject>();
    private GameObject TriggerSetChange;

    //create a list of key
    private List<GameObject> furnitureSets = new List<GameObject>();
    private GameObject defaultFurnitureSets;






    //info about where to put the spawn room
    //public Vector3 init_Pos;
    public Vector3 roomPosChange;

    //info about trigger
    //public GameObject first_Trigger;
    //info about key
    //public GameObject first_Key;

    //local private variables
    Vector3 spawnPos;


    //room trigger keyObj Chest
    public List<GameObject> initialObjects;




    //int currRoom = 0;

    //local private store init pos

    Vector3 currentPos_Room;
    Vector3 currentPos_Trigger;
    Vector3 currentPos_Stairs;
    Vector3 currentPos_FurnitureSets;
    Vector3 currentPos_FurnitureSetsUp;

    Vector3 currentPos_RoomUp;
    Vector3 currentPos_StairsUp;
    Vector3 currentPos_TriggerUp;

    Vector3 currentPos_TriggerSet;
    Vector3 currentPos_TriggerSetUp;

    bool turnOnFurnitureSpawn = false;




    // Start is called before the first frame update
    void Start()
    {

        //init
        enableGenerate = false;
        currentPos_Room = initialObjects[0].transform.position;
        currentPos_RoomUp = currentPos_Room;
        currentPos_Stairs = initialObjects[1].transform.position;
        currentPos_Trigger = initialObjects[2].transform.position;
        currentPos_FurnitureSets = initialObjects[3].transform.position;
        currentPos_TriggerUp = initialObjects[4].transform.position;
        currentPos_StairsUp = initialObjects[5].transform.position;
        currentPos_TriggerSet = initialObjects[6].transform.position;
        currentPos_TriggerSetUp = initialObjects[7].transform.position;

        //push them into the list and remove from current list after getting the current pos
        rooms.Add(initialObjects[0]);
        stairs.Add(initialObjects[1]);
        triggers.Add(initialObjects[2]);
        furnitureSets.Add(initialObjects[3]);
        triggersUp.Add(initialObjects[4]);
        stairsUp.Add(initialObjects[5]);

        //assign global setting
        global_setting = GameObject.Find("Global").GetComponent<Setting>();

        //load prefab
        init_roomPrefab();
        init_FurniturePrefab();
        init_triggerPrefab();

    }
    
    // Update is called once per frame
    private void Update()
    {
        
        //Debug.Log("Trigge " + enableRoomChange + " Key " + global_setting.enableDoor);

        //============================================================
        //when trigger is triggered and key has found enable generation

        //  if (enableGenerate && global_setting.enableRoomChange)
        if (enableGenerate)
        {
            turnOnFurnitureSpawn = true;
            furnitureSetGenerate = false;
            enableGenerate = false;
            generateRoomDown();
            generateTrigger();

            destroySpawnedObj();

            // generateKey();

            // //generate another stuffs here;
            //spawnPos = currentPos_Room;
            //GameObject newRoom = Instantiate(defaultRoom, spawnPos, Quaternion.Euler(0, 0, 0));
            //destroySpawnedObj();

            // global_setting.enableRoomChange = false;
        }

        if (furnitureSetGenerate && turnOnFurnitureSpawn)
        {

            Debug.Log(" generate next");
            generateFurniture();
            furnitureSetGenerate = false;
            turnOnFurnitureSpawn = false;

        }else if(turnOnFurnitureSpawn == false && furnitureSetGenerate)
        {
            //
            Debug.Log("changing room");
            //change furniture for all room
            foreach (GameObject furniture in furnitureSets)
            {
                furniture.GetComponent<RoomSetLogic>().NextRoomCheck();
            }
            furnitureSetGenerate = false;
        }


        //==============================================================

    }



    void init_roomPrefab()
    {

        //store all types of room
        prefabs_rooms = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Room"));
        //store different rooms into obj variable
        for (int i = 0; i < prefabs_rooms.Count; i++)
        {
            //put assign default room
            if (prefabs_rooms[i].name == "Room")
            {
                defaultRoom = prefabs_rooms[i];
            }else if (prefabs_rooms[i].name == "Stairs")
            {
                defaultStairs = prefabs_rooms[i];
            }
        }
    }

    void init_triggerPrefab()
    {
        //store all types of trigger
        prefabs_triggers = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Trigger"));
        //store different trigger into obj variable
        for (int i = 0; i < prefabs_triggers.Count; i++)
        {
            //put assign default room
            if (prefabs_triggers[i].name == "Trigger")
            {
                defaultTrigger = prefabs_triggers[i];

            }else if(prefabs_triggers[i].name == "TriggerUp")
            {
                TriggerUp = prefabs_triggers[i];
            }else if(prefabs_triggers[i].name == "TriggerSetChange")
            {
                TriggerSetChange = prefabs_triggers[i];
            }
        }
    }

    void init_FurniturePrefab()
    {

        //store all types of keys
        prefabs_keys = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Furniture"));
        //store different trigger into obj variable
        for (int i = 0; i < prefabs_keys.Count; i++)
        {
            //put assign default room
            if (prefabs_keys[i].name == "Furniture Sets")
            {
                defaultFurnitureSets = prefabs_keys[i];
                
            }
        }
    }


    void generateRoomDown()
    {


        if (generateDown)
        {
            //make a room down
            //increment init_pos (for new room)
            spawnPos = currentPos_Room;
            spawnPos.x += roomPosChange.x;
            spawnPos.y += roomPosChange.y;
            GameObject newRoom = Instantiate(defaultRoom, spawnPos, Quaternion.Euler(0, 0, 0));
            rooms.Add(newRoom);
            currentPos_Room = spawnPos;
            //make a stair
            spawnPos = currentPos_Stairs;
            spawnPos.x += roomPosChange.x;
            spawnPos.y += roomPosChange.y;
            GameObject newStairs = Instantiate(defaultStairs, spawnPos, Quaternion.Euler(0, -90, 0));
            stairs.Add(newStairs);
            currentPos_Stairs = spawnPos;
            //make checker
            spawnPos = currentPos_TriggerSet;
            spawnPos.x += roomPosChange.x;
            spawnPos.y += roomPosChange.y;
            GameObject newTriggerSet = Instantiate(TriggerSetChange, spawnPos, Quaternion.Euler(0, 0, 0));
            triggersSet.Add(newTriggerSet);
            currentPos_TriggerSet = spawnPos;
        }
        else if (generateUp)
        {
            //make a room down
            //increment init_pos (for new room)
            spawnPos = currentPos_RoomUp;
            spawnPos.x -= roomPosChange.x;
            spawnPos.y -= roomPosChange.y;
            GameObject newRoom = Instantiate(defaultRoom, spawnPos, Quaternion.Euler(0, 0, 0));
            rooms.Add(newRoom);
            currentPos_RoomUp = spawnPos;
            //make a stair
            spawnPos = currentPos_StairsUp;
            spawnPos.x -= roomPosChange.x;
            spawnPos.y -= roomPosChange.y;
            GameObject newStairs = Instantiate(defaultStairs, spawnPos, Quaternion.Euler(0, -90, 0));
            stairsUp.Add(newStairs);
            currentPos_StairsUp = spawnPos;
            //make checker
            spawnPos = currentPos_TriggerSetUp;
            spawnPos.x -= roomPosChange.x;
            spawnPos.y -= roomPosChange.y;
            GameObject newTriggerSetUp = Instantiate(TriggerSetChange, spawnPos, Quaternion.Euler(0, 0, 0));
            triggersSetUp.Add(newTriggerSetUp);
            currentPos_TriggerSetUp = spawnPos;
        }

    }

    


    void generateFurniture()
    {




        Debug.Log("yes");



        if (generateDown)
        {
            //make furniture set
            spawnPos = currentPos_FurnitureSets;
            spawnPos.x += roomPosChange.x;
            spawnPos.y += roomPosChange.y;
            GameObject newSet = Instantiate(defaultFurnitureSets, spawnPos, Quaternion.Euler(0, 0, 0));
            furnitureSets.Add(newSet);
            currentPos_FurnitureSets = spawnPos;
            newSet.GetComponent<RoomSetLogic>().NextRoomCheck();

        }
        else if (generateUp)
        {
            //make furniture set
            spawnPos = currentPos_FurnitureSetsUp;
            spawnPos.x -= roomPosChange.x;
            spawnPos.y -= roomPosChange.y;
            GameObject newSet = Instantiate(defaultFurnitureSets, spawnPos, Quaternion.Euler(0, 0, 0));
            furnitureSets.Add(newSet);
            currentPos_FurnitureSetsUp = spawnPos;
            newSet.GetComponent<RoomSetLogic>().NextRoomCheck();

        }

    }


    void generateTrigger()
    {



        if (generateDown)
        {
            //make a trigger down
            spawnPos = currentPos_Trigger;
            spawnPos.x += roomPosChange.x;
            spawnPos.y += roomPosChange.y;
            GameObject newTrigger = Instantiate(defaultTrigger, spawnPos, Quaternion.Euler(0, 0, 0));
            newTrigger.tag = "triggers";
            triggers.Add(newTrigger);
            currentPos_Trigger = spawnPos;
        }
        else if (generateUp)
        {
            //make a trigger up
            spawnPos = currentPos_TriggerUp;
            spawnPos.x -= roomPosChange.x;
            spawnPos.y -= roomPosChange.y;
            GameObject newTriggerUp = Instantiate(TriggerUp, spawnPos, Quaternion.Euler(0, 0, 0));
            newTriggerUp.tag = "triggersUp";
            triggersUp.Add(newTriggerUp);
            currentPos_TriggerUp = spawnPos;
        }


    }

    //void generateFurniture()
    //{
    //    //generate Furniture
    //    // Given children 
    //    transform.GetChild(currRoom).gameObject.SetActive(false);

    //    currRoom = (currRoom + 1) % transform.childCount;
    //    GameObject furnset = transform.GetChild(currRoom).gameObject;
    //    furnset.SetActive(true);

    //    //Vector3 pos;
    //    foreach (Transform child in furnset.transform)
    //    {
    //        spawnPos = currentPos_Room;
    //        // spawnPos.x -= roomWidth/2;

    //        child.transform.position = spawnPos;
    //    }
    //    //        spawnPos = currentPos_Key;
    //    //        spawnPos.x += roomWidth;

    //}


    void destroySpawnedObj()
    {
        //destroy all objects
        //GameObject.Destroy(rooms[0]);
        //GameObject.Destroy(furnitureSets[0]);

        //rooms.RemoveAt(0);
        //furnitureSets.RemoveAt(0);

        //if (stairs.Count > 2)
        //{
        //    GameObject.Destroy(stairs[0]);
        //    stairs.RemoveAt(0);
        //}

        if (generateDown)
        {
            GameObject.Destroy(triggers[0]);
            triggers.RemoveAt(0);
        }
        else if (generateUp)
        {

            GameObject.Destroy(triggersUp[0]);
            triggersUp.RemoveAt(0);
        }

    }


}
