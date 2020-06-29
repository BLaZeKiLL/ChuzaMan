using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chuzaman.Player {

    public class PlayerController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;
        
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            } else if (Input.GetKeyDown(KeyCode.S)) {
                _direction = Vector2.down;
            } else if (Input.GetKeyDown(KeyCode.A)) {
                _direction = Vector2.left;
            } else if (Input.GetKeyDown(KeyCode.D)) {
                _direction = Vector2.right;
            } else {
                _direction = Vector2.zero;
            }
        }

    }

}
