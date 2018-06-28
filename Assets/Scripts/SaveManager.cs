namespace Assets
{
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    [System.Serializable]
    public static class SaveManager
    {
        public static List<Game> savedGames = new List<Game>();
        private static int settingsField1;
        static string savePath;

        private static void Awake()
        {
            MonoBehaviour.print("Hi");
            savePath = Application.persistentDataPath + "/save.dat";
        }

        public static void saveManager()
        {
            loadDataFromDisk();
        }

        /**
            * Saves the save data to the disk
            */

        public static void saveDataToDisk()
        {
            MonoBehaviour.print("Saving");
            SaveManager.savedGames.Add(Game.Instance);
            savePath = Application.persistentDataPath + "/save.dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);
            bf.Serialize(file, SaveManager.savedGames);
            file.Close();
        }

        /**
            * Loads the save data from the disk
            */

        public static void loadDataFromDisk()
        {
            savePath = Application.persistentDataPath + "/save.dat";
            if (File.Exists(savePath))
            {
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                SaveManager.savedGames = (List<Game>)bf.Deserialize(file);
                file.Close();
            }
        }

        // Sets settingsField1
        public static void setSettingsField1(int newSettingsField1)
        {
            settingsField1 = newSettingsField1;
        }

        // Returns settingsField1
        public static int getSettingsField1()
        {
            return settingsField1;
        }
    }

    
}