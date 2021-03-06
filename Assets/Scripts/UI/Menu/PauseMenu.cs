﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.UI.Menu {

    public class PauseMenu : MonoBehaviour {

        [SerializeField] private GameObject _root;

        public void Show() {
            Time.timeScale = 0f;
            _root.SetActive(true);
        }

        private void Hide() {
            _root.SetActive(false);
        }

        public void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Resume() {
            Time.timeScale = 1;
            Hide();
        }

    }

}