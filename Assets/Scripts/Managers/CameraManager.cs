using System;

using Chuzaman.Player;

using Cinemachine;

using UnityEngine;

namespace Chuzaman.Managers {

    public class CameraManager : MonoBehaviour {

        public static CameraManager Current;
        
        private Animator _animator;
        
        private static readonly int Chuza = Animator.StringToHash("Chuza");
        private static readonly int Dogga = Animator.StringToHash("Dogga");
        private static readonly int Win = Animator.StringToHash("Win");

        private void Awake() {
            _animator = GetComponent<Animator>();

            Current = this;
        }

        private void OnEnable() {
            GameManager.Current.OnCharacterUpdate += OnOnCharacterUpdate;
        }

        private void OnOnCharacterUpdate(object sender, GameManager.CharacterUpdateEventArgs args) {
            Debug.Log("Updating Camera Follow");

            switch (args.Character) {
                case Character.DOGGA:
                    EnableDoggaCam();
                    break;
                case Character.CHUZA:
                    EnableChuzaCam();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable() {
            GameManager.Current.OnCharacterUpdate -= OnOnCharacterUpdate;
        }

        private void EnableChuzaCam() {
            _animator.SetBool(Chuza, true);
            _animator.SetBool(Dogga, false);
            _animator.SetBool(Win, false);
        }

        private void EnableDoggaCam() {
            _animator.SetBool(Dogga, true);
            _animator.SetBool(Chuza, false);
            _animator.SetBool(Win, false);
        }

        public void EnableWinCam() {
            _animator.SetBool(Win, true);
            _animator.SetBool(Chuza, false);
            _animator.SetBool(Dogga, false);
        }

    }

}