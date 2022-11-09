using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private string text;
    public string paragraphText => text;

    public bool Interact(Interactor interactor)
    {
        Debug.Log(message: "Opening Toy!");
        return true;
    }

}
