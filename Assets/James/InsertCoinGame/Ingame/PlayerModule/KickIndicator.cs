using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class KickIndicator : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private Image img;
        public void Hide()
        {
            canvasGroup.DOFade(0, .3f);
        }
        public void ShowKick(float t)
        {
            img.color = t >= 1 ? Color.red : Color.white;
            canvasGroup.alpha = t;

        }
    }
}
