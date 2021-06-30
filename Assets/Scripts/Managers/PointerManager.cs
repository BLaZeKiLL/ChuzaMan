using UnityEngine;

namespace Chuzaman.Managers {

    public class PointerManager : MonoBehaviour {
        
        [SerializeField] private float _viewThreshold = 10f;

        public Transform Target { get; set; }
        
        private SpriteRenderer _Sprite;

        private void Awake() {
            _Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update() {
            if (Target == null) return;
            
            var diff = Target.position - transform.position;
            
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
            _Sprite.enabled = false;
        }

        private void Show() {
            if (!_Sprite.enabled) _Sprite.enabled = true;
        }

    }

}