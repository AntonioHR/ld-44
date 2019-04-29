using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.Utils;
using UnityEngine;

namespace Assets.TonhoHR.Utils
{
    class SelfDestruct : MonoBehaviour
    {
        public bool autoStart = true;
        public float time = 5;
        public void Start()
        {
            if (autoStart)
                StartCountdown();
        }

        private void StartCountdown()
        {
            Destroy(gameObject, time);
        }
    }
}
