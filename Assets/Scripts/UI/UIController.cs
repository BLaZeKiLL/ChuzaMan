using CodeBlaze.UI.Menu;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI {

    public class UIController : MonoBehaviour {

        public static UIController Current;
        
        [SerializeField] private TextMeshProUGUI _CoinsCount;
        [SerializeField] private WinMenu _winMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private GameOverMenu _gameOverMenu;

        private void Awake() {
            Current = this;
        }

        public void SetCoinsCount(int count) {
            _CoinsCount.text = $"{count}";
        }

        public void ShowWinMenu() {
            _winMenu.Show();
        }

        public void ShowPauseMenu() {
            _pauseMenu.Show();
        }
    
        public void ShowGameOverMenu() {
            _gameOverMenu.Show();
        }

        public void MainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

    }

}