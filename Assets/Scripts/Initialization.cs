using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{

    [HideInInspector]
    public class keyObjects
    {
        public string name;
        public GameObject obj;
        public bool isInteracted; 
    }

    [HideInInspector] public  List<keyObjects> keyList = new List<keyObjects>();
    [HideInInspector] public keyObjects tempKey;

    private void Awake()
    {
        tempKey = new keyObjects();
        tempKey.name = "Notepad";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Trophy";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Blox";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Box";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);
    }


}
