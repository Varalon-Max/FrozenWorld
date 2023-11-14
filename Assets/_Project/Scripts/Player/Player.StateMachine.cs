using System;
using System.Collections.Generic;

namespace _Project.Scripts.Player
{
    public partial class Player
    {
        private IPlayerState _currentState;
        private readonly Dictionary<Type, IPlayerState> _states = new();

        public void SetState<T>() where T : IPlayerState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public IPlayerState GetState()
        {
            return _currentState;
        }

        private void InitializeStates()
        {
            _states.Add(typeof(CalmState), new CalmState());

            _states.Add(typeof(AcceleratedState),
                new AcceleratedState(accelerateTime, this));

            _states.Add(typeof(AccelerateOnCooldownState),
                new AccelerateOnCooldownState(acceleratedCooldown, this));
        }

        private void SetDefaultState()
        {
            _currentState = _states[typeof(CalmState)];
            _currentState.Enter();
        }

        private void SubscribeForEvents()
        {
            _states[typeof(AcceleratedState)].OnStart += OnAcceleratedStateEnter;
            _states[typeof(AcceleratedState)].OnExit += OnAccelerationStateExit;
            _states[typeof(AccelerateOnCooldownState)].OnUpdate += OnAccelerateOnCooldownUpdate;
        }

        private void OnAcceleratedStateEnter()
        {
            OnAccelerationStart?.Invoke();
        }

        private void OnAccelerationStateExit()
        {
            OnAccelerationEnd?.Invoke();
        }

        private void OnAccelerateOnCooldownUpdate()
        {
            OnAccelerationCooldownChanged?.Invoke(
                ((AccelerateOnCooldownState)_states[typeof(AccelerateOnCooldownState)]).CooldownCounter);
        }

        private void UnsubscribeFromEvents()
        {
            _states[typeof(AcceleratedState)].OnStart -= OnAcceleratedStateEnter;
            _states[typeof(AcceleratedState)].OnExit -= OnAccelerationStateExit;
            _states[typeof(AccelerateOnCooldownState)].OnUpdate -= OnAccelerateOnCooldownUpdate;
        }
    }
}