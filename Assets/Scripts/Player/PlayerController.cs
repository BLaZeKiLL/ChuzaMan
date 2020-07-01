using System;
using System.Collections;
using System.Collections.Generic;

using Chuzaman.Managers;

using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;
        [SerializeField] private Transform _visual;
        
        [SerializeField] private AudioClip _landingSound;
        [SerializeField] private AudioClip _coinSound;

        
        public bool Active { get; set; }
        
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        
        private Vector2 _direction;
        private bool _moving;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (!Active) return;
            
            if (_rigidbody.velocity == Vector2.zero) {
                GetInput();
            }
            _rigidbody.velocity = _direction * _speed;
        }

        // Will be called twice
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("Dogga Chuza Meet");
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Coin")) {
                _audioSource.PlayOneShot(_coinSound);
                GameManager.Current.AddCoin();
            }
        }

        private void GetInput() {
            if (Input.GetKeyDown(KeyCode.W)) {
                _direction = Vector2.up;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.S)) {
                _direction = Vector2.down;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.A)) {
                _direction = Vector2.left;
                _moving = true;
            } else if (Input.GetKeyDown(KeyCode.D)) {
                _direction = Vector2.right;
                _moving = true;
            } else if (_moving) { // Landed
                OnLanding();
            }
        }

        private void OnLanding() {
            _moving = false;
            
            // Play Sound
            _audioSource.PlayOneShot(_landingSound);

            // Rotate
            if (_direction == Vector2.up) {
                _visual.rotation = Quaternion.Euler(0, 0, 180);
            } else if (_direction == Vector2.down) {
                _visual.rotation = Quaternion.Euler(0, 0, 0);
            } else if (_direction == Vector2.right) {
                _visual.rotation = Quaternion.Euler(0, 0, 90);
            } else if (_direction == Vector2.left) {
                _visual.rotation = Quaternion.Euler(0, 0, -90);
            }

            // Reset Direction
            _direction = Vector2.zero;
        }

    }

}
