namespace Assets
{
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;

    public class ShooterScript : MonoBehaviour
    {
        //CharacterController controller;
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;


        private CharacterMovementScript player;
        private GameObject p_player;
        public GameObject p_projectile;
        private float health = 20.0f;
        private float movementSpeed = 0.07f;
        public NavMeshAgent agent;
        private IEnumerator coroutine;
        private GameObject tempProj;
        public bool counted = false;
        public GameObject parentRoom;

        // Use this for initialization
        private void Start()
        {
            //controller = GetComponent<CharacterController>();
            player = globalController.getPlayer();
            p_player = player.gameObject;
            coroutine = WaitAndShoot(2.0f);
            StartCoroutine(coroutine);
        }

        // Update is called once per frame
        private void Update()
        {
            
            Vector3 lookAt = p_player.transform.position;
            lookAt.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAt);

            





            //Destroy the object if health reaches 0
            if (health <= 0.0f)
            {
                parentRoom.GetComponent<cameraMovement>().enemyCount--;
                Destroy(gameObject);
            }
        }

        private IEnumerator WaitAndShoot(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                tempProj = Instantiate<GameObject>(p_projectile, (gameObject.transform.position), gameObject.transform.rotation);
                tempProj.GetComponent<ProjectileScript>().setParent(gameObject.transform.position);
            }
        }

        public void takeDamage(float Dam)
        {
            health -= Dam;
        }

        public float getHealth()
        {
            return health;
        }
    }
}