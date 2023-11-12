using System;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class MoveController : MonoBehaviour, IPlayerController
    {
        public static Vector2 Input { get; private set; }
        public Vector2 Speed { get; private set; }
        public static bool OnGround { get; private set; }
        public event Action<bool> GroundedChanged;
        public event Action Jumped;
        
        [SerializeField] private float forceApplied;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float movingSpeed;
        [SerializeField] private float acceleratedSpeed;
        [SerializeField] private float fallMultiplyer;
        [SerializeField] private float defaultGravityScale;
        [SerializeField, Self] private Rigidbody2D playerRigidbody;
        [SerializeField, Self] private Player player;
        [SerializeField, Self] private BoxCollider2D boxCollider2D;
        [SerializeField, Self] private GameInput gameInput;

        private float _coyoteTime = 0.2f;
        private float _coyoteTimeCounter;
        private float _currentSpeed;
        private Vector2 _currentMoveDirection;

        public static event Action OnAnyJumpStarted;
        // public static event Action OnAnyJump;
        
        private void OnValidate()
        {
            this.ValidateRefs();
            defaultGravityScale = playerRigidbody.gravityScale;
        }

        private void OnEnable()
        {
            gameInput.OnJump += Jump;
            player.OnAccelerationStart += Accelerate;
            player.OnAccelerationEnd += UnAccelerate;
        }
        
        private void OnDisable()
        {
            gameInput.OnJump -= Jump;
            player.OnAccelerationStart -= Accelerate;
            player.OnAccelerationEnd -= UnAccelerate;
        }

        public void Jump()
        {
            if (IsGrounded() || _coyoteTimeCounter>0)
            {
                Jumped?.Invoke();
                OnAnyJumpStarted?.Invoke();
                playerRigidbody.AddForce(Vector2.up*forceApplied, ForceMode2D.Impulse);
                _coyoteTimeCounter = 0;
            }
        }

        private void Update()
        {
            OnGround = IsGrounded();
            Input = gameInput.MoveDirection;
            Speed = playerRigidbody.velocity;
            if (IsGrounded())
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
            Movement(gameInput.MoveDirection);
        }
        
        private void FixedUpdate()
        {
            if (playerRigidbody.velocity.y < 0)
            {
                playerRigidbody.velocity += Vector2.up * (Physics.gravity.y * fallMultiplyer * Time.deltaTime);
            }
        }

        public void Movement(Vector2 moveDirection)
        {
           _currentMoveDirection = moveDirection;
            float extraWight = 0.1f;
            
            if (!CheckHitsWall())
            {
                transform.position += new Vector3(moveDirection.x*movingSpeed, 0, 0)*Time.deltaTime; 
            }

            bool CheckHitsWall()
            {
                RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, 
                    boxCollider2D.bounds.size-new Vector3(0,0.1f,0),
                    0f, 
                    moveDirection,extraWight, groundLayer);
                
                return ray.collider != null;
            }
        }

        private void Accelerate()
        {
            if (player.IsReadyToAccelerate())
            {
                playerRigidbody.gravityScale = 0f;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(_currentMoveDirection*acceleratedSpeed, ForceMode2D.Impulse);   
            }
        }

        private void UnAccelerate()
        {
            playerRigidbody.gravityScale = defaultGravityScale;
            playerRigidbody.velocity = Vector2.zero;
        }
        

        private bool IsGrounded()
        {
            float extraHeight = 0.1f;
            
            Bounds bounds = boxCollider2D.bounds;
            RaycastHit2D ray = Physics2D.BoxCast(bounds.center, 
                bounds.size,
                0f, 
                Vector2.down,extraHeight, groundLayer);
            GroundedChanged?.Invoke(ray.collider != null);
            return ray.collider != null;
        }
    }
}