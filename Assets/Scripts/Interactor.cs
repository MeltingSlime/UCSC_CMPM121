using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;
    
    void Start()
    {
        
    }

    public void Update()
    {
        //Debug.Log(interactable);
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if(numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();
            /*if(interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(interactor:this);
            }*/
            if (interactable != null)
            {
               
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.Q)) interactable.Interact(interactor: this);
                if (Input.GetKeyDown(KeyCode.E)) _interactionPromptUI.NoteSetUp(interactable.paragraphText);
            }
            
        }
        else
        {
            if (interactable != null) interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
