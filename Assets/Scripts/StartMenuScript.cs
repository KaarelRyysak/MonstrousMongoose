namespace Assets
{
    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class StartMenuScript : MonoBehaviour
    {
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;
        public Canvas _startMenu;
        public Canvas _optionsMenu;
        public Text row1Text;
        public Text row2Text;
        public Text row3Text;

        private void Start()
        {
            SaveManager.Instance.loadDataFromDisk();
            row1Text.text = "1. Current Value [" + SaveGlob.Instance.settingsField1 + "]";
            row2Text.text = "2. Current Value [" + SaveGlob.Instance.settingsField2 + "]";
            //print("Setting value is: " + SaveGlob.Instance.getSettingsField1());
            
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
            SaveManager.Instance.saveDataToDisk();
            
        }

        public void setRow1To1 ()
        {
            row1Text.text = "1. Current Value [1]";
            SaveGlob.Instance.setSettingsField1(1);
        }

        public void setRow1To2 ()
        {
            row1Text.text = "1. Current Value [2]";
            
            SaveGlob.Instance.setSettingsField1(2);
        }

        public void setRow1To3 ()
        {
            row1Text.text = "1. Current Value [3]";
            SaveGlob.Instance.setSettingsField1(3);
        }

        public void setRow2To1 ()
        {
            row2Text.text = "2. Current Value [1]";
            SaveGlob.Instance.setSettingsField2(1);
        }

        public void setRow2To2 ()
        {
            row2Text.text = "2. Current Value [2]";
            
            SaveGlob.Instance.setSettingsField2(2);
        }

        public void setRow2To3 ()
        {
            row2Text.text = "2. Current Value [3]";
            SaveGlob.Instance.setSettingsField2(3);
        }
    }
}
