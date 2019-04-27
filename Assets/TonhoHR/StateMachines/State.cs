using System;

namespace TonhoHR.StateMachines
{
    public abstract class State<TState, TContext> : IState<TState, TContext>
        where TState : IState<TState, TContext>
    {
        private const string NotInitalizedMessage = "Tried to access a state's context before it was initialized. A state's context can only be accessed after it's OnBegin method was called";
        private IStateMachine<TState, TContext> owner;
        private TContext context;

        protected TContext Context
        {
            get
            {
                if (context == null)
                    throw new InvalidOperationException(NotInitalizedMessage);
                else
                    return context;
            }
        }

        protected virtual void Begin() { }
        protected virtual void End() { }
        protected virtual void Update() { }

        protected void ChangeState(TState nextState)
        {
            owner.ChangeStateTo(nextState);
        }

        #region IState impl
        void IState<TState, TContext>.Init(IStateMachine<TState, TContext> owner, TContext context)
        {
            this.owner = owner;
            this.context = context;
        }

        void IState<TState, TContext>.OnBegin()
        {
            Begin();
        }
        void IState<TState, TContext>.OnEnd()
        {
            End();
        }
        void IState<TState, TContext>.OnUpdate()
        {
            Update();
        }
        #endregion
    }

}