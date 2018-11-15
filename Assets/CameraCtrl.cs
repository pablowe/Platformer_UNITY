using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// makes camera follow player
///</summary>
public class CameraCtrl : MonoBehaviour {
    public Transform player;
    private int xOffset = 3;
    public float yOffset;
	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        // follow in x axis
        transform.position = new Vector3(player.position.x+xOffset, player.position.y+yOffset, transform.position.z);
	}
}
