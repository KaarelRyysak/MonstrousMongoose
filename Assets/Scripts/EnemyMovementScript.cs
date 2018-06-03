namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyMovementScript : MonoBehaviour
    {
    	//CharacterController controller;
    	private GlobalObjectScript globalController = GlobalObjectScript.Instance;
        private CharacterMovementScript player;
        private GameObject _player;

        // Use this for initialization
        void Start()
        {
        	//controller = GetComponent<CharacterController>();
        	player = globalController.getPlayer();
            _player = player.gameObject;
        	Debug.Log(player.gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
        	
        	Vector3 lookAt = _player.transform.position;
            lookAt.y = 1.0f;
            gameObject.transform.LookAt(lookAt);
			
        }
    }
}
