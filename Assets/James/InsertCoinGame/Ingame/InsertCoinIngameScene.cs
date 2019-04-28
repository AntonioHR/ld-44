using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using James.InsertCoinGame.Ingame.PlayerModule;
using James.InsertCoinGame.Ingame.InputModule;
using James.InsertCoinGame.Ingame.Ui;
using UnityEngine.Events;

namespace James.InsertCoinGame.Ingame
{
    public partial class InsertCoinIngameScene : MonoBehaviour
    {
        public event Action<int> CoinCollect;
        public event Action GameStarted;

        Fsm fsm;
        Player player;
        private int coins;
        [Inject]
        private InsertCoinInput input;
        [Inject]
        private InsertCoinUI ui;
        [SerializeField]
        private UnityEvent GameStartedEvent;

        public bool HasGameStarted { get; private set; }

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

        public void CollectCoin()
        {
            coins++;
            if (CoinCollect != null)
                CoinCollect(coins);

            fsm.OnCoinCollected();
        }

        private void OnGameStarted()
        {
            HasGameStarted = true;
            GameStartedEvent.Invoke();
            if (GameStarted != null)
                GameStarted();
        }
    }
}
