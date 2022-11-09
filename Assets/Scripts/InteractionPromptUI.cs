using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField]private GameObject uiPanel;
    [SerializeField]private GameObject Note_uiPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private TextMeshProUGUI Note_promptText;
    // Start is called before the first frame update
    void Start()
    {
        /*mainCam = Camera.main;*/
        uiPanel.SetActive(false);   
    }


    // Update is called once per frame
    void LateUpdate()
    {
        /*var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);*/
    }

    public bool IsDisplayed = false;
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        IsDisplayed = false;
        Note_uiPanel.SetActive(false);
    }
    public void NoteSetUp(string promptText)
    {
        Note_promptText.text = promptText;
        Note_uiPanel.SetActive(true);
        //IsDisplayed = true;
    }
    
}