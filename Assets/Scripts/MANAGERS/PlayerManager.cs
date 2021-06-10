using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public float speedX;
	public float jumpSpeedY;

	bool facingRight, isJumping, canDoubleJump;
	public float speed;
	public float feetRadius;
	public float boxWidth, boxHeight;
	public float delayForDoubleJump;

	public Transform feet;
	public LayerMask whatIsGround;

	Animator anim;
	Rigidbody2D rb;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		facingRight = true;
	}
	

	void Update () 
	{
		// player movement
		MovePlayer(speed);

		// Flipping code
		Flip ();

		// left player movement
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			speed = -speedX;
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			speed = 0;
		}
		//

		// right player movement
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			speed = speedX;
		}
		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			speed = 0;
		}
		//

		// jumping player code
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			isJumping = true;
			rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
			anim.SetInteger("State",3);
		}
	}

	void MovePlayer(float playerSpeed)
	{
		// code for player movement 

		if(playerSpeed < 0 && !isJumping || playerSpeed > 0 && !isJumping)
		{
			anim.SetInteger("State",2);
		}
		if(playerSpeed == 0 && !isJumping)
		{
			anim.SetInteger("State",0);
		}

		rb.velocity = new Vector3(speed, rb.velocity.y,0);
	}

	void Flip()
	{
		// code to flip the player direction
		if(speed > 0 && !facingRight || speed < 0 && facingRight)
		{
			facingRight = !facingRight;

			Vector3 temp = transform.localScale;
			temp.x *= -1;
			transform.localScale = temp;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "GROUND")
		{
			isJumping = false;
			anim.SetInteger("State",0);
		}
	}

	public void WalkLeft()
	{
		speed = -speedX;
	}

	public void WalkRight()
	{
		speed = speedX;
	}

	public void StopMoving()
	{
		speed = 0;
	}

	public void Jump()
	{
		isJumping = true;
		rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
		anim.SetInteger("State",3);
	}
}










