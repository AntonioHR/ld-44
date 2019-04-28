using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.StateMachines;
using TonhoHR.Utils;
using UnityEngine;
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

            public Coroutine WaitForThenDo(float time, Action callback)
            {
                return Body.WaitThenDo(time, callback);
            }
            public Coroutine WaitUntilThenDo(Func<bool> check, Action callback)
            {
                return Body.WaitUntilThenDo(check, callback);
            }
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
                Body.ShowKickUi();
            }
            public override void OnKickUp()
            {
                Body.HideKickUi();
                Vector3 direction = Body.Movement.Direction;
                ChangeState(new KickState(Body.Movement.Direction));
            }
        }
        private class KickState : State
        {
            private Vector3 direction;

            public KickState(Vector3 direction)
            {
                this.direction = direction;
            }

            protected override void Begin()
            {
                Body.Movement.DirectionLocked = true;
                PerformKick();
                Body.PerformKickAnimation(OnKickAnimationFinished);
            }

            private void OnKickAnimationFinished()
            {
                ChangeState(new IdleState());
            }

            private void PerformKick()
            {
                var coins = Context.coinChecker.CurrentObjects;
                foreach (var coin in coins)
                {
                    coin.Kick(direction);
                }
            }

            protected override void End()
            {
                Body.Movement.DirectionLocked = false;
            }
        }
    }

}
