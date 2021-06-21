using System.Collections.Generic;
using System.IO;
using System.Linq;

using Chuzaman.Net;

using CodeBlaze.UI;

using MLAPI;

using UnityEngine;


namespace Chuzaman.Managers {

    public class GameManager : MonoBehaviour {

        public static GameManager Current;

        [SerializeField] private GameObject _PlayerPrefab;
        [SerializeField] private UIController _UI;
        [SerializeField] private Transform _SpawnPoints;
        [SerializeField] private AudioClip _WinSound;
        
        private SessionManager _SessionManager;
        private AudioSource _AudioSource;

        private bool win;
        
        private void Awake() {
            Current = this;
            _AudioSource = GetComponent<AudioSource>();
            _SessionManager = NetworkManager.Singleton.GetComponent<SessionManager>();
        }

        public void GameWin() {
            if (win) return;

            win = true;
            _AudioSource.PlayOneShot(_WinSound);
            PointerManager.Current.Hide();
            // CameraManager.Current.EnableWinCam();
            UIController.Current.ShowWinMenu();
        }

        public void StartGame() {
            var pts = new HashSet<Transform>(_SpawnPoints.GetComponentsInChildren<Transform>());

            pts.Remove(_SpawnPoints.transform); // Why does unity return parent also ?
            
            foreach (var (player, point) in _SessionManager.Zip(pts, (player, point) => (player, point.localPosition))
            ) {
                var net = Instantiate(_PlayerPrefab, point, Quaternion.identity).GetComponent<NetworkObject>();
                net.SpawnAsPlayerObject(player.ID, Stream.Null);
            }
        }

    }

}