using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public partial class Player
    {
        private PlayerBody body;
        private Fsm fsm;

        public Player(PlayerBody body, Fsm fsm)
        {
            this.body = body;
            this.fsm = fsm;
        }
        [Inject]
        private void Init()
        {
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
