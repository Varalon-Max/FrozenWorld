using System;

namespace _Project.Scripts.Player
{
    public class CalmState : IPlayerState
    {
        public void Enter()
        {
            OnStart?.Invoke();
            // no-op
        }

        public void Update(float deltaTime)
        {
            OnUpdate?.Invoke();
            // no-op
        }

        public void Exit()
        {
            OnExit?.Invoke();
            // no-op
        }

        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnExit;
    }
}