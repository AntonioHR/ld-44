using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerBody : MonoBehaviour
    {
        private Player owner;

        [SerializeField]
        private PlayerMovement movement;


        public Player Owner { get { return owner; } }
        public PlayerMovement Movement { get { return movement; } }

        [Inject]
        public void Inject(Player owner)
        {
            this.owner = owner;
        }
    }
}
