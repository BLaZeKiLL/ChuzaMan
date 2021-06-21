using Chuzaman.Entities;
using Chuzaman.Managers;

using MLAPI;

using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : NetworkBehaviour {

        [SerializeField] private PlayerData _playerData;

        private SpriteRenderer _visual;
        
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        
        private Vector2 _direction;
        private bool _moving;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _visual = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start() {
            FindObjectOfType<CameraManager>().EnablePlayerCam(transform);
            _visual.sprite = _playerData.Sprite;
        }

        private void Update() {
            if (_rigidbody.velocity == Vector2.zero) {
                GetInput();
            }
            _rigidbody.velocity = _direction * _playerData.Speed;
        }

        // Will be called twice
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                GameManager.Current.GameWin();
                // Destroy(gameObject); // makes win transition wierd
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Coin")) {
                _audioSource.PlayOneShot(_playerData.CoinSound);
            }
        }

        private void GetInput() {
            if (!IsOwner) return;
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                _direction = Vector2.up;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                _direction = Vector2.down;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                _direction = Vector2.left;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                _direction = Vector2.right;
                _moving = true;
            } else if (_moving) { // Landed
                OnLanding();
            }
        }

        private void OnLanding() {
            _moving = false;
            
            // Play Sound
            _audioSource.PlayOneShot(_playerData.LandingSound);

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

    }

}
