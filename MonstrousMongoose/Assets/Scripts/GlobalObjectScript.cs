namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GlobalObjectScript : MonoBehaviour
    {
        public static GlobalObjectScript Instance;
        private float playerHealth;
        private int playerDashLimit;
        private int playerDashCount;
        private GameObject player;
        private Canvas _canvas;
        private Text Health;
        private Text Dash;
        private string healthText = "Health: ";
        private string dashText = "Dash: ";

        public  GameObject character;
        public Canvas canvas;
        public Text HealthText;
        public Text DashText;

        //Ensures there is only ever one GLobal Object active at a time
        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            playerHealth = 100.00f;
            playerDashLimit = 3;
            playerDashCount = 3;
            player = Instantiate<GameObject>(character);
            player.GetComponent<CharacterMovementScript>().health = playerHealth;
            player.GetComponent<CharacterMovementScript>().dash = playerDashCount;

            _canvas = Instantiate<Canvas>(canvas);
            Health = Instantiate<Text>(HealthText, _canvas.transform);
            Dash = Instantiate<Text>(DashText, _canvas.transform);
        }

        //Gets and sets for player health and dashes
        public float getHealth()
        {
            return playerHealth;
        }
        public int getDashLimit()
        {
            return playerDashLimit;
        }
        public int getDashCount()
        {
            return playerDashCount;
        }

        public void setHealth(float health)
        {
            playerHealth = health;
        }
        public void setDashLimit(int dashLimit)
        {
            playerDashLimit = dashLimit;
        }
        public void setDashCount(int dashCount)
        {
            playerDashCount = dashCount;
        }

        private void Update()
        {
            Health.text = healthText + getHealth();
            Dash.text = dashText + getDashCount();
        }
    }
}
