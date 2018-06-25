namespace Assets
{
    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class StartMenuScript : MonoBehaviour
    {
        public Canvas _startMenu;
        public Canvas _optionsMenu;
        private int row1Setting;

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
        }

        public void setRow1To2 ()
        {
            row1Setting = 2;
        }

        public void setRow1To3 ()
        {
            row1Setting = 3;
        }
    }
}
