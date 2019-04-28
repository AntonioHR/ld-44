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
        private Player owner;

        [SerializeField]
        private PlayerMovement movement;
        [SerializeField]
        Transform feet;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private TriggerNotifier kickArea;

        public Player Owner { get { return owner; } }
        public PlayerMovement Movement { get { return movement; } }

        public TriggerNotifier KickArea { get { return kickArea; } }

        [Inject]
        public void Inject(Player owner)
        {
            this.owner = owner;
        }

        public void ShowKickUi()
        {
            canvasGroup.DOFade(1, .3f);
        }
        public void HideKickUi()
        {
            canvasGroup.DOFade(0, .3f);
        }

        public void PerformKickAnimation(TweenCallback kickCallback)
        {
            feet.DOPunchPosition(Vector3.forward * .5f, .5f, 0, 0).OnComplete(kickCallback);
        }
        #region Debug
        [ShowNativeProperty]
        public int CoinCount { get { return owner.CoinsCount; } }
        #endregion
    }
}
