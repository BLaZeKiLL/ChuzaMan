using UnityEngine;

namespace Chuzaman.Managers {

    public class PointerManager : MonoBehaviour {
        
        [SerializeField] private float _viewThreshold = 3f;

        private SpriteRenderer _spriteRenderer;
        
        private Vector3 _target;

        private void Awake() {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update() {
            var diff = _target - transform.position;
            
            if (diff.magnitude <= _viewThreshold) {
                Hide();
            } else {
                Show();
                
                diff.Normalize();
 
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            }
        }

        public void Hide() {
            _spriteRenderer.enabled = false;
        }

        private void Show() {
            if (!_spriteRenderer.enabled) _spriteRenderer.enabled = true;
        }

    }

}