namespace Assets
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using System.Runtime.Serialization;
    using UnityEngine.SceneManagement;

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
            SaveManager.Instance.loadDataFromDisk();
            // Start player at set health
            defaultPlayerHealth = 100.00f;
            // Set limit on dashes
            defaultPlayerDashLimit = 3;
            // Start player with set dashes
            playerDashCount = 3;
        }


        private void Update()
        {
            if (!SceneManager.GetActiveScene().name.Equals("Menu"))
            {
                // Updates Health prefab previously instantiated
                HealthTextObject.text = healthString + player.getHealth();
                // Updates Dash prefab previously instantiated
                DashTextObject.text = dashString + player.getDashCount();
            }
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

        public void startLevel ()
        {
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

    }

    public class SaveGlob
    {
        public static SaveGlob Instance;
        public int settingsField1;
        public int settingsField2;
        public int settingsField3;

        // Sets settingsField1
        public void setSettingsField1(int newSettingsField1)
        {
            settingsField1 = newSettingsField1;
        }
        // Returns settingsField1
        public int getSettingsField1()
        {
            return settingsField1;
        }

        // Sets settingsField2
        public void setSettingsField2(int newSettingsField2)
        {
            settingsField2 = newSettingsField2;
        }
        // Returns settingsField2
        public int getSettingsField2()
        {
            return settingsField2;
        }

        // Sets settingsField3
        public void setSettingsField3(int newSettingsField2)
        {
            settingsField2 = newSettingsField2;
        }
        // Returns settingsField3
        public int getSettingsField3()
        {
            return settingsField3;
        }
    }

    public class SaveManager
    {
        public static SaveManager Instance;
        private SaveGlob saveGlob;    // the Dictionary used to save and load data to/from disk
        protected string savePath;
        public SaveManager()
        {
         this.savePath = Application.persistentDataPath + "/save.dat";
         this.saveGlob = new SaveGlob();
         this.loadDataFromDisk();
        }
         /**
          * Saves the save data to the disk
          */
        public void saveDataToDisk()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);
            bf.Serialize(file, saveGlob);
            file.Close();
        }
 
        /**
         * Loads the save data from the disk
         */
        public void loadDataFromDisk()
        {
            if(File.Exists(savePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                this.saveGlob = (SaveGlob)bf.Deserialize(file);
                file.Close();
            }
        }
    }
    
}