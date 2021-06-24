using System.Linq;

using Chuzaman.Entities;
using Chuzaman.Managers;

using CodeBlaze.UI;

using MLAPI;
using MLAPI.SceneManagement;

using UnityEngine;

namespace Chuzaman.Net {

    public class NetworkController : MonoBehaviour {

        public int PlayerCount = 2;

        private NetworkManager _Manager;
        private SessionManager _SessionManager;

        private int _CurrentLevel;

        protected void Awake() {
            _Manager = GetComponent<NetworkManager>();
            _SessionManager = GetComponent<SessionManager>();
        }

        private void Start() {
#if UNITY_SERVER
            _Manager.ConnectionApprovalCallback += OnConnectionApprovalCallback;
            _Manager.OnClientConnectedCallback += OnClientConnectedCallback;
            _Manager.OnClientDisconnectCallback += OnClientDisconnectCallback;
            
            _Manager.StartServer();
#else
            _Manager.OnClientConnectedCallback += OnServerConnectedCallback;
#endif
        }
        
        public void StartClient(Character character) {
            _Manager.NetworkConfig.ConnectionData = new []{ (byte) character };
            _Manager.StartClient();
        }
        
        public void LoadNextLevel() {
            _CurrentLevel = _CurrentLevel % 7 + 1;
            CBSL.Logging.Logger.Info<NetworkController>($"Switching Scene : Level{_CurrentLevel}");
            var progress = NetworkSceneManager.SwitchScene($"Level{_CurrentLevel}");

            progress.OnComplete += OnSceneSwitchComplete;
        }

        private void OnConnectionApprovalCallback(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback) {
            var character = (Character) connectionData[0];

            if (_SessionManager.Count() == PlayerCount) {
                CBSL.Logging.Logger.Warn<NetworkController>("Max player count reached");
                callback(false, null, false, null, null);
            }
            
            _SessionManager.AddPlayer(clientId, character);
            callback(false, null, true, null, null);
        }

        private void OnClientConnectedCallback(ulong id) {
            if (_SessionManager.Count() == PlayerCount) {
                LoadNextLevel();
            }
        }

        private void OnClientDisconnectCallback(ulong id) {
            _SessionManager.RemovePlayer(id);

            if (_SessionManager.Any()) return;

            NetworkSceneManager.SwitchScene("MainMenu");
            _CurrentLevel = 0;
            CBSL.Logging.Logger.Info<NetworkController>("Server Reset");
        }
        
        private void OnServerConnectedCallback(ulong id) {
            FindObjectOfType<MainMenuController>().ShowWaiting();
        }

        private void OnSceneSwitchComplete(bool timeout) {
            if (timeout) return; // Disconnect all, log error & reset
            
            FindObjectOfType<GameManager>().StartGame();
        }

    }

}