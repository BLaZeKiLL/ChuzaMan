using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI.Menu {

    public class WinMenu : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _timeTaken;
        [SerializeField] private GameObject _root;

        public void Show(int ticks) {
            _root.SetActive(true);
        }

        public void Hide() {
            _root.SetActive(false);
        }

        public void NextLevel() {
            Time.timeScale = 1f;
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }

    }

}