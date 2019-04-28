using DG.Tweening;
using System;
using TonhoHR.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace James.InsertCoinGame.Ingame.Ui
{
    public class InsertCoinUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup startCanvasGroup;
        [SerializeField]
        private CanvasGroup endCanvasGroup;
        [SerializeField]
        private CanvasGroup dialogueGroup;
        [SerializeField]
        private Text dialogueText;
        [SerializeField]
        private Text titleDont;
        [SerializeField]
        private Text titleInsert;
        [TextArea]
        [SerializeField]
        private string[] startText;
        [SerializeField]
        private Image coinImg;
        [SerializeField]
        private Sprite[] coinSprites;
        private Tweener titleTween;


        private void Awake()
        {
            startCanvasGroup.alpha = 1;
            titleTween = FlashText().SetLoops(-1, LoopType.Yoyo);
        }

        private Tweener FlashText(float period = .5f)
        {
            return titleInsert.DOFade(0, period).SetEase((t, d, o, a) => (t / d) > .5f ? 1 : 0).SetLoops(2, LoopType.Yoyo);
        }

        public void PerformStartSequence(TweenCallback callback)
        {
            titleTween.Complete();
            titleTween.Kill();
            var c = titleInsert.color;
            c.a = 1;
            titleInsert.color = c;

            var seq = DOTween.Sequence();
            seq.Append(FlashText(.05f));
            seq.Append(FlashText(.05f));
            seq.Append(FlashText(.05f));
            seq.AppendInterval(.5f);
            seq.Append(dialogueGroup.DOFade(1, .05f));
            bool inLoop = false;
            foreach (var str in startText)
            {
                if (inLoop)
                    seq.AppendInterval(1f);
                inLoop = true;
                seq.AppendCallback(() => dialogueText.text = "");
                seq.Append(dialogueText.DOText(str, 2));
            }
            seq.AppendInterval(1f);
            seq.Append(titleDont.DOText("Don't", .5f));
            seq.AppendCallback(() =>
            {
                coinImg.enabled = true;
                coinImg.AnimateImageOnce(coinSprites, .2f, () =>
                {
                    startCanvasGroup.DOFade(0, .5f).SetDelay(.7f).OnComplete(callback);
                });
            });
        }

        public void RunLoseSequence(TweenCallback reset)
        {
            endCanvasGroup.DOFade(1, .5f).OnComplete(reset);
        }
    }
}