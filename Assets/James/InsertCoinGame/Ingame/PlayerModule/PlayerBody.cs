using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerBody : MonoBehaviour
    {
        private Player owner;


        public Player Owner { get { return owner; } }

        public void Inject(Player owner)
        {
            this.owner = owner;
        }
    }
}
