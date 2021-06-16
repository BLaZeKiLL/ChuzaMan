using UnityEngine;

namespace Chuzaman.Entities {

    [CreateAssetMenu(fileName = "PlayerData", menuName = "ChuzaDogga/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject {

        public float Speed = 15f;
        public Sprite Sprite;
        public AudioClip LandingSound;
        public AudioClip CoinSound;
        public AudioClip ActivateSound;

    }

}