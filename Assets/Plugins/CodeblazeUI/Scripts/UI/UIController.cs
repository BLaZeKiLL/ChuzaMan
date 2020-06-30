using CodeBlaze.UI.Menu;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBlaze.UI {

    public class UIController : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _CoinsCount;
        [SerializeField] private WinMenu _winMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private GameOverMenu _gameOverMenu;

        public void SetCoinsCount(int count) {
            _CoinsCount.text = $"{count}";
        }

        public void ShowWinMenu(int ticks) {
            _winMenu.Show(ticks);
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