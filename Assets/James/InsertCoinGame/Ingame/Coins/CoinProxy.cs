using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.ObjectCheckers;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    class CoinProxy : MonoBehaviour, IProxyFor<Coin>
    {
        [SerializeField]
        private Coin owner;

        public Coin Owner => owner;
    }
}
