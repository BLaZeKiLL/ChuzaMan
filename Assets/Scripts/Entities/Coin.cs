using System;

using UnityEngine;

namespace Chuzaman.Entities {

    public class Coin : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }

    }

}