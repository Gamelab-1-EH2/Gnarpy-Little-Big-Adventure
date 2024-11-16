using StateMachines.States;

namespace StateMachines
{
    public class StateMachine
    {
        protected State _currentState;

        public StateMachine(State initialState)
        {
            _currentState = initialState;
            _currentState.OnStateExit += ChangeState;
            _currentState.Enter();
        }

        public virtual void Process() => _currentState.Process();

        protected virtual void ChangeState(State state)
        {
            //Exit last state
            _currentState.Exit();
            _currentState.OnStateExit -= ChangeState;

            //Update State
            _currentState = state;
            _currentState.OnStateExit += ChangeState;
            _currentState.Enter();
        }
        
    }
}
