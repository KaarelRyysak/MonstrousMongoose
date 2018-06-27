﻿namespace Assets
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class StartMenuScript : MonoBehaviour
    {
        private GlobalObjectScript globalController = GlobalObjectScript.Instance;
        private SaveManager saveManager;
        public Canvas _startMenu;
        public Canvas _optionsMenu;
        public Text row1Text;
        public Text row2Text;
        public Text row3Text;

        public void loadStart(SaveManager saveManager)
        {
            saveManager.loadDataFromDisk();
            int settingsField1 = saveManager.getSettingsField1();
            this.saveManager = saveManager;
            row1Text.text = "1. Current Value [" + settingsField1.ToString() + "]";
            //row2Text.text = "2. Current Value [" + settingsField1 + "]";
            print("Setting value is: " + settingsField1);
        }

        public void startGame()
        {
            SceneManager.LoadScene("Murderous Mongoose", LoadSceneMode.Single);
        }

        public void optionsMenu()
        {
            _startMenu.sortingOrder = 0;
            _optionsMenu.sortingOrder = 1;
        }

        public void startMenu()
        {
            _startMenu.sortingOrder = 1;
            _optionsMenu.sortingOrder = 0;
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void saveGame()
        {
            saveManager.saveDataToDisk();
        }

        public void setRow1To1()
        {
            row1Text.text = "1. Current Value [1]";
            saveManager.setSettingsField1(1);
        }

        public void setRow1To2()
        {
            row1Text.text = "1. Current Value [2]";

            saveManager.setSettingsField1(2);
        }

        public void setRow1To3()
        {
            row1Text.text = "1. Current Value [3]";
            saveManager.setSettingsField1(3);
        }
        /** 
        public void setRow2To1()
        {
            row2Text.text = "2. Current Value [1]";
            saveManager.setSettingsField2(1);
        }

        public void setRow2To2()
        {
            row2Text.text = "2. Current Value [2]";

            saveManager.setSettingsField2(2);
        }

        public void setRow2To3()
        {
            row2Text.text = "2. Current Value [3]";
            saveManager.setSettingsField2(3);
        }
        */
    }
}