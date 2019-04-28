using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using James.InsertCoinGame.Ingame.BlackHoles;
using TonhoHR.ObjectCheckers;
using TonhoHR.Utils;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private float MaxKickForce = 10;
        [SerializeField]
        private Rigidbody body;
        [SerializeField]
        private List<CoinUnit> units;
        [SerializeField]
        private Transform unitsParent;
        [SerializeField]
        private CoinUnit unitPrefab;

        [SerializeField]
        private TriggerNotifier otherCoinsTrigger;

        private bool kicked;
        private bool consumed;
        public bool IsMidAir { get { return kicked; } }
        public bool WasConsumed { get { return consumed; } }
        public int UnitCount { get { return units.Count; } }

        List<BlackHole> blackHoles = new List<BlackHole>();
        private CheckForObjects<Coin> coinsCheck;

        private void Start()
        {
            coinsCheck = new CheckForObjects<Coin>(otherCoinsTrigger);
            coinsCheck.ObjectEntered += OnHitOtherCoin;
        }

        private void OnHitOtherCoin(Coin other)
        {
            if(other != this && other != null && IsMidAir && !other.IsMidAir && !other.WasConsumed)
            {
                Consume();
                other.AddUnit(UnitCount);
                other.KickAbsoulte(body.velocity);
            }
        }

        private void Consume()
        {
            consumed = true;
            Destroy(gameObject);
        }

        public void Kick(Vector3 direction, float force)
        {
            Debug.LogFormat("KickForce: {0}", force);
            Debug.Assert(force <= 1 && force >= 0);

            Vector3 absoluteForce = direction * MaxKickForce * force;
            KickAbsoulte(absoluteForce);
        }
        public void KickAbsoulte(Vector3 force)
        {
            if (IsMidAir && force.magnitude <= body.velocity.magnitude)
                return;
            //body.velocity = Vector3.zero;

            body.AddForce(force, ForceMode.Impulse);
            kicked = true;
            this.WaitUntilThenDo(() => body.velocity.magnitude < 0.001f, OnKickOver);
            CheckKicks();
        }

        private void CheckKicks()
        {
            foreach (var coin in coinsCheck.CurrentObjects)
            {
                OnHitOtherCoin(coin);
            }
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

        public void AddUnit(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnUnit();
            }
            AnimateHit();
        }

        private void SpawnUnit()
        {
            var newUnit = GameObject.Instantiate(unitPrefab, unitsParent);
            newUnit.transform.position = units.Last().NextPos;
            units.Add(newUnit);
        }

        private void AnimateHit()
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].AnimateHit(i, units.Count);
            }
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