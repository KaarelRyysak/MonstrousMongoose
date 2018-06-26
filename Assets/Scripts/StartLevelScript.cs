namespace Assets
{
    using UnityEngine;

    public class StartLevelScript : MonoBehaviour
    {
        // Use this for initialization
        private void Awake()
        {
            GlobalObjectScript.Instance.startLevel();
        }
    }
}