﻿using DG.Tweening;
using System;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    public class CoinUnit : MonoBehaviour
    {
        [SerializeField]
        private Transform nextRef;
        [SerializeField]
        private Transform core;
        [SerializeField]
        private Vector3 maxSpeedOffset = Vector3.back;
        [SerializeField]
        private float animationPunch = 1;
        [SerializeField]
        private float heightDelay = .1f;
        [SerializeField]
        private float punchTime = .1f;
        [SerializeField]
        private float minPunchAlpha = .2f;

        private Tweener tween;
        private Coin owner;
        private Vector3 coreLocalStart;
        private Vector3 pilePosDamp;

        public Vector3 NextPos { get { return nextRef.position; } }

        public int Height { get; private set; }
        private float HeightAlpha { get { return (float)Height / (float)owner.UnitCount; } }
        private void Awake()
        {
            coreLocalStart = core.localPosition;
        }
        public void Init(Coin owner, int myHeight)
        {
            this.owner = owner;
            this.Height = myHeight;
        }

        public void AnimateHit()
        {
            if (tween != null && tween.IsActive())
                tween.Complete();
            int totalHeight = owner.UnitCount;

            float t = Mathf.InverseLerp(0, totalHeight, Height);
            float totalTime = punchTime + heightDelay / (totalHeight - 1);
            float myDelay = heightDelay * (1 - t);
            float myPunch = (minPunchAlpha+ t * (1-minPunchAlpha)) * animationPunch;
            tween = transform.DOPunchPosition(Vector3.up * myPunch, totalTime - myDelay, 0, 0).SetDelay(myDelay);
        }
        private void OnDestroy()
        {
            if(tween != null && tween.IsActive())
                tween.Kill();
        }

        public void ShowSpeed(Vector3 velocity)
        {
            var target = coreLocalStart - velocity * Height * 0.01f;
            core.localPosition = Vector3.SmoothDamp(core.localPosition, target, ref pilePosDamp, .2f);
        }
    }
}