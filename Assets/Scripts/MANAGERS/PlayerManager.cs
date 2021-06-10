﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages:
/// 1. the player movement and flipping
/// 2. the player animation
/// </summary>
public class PlayerManager : MonoBehaviour
{
    //1. declare public bool isGrounded, Transform feet, float feetRadius, layerMask whatIsGround
    //2. show Physics2D.OverlapCircle() method to check if player is grounded
    //3. then show preferred way for this cat by using Physics2D.OverlapBox

    [Tooltip("this is a positive integer which speed up the player movement")]
    public int speedBoost;  // set this to 5
    public float jumpSpeed; // set this to 600
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public LayerMask whatIsGround;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool isJumping, canDoubleJump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);

        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value will be 1, -1 or 0
        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullets();
        }

        ShowFalling();
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }

    void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)
            sr.flipX = true;
        else if (playerSpeed > 0)
            sr.flipX = false;

        if (!isJumping)
            anim.SetInteger("State", 1);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if (canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            canDoubleJump = false;
        }
    }

    void FireBullets()
    {
        // makes the player fire bullets in the left direction
        if (sr.flipX)
        {
            Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
        }

        // makes the player fire bullets in the right direction
        if (!sr.flipX)
        {
            Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GROUND"))
            isJumping = false;
    }
}
