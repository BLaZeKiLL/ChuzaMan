using Chuzaman.Entities;
using Chuzaman.Managers;

using TMPro;

using UnityEngine;

namespace CodeBlaze.UI.Menu {

    public class WinMenu : MonoBehaviour {

        [SerializeField] private GameObject _Root;
        [SerializeField] private GameObject _Title;
        [SerializeField] private GameObject _Waiting;
        [SerializeField] private GameObject _NextButton;

        [SerializeField] private TextMeshProUGUI _ChuzaCoins;
        [SerializeField] private TextMeshProUGUI _DoggaCoins;

        public void Show(WinData data) {
            _Root.SetActive(true);

            _ChuzaCoins.text = $"{data.ChuzaCoins}";
            _DoggaCoins.text = $"{data.DoggaCoins}";
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