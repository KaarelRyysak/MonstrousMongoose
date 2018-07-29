using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    private float movementSpeed = 0.09f;
    private Vector3 parentLocale;
    private float shotDistance = 10;

	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * movementSpeed);
        if (Vector3.Distance(gameObject.transform.position, parentLocale) >= shotDistance)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider otherCollider)
    {
        //Output the Collider's GameObject's name
        Debug.Log(otherCollider.gameObject.name);
    }

    public void setParent (Vector3 x)
    {
        parentLocale = x;
    }
}
