using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour {
    public GameObject gameObject;
    private int timer;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Example());
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    IEnumerator Example()
    {
        
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
