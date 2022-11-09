using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithNotes : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private string text;
    public string paragraphText => text;


    private GameObject global;
    private Initialization global_init;

    private void Start()
    {
        global = GameObject.Find("Global");
        global_init = global.GetComponent<Initialization>();

    }



    public bool Interact(Interactor interactor)
    {
        global_init.keyList.Find(x => x.name == "Box").isInteracted = true;


        Debug.Log(message: "Opening Box with Notes!");
        return true;
    }
}
