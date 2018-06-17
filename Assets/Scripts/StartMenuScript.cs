namespace Assets
{
    
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartMenuScript : MonoBehaviour
    {
        public void startGame ()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void quitGame ()
        {
            Application.Quit();
        }
    }
}
