using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// destroy bullet after delay
/// </summary>
public class DestroyWithDelay : MonoBehaviour {
    public float delay;
	
	void Start ()
    {
        Destroy(gameObject, delay);
	}

}
