using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame
{
    public partial class InsertCoinIngameScene : MonoBehaviour
    {
        Fsm fsm;

        [Inject]
        public void Inject(Fsm fsm)
        {
            this.fsm = fsm;
        }

        public void Start()
        {
            fsm.Begin(this);
        }
        public void Update()
        {
            fsm.Update();
        }
    }
}
