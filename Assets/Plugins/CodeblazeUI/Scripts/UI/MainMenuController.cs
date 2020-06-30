using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI {

    public class MainMenuController : MonoBehaviour {

        public void StartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Quit() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

    }

}