using System;
using System.IO;

using Chuzaman.Entities;
using Chuzaman.Managers;
using Chuzaman.Net;

using CodeBlaze.UI;

using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using MLAPI.Serialization.Pooled;
using MLAPI.Spawning;

using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : NetworkBehaviour {

        [SerializeField] private PlayerData _ChuzaData;
        [SerializeField] private PlayerData _DoggaData;
        
        private PlayerData _PlayerData;
        private SpriteRenderer _Visual;
        private Rigidbody2D _Rigidbody;
        private AudioSource _AudioSource;
        private SessionManager _SessionManager;
        private UIController _UI;
        
        private NetworkVariable<Vector2> _Input;
        private Vector2 _Direction;
        private bool _Update;
        private bool _Initialized;
        private ulong _ID;
        private int _Coins;
        
        private void Awake() {
            _Rigidbody = GetComponent<Rigidbody2D>();
            _AudioSource = GetComponent<AudioSource>();
            _Visual = GetComponentInChildren<SpriteRenderer>();
            _UI = FindObjectOfType<UIController>();

            _Input = new NetworkVariable<Vector2>(new NetworkVariableSettings {
                WritePermission = NetworkVariablePermission.OwnerOnly,
            }, Vector2.zero);
        }

        public override void NetworkStart(Stream stream) {
            using var reader = PooledNetworkReader.Get(stream);
            
            _PlayerData = GetPlayerData((Character) reader.ReadByte());
            _Visual.sprite = _PlayerData.Sprite;

            if (IsOwner) {
                FindObjectOfType<CameraManager>().EnablePlayerCam(transform);
            } else {
                _Input.OnValueChanged += (value, newValue) => {
                    _Direction = newValue;
                    _Update = true;
                };

                FindObjectOfType<PointerManager>().Target = transform;
            }

            _ID = GetComponent<NetworkObject>().OwnerClientId;
            _SessionManager = NetworkManager.Singleton.GetComponent<SessionManager>();
            _Initialized = true;
        }

        private void Update() {
            if (!_Initialized) return;
            
            if (_Rigidbody.velocity == Vector2.zero && !_Update) {
                if (_Direction != Vector2.zero) {
                    Land();
                } else if (IsOwner) {
                    GetInput();
                }
            }
            
            _Rigidbody.velocity = _Direction * _PlayerData.Speed;
            _Update = false;
        }
        
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player") && IsServer) {
                FindObjectOfType<GameManager>().GameWin();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Coin")) return;

            _AudioSource.PlayOneShot(_PlayerData.CoinSound);

            if (IsServer) {
                Destroy(NetworkSpawnManager.SpawnedObjects[other.GetComponent<NetworkObject>().NetworkObjectId]);
                _SessionManager.AddCoin(_ID);
            } else if (IsOwner) {
                _UI.SetCoinsCount(++_Coins);
            }
        }

        private void GetInput() {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                _Direction = Vector2.up;
                _Input.Value = _Direction;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                _Direction = Vector2.down;
                _Input.Value = _Direction;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                _Direction = Vector2.left;
                _Input.Value = _Direction;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                _Direction = Vector2.right;
                _Input.Value = _Direction;
            }
        }

        private void Land() {
            // Play Sound
            _AudioSource.PlayOneShot(_PlayerData.LandingSound);

            // Rotate
            if (_Direction == Vector2.up) {
                _Visual.transform.rotation = Quaternion.Euler(0, 0, 180);
            } else if (_Direction == Vector2.down) {
                _Visual.transform.rotation = Quaternion.Euler(0, 0, 0);
            } else if (_Direction == Vector2.right) {
                _Visual.transform.rotation = Quaternion.Euler(0, 0, 90);
            } else if (_Direction == Vector2.left) {
                _Visual.transform.rotation = Quaternion.Euler(0, 0, -90);
            }

            // Reset Direction
            _Direction = Vector2.zero;
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
