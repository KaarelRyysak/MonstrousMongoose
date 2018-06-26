namespace Assets
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    [System.Serializable]
    public class SaveManager : MonoBehaviour
    {
        private GlobalObjectScript globalObject = GlobalObjectScript.Instance;
        private SaveGlob saveGlob;    // the Dictionary used to save and load data to/from disk
        protected string savePath;

        private void awake()
        {
            this.savePath = Application.persistentDataPath + "/save.dat";
        }

        public void saveManager(SaveGlob saveGlob)
        {
            this.saveGlob = saveGlob;
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
            if (File.Exists(savePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                this.saveGlob = (SaveGlob)bf.Deserialize(file);
                file.Close();
            }
        }
    }
}