using James.InsertCoinGame.Ingame.Coins;
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

        private CheckForObjects<Coin> coinChecker;
        public int CoinsCount { get { return coinChecker.CurrentObjects.Count(); } }

        public Player(PlayerBody body, Fsm fsm)
        {
            this.body = body;
            this.fsm = fsm;
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
    }

}
