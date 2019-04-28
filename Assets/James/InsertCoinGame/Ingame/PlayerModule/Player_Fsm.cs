using AntonioHR.Utils;
using James.InsertCoinGame.Ingame.Coins;
using James.InsertCoinGame.Ingame.InputModule;
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
            protected PlayerConfigs Configs { get { return Context.configs; } }
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
            Stopwatch stopwatch;
            protected override void Begin()
            {
                stopwatch = Stopwatch.CreateAndStart();
                Body.Movement.DisableWalk();
            }
            public override void OnKickUp()
            {
                Body.KickIndicator.Hide();
                Vector3 direction = Body.Movement.LookDirection;
                ChangeState(new KickState(Body.Movement.LookDirection, GetKickForce()));
            }
            protected override void Update()
            {
                Body.KickIndicator.ShowKick(GetKickForce());
            }
            private float GetKickForce()
            {
                float lerp = Mathf.InverseLerp(0, Configs.KickLoadTime, stopwatch.ElapsedSeconds);
                lerp = Configs.kickCurve.Evaluate(lerp);
                return Mathf.Lerp(Configs.KickMin, 1, lerp);
            }
        }
        private class KickState : State
        {
            private float forceAlpha;
            private Vector3 direction;

            public KickState(Vector3 direction, float force)
            {
                this.direction = direction;
                this.forceAlpha = force;
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
                var coins = Context.coinChecker.CurrentObjects.Where(c => c != null);
                foreach (var coin in coins.Where(c=>CanHit(c)))
                {
                    coin.Kick(direction, forceAlpha);
                }
                
            }

            private bool CanHit(Coin c)
            {
                var kickPos = Body.KickArea.transform.position;
                var delta = c.transform.position - kickPos;
                if(Physics.Raycast(kickPos,delta, out RaycastHit hit))
                {

                    bool result = hit.collider.GetComponentInParent<Coin>() == c;
                    if (result)
                        Debug.LogWarningFormat(hit.collider.gameObject, "Couldn't kick coin. Hit {0} instead", hit.collider);
                    return result;
                }
                return false;
            }

            protected override void End()
            {
                Body.Movement.DirectionLocked = false;
            }
        }
    }

}
