using James.InsertCoinGame.Ingame.Coins;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.ObjectCheckers;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame.BlackHoles
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField]
        private float force = 5;
        [SerializeField]
        private float radius = 3;
        [SerializeField]
        private TriggerNotifier gravityArea;
        [SerializeField]
        private TriggerNotifier eatArea;
        [SerializeField]
        private SphereCollider collider;
        [SerializeField]
        private ParticleSystem[] particleSystems;
        private CheckForObjects<Coin> gravityCheck;
        private CheckForObjects<Coin> eatCheck;

        [Inject]
        private InsertCoinIngameScene gameScene;



        #region DebugStuff
        [SerializeField]
        private Coin[] DebugCoins;
        #endregion

        private void Awake()
        {
            gravityCheck = new CheckForObjects<Coin>(gravityArea);
            gravityCheck.ObjectEntered += OnCoinEnteredGravity;
            gravityCheck.ObjectLeft += OnCoinLeftGravity;

            //eatCheck = new CheckForObjects<Coin>(eatArea);
            //eatCheck.ObjectEntered += OnCoinEnteredCore;
            //eatCheck.ObjectLeft += OnCoinLeftCore;

            ApplyRange();
        }

        private void OnCoinLeftCore(Coin obj)
        {
        }

        private void OnCoinEnteredCore(Coin obj)
        {
        }

        private void OnCoinLeftGravity(Coin coin)
        {
            coin.RemoveBlackhole(this);
            this.DebugCoins = gravityCheck.CurrentObjects.ToArray();
        }

        private void OnCoinEnteredGravity(Coin coin)
        {
            coin.AddBlachHole(this);
            this.DebugCoins = gravityCheck.CurrentObjects.ToArray();
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

        public Vector3 GetPullFor(Coin coin)
        {
            var direction = transform.position - coin.transform.position;
            direction.Normalize();

            return direction * force;
        }
    }
}
