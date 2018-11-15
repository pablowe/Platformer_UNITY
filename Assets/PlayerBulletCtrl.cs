using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles players bullet
/// </summary>

public class PlayerBulletCtrl : MonoBehaviour {
    Rigidbody2D rb;
    public Vector2 velocity;
	
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	
	void Update ()
    {
        rb.velocity = velocity;
	}
}
