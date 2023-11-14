using System;

namespace _Project.Scripts.Player
{
    public class AcceleratedState : IPlayerState
    {
        private float _accelerateTimeCounter;
        private float _accelerateTime;
        private Player _context;

        public AcceleratedState(float accelerateTime, Player context)
        {
            _accelerateTime = accelerateTime;
            _context = context;
        }

        public void Enter()
        {
            _accelerateTimeCounter = _accelerateTime;
            OnStart?.Invoke();
        }

        public void Update(float deltaTime)
        {
            _accelerateTimeCounter -= deltaTime;
            
            if (_accelerateTimeCounter<=0)
            {
                _context.SetState<AccelerateOnCooldownState>();
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