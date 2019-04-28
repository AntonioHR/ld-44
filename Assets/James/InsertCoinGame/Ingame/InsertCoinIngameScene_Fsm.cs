using AntonioHR.Utils;
using James.InsertCoinGame.Ingame.InputModule;
using James.InsertCoinGame.Ingame.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.StateMachines;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame
{
    public partial class InsertCoinIngameScene
    {
        public class Fsm : StateMachine<State, InsertCoinIngameScene>
        {
            public override State StartingState => new SceneStartState();

            internal void OnCoinCollected()
            {
                CurrentState.OnCoinCollected();
            }
        }
        public abstract class State : State<State, InsertCoinIngameScene>
        {
            public virtual void OnCoinCollected()
            {
            }
        }

        public class SceneStartState : State
        {
            public const float minDelay = 1;

            //State
            private bool startedTransition;
            private Stopwatch stopwatch;

            protected override void Begin()
            {
                stopwatch = Stopwatch.CreateAndStart();
            }
            protected override void Update()
            {
                if (!startedTransition && Context.input.PressedStart && stopwatch.ElapsedSeconds > minDelay)
                {
                    StartTransition();
                }
            }

            private void StartTransition()
            {
                startedTransition = true;
                Context.ui.PerformStartSequence(GoToIngame);
            }

            private void GoToIngame()
            {
                Context.player.OnGameStarted();
                Context.OnGameStarted();
                ChangeState(new IngameState());
            }
        }

        public class IngameState : State
        {
            protected override void Begin()
            {
                Debug.Log("Started Scene");
            }
        }


        public static void BindAllStateFactories(DiContainer container)
        {
            container.Bind<Fsm>().AsTransient();
        }
    }
}
