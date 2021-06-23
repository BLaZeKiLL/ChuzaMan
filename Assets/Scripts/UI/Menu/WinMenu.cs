using Chuzaman.Managers;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI.Menu {

    public class WinMenu : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _TimeTaken;
        [SerializeField] private GameObject _Root;
        [SerializeField] private GameObject _Title;
        [SerializeField] private GameObject _Waiting;
        [SerializeField] private GameObject _NextButton;

        public void Show() {
            _Root.SetActive(true);
        }

        public void Hide() {
            _Root.SetActive(false);
        }

        public void NextLevel() {
            Time.timeScale = 1f;
            _Title.SetActive(false);
            _NextButton.SetActive(false);
            _Waiting.SetActive(true);
            FindObjectOfType<GameManager>().NextLevel();
        }

    }

}