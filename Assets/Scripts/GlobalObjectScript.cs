namespace Assets
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [System.Serializable]
    public class GlobalObjectScript : MonoBehaviour
    {
        private StartMenuScript startMenuScript;
        private CharacterMovementScript player;
        private WalkerScript enemy;

        public static GlobalObjectScript Instance;
        private float defaultPlayerHealth;
        private int defaultPlayerDashLimit;

        private int playerDashCount;
        private Canvas _canvas;
        private Text HealthTextObject;
        private Text DashTextObject;
        private string healthString = "Health: ";
        private string dashString = "Dash: ";

        public GameObject character;
        public GameObject p_walker;
        public GameObject p_shooter;
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
            print("Started");
            if (GetComponentInChildren<StartMenuScript>())
            {
                startMenuScript = GetComponentInChildren<StartMenuScript>();
                startMenuScript.loadStart();
            }

            SaveManager.saveManager();
            // Start player at set health
            defaultPlayerHealth = 100.00f;
            // Set limit on dashes
            defaultPlayerDashLimit = 3;
            // Start player with set dashes
            playerDashCount = 3;
        }

        private void Start()
        {
            
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
            return p_walker;
        }

        // Returns player gameObject
        public CharacterMovementScript getPlayer()
        {
            return player;
        }

        public void startLevel()
        {
            print("Level Started");
            // Instantiate player
            player = Instantiate<GameObject>(character, new Vector3(0, 1.0f, 0), gameObject.transform.rotation).GetComponent<CharacterMovementScript>();
            // Instantiate a walker
            enemy = Instantiate<GameObject>(p_walker, new Vector3(5.0f, 1.0f, 2), gameObject.transform.rotation).GetComponent<WalkerScript>();
            // Instantiate a shooter
            enemy = Instantiate<GameObject>(p_shooter, new Vector3(5.0f, 1.0f, 0), gameObject.transform.rotation).GetComponent<WalkerScript>();
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