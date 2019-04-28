using DG.Tweening;
using System;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    public class CoinUnit : MonoBehaviour
    {
        [SerializeField]
        private Transform nextRef;
        [SerializeField]
        private float animationPunch = 1;
        [SerializeField]
        private float heightDelay = .1f;
        [SerializeField]
        private float punchTime = .1f;
        [SerializeField]
        private float minPunchAlpha = .2f;

        private Tweener tween;

        public Vector3 NextPos { get { return nextRef.position; } }

        public void AnimateHit(int myHeight, int totalHeight)
        {
            if (tween != null && tween.IsActive())
                tween.Kill();

            float t = Mathf.InverseLerp(0, totalHeight, myHeight);
            float totalTime = punchTime + heightDelay * (totalHeight - 1);
            float myDelay = heightDelay * (1 - t);
            float myPunch = (minPunchAlpha+ t * (1-minPunchAlpha)) * animationPunch;
            tween = transform.DOPunchPosition(Vector3.up * myPunch, totalTime - myDelay, 0, 0).SetDelay(myDelay);
        }
        private void OnDestroy()
        {
            if(tween != null && tween.IsActive())
                tween.Kill();
        }
    }
}