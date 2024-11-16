using System;

namespace StateMachines.States
{
    public abstract class State
    {
        public Action<State> OnStateExit;

        public State() { }

        public abstract void Enter();
        public abstract void Process();
        public abstract void Exit();

        public abstract override string ToString();
    }
}
