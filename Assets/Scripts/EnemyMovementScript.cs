namespace Assets
{
    using UnityEngine;

    public class EnemyMovementScript : MonoBehaviour
    {
        //CharacterController controller;
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;

        private CharacterMovementScript player;
        private GameObject _player;
        private float Health = 50.0f;

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
            lookAt.y = 1.0f;
            gameObject.transform.LookAt(lookAt);
            if (Health <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        public void takeDamage(float Dam)
        {
            Health -= Dam;
        }

        public float getHealth()
        {
            return Health;
        }
    }
}