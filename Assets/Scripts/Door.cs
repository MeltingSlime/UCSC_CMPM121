using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    private Setting globalSetting_script;
    public string InteractionPrompt => prompt;
    [SerializeField] private string text;
    public string paragraphText => text;

    private void Start()
    {
        //get script from global setting
        globalSetting_script = GameObject.Find("Global").GetComponent<Setting>();
    }


    public bool Interact(Interactor interactor)
    {

      //  globalSetting_script.enableRoomChange = true;

        var inventory = interactor.GetComponent<Inventory>();
        if(inventory == null) return false;
        if (inventory.HasKey)
        {

            Debug.Log("Opening door!");
            return true;
        }
        Debug.Log(message: "No key found!");
        return false;
    }
}

