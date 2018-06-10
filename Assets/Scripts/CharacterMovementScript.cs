namespace Assets
{
    using UnityEngine;

    public class CharacterMovementScript : MonoBehaviour
    {
        public static CharacterMovementScript Instance;
        private CharacterController controller;

        private float mouseXInput;
        private float mouseYInput;
        private Transform alpha;
        private float dashCool = 1.50f;
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;
        private GameObject _holoChar;

        public float dashDamage = 10.0f;
        public float health;
        public int dash;
        public float dashSpeed = 50.0f;
        public float speed = .001f;
        public float rotateSpeed = 0.35f;
        public float radius = 4.0f;
        public float dashLength = 4.0f;
        public GameObject target;
        public GameObject target1 = null;
        public GameObject attack;
        public GameObject holoChar;

        // Use this for initialization
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller = GetComponent<CharacterController>();
            target1 = Instantiate<GameObject>(target, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z + 1), gameObject.transform.rotation);
            _holoChar = Instantiate<GameObject>(holoChar, target1.transform.position, gameObject.transform.rotation);
        }

        // Update is called once per frame
        private void Update()
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
            target1.transform.Translate(new Vector3(mouseXInput, 0, mouseYInput));

            _holoChar.transform.SetPositionAndRotation(target1.transform.position, gameObject.transform.rotation);

            if (Vector3.Distance(gameObject.transform.position, _holoChar.transform.position) > radius)
            {
                Vector3 v = _holoChar.transform.position - gameObject.transform.position;
                v = Vector3.ClampMagnitude(v, radius);
                Vector3 clampedLocale = gameObject.transform.position + v;
                _holoChar.transform.SetPositionAndRotation(clampedLocale, gameObject.transform.rotation);
            }

            //Grab position of crosshair and point character in that direction
            Vector3 lookAt = target1.transform.position;
            lookAt.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAt);

            //Character Movement
            Vector3 characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (characterMovement != Vector3.zero)
            {
                characterMovement.Normalize();
            }
            controller.Move(characterMovement * speed);

            //Dash attack when right mouse is pressed
            if (Input.GetKeyDown(KeyCode.Mouse1) && dash > 0)
            {   
                //Declare layermasks and combine them
                int layerMask0 = 1 << 0;
                int layerMask8 = 1 << 8;
                int finalMask = ((1 << 0) | (1 << 8));

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dashLength, layerMask0))
                {
                    //Create the layer mask
                    int enemyLayerMask = 1 << 10;
                    

                    RaycastHit hit1;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, hit.distance, enemyLayerMask));
                    {

                        print("Found an Enemy - of type: " + hit1.transform.gameObject.name);
                        hit1.transform.gameObject.GetComponent<EnemyMovementScript>().takeDamage(dashDamage);
                        print(hit1.transform.gameObject.GetComponent<EnemyMovementScript>().getHealth());

                    }
                print("Distance dashed: " + hit.distance);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1);
                gameObject.transform.Translate(Vector3.forward * (hit.distance - 0.5f));

                    /**
                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        //print("Found an Enemy - of type: " + hit.transform.gameObject.name);
                        hit.transform.gameObject.GetComponent<EnemyMovementScript>().takeDamage(dashDamage);
                        print(hit.transform.gameObject.GetComponent<EnemyMovementScript>().getHealth());
                        gameObject.transform.Translate(Vector3.forward * (hit.distance));

                        //Code to continue past the enemy

                        //Create the layer mask and invert it.
                        int enemyLayerMask = 1 << 8;
                        enemyLayerMask = ~enemyLayerMask;

                        RaycastHit hit1;

                        //Fire the ray which passes through enemies to find the next collision
                        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, (dashLength - hit.distance), enemyLayerMask);
                        //Tp there lol
                        gameObject.transform.Translate(Vector3.forward * (hit1.distance));
                    }
                    else
                    {
                        print("Found an object - distance: " + hit.distance);
                        gameObject.transform.Translate(Vector3.forward * (hit.distance - 0.6f));
                    }**/
                }
                else
                {
                    hit.distance = dashLength;
                    print("Distance dashed: " + hit.distance);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1);
                    gameObject.transform.Translate(Vector3.forward * (hit.distance - 0.6f));
                }
                dash -= 1;
                Invoke("dashCooldown", dashCool);
            }
        }

        private void dashCooldown()
        {
            dash += 1;
        }

        // Returns Position
        public Vector3 getPosition()
        {
            return gameObject.transform.position;
        }
    }
}