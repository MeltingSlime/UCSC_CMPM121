using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAngle : MonoBehaviour
{
    [SerializeField] GameObject Room;
    Vector3 AngleVector;
    Quaternion tempQuat;
    

    // Update is called once per frame
    void Update()
    {
       // AngleVector = Room.transform.position - transform.position;
        //transform.rotation = Quaternion.Euler(AngleVector);
      
       // Debug.Log(AngleVector);
    }
}
