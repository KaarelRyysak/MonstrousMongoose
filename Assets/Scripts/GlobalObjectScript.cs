namespace Assets
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;

    [System.Serializable]
    public class GlobalObjectScript : MonoBehaviour
    {
        public static GlobalObjectScript Instance;
        private float playerHealth;
        private int playerDashLimit;
        private int playerDashCount;
        private CharacterMovementScript player;
        private EnemyMovementScript enemy;
        private Canvas _canvas;
        private Text HealthTextObject;
        private Text DashTextObject;
        private string healthText = "Health: ";
        private string dashText = "Dash: ";
        private int num;

        public GameObject character;
        public GameObject p_enemy;
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
            // Start player at set health
            playerHealth = 100.00f;
            // Set limit on dashes
            playerDashLimit = 3;
            // Start player with set dashes
            playerDashCount = 3;
            // Instantiate player
            player = Instantiate<GameObject>(character, new Vector3(0, 1.0f, 0), gameObject.transform.rotation).GetComponent<CharacterMovementScript>();
            // Instantiate an enemy
            enemy = Instantiate<GameObject>(p_enemy, new Vector3(5.0f, 1.0f, 0), gameObject.transform.rotation).GetComponent<EnemyMovementScript>();
            // Sets player health on player gameobject
            player.health = playerHealth;
            // Sets players dashes on player gameobject
            player.dash = playerDashCount;
            // Instantiates canvas prefab
            _canvas = Instantiate<Canvas>(canvas);
            // Instantiates Health UIText prefab with canvas as parent
            HealthTextObject = Instantiate<Text>(HealthText, _canvas.transform);
            // Instantiates Dash UIText prefab with canvas as parent
            DashTextObject = Instantiate<Text>(DashText, _canvas.transform);
        }


        private void Update()
        {
            // Updates Health prefab previously instantiated
            HealthTextObject.text = healthText + getHealth();
            // Updates Dash prefab previously instantiated
            DashTextObject.text = dashText + getDashCount();
        }


        // Returns players health
        public float getHealth()
        {
            return playerHealth;
        }

        // Returns Dash limit
        public int getDashLimit()
        {
            return playerDashLimit;
        }

        // Returns number of dashes
        public int getDashCount()
        {
            return playerDashCount;
        }

        // Returns Enemy game object?
        public GameObject getEnemy()
        {
            return p_enemy;
        }

        // Returns player gameObject
        public CharacterMovementScript getPlayer()
        {
            return player;
        }

        // Set health on both player gameobject and global object
        public void setHealth(float health)
        {
            playerHealth = health;
            player.health = health;
        }

        // Set dash limit on both player gameobject and global object
        public void setDashLimit(int dashLimit)
        {
            playerDashLimit = dashLimit;
        }

        // Set number of dashes availible on both player gameobject and global object
        public void setDashCount(int dashCount)
        {
            playerDashCount = dashCount;

        }

        public void saveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/Save.save");
            bf.Serialize(file, num);
            file.Close();
        }

        public void loadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/Save.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/Save.save", FileMode.Open);
                num = (int)bf.Deserialize(file);
                file.Close();
            }
        }
    }
}