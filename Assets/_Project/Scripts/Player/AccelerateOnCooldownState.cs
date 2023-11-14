using System;

namespace _Project.Scripts.Player
{
    public class AccelerateOnCooldownState : IPlayerState
    {
        private float _accelerateCooldownCounter;
        private float _acceleratedCooldown;
        private Player _context;

        public float CooldownCounter => 1 - _accelerateCooldownCounter / _acceleratedCooldown;
        
        public AccelerateOnCooldownState(float acceleratedCooldown, Player context)
        {
            _acceleratedCooldown = acceleratedCooldown;
            _context = context;
        }

        public void Enter()
        {
            _accelerateCooldownCounter = _acceleratedCooldown;
            OnStart?.Invoke();
        }

        public void Update(float deltaTime)
        {
            _accelerateCooldownCounter -= deltaTime;
            OnUpdate?.Invoke();

            if (_accelerateCooldownCounter <= 0)
            {
                _context.SetState<CalmState>();
            }
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }

        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnExit;
    }
}