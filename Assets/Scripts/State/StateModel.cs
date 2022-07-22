namespace State
{
    public class StateModel
    {
        public readonly int StateID;
        public readonly string StateName;

        public StateModel(int stateID, string stateName)
        {
            StateID = stateID;
            StateName = stateName;
        }

        /// <summary>
        /// event on game state change to this state
        /// override this for handle each state
        /// </summary>
        public virtual void OnStateIn(params object[] args) { }

        /// <summary>
        /// event on game state change from this state to next state
        /// override this for handle  each state
        /// </summary>
        public virtual void OnStateOut() { }
    }
}