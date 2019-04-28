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
        private Image bar;
        [SerializeField]
        private Image core;

        Vector3 color;
        Vector3 colorDampVel;

        bool hidden = true;
        public void Hide()
        {
            hidden = true;
            canvasGroup.DOFade(0, .3f);
            color = Vector3.one;
        }
        public void ShowKick(float t)
        {
            if (hidden)
                canvasGroup.DOFade(1, .3f);
            hidden = false;

            bar.fillAmount = t;
            if(t > .99f)
            {
                DampIndicatorColorTo(Vector3.right);
            } else
            {
                DampIndicatorColorTo(Vector3.one);
            }

        }

        private void DampIndicatorColorTo(Vector3 target)
        {
            color = Vector3.SmoothDamp(color, target, ref colorDampVel, .3f);
            core.color = new Color(color.x, color.y, color.z);
        }
    }
}
