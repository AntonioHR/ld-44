using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using James.InsertCoinGame.Ingame.BlackHoles;
using TonhoHR.ObjectCheckers;
using TonhoHR.Utils;
using UnityEngine;
using UnityEngine.Events;

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
        private float resistanceAtTwo = 15;
        [SerializeField]
        private float resistanceAtTen = 5;
        [SerializeField]
        private CoinData coinData;

        [SerializeField]
        private TriggerNotifier otherCoinsTrigger;

        [SerializeField]
        private float breakTime = .3f;
        bool didFirstFall = false;

        private float MaxKickResisted
        {
            get
            {
                if (UnitCount <= 2)
                    return float.PositiveInfinity;

                float alpha = Mathf.InverseLerp(2, 10, UnitCount);
                return Mathf.Lerp(resistanceAtTwo, resistanceAtTen, alpha);
            }
        }

        private bool kicked;
        private Vector3 lastKick;
        private bool consumed;
        private bool collected;

        public bool IsMidAir { get { return kicked; } }
        public bool WasConsumed { get { return consumed; } }
        public int UnitCount { get { return units.Count; } }

        public bool CanCollect { get { return !consumed; } }


        List<BlackHole> blackHoles = new List<BlackHole>();


        private CheckForObjects<Coin> coinsCheck;
        [SerializeField]
        private float fallForceAlpha = .7f;
        [SerializeField]
        private UnityEvent HitGroundEvent;

        private void Awake()
        {
            coinsCheck = new CheckForObjects<Coin>(otherCoinsTrigger);
            coinsCheck.ObjectEntered += OnHitOtherCoin;
            int h = 0;
            foreach (var unit in units)
            {
                unit.Init(this, h++);
            }
        }
        public void FixedUpdate()
        {
            if (!kicked)
                SetPull(blackHoles.Aggregate(Vector3.zero, (pull, bh) => pull + bh.GetPullFor(this)));
            if(!consumed)
                DoStackSpeedDisplay();
        }
        private void DoStackSpeedDisplay()
        {
            foreach (var unit in units)
            {
                unit.ShowSpeed(body.velocity);
            }
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
            gameObject.SetActive(false);
            Destroy(gameObject,1);
        }

        public void Kick(Vector3 direction, float forceAlpha)
        {
            Debug.LogFormat("KickForce: {0}", forceAlpha);
            Debug.Assert(forceAlpha <= 1 && forceAlpha >= 0);

            direction += Vector3.up * .2f;
            Vector3 absoluteForce = direction * MaxKickForce * forceAlpha;
            KickAbsoulte(absoluteForce);
        }
        public void KickAbsoulte(Vector3 force)
        {
            body.AddForce(force, ForceMode.Impulse);
            kicked = true;
            lastKick = force;
            if(force.magnitude > MaxKickResisted)
            {
                float delta = force.magnitude - MaxKickResisted;
                delta = delta / 5;

                float t = Mathf.Lerp(breakTime, .05f, delta);
                this.WaitThenDo(t, Explode);
            }


            this.WaitUntilThenDo(() => body.velocity.magnitude < 2f, HitGroundTrigger, skipFirstFrame: true, skipMode: CoroutineUtils.SkipMode.FixedUpdate);
            this.WaitUntilThenDo(() => body.velocity.magnitude < 0.001f, OnKickOver, skipFirstFrame: true, skipMode:CoroutineUtils.SkipMode.FixedUpdate);
            CheckKicks();
        }

        private void HitGroundTrigger()
        {
            if (!consumed && !didFirstFall)
                HitGroundEvent.Invoke();
            didFirstFall = true;
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
            newUnit.Init(this, units.Count);
            newUnit.transform.position = units.Last().NextPos;
            units.Add(newUnit);
        }
        private void AnimateHit()
        {
            Debug.Assert(!consumed);
            foreach (var unit in units)
            {
                unit.AnimateHit();
            }
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

        [NaughtyAttributes.Button]
        public void Explode()
        {
            Consume();
            for (int i = 0; i < UnitCount; i++)
            {
                var newCoin = Instantiate(coinData.coinPrefab, transform.parent);
                newCoin.kicked = true;
                newCoin.transform.position = units[i].transform.position;
                Vector3 direction = UnityEngine.Random.insideUnitCircle;
                direction.z = direction.y;
                direction.y = 0;
                float heightAlpha = i / (float)UnitCount;
                direction -= 2 * lastKick.normalized;
                direction.Normalize();
                if(heightAlpha > 0)
                    newCoin.Kick(direction, heightAlpha * fallForceAlpha);
            }
        }


        public void Collect(TweenCallback callback)
        {
            Debug.Assert(!consumed);
            consumed = true;
            collected = true;
            var seq = DOTween.Sequence();
            foreach (var unit in units)
            {
                var collect = unit.AnimateCollect();
                collect.AppendCallback(callback);
                seq.Insert(0, collect);
            }
            seq.OnComplete(Consume);
        }
    }
}