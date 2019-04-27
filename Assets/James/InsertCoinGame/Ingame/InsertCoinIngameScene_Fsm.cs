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
            SceneStartState.Factory sceneStartFactory;

            public Fsm(SceneStartState.Factory sceneStartFactory)
            {
                this.sceneStartFactory = sceneStartFactory;
            }

            public override State StartingState => sceneStartFactory.Create();
        }
        public abstract class State : State<State, InsertCoinIngameScene>
        {

        }

        public class SceneStartState : State
        {
            public const float minDelay = 1;

            //Dependencies
            private InsertCoinInput input;
            private InsertCoinUI ui;
            private IngameState.Factory ingameStateFactory;
            //State
            private bool startedTransition;
            private Stopwatch stopwatch;

            public SceneStartState(InsertCoinInput input, InsertCoinUI ui, IngameState.Factory ingameStateFactory)
            {
                this.input = input;
                this.ui = ui;
                this.ingameStateFactory = ingameStateFactory;
            }

            protected override void Begin()
            {
                stopwatch = Stopwatch.CreateAndStart();
            }
            protected override void Update()
            {
                if (!startedTransition && input.PressedStart && stopwatch.ElapsedSeconds > minDelay)
                {
                    StartTransition();
                }

            }

            private void StartTransition()
            {
                startedTransition = true;
                ui.HideText(GoToIngame);
            }

            private void GoToIngame()
            {
                ChangeState(ingameStateFactory.Create());
            }

            public class Factory :  PlaceholderFactory<SceneStartState>{ }
        }
        public class IngameState : State
        {
            protected override void Begin()
            {
                Debug.Log("Started Scene");
            }

            public class Factory : PlaceholderFactory<IngameState> { }
        }


        public static void BindAllStateFactories(DiContainer container)
        {
            container.Bind<Fsm>().AsTransient();
            container.BindFactory<SceneStartState, SceneStartState.Factory>();
            container.BindFactory<IngameState, IngameState.Factory>();
        }
    }
}
