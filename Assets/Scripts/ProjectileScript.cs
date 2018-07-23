using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    private float movementSpeed = 0.09f;

    // Use this for initialization
    private void Start()
    {
        
}

    
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * movementSpeed);


    }
    void OnTriggerEnter(Collider otherCollider)
    {
        //Output the Collider's GameObject's name
        Debug.Log(otherCollider.gameObject.name);
    }


}
