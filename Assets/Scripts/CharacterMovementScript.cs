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
        private float distanceTravelled;

        public float dashDamage = 10.0f;
        public float health;
        public int dash;
        public float dashSpeed = 50.0f;
        public float speed = .001f;
        public float rotateSpeed = 0.35f;
        public float radius = 8.0f;
        public float dashLength = 8.0f;
        public GameObject p_target;
        public GameObject target = null;
        public GameObject attack;
        public GameObject holoChar;
        public GameObject p_TargetAnalyzer;
        // Use this for initialization
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller = GetComponent<CharacterController>();
            target = Instantiate<GameObject>(p_target, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z + 1), gameObject.transform.rotation);
            _holoChar = Instantiate<GameObject>(holoChar, target.transform.position, gameObject.transform.rotation);
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
            target.transform.Translate(new Vector3(mouseXInput, 0, mouseYInput));

            //Grab position of crosshair and point character in that direction
            Vector3 lookAt = target.transform.position;
            lookAt.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAt);

            //Cast a ray in front of the character
            int collisionLayerMask = 1 << 8;
            Ray forwardRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            RaycastHit collisionHit;
            

            //Position holoChar
            bool mainRayCollision = false;
            if ( (Physics.Raycast(forwardRay, out collisionHit, dashLength, collisionLayerMask, QueryTriggerInteraction.Ignore)) && (collisionHit.distance < (Vector3.Distance(gameObject.transform.position, target.transform.position) )) )
            {
                mainRayCollision = true;
                _holoChar.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
                _holoChar.transform.Translate(Vector3.forward * (collisionHit.distance - 0.6f));
            }
            else
            {
                _holoChar.transform.SetPositionAndRotation(target.transform.position, gameObject.transform.rotation);

               if (Vector3.Distance(gameObject.transform.position, _holoChar.transform.position) > radius)
                {
                    Vector3 v = _holoChar.transform.position - gameObject.transform.position;
                    v = Vector3.ClampMagnitude(v, radius);
                    Vector3 clampedLocale = gameObject.transform.position + v;
                    _holoChar.transform.SetPositionAndRotation(clampedLocale, gameObject.transform.rotation);
                }
            }

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
                RaycastHit hit;
                RaycastHit hit1;

                GameObject _targetAnalyzer = Instantiate<GameObject>(p_TargetAnalyzer, target.transform.position, target.transform.rotation);

                if (Physics.Linecast(gameObject.transform.position, _targetAnalyzer.transform.position, out hit1))
                {
                    if (hit1.transform.gameObject.CompareTag("Enemy"))
                    {
                        hit1.transform.gameObject.GetComponent<EnemyMovementScript>().takeDamage(dashDamage);
                        print(hit1.transform.gameObject.GetComponent<EnemyMovementScript>().getHealth());
                        Debug.Log("hit");
                    }
                }
                Destroy(_targetAnalyzer);
                
                if (mainRayCollision)
                {
                        print("Distance dashed: " + collisionHit.distance);
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * collisionHit.distance, Color.red, 1);
                        gameObject.transform.Translate(Vector3.forward * (collisionHit.distance - 0.6f));
                }
                else
                {
                    distanceTravelled = Mathf.Clamp((Vector3.Distance(gameObject.transform.position, target.transform.position) - 0.01f), 0.0f, dashLength);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceTravelled, Color.red, 1);
                    Debug.Log(distanceTravelled);
                    gameObject.transform.Translate(Vector3.forward * distanceTravelled);
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