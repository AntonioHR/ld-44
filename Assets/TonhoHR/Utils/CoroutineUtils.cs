using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TonhoHR.Utils
{
    public static class CoroutineUtils
    {
        public static Coroutine WaitThenDo(this MonoBehaviour me, float time, Action callback)
        {
            return me.StartCoroutine(WaitThenDo(time, callback));
        }
        public static IEnumerator WaitThenDo(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }
        public static Coroutine WaitUntilThenDo(this MonoBehaviour me, Func<bool> check, Action callback)
        {
            return me.StartCoroutine(WaitUntilThenDo(check, callback));
        }
        public static IEnumerator WaitUntilThenDo(Func<bool> check, Action callback)
        {
            yield return new WaitUntil(check);
            callback();
        }
    }
}
