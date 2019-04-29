using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.TonhoHR.Utils
{
    class DelayedTrigger : MonoBehaviour
    {
        public UnityEvent ev;
        public bool autoStart = true;
        public float time = 5;
        public void Start()
        {
            if (autoStart)
                StartCountdown();
        }

        private void StartCountdown()
        {
            this.WaitThenDo(time, ev.Invoke);
        }
    }
}
