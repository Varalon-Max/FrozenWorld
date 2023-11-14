using System;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public partial class Player: MonoBehaviour
    {
        public event Action OnAccelerationEnd;
        public event Action OnAccelerationStart;
        public event Action<float> OnAccelerationCooldownChanged;
        
        [SerializeField, Self] private GameInput gameInput;
        
        [SerializeField] private float accelerateTime;
        [SerializeField] private float acceleratedCooldown;
        
        private void Awake()
        {
            InitializeStates();
        }

        private void Start()
        {
            SetDefaultState();
        }
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Update()
        {
            _currentState.Update(Time.deltaTime);
        }

        private void OnEnable()
        {
            gameInput.OnAccelerate += SetAccelerator;
            SubscribeForEvents();
        }
        
        private void OnDisable()
        {
            gameInput.OnAccelerate -= SetAccelerator;
            UnsubscribeFromEvents();
        }

        private void SetAccelerator()
        {
            if (IsReadyToAccelerate())
            {
                SetState<AcceleratedState>();
            }
        }

        private bool IsReadyToAccelerate()
        {
            return _currentState is CalmState;
        }
    }
}