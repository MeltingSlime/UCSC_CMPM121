using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAroundRoom : MonoBehaviour
{

    [SerializeField]
    GameObject room;





    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(room.transform.position, Vector3.right, 10 * Time.deltaTime);
    }
}
