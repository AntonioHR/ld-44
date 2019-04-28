using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Coins
{
    [CreateAssetMenu(menuName ="InsertCoin/CoinData")]
    class CoinData : ScriptableObject
    {
        public Coin coinPrefab;
    }
}
