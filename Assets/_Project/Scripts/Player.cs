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
        public static Player player { get; private set; }

        public Action OnAccelerationEnd;
        
        [SerializeField, Self] private GameInput gameInput;
        
        [SerializeField] private float accelerateTime;

        private float _accelerateTimeCounter;
        private PlayerState _playerState;

        private void Awake()
        {
            player = this;
        }

        private void Start()
        {
            _playerState = new PlayerState();
            _playerState.SetState(State.Calm);
            _accelerateTimeCounter = accelerateTime;
        }
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Update()
        {
            Debug.Log(_playerState.GetState());
            if (_playerState.GetState()==State.Accelerated)
            {
                _accelerateTimeCounter -= Time.deltaTime;
            }

            if (_accelerateTimeCounter<=0)
            {
                _playerState.SetState(State.Calm);
                OnAccelerationEnd?.Invoke();
                _accelerateTimeCounter = accelerateTime;
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

        private void SetAccelerator()
        {
            _playerState.SetState(State.Accelerated);
        }

        public State GetPlayerState()
        {
            return _playerState.GetState();
        }
        
    }
}