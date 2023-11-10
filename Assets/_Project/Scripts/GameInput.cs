using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameInput: MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        private MoveController moveController;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            moveController = GetComponent<MoveController>();
            playerInputActions.Player.Jump.performed += moveController.Jump;
        }

        private void Update()
        {
            moveController.Movement(playerInputActions.Player.Move.ReadValue<Vector2>());
        }
    }
}