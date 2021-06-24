using System;
using System.IO;

using Chuzaman.Entities;
using Chuzaman.Managers;

using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Serialization.Pooled;

using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : NetworkBehaviour {

        [SerializeField] private PlayerData _ChuzaData;
        [SerializeField] private PlayerData _DoggaData;
        
        private PlayerData _PlayerData;
        private SpriteRenderer _visual;
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        
        private NetworkVariable<Vector2> _input;
        private Vector2 _direction;
        private bool _update;
        
        private bool _initialized;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _visual = GetComponentInChildren<SpriteRenderer>();

            _input = new NetworkVariable<Vector2>(new NetworkVariableSettings {
                WritePermission = NetworkVariablePermission.OwnerOnly,
            }, Vector2.zero);
        }

        public override void NetworkStart(Stream stream) {
            using var reader = PooledNetworkReader.Get(stream);
            
            _PlayerData = GetPlayerData((Character) reader.ReadByte());
            _visual.sprite = _PlayerData.Sprite;

            if (IsOwner) {
                FindObjectOfType<CameraManager>().EnablePlayerCam(transform);
            } else {
                _input.OnValueChanged += (value, newValue) => {
                    _direction = newValue;
                    _update = true;
                };
            }

            _initialized = true;
        }

        private void Update() {
            if (!_initialized) return;
            
            if (_rigidbody.velocity == Vector2.zero && !_update) {
                if (_direction != Vector2.zero) {
                    Land();
                } else if (IsOwner) {
                    GetInput();
                }
            }
            
            _rigidbody.velocity = _direction * _PlayerData.Speed;
            _update = false;
        }
        
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player") && IsOwner) {
                FindObjectOfType<GameManager>().GameWin();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Coin")) {
                _audioSource.PlayOneShot(_PlayerData.CoinSound);
            }
        }

        private void GetInput() {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                _direction = Vector2.up;
                _input.Value = _direction;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                _direction = Vector2.down;
                _input.Value = _direction;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                _direction = Vector2.left;
                _input.Value = _direction;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                _direction = Vector2.right;
                _input.Value = _direction;
            }
        }

        private void Land() {
            // Play Sound
            _audioSource.PlayOneShot(_PlayerData.LandingSound);

            // Rotate
            if (_direction == Vector2.up) {
                _visual.transform.rotation = Quaternion.Euler(0, 0, 180);
            } else if (_direction == Vector2.down) {
                _visual.transform.rotation = Quaternion.Euler(0, 0, 0);
            } else if (_direction == Vector2.right) {
                _visual.transform.rotation = Quaternion.Euler(0, 0, 90);
            } else if (_direction == Vector2.left) {
                _visual.transform.rotation = Quaternion.Euler(0, 0, -90);
            }

            // Reset Direction
            _direction = Vector2.zero;
        }
        
        private PlayerData GetPlayerData(Character character) {
            return character switch {
                Character.DOGGA => _DoggaData,
                Character.CHUZA => _ChuzaData,
                _ => throw new ArgumentOutOfRangeException(nameof(character), character, null)
            };
        }

    }

}
