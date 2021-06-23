using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Chuzaman.Entities;
using Chuzaman.Managers;

using UnityEngine;

namespace Chuzaman.Net {

    public class SessionManager : MonoBehaviour, IEnumerable<PlayerSession> {

        private Dictionary<ulong, PlayerSession> Sessions;

        private void Awake() {
            Sessions = new Dictionary<ulong, PlayerSession>();
        }

        public void AddPlayer(ulong id, Character character) {
            Sessions.Add(id, new PlayerSession {
                ID = id,
                Character = character,
                Coins = 0
            });
            CBSL.Logging.Logger.Info<SessionManager>($"Session Created For ID : {id}");
        }

        public void RemovePlayer(ulong id) {
            Sessions.Remove(id);
            CBSL.Logging.Logger.Info<SessionManager>($"Session Removed For ID ; {id}");
        }

        public PlayerSession GetPlayer(ulong id) {
            return Sessions[id];
        }
        
        public IEnumerator<PlayerSession> GetEnumerator() {
            return Sessions.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Sessions.Select(x => x.Value).GetEnumerator();
        }

    }

}