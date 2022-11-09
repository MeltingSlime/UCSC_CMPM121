using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingTable : MonoBehaviour
{
    //get data from global
    public GameObject global;
    Setting global_setting;
    Vector3 tempPos;
    


    // Start is called before the first frame update
    void Start()
    {
        generatePos();
    }

    // Update is called once per frame
    void Update()
    {
        if (global_setting.regeneratePos)
        {
            generatePos();
            global_setting.regeneratePos = false;
        }
    }

    void generatePos()
    {
        global_setting = global.GetComponent<Setting>();
        tempPos = new Vector3(Random.Range(global_setting.roomX.x, global_setting.roomX.y),
            transform.position.y, Random.Range(global_setting.roomZ.x, global_setting.roomZ.y));
        transform.position = tempPos;
    }


}
