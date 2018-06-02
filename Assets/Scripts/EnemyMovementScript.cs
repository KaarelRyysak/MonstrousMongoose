namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyMovementScript : MonoBehaviour
    {
    	CharacterController controller;
    	private GlobalObjectScript globalController = GlobalObjectScript.Instance;
    	GameObject player;
    	public int test;

        // Use this for initialization
        void Start()
        {
        	controller = GetComponent<CharacterController>();
        	player = globalController.getEnemy();
        }

        // Update is called once per frame
        void Update()
        {
        	/**
        	Vector3 lookAt = player.transform.position;
            lookAt.y = 1.0f;
            gameObject.transform.LookAt(lookAt);
			**/
        }
    }
}
