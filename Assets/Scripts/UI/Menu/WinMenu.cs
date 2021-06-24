using Chuzaman.Managers;

using UnityEngine;

namespace CodeBlaze.UI.Menu {

    public class WinMenu : MonoBehaviour {

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