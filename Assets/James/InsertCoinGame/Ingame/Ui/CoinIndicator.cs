using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace James.InsertCoinGame.Ingame.Ui
{
    class CoinIndicator : MonoBehaviour
    {

        [SerializeField]
        private Text creds;

        [Inject]
        public void Init(InsertCoinIngameScene scene)
        {
            scene.CoinCollect += UpdateCoins;
            UpdateCoins(scene.Coins);
        }

        private void UpdateCoins(int count)
        {
            creds.text = count.ToString("00");
        }
    }
}
