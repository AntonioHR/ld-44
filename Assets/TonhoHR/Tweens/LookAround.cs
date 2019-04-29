using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.Utils;
using UnityEngine;

namespace Assets.TonhoHR.Tweens
{
    public class LookAround :MonoBehaviour
    {

        public Vector3 rotation = new Vector3(0, 15, 0);
        public float halfTime = .5f;
        public float interval = .5f;
        public float minDelay = 1;
        public float maxDelay = 2;
        public bool autoStart = false;

        private void Start()
        {
            if (autoStart)
                StartRandomizedLoop();
        }

        public void StartRandomizedLoop()
        {
            LookAndLoopThenWait();
        }

        private void WaitThenLookAndLoop()
        {
            this.WaitThenDo(UnityEngine.Random.Range(minDelay, maxDelay), LookAndLoopThenWait);
        }
        private void LookAndLoopThenWait()
        {
            DoLookOnce().OnComplete(WaitThenLookAndLoop);
        }

        public Tween DoLookOnce()
        {
            var seq = DOTween.Sequence();

            seq.Append(transform.DORotate(rotation, halfTime).SetRelative()).SetEase(Ease.Linear);
            seq.AppendInterval(interval);
            seq.Append(transform.DORotate(-rotation, halfTime).SetRelative()).SetEase(Ease.Linear);
            seq.AppendInterval(interval);
            seq.Append(transform.DORotate(-rotation, halfTime).SetRelative()).SetEase(Ease.Linear);
            seq.AppendInterval(interval);
            seq.Append(transform.DORotate(rotation, halfTime).SetRelative()).SetEase(Ease.Linear);

            return seq;
        }
    }
}
