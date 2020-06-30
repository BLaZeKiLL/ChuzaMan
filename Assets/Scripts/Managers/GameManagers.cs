using System;

using Chuzaman.Player;

using UnityEngine;

using Random = UnityEngine.Random;


namespace Managers {

    public enum Character {

        DOGGA,
        CHUZA

    }
    
    public class GameManagers : MonoBehaviour {

        [SerializeField] private PlayerController _dogga;
        [SerializeField] private PlayerController _chuza;

        private Character _activeCharacter;
        
        private void Start() {
            var flip = Random.Range(0, 2);

            if (flip == 0) {
                _activeCharacter = Character.CHUZA;
                _chuza.Active = true;
            } else {
                _activeCharacter = Character.DOGGA;
                _dogga.Active = true;
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                FlipActiveCharacter();
            }
        }

        private void FlipActiveCharacter() {
            switch (_activeCharacter) {
                case Character.DOGGA:
                    _activeCharacter = Character.CHUZA;
                    _chuza.Active = true;
                    _dogga.Active = false;
                    break;
                case Character.CHUZA:
                    _activeCharacter = Character.DOGGA;
                    _dogga.Active = true;
                    _chuza.Active = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

}