using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TonhoHR.Tweens
{
    public class ScalePunch : MonoBehaviour
    {
        public Vector3 punch = new Vector3(-.5f, -.5f, 0);
        public int vibrato = 4;
        public float elasticity = .3f;
        public float time = .3f;

        private bool startLoooped = false;

        Tween tween;

        private void Start()
        {
            if(startLoooped)
            {
                tween = TweenOnce().SetLoops(-1);
            }
        }

        public void DoOnce()
        {
            CompleteTweenIfActive();
            tween = TweenOnce();
        }

        private void CompleteTweenIfActive()
        {
            if (tween != null && tween.IsActive())
                tween.Complete();
        }

        private Tween TweenOnce()
        {
            return transform.DOPunchScale(punch, time, vibrato, elasticity);
        }
        private void OnDestroy()
        {
            if (tween != null && tween.IsActive())
                tween.Kill();
        }
    }
}
