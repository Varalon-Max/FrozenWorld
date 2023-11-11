using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts;
using _Project.Scripts.Enums;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class Player: MonoBehaviour
    {
        public event Action OnAccelerationEnd;
        public event Action OnAccelerationStart;
        
        [SerializeField, Self] private GameInput gameInput;
        
        [SerializeField] private float accelerateTime;
        [SerializeField] private float acceleratedCooldown;

        private float _accelerateTimeCounter;
        private float _accelerateCooldownCounter;
        private PlayerState _playerState;

        private void Start()
        {
            _playerState = new PlayerState();
            _playerState.SetState(State.Calm);
            _accelerateTimeCounter = accelerateTime;
            _accelerateCooldownCounter = acceleratedCooldown;
        }
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Update()
        {
            if (_playerState.GetState()==State.Accelerated)
            {
                _accelerateTimeCounter -= Time.deltaTime;
                EndAcceleration();
            }

            if (_playerState.GetState()==State.AccelerateOnCooldown)
            {
                _accelerateCooldownCounter -= Time.deltaTime;
                EndCooldown();
            }
        }

        private void OnEnable()
        {
            gameInput.OnAccelerate += SetAccelerator;
        }
        
        private void OnDisable()
        {
            gameInput.OnAccelerate -= SetAccelerator;
        }

        private void EndAcceleration()
        {
            if (_accelerateTimeCounter<=0)
            {
                _playerState.SetState(State.AccelerateOnCooldown);
                OnAccelerationEnd?.Invoke();
                _accelerateTimeCounter = accelerateTime;
            }
        }

        private void EndCooldown()
        {
            if (_accelerateCooldownCounter<=0)
            {
                _playerState.SetState(State.Calm);
                _accelerateCooldownCounter = acceleratedCooldown;
            }
        }

        private void SetAccelerator()
        {
            if (IsReadyToAccelerate())
            {
                OnAccelerationStart?.Invoke();
                _playerState.SetState(State.Accelerated);
            }
        }

        public bool IsReadyToAccelerate()
        {
            return _playerState.GetState() == State.Calm;
        }
    }
}