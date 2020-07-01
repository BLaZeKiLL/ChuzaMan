using System;

using Chuzaman.Player;

using Cinemachine;

using UnityEngine;

namespace Chuzaman.Managers {

    public class CameraManager : MonoBehaviour {

        [SerializeField] private CinemachineVirtualCamera _camera;
        
        private void OnEnable() {
            GameManager.Current.OnCharacterUpdate += OnOnCharacterUpdate;
        }

        private void OnOnCharacterUpdate(object sender, PlayerController _player) {
            Debug.Log("Updating Camera Follow");
            _camera.Follow = _player.transform;
        }

        private void OnDisable() {
            GameManager.Current.OnCharacterUpdate -= OnOnCharacterUpdate;
        }

    }

}