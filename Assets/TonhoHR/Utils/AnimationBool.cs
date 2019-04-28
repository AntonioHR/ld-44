using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TonhoHR.Utils
{
    class AnimationBool : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private string id;

        public void SetToTrue()
        {
            animator.SetBool(id, true);
        }
        public void SetToFalse()
        {
            animator.SetBool(id, false);
        }
    }
}
