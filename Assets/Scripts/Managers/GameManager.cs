using System;

using Chuzaman.Player;

using Cinemachine;

using CodeBlaze.UI;

using UnityEngine;

using Random = UnityEngine.Random;


namespace Chuzaman.Managers {

    public enum Character {

        DOGGA,
        CHUZA

    }
    
    public class GameManager : MonoBehaviour {

        public class CharacterUpdateEventArgs : EventArgs {

            public PlayerController PlayerController { get; set; }
            public Character Character { get; set; }

        }
        
        public static GameManager Current;

        [SerializeField] private PlayerController _dogga;
        [SerializeField] private PlayerController _chuza;
        [SerializeField] private UIController _ui;

        [SerializeField] private AudioClip _winSound;
        
        public event EventHandler<CharacterUpdateEventArgs> OnCharacterUpdate;

        private Character _activeCharacter;

        private AudioSource _audioSource;

        private int _coins;
        private bool win;

        public void AddCoin() {
            _coins++;
            UIController.Current.SetCoinsCount(_coins);
        }

        public void GameWin() {
            if (win) return;

            win = true;
            _audioSource.PlayOneShot(_winSound);
            CameraManager.Current.EnableWinCam();
            UIController.Current.ShowWinMenu();
        }
        
        private void Awake() {
            Current = this;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            win = false;
            var flip = Random.Range(0, 2);

            if (flip == 0) {
                _activeCharacter = Character.CHUZA;
                _chuza.Active = true;
                OnCharacterUpdate?.Invoke(this, new CharacterUpdateEventArgs {
                    PlayerController = _chuza,
                    Character = Character.CHUZA
                });
            } else {
                _activeCharacter = Character.DOGGA;
                _dogga.Active = true;
                OnCharacterUpdate?.Invoke(this, new CharacterUpdateEventArgs {
                    PlayerController = _dogga,
                    Character = Character.DOGGA
                });
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                FlipActiveCharacter();
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                _ui.ShowPauseMenu();
            }
        }

        private void FlipActiveCharacter() {
            switch (_activeCharacter) {
                case Character.DOGGA:
                    _activeCharacter = Character.CHUZA;
                    _chuza.Active = true;
                    _dogga.Active = false;
                    OnCharacterUpdate?.Invoke(this, new CharacterUpdateEventArgs {
                        PlayerController = _chuza,
                        Character = Character.CHUZA
                    });
                    break;
                case Character.CHUZA:
                    _activeCharacter = Character.DOGGA;
                    _dogga.Active = true;
                    _chuza.Active = false;
                    OnCharacterUpdate?.Invoke(this, new CharacterUpdateEventArgs {
                        PlayerController = _dogga,
                        Character = Character.DOGGA
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

}