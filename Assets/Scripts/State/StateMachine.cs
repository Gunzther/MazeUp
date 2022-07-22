using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateMachine
    {
        public delegate void StateChangedHandler(
            StateModel oldState,
            StateModel newState);

        public event StateChangedHandler StateChanged;
        public StateModel CurrentState { get; private set; }

        protected Dictionary<int, StateModel> m_States;

        private bool m_IsInitialized = false;

        protected void Init(int starterState, StateModel[] gameStateModels)
        {
            if (!m_IsInitialized)
            {
                StateChanged += OnStateChanged;
                InitStates(gameStateModels);
                CurrentState = m_States[starterState];
                m_IsInitialized = true;
            }
        }

        public void GoToState(int stateID, params object[] args)
        {
            if (m_States.TryGetValue(stateID, out StateModel nextState))
            {
                SetState(nextState);
            }
            else
            {
                Debug.LogError($"[{nameof(StateMachine)}] Cannot change state from {CurrentState.StateName} to {nextState.StateName} state");
            }
        }

        public void Clear()
        {
            StateChanged -= OnStateChanged;
            m_States.Clear();
            m_IsInitialized = false;
        }

        private void InitStates(StateModel[] gameStateModels)
        {
            m_States ??= new Dictionary<int, StateModel>();

            foreach (StateModel state in gameStateModels)
            {
                m_States.Add(state.StateID, state);
            }
        }

        private void SetState(StateModel newState)
        {
            StateModel oldState = CurrentState;
            CurrentState = newState;
            StateChanged?.Invoke(oldState, CurrentState);
        }

        private static void OnStateChanged(
            StateModel oldStateModel,
            StateModel newStateModel)
        {
            oldStateModel?.OnStateOut();
            newStateModel?.OnStateIn();
        }
    }
}