namespace Assets
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [System.Serializable]
    public class GlobalObjectScript : MonoBehaviour
    {
        public SaveManager saveManager;
        public SaveGlob saveGlob;
        public StartMenuScript startMenuScript;

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
            saveGlob = GetComponentInChildren<SaveGlob>();
            saveManager = GetComponentInChildren<SaveManager>();
            startMenuScript = GetComponentInChildren<StartMenuScript>();
            startMenuScript.loadStart(saveManager, saveGlob);
            saveManager.saveManager(saveGlob);
            saveManager.loadDataFromDisk();
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

        public void startLevel()
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
}