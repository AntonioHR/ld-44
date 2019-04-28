
using System;

namespace TonhoHR.StateMachines
{
    public interface IStateMachine<TState, TContext>
        where TState : IState<TState, TContext>
    {
        void ChangeStateTo(TState state);
        void Update();
    }
    public interface IState<TState, TContext>
        where TState : IState<TState, TContext>
    {
        void Init(IStateMachine<TState, TContext> owner, TContext context);
        void OnBegin();
        void OnEnd();
        void OnUpdate();
    }
    public abstract class StateMachine<TState, TContext> :
        IStateMachine<TState, TContext>
        where TState : IState<TState, TContext>
    {

        protected TContext Context { get; private set; }
        protected TState CurrentState { get; private set; }


        public void Begin(TContext context)
        {
            Context = context;
            MoveInto(StartingState);
        }

        public abstract TState StartingState { get; }


        private void MoveOutOf(TState oldState)
        {
            oldState.OnEnd();
        }
        private void MoveInto(TState newState)
        {
            CurrentState = newState;
            newState.Init(this, Context);
            newState.OnBegin();
        }

        #region  IStateMachine Implementation
        void IStateMachine<TState, TContext>.ChangeStateTo(TState newState)
        {
            var oldState = CurrentState;
            MoveOutOf(oldState);
            MoveInto(newState);
        }
        public void Update()
        {
            CurrentState.OnUpdate();
        }
        #endregion
    }
}
