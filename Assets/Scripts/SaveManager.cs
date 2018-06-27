namespace Assets
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    [System.Serializable]
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;
        private GlobalObjectScript globalObject = GlobalObjectScript.Instance;
        private int settingsField1;
        protected string savePath;

        private void Awake()
        {
            this.savePath = Application.persistentDataPath + "/save.dat";
        }

        public void saveManager()
        {
            this.loadDataFromDisk();
        }

        /**
            * Saves the save data to the disk
            */

        public void saveDataToDisk()
        {
            print("Did a save");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);
            bf.Serialize(file, settingsField1);
            file.Close();
        }

        /**
            * Loads the save data from the disk
            */

        public void loadDataFromDisk()
        {
            if (File.Exists(savePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                this.settingsField1 = (int)bf.Deserialize(file);
                file.Close();
            }
        }

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
    }

    
}