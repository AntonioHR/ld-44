using James.InsertCoinGame.Ingame.Coins;
using James.InsertCoinGame.Ingame.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.ObjectCheckers;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public partial class Player
    {
        private PlayerBody body;
        private Fsm fsm;
        private PlayerConfigs configs;

        private CheckForObjects<Coin> coinChecker;
        public int CoinsCount { get { return coinChecker.CurrentObjects.Count(); } }

        public Player(PlayerBody body, Fsm fsm, PlayerConfigs configs)
        {
            this.body = body;
            this.fsm = fsm;
            this.configs = configs;
        }
        [Inject]
        private void Init()
        {
            coinChecker = new CheckForObjects<Coin>(body.KickArea);
            fsm.Begin(this);
        }

        public void OnKickDown()
        {
            fsm.OnKickDown();
        }
        public void OnKickUp()
        {
            fsm.OnKickUp();
        }

        public void Tick()
        {
            fsm.Update();
        }
    }

}
