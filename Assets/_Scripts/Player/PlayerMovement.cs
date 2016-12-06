﻿	using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	private Animator anim;

	private bool jump;

	private float speed;
	public float SetSpeed
	{
		set
		{
			speed = value;
		}
	}

	private float jumpSpeed;
    public float SetJumpSpeed
    {
        set
        {
            jumpSpeed = value;
        }
    }
    private float jumpHeight;
    public float SetjumpHeight
    {
        set
        {
            jumpHeight = value;
        }
    }

    private Rigidbody2D rigid;

	private void Start()
	{
		speed = 0f;
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent <Animator>();
		jump = false;
	}

    private void FixedUpdate()
    {
        checkIfFalling();
        walk();
        checkIfJump();
        checkIfSliding();
        extraAnimations();
    }

    private void checkIfSliding()
    {
        if(distanceToGround() > 0.12f && distanceToGround() <= 0.3f && anim.GetBool("slide") == false)
        {
            anim.SetBool("slide", true);
        }
        else if(anim.GetBool("slide") == true)
        {
            anim.SetBool("slide", false);
        }
    }

    private void checkIfFalling()
    {
        if (distanceToGround() <= 0.12f)
        {
            anim.SetBool("Falling", false);
        }
        else if (distanceToGround() > 0.3f && !jump)
        {
            anim.SetBool("Falling", true);
        }
    }

    public void Jump()
    {
        if (!jump)
        {
            Debug.Log("je sprinkt");
            jump = true;
            anim.SetBool("Jump", true);         
        }
    }

    private void checkIfJump()
    {
        if (jump)
        {
            /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                if (distanceToGround() <= 0.11f )
                {
                    Debug.Log("je kan weer springen");
                    anim.SetBool("Jump", false);
                    jump = false;
                }
            }   */
           
            rigid.velocity = new Vector2(0, jumpSpeed-0.1f);
            jumpSpeed -= 0.4f;
        }
    }

    private void walk()
    {
        rigid.velocity = new Vector2(speed, 0);

        anim.SetFloat("Walk", Mathf.Abs(rigid.velocity.x));

        if (rigid.velocity.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rigid.velocity.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private float distanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100);
        if (hit.collider)
        {
            if (hit.collider.tag == Tags.ground)
            {
                    return hit.distance;
            }
        }
        return 100f;
    }
	
    private void extraAnimations()
    {
        if(anim.GetFloat("Walk") == 0)
        {
            if(Random.Range(1,3000) == 9)
            {
                Debug.Log("eating apple");
                anim.SetBool("eatApple", true);                
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleApple"))
            {
                anim.SetBool("eatApple", false);
            }

        }
    }
}
