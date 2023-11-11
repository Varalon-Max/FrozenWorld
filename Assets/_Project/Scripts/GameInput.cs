using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public class GameInput: MonoBehaviour
    {
        public event Action OnJump;
        public event Action OnAccelerate;
        public Vector2 MoveDirection { get; private set; }
        
        private PlayerInputActions _playerInputActions;

        private void OnEnable()
        {
            _playerInputActions.Enable();
            _playerInputActions.Player.Jump.performed += Jump;
            _playerInputActions.Player.FireAccelerate.performed += Accelerate;
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            OnJump?.Invoke();
        }

        private void Accelerate(InputAction.CallbackContext obj)
        {
            OnAccelerate?.Invoke();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
            _playerInputActions.Player.Jump.performed -= Jump;
            _playerInputActions.Player.FireAccelerate.performed -= Accelerate;
        }
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        private void Update()
        {
            MoveDirection = _playerInputActions.Player.Move.ReadValue<Vector2>();
        }
    }
}