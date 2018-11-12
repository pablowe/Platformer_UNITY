using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Player animations
/// 2. Player movement and flipping
/// </summary>
public class PlayerCtrl : MonoBehaviour {

    [Tooltip("positive speed boost intiger")]

    public int speedBoost;
    public int jumpSpeed;

    Rigidbody2D rbody;
    SpriteRenderer sr;
    Animator anim;

    bool isJumping;
	
	void  Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        float playerSpeed = Input.GetAxisRaw("Horizontal"); // 1, -1, 0
        
        playerSpeed *= speedBoost;
  
        if(playerSpeed != 0)
        {
            MoveHorizontal(playerSpeed);
        }
        else
        {
            StopMoving();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump(playerSpeed);
        }
	}

    void MoveHorizontal(float playerSpeed)
    {
        rbody.velocity = new Vector2(playerSpeed, rbody.velocity.y);

        if (playerSpeed < 0)
        {
            sr.flipX = true;
        }
        else if(playerSpeed > 0)
        {
            sr.flipX = false;
        }
        if (!isJumping)
        {
            anim.SetInteger("State", 1);
        }
    }

    void StopMoving()
    {
        rbody.velocity = new Vector2(0, rbody.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("State", 0);
        }
    }

    void ShowFalling()
    {
        if (rbody.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump(float playerSpeed)
    {
        isJumping = true;
        rbody.AddForce(new Vector2(playerSpeed, jumpSpeed));
        anim.SetInteger("State", 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(tag: "Ground"))
        {
            Debug.Log("Collision!");
            isJumping = false;
        }
    }


}
