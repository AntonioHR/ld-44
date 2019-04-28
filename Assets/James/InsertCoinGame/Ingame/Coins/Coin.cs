using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private float KickForce;
        [SerializeField]
        private Rigidbody body;
        public void Kick(Vector3 direction)
        {
            body.AddForce(direction * KickForce, ForceMode.Impulse);
        }
    }
}