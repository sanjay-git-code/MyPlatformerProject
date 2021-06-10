using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles the player bullet movement and collisions with the enemies
/// </summary>
public class PlayerBulletCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.velocity = velocity;
    }
}
