using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableMesh_trigger : MonoBehaviour
{

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //hide the obj
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

}
