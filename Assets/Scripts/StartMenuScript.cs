namespace Assets
{
    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class StartMenuScript : MonoBehaviour
    {
        public static StartMenuScript Instance;
        public Canvas _startMenu;
        public Canvas _optionsMenu;
        public Image row1_1;
        public Text row1;
        private int row1Setting;

        private void Start()
        {
            GlobalObjectScript.Instance.loadGame();
            switch (GlobalObjectScript.Instance.getNum())
            {
                case 1:
                row1.text = "1";
                break;
                case 2:
                row1.text = "2";
                break;
                case 3:
                row1.text = "3";
                break;
            }
            
            
        }
        public void startGame ()
        {
            SceneManager.LoadScene("Murderous Mongoose", LoadSceneMode.Single);
        }

        public void optionsMenu ()
        {
            _startMenu.sortingOrder = 0;
            _optionsMenu.sortingOrder = 1;
        }

        public void startMenu ()
        {
            _startMenu.sortingOrder = 1;
            _optionsMenu.sortingOrder = 0;
        }

        public void quitGame ()
        {
            Application.Quit();
        }

        public void saveGame ()
        {
            GlobalObjectScript.Instance.saveGame();
            
        }

        public void setRow1To1 ()
        {
            row1Setting = 1;
            row1_1.color = Color.red;
            row1.text = "1";
        }

        public void setRow1To2 ()
        {
            row1Setting = 2;
            row1.text = "2";
        }

        public void setRow1To3 ()
        {
            row1Setting = 3;
            row1.text = "3";
        }
    }
}
