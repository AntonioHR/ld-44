using AntonioHR.Utils;
using James.InsertCoinGame.Ingame.InputModule;
using James.InsertCoinGame.Ingame.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonhoHR.StateMachines;
using TonhoHR.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace James.InsertCoinGame.Ingame
{
    public partial class InsertCoinIngameScene
    {

        public class Fsm : StateMachine<State, InsertCoinIngameScene>
        {
            public override State StartingState => new SceneStartState();

            public void OnCoinCollected()
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
            public override void OnCoinCollected()
            {
                if(Context.Coins >= Context.maxCoins)
                {
                    ChangeState(new YouLoseState());
                }
            }
        }


        private class YouLoseState : State
        {
            protected override void Begin()
            {
                Context.ui.RunLoseSequence(Reset);
            }
            private void Reset()
            {
                Context.WaitUntilThenDo(()=>Context.input.PressedStart, ()=>SceneManager.LoadScene(0));

            }
        }

        public static void BindAllStateFactories(DiContainer container)
        {
            container.Bind<Fsm>().AsTransient();
        }
    }
}
