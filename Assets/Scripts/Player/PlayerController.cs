using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;

        [SerializeField] private AudioClip _landingSound;
        [SerializeField] private Transform _visual;
        
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        
        private Vector2 _direction;
        private bool _moving;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (_rigidbody.velocity == Vector2.zero) {
                GetInput();
            }
            _rigidbody.velocity = _direction * _speed;
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
                _moving = false;
                Debug.Log($"Moving : {_moving}");
                
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

}
