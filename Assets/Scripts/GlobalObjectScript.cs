namespace Assets
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using System.Runtime.Serialization;

    [System.Serializable]
    public class GlobalObjectScript : MonoBehaviour
    {
        public static GlobalObjectScript Instance;
        private float defaultPlayerHealth;
        private int defaultPlayerDashLimit;
        
        private int playerDashCount;
        private CharacterMovementScript player;
        private EnemyMovementScript enemy;
        private Canvas _canvas;
        private Text HealthTextObject;
        private Text DashTextObject;
        private string healthString = "Health: ";
        private string dashString = "Dash: ";
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
            defaultPlayerHealth = 100.00f;
            // Set limit on dashes
            defaultPlayerDashLimit = 3;
            // Start player with set dashes
            playerDashCount = 3;
            // Instantiate player
            player = Instantiate<GameObject>(character, new Vector3(0, 1.0f, 0), gameObject.transform.rotation).GetComponent<CharacterMovementScript>();
            // Instantiate an enemy
            enemy = Instantiate<GameObject>(p_enemy, new Vector3(5.0f, 1.0f, 0), gameObject.transform.rotation).GetComponent<EnemyMovementScript>();
            // Sets player health on player gameobject
            player.setHealth(defaultPlayerHealth);
            // Sets players dashes on player gameobject
            player.setDashCount(defaultPlayerDashLimit);
            // Sets players dash limit on player gameobject
            player.setDashLimit(defaultPlayerDashLimit);
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
            HealthTextObject.text = healthString + player.getHealth();
            // Updates Dash prefab previously instantiated
            DashTextObject.text = dashString + player.getDashCount();
        }

        //SETTERS AND GETTERS

        // Returns players health
        public float getHealth()
        {
            return player.getHealth();
        }

        // Returns Dash limit
        public int getDashLimit()
        {
            return player.getDashLimit();
        }

        // Returns number of dashes
        public int getDashCount()
        {
            return playerDashCount;
        }

        // Returns Enemy game object
        public GameObject getEnemy()
        {
            return p_enemy;
        }

        // Returns player gameObject
        public CharacterMovementScript getPlayer()
        {
            return player;
        }

        public void saveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(Application.persistentDataPath + "/Save.save"))
            {
                print("Save was attempted");
                num = 3;
                try
                {
                    bf.Serialize(file, num);
                }
                catch (SerializationException e) 
                {
                    print("Failed to serialize. Reason: " + e.Message);
                }
                file.Close();
            }

            
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