using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using James.InsertCoinGame.Ingame.PlayerModule;

namespace James.InsertCoinGame.Ingame
{
    public partial class InsertCoinIngameScene : MonoBehaviour
    {
        Fsm fsm;
        Player player;

        [Inject]
        public void Inject(Fsm fsm, Player player)
        {
            this.fsm = fsm;
            this.player = player;
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
