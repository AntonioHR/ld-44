using System;
using System.Collections;
using System.Collections.Generic;
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

        public void Kick(Vector3 direction)
        {
            body.AddForce(direction * KickForce, ForceMode.Impulse);
            kicked = true;
            this.WaitUntilThenDo(() => body.velocity.magnitude < .3f, KickOver);
        }

        private void KickOver()
        {
            throw new NotImplementedException();
        }

        public void SetPull(Vector3 force)
        {
            if (kicked)
                return;
            body.velocity = force;
            body.AddForce(force);
        }
    }
}