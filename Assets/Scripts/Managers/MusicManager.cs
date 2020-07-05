using System;

using UnityEngine;

namespace Chuzaman.Managers {

    public class MusicManager : MonoBehaviour {

        public static MusicManager Current;

        private void Awake() {
            if (Current != null) Destroy(gameObject);
            else {
                DontDestroyOnLoad(gameObject);
                Current = this;
            }
        }

    }

}