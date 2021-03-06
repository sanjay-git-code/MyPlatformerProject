using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow the player
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //makes the camera follow the player in y axis
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);

    }
}
