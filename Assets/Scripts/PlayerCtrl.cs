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
    public float jumpSpeed;

    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public GameObject leftBullet, rightBullet;

    public Transform leftBulletSpawnPos, rightBulletSpawnPos;

    Rigidbody2D rbody;
    SpriteRenderer sr;
    Animator anim;

    bool leftPressed, rightPressed;

    bool isJumping, canDoubleJump;
	
	void  Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        // isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

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

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullets();
        }

        if (leftPressed)
        {
            MoveHorizontal(-speedBoost);
        }

        if (rightPressed)
        {
            MoveHorizontal(speedBoost);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
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
        if (isGrounded)
        {
            isJumping = true;
            rbody.AddForce(new Vector2(playerSpeed, jumpSpeed));
            anim.SetInteger("State", 2);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if(canDoubleJump && !isGrounded)
        {
            rbody.velocity = Vector2.zero;
            rbody.AddForce(new Vector2(playerSpeed, jumpSpeed));
            anim.SetInteger("State", 2);

            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void FireBullets()
    {
        if (sr.flipX)
        {
            Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
        }
    }

    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;
    }

    public void MobileStop()
    {
        rightPressed = false;
        leftPressed = false;

        StopMoving();
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
