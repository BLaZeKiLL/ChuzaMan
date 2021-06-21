using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI.Menu {

    public class GameOverMenu : MonoBehaviour {

        [SerializeField] private GameObject _root;

        public void Show() {
            _root.SetActive(true);
        }

        public void Hide() {
            _root.SetActive(false);
        }

        public void Restart() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

}