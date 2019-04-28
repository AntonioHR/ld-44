using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.Spawners
{
    [CreateAssetMenu(fileName ="CoinSpawnPreset", menuName ="InsertCoin/CoinSpawnPreset")]
    public class CoinSpawnerSetup : ScriptableObject
    {
        public float interval = 1;
        public Vector3 spawnVector = new Vector3(0, 0, 1);
        public float startingKick = 2;
        public float arc = 90;
    }
}
