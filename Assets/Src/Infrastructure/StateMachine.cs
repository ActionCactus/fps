using System.Collections.Generic;
using System;


namespace FPS.Infrastructure
{
    // A directed cyclic graph.  Observable.
    public class StateMachine
    {
        public string Identity;

        public StateMachine()
        {

        }
    }

    // Extension of StateMachine, but inputs come from state transitions of other state machines registered internally.
    public class CombinatorialStateMachine
    {

    }

    public class State
    {
        public List<State> References { get => references; }
        protected Enum id;
        protected List<State> references;

        public State(Enum id)
        {
            this.id = id;
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
}