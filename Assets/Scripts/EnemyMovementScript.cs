namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyMovementScript : MonoBehaviour
    {
    	//CharacterController controller;
    	private GlobalObjectScript globalController = GlobalObjectScript.Instance;
    	private CharacterMovementScript characterController = CharacterMovementScript.Instance;
    	GameObject player;

        // Use this for initialization
        void Start()
        {
        	//controller = GetComponent<CharacterController>();
        	player = globalController.getPlayer();
        	//Debug.Log(controller);
        }

        // Update is called once per frame
        void Update()
        {
        	
        	Vector3 lookAt = characterController.getPosition();
            lookAt.y = 1.0f;
            gameObject.transform.LookAt(lookAt);
			
        }
    }
}
