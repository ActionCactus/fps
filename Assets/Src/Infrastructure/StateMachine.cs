using System.Collections.Generic;


namespace FPS.Infrastructure
{
    // A directed cyclic graph.  Observable.
    class StateMachine
    {
        public string Identity;

        public StateMachine()
        {

        }
    }

    // Extension of StateMachine, but inputs come from state transitions of other state machines registered internally.
    class CombinatorialStateMachine
    {

    }

    class State
    {
        protected string id { get; }
        protected List<State> references;

        public State()
        {
            this.references = new List<State>();
        }

        public State AcyclicRef(State state)
        {
            this.references.Add(state);
            return this;
        }

        public State CyclicRef(State state)
        {
            this.AcyclicRef(state.AcyclicRef(this));

            return this;
        }
    }

    class State<T> : State
    {
        protected new T id { get; }
    }
}