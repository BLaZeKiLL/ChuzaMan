using System.Collections.Generic;
using System.Linq;

using Chuzaman.Net;
using Chuzaman.Player;

using CodeBlaze.UI;

using MLAPI;
using MLAPI.Messaging;
using MLAPI.Serialization.Pooled;

using UnityEngine;


namespace Chuzaman.Managers {

    public class GameManager : NetworkBehaviour {
        
        [SerializeField] private GameObject _PlayerPrefab;
        [SerializeField] private Transform _SpawnPoints;
        [SerializeField] private AudioClip _WinSound;

        private SessionManager _SessionManager;
        private AudioSource _AudioSource;
        private int _NextCount;

        private bool win;
        
        private void Awake() {
            _AudioSource = GetComponent<AudioSource>();
            _SessionManager = NetworkManager.Singleton.GetComponent<SessionManager>();
        }

        public void GameWin() {
            if (win) return;

            win = true;
            _AudioSource.PlayOneShot(_WinSound);
            FindObjectOfType<PointerManager>().Hide();
            FindObjectOfType<CameraManager>().EnableWinCam();
            FindObjectOfType<UIController>().ShowWinMenu();
        }

        public void StartGame() {
            var pts = new HashSet<Transform>(_SpawnPoints.GetComponentsInChildren<Transform>());

            pts.Remove(_SpawnPoints.transform); // Why does unity return parent also ?
            
            foreach (var (player, point) in _SessionManager.Zip(pts, (player, point) => (player, point.localPosition))) {
                var obj = Instantiate(_PlayerPrefab, point, Quaternion.identity);

                using var stream = PooledNetworkBuffer.Get();
                using var writer = PooledNetworkWriter.Get(stream);
                
                writer.WriteByte((byte) player.Character);
                writer.WritePadBits();
                
                var net = obj.GetComponent<NetworkObject>();

                net.SpawnAsPlayerObject(player.ID, stream, true);
            }
        }

        public void NextLevel() {
            NextLevelServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void NextLevelServerRpc() {
            _NextCount++;
            var netController = NetworkManager.Singleton.GetComponent<NetworkController>();

            if (_NextCount == netController.PlayerCount) {
                netController.LoadNextLevel();
            }
        }

    }

}