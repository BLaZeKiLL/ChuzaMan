using Cinemachine;

using UnityEngine;

namespace Chuzaman.Managers {

    public class CameraManager : MonoBehaviour {
        
        [SerializeField]
        private CinemachineVirtualCamera _PlayerCamera;
        
        private Animator _Animator;

        private static readonly int Player = Animator.StringToHash("Player");
        private static readonly int Win = Animator.StringToHash("Win");

        private void Awake() {
            _Animator = GetComponent<Animator>();
        }

        public void EnablePlayerCam(Transform player) {
            _PlayerCamera.Follow = player;
            
            _Animator.SetBool(Player, true);
            _Animator.SetBool(Win, false);
        }

        public void EnableWinCam() {
            _Animator.SetBool(Win, true);
            _Animator.SetBool(Player, false);
        }

    }

}