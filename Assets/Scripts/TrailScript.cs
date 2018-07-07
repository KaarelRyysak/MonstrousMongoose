using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour {
    public GameObject gameObject;
    private int timer;
	// Use this for initialization
	void Start ()
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.fixedTime > 2)
        {
            Destroy(gameObject);
        }
	}
}
