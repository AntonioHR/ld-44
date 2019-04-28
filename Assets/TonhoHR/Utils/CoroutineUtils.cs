using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
        public enum SkipMode { FixedUpdate, ReturnNull}
        public static Coroutine WaitUntilThenDo(this MonoBehaviour me, Func<bool> check, Action callback, bool skipFirstFrame = false, SkipMode skipMode = SkipMode.ReturnNull)
        {
            return me.StartCoroutine(WaitUntilThenDo(check, callback, skipFirstFrame, skipMode));
        }
        public static IEnumerator WaitUntilThenDo(Func<bool> check, Action callback, bool skipFirstFrame, SkipMode skipMode)
        {
            if (skipFirstFrame)
            {
                switch (skipMode)
                {
                    case SkipMode.FixedUpdate:
                        yield return new WaitForFixedUpdate();
                        break;
                    case SkipMode.ReturnNull:
                        yield return null;
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitUntil(check);
            callback();
        }


        public static Coroutine AnimateImageOnce(this Image img, Sprite[] sprites, float timePerSprite, Action callback)
        {

            return img.StartCoroutine(img.DOAnimateImageOnce(sprites, timePerSprite, callback));
        }
        public static IEnumerator DOAnimateImageOnce(this Image img, Sprite[] sprites, float timePerSprite, Action callback)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                img.sprite = sprites[i];
                yield return new WaitForSeconds(timePerSprite);
            }
            if (callback != null)
                callback();
        }
    }
}
