using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    //[HideInInspector] public bool enableRoomChange;
    //old used code
    public Vector2 roomX = new Vector2(-26f, -2f);
    public Vector2 roomZ = new Vector2(-6f, 18f);
    [HideInInspector] public bool regeneratePos = false;

    private void Start()
    {
        //enableRoomChange = false;
    }



}
