namespace Assets
{
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    [System.Serializable]
    public class SaveManager : MonoBehaviour
    {
        public static List<Game> savedGames;
        public static Settings gameSettings;
        public static Game gameInstance;
        static string savePath;

        private void Awake()
        {
            savedGames = new List<Game>();
            gameInstance = new Game();
            gameSettings = new Settings();
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
            //savedGames.Add(SaveManager.gameInstance);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);
            bf.Serialize(file, gameSettings);
            file.Close();
        }

        /**
            * Loads the save data from the disk
            */

        public static void loadDataFromDisk()
        {
            if (File.Exists(savePath))
            {
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                gameSettings = (Settings)bf.Deserialize(file);
                file.Close();
            }
        }

        

       
    }

    
}