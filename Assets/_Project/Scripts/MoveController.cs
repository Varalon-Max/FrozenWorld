using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float forceApplied;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float movingSpeed;
        private BoxCollider2D boxCollider2D;
        private Rigidbody2D playerRigidbody;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (IsGrounded())
            {
                playerRigidbody.AddForce(Vector2.up*forceApplied, ForceMode2D.Impulse);   
            }
        }

        public void Movement(Vector2 _moveDirection)
        {
            float extraWight = 0.1f;
            Vector2 moveDirection = _moveDirection;
            RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, 
                boxCollider2D.bounds.size-new Vector3(0,0.1f,0),
                0f, 
                _moveDirection,extraWight, groundLayer);
            if (ray.collider==null)
            {
                transform.position += new Vector3(moveDirection.x*movingSpeed, 0, 0)*Time.deltaTime; 
            }
        }

        private bool IsGrounded()
        {
            float extraHight = 0.1f;
            RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, 
                boxCollider2D.bounds.size,
                0f, 
                Vector2.down,extraHight, groundLayer);
           
            return ray.collider != null;
        }
    }
}