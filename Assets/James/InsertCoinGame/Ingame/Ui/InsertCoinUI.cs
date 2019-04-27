using DG.Tweening;
using System;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Ui
{
    public class InsertCoinUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        public void HideText(TweenCallback callback)
        {
            canvasGroup.DOFade(0, .3f).OnComplete(callback);
        }
    }
}