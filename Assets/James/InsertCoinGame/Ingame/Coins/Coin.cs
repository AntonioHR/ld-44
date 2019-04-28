using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using James.InsertCoinGame.Ingame.BlackHoles;
using TonhoHR.Utils;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private float KickForce;
        [SerializeField]
        private Rigidbody body;
        private bool kicked;

        List<BlackHole> blackHoles = new List<BlackHole>();

        public void Kick(Vector3 direction)
        {
            body.AddForce(direction * KickForce, ForceMode.Impulse);
            kicked = true;
            this.WaitUntilThenDo(() => body.velocity.magnitude < 0.001f, OnKickOver);
            //this.WaitThenDo(.5f, OnKickOver);
        }

        private void OnKickOver()
        {
            kicked = false;
        }

        private void FixedUpdate()
        {
            if(!kicked)
                SetPull(blackHoles.Aggregate(Vector3.zero, (pull, bh) => pull + bh.GetPullFor(this)));
        }
        public void SetPull(Vector3 force)
        {
            body.velocity = force;
            body.AddForce(force);
        }

        internal void AddBlachHole(BlackHole blackHole)
        {
            blackHoles.Add(blackHole);
        }

        internal void RemoveBlackhole(BlackHole blackHole)
        {
            blackHoles.Remove(blackHole);
        }
    }
}