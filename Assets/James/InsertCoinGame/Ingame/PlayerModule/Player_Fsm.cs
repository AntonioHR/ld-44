using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.StateMachines;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public partial class Player
    {
        public class Fsm : StateMachine<State, Player>
        {
            public override State StartingState => new IdleState();

            public void OnKickDown()
            {
                CurrentState.OnKickDown();
            }
            public void OnKickUp()
            {
                CurrentState.OnKickUp();
            }
        }
        public abstract class State : State<State, Player>
        {
            protected PlayerBody Body { get { return Context.body; } }
            public virtual void OnKickDown() { }
            public virtual void OnKickUp() { }
        }
        public class IdleState : State
        {
            protected override void Begin()
            {
                Body.Movement.EnableWalk();
            }
            public override void OnKickDown()
            {
                ChangeState(new KickPrepareState());
            }
        }
        public class KickPrepareState : State
        {
            protected override void Begin()
            {
                Body.Movement.DisableWalk();
            }
            public override void OnKickUp()
            {
                ChangeState(new IdleState());
            }
        }
    }

}
