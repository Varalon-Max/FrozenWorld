using _Project.Scripts.Enums;

namespace _Project.Scripts
{
    public class PlayerState
    {
        private State _currentState;
        public void SetState(State newState)
        {
            _currentState = newState;
        }

        public State GetState()
        {
            return _currentState;
        }
    }
}