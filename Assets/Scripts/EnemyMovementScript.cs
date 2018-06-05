namespace Assets
{
    using UnityEngine;

    public class EnemyMovementScript : MonoBehaviour
    {
        //CharacterController controller;
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;

        private CharacterMovementScript player;
        private GameObject _player;
        private float health = 50.0f;
        private float movementSpeed = 0.1f;

        // Use this for initialization
        private void Start()
        {
            //controller = GetComponent<CharacterController>();
            player = globalController.getPlayer();
            _player = player.gameObject;
            Debug.Log(player.gameObject.name);
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 lookAt = _player.transform.position;
            lookAt.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAt);

            //Enemy moves toward player
            gameObject.transform.Translate(Vector3.forward * movementSpeed);

            //Destroy the object if health reaches 0
            if (health <= 0.0f)
            {
                Destroy(gameObject);
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