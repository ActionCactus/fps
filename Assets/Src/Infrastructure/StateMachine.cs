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
        public Dictionary<Enum, State> References { get => references; }
        protected Enum id;
        protected Dictionary<Enum, State> references;

        public State(Enum id)
        {
            this.id = id;
            this.references = new Dictionary<Enum, State>();
        }

        public State AcyclicRef(State state)
        {
            if (this.references.ContainsKey(state.id))
            {
                throw new Exception($"State with ID {state.id} already added as a reference to {this.id}");
            }
            this.References[state.id] = state;

            return this;
        }

        public State CyclicRef(State state)
        {
            this.AcyclicRef(state.AcyclicRef(this));

            return this;
        }
    }
}