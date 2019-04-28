using James.InsertCoinGame.Ingame.Coins;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.ObjectCheckers;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.BlackHoles
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField]
        private float force = 5;
        [SerializeField]
        private float radius = 3;
        [SerializeField]
        private TriggerNotifier notifier;
        [SerializeField]
        private SphereCollider collider;
        [SerializeField]
        private ParticleSystem[] particleSystems;
        private CheckForObjects<Coin> coinsDetector;
        #region DebugStuff
        [ShowNativeProperty]
        public int DebugCoins { get { return coinsDetector == null ? 0 : coinsDetector.CurrentObjects.Count(); } }
        #endregion

        private void Start()
        {
            coinsDetector = new CheckForObjects<Coin>(notifier);
            coinsDetector.ObjectEntered += OnCoinEntered;
            coinsDetector.ObjectLeft += OnCoinLeft;

            ApplyRange();
        }

        private void OnCoinLeft(Coin coin)
        {
            throw new NotImplementedException();
        }

        private void OnCoinEntered(Coin coin)
        {
            throw new NotImplementedException();
        }

        private void OnValidate()
        {
            ApplyRange();
        }

        private void ApplyRange()
        {
            foreach (var part in particleSystems)
            {
                var emission = part.shape;
                emission.radius = radius;
            }
            if (collider != null)
                collider.radius = radius;
        }

        private void Update()
        {
            //foreach (var coin in coinsDetector.CurrentObjects)
            //{
            //    Pull(coin);
            //}
        }

        //private void Pull(Coin coin)
        //{
        //    var pullDirection = transform.position - coin.transform.position;
        //    pullDirection.Normalize();
        //    coin.SetPull(force * pullDirection);
        //}
    }
}
