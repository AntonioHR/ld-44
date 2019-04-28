using James.InsertCoinGame.Ingame.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerInput : MonoBehaviour
    {
        private InsertCoinInput input;
        private Player owner;

        [Inject]
        public void Inject(InsertCoinInput input, Player owner)
        {
            this.input = input;
            this.owner = owner;
        }

        private void Update()
        {
            if(input.KickDown)
            {
                owner.OnKickDown();
            }
            if(input.KickUp)
            {
                owner.OnKickUp();
            }
        }
    }
}
