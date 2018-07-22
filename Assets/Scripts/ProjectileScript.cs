using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    private CharacterController controller;
    private float movementSpeed = 0.09f;

    // Use this for initialization
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
}

    
	
	// Update is called once per frame
	void Update () {
        controller.Move(transform.forward * movementSpeed);

        
    }
    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        Debug.Log(collision.collider.name);
    }


}
