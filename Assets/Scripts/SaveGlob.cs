namespace Assets
{
    using UnityEngine;

    [System.Serializable]
    public class SaveGlob : MonoBehaviour
    {
        public int settingsField1 = 0;
        public int settingsField2 = 0;
        public int settingsField3 = 0;

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

        // Sets settingsField2
        public void setSettingsField2(int newSettingsField2)
        {
            settingsField2 = newSettingsField2;
        }

        // Returns settingsField2
        public int getSettingsField2()
        {
            return settingsField2;
        }

        // Sets settingsField3
        public void setSettingsField3(int newSettingsField2)
        {
            settingsField2 = newSettingsField2;
        }

        // Returns settingsField3
        public int getSettingsField3()
        {
            return settingsField3;
        }
    }
}