namespace Assets {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterMovementScript : MonoBehaviour
    {
        CharacterController controller;

        private float mouseXInput;
        private float mouseYInput;
        private Transform alpha;
        private float dashCool = 1.00f;
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;
        private CharacterController Target1Cont;

        public float health;
        public int dash;
        public float dashSpeed = 50.0f;
        public float speed = .50f;
        public float rotateSpeed = 0.75f;
        public float radius = 8.0f;
        public GameObject target;
        public GameObject target1 = null;



        // Use this for initialization
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller = GetComponent<CharacterController>();
            target1 = Instantiate<GameObject>(target, new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z + 1), gameObject.transform.rotation);
            Target1Cont = target1.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            globalController.setHealth(health);
            globalController.setDashCount(dash);

            //Escape 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Application.Quit();
            }

            //Grab mouse input and move crosshair
            mouseXInput = Input.GetAxis("Mouse X") * rotateSpeed;
            mouseYInput = Input.GetAxis("Mouse Y") * rotateSpeed;
            Target1Cont.Move(new Vector3(mouseXInput, 0, mouseYInput));

            //Grab position of crosshair and point character in that direction
            Vector3 lookAt = target1.transform.position;
            lookAt.y = 1.0f;
            gameObject.transform.LookAt(lookAt);

            //Character Movement
            Vector3 characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (characterMovement != Vector3.zero)
            {
                characterMovement.Normalize();
            }
            controller.Move(characterMovement * speed);


            //Clamp crosshair within distance on player
            if (Vector3.Distance(gameObject.transform.position, target1.transform.position) > radius)
            {
                Vector3 v = target1.transform.position - gameObject.transform.position;
                v = Vector3.ClampMagnitude(v, radius);
                Vector3 clampedLocale = gameObject.transform.position + v;
                target1.transform.SetPositionAndRotation(clampedLocale, new Quaternion());
            }

            //Dash attack when right mouse is pressed
            if (Input.GetKeyDown(KeyCode.Mouse1) && dash > 0)
            {
                Vector3 dashAt = new Vector3(target1.transform.position.x, 1, target1.transform.position.z);
                gameObject.transform.position = dashAt;
                dash -= 1;
                Invoke("dashCooldown", dashCool);
            }

        }

        void dashCooldown()
        {
            dash += 1;
        }
    }
}

