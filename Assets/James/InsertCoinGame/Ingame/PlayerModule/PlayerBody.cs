using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerBody : MonoBehaviour
    {
        [Inject]
        private Player owner;

        [SerializeField]
        private PlayerMovement movement;
        [SerializeField]
        Transform feet;
        [SerializeField]
        private KickIndicator kickIndicator;
        [SerializeField]
        private TriggerNotifier kickArea;

        public Player Owner { get { return owner; } }
        public PlayerMovement Movement { get { return movement; } }

        public TriggerNotifier KickArea { get { return kickArea; } }
        public KickIndicator KickIndicator { get => kickIndicator; }

        private void Update()
        {
            owner.Tick();
        }


        public void PerformKickAnimation(TweenCallback kickCallback)
        {
            feet.DOPunchPosition(Vector3.forward * .5f, .5f, 0, 0).OnComplete(kickCallback);
        }
        #region Debug
        [ShowNativeProperty]
        public int CoinCount { get { return owner == null ? 0 : owner.CoinsCount; } }

        #endregion
    }
}
