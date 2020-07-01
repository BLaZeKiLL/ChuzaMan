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

        public static GameManager Current;

        [SerializeField] private PlayerController _dogga;
        [SerializeField] private PlayerController _chuza;
        [SerializeField] private UIController _ui;

        public event EventHandler<PlayerController> OnCharacterUpdate;

        private Character _activeCharacter;

        private int _coins;

        public void AddCoin() {
            _coins++;
            UIController.Current.SetCoinsCount(_coins);
        }
        
        private void Awake() {
            Current = this;
        }

        private void Start() {
            var flip = Random.Range(0, 2);

            if (flip == 0) {
                _activeCharacter = Character.CHUZA;
                _chuza.Active = true;
                OnCharacterUpdate?.Invoke(this, _chuza);
            } else {
                _activeCharacter = Character.DOGGA;
                _dogga.Active = true;
                OnCharacterUpdate?.Invoke(this, _dogga);
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
                    OnCharacterUpdate?.Invoke(this, _chuza);
                    break;
                case Character.CHUZA:
                    _activeCharacter = Character.DOGGA;
                    _dogga.Active = true;
                    _chuza.Active = false;
                    OnCharacterUpdate?.Invoke(this, _dogga);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

}