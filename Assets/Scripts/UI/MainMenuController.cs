using Chuzaman.Entities;
using Chuzaman.Net;

using MLAPI;

using UnityEngine;

namespace CodeBlaze.UI {

    public class MainMenuController : MonoBehaviour {

        [SerializeField] private Transform _StartDogga;
        [SerializeField] private Transform _StartChuza;
        [SerializeField] private Transform _Title;
        [SerializeField] private Transform _Waiting;
        
        public void StartChuza() {
            NetworkManager.Singleton.GetComponent<NetworkController>().StartClient(Character.CHUZA);
        }

        public void StartDogga() {
            NetworkManager.Singleton.GetComponent<NetworkController>().StartClient(Character.DOGGA);
        }

        public void Quit() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void ShowWaiting() {
            _StartChuza.gameObject.SetActive(false);
            _StartDogga.gameObject.SetActive(false);
            _Title.gameObject.SetActive(false);
            _Waiting.gameObject.SetActive(true);
        }

    }

}