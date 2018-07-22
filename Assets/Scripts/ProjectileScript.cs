using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    // Use this for initialization
    private float movementSpeed = 0.09f;
    
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * movementSpeed);
    }
}
