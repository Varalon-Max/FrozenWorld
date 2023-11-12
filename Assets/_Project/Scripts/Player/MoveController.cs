using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField, Self] private GameInput gameInput;
        
        [SerializeField] private float forceApplied;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float movingSpeed;
        [SerializeField] private float acceleratedSpeed;
        [SerializeField] private float fallMultiplyer;
        [SerializeField] private float defaultGravityScale;
        [SerializeField, Self] private Rigidbody2D _playerRigidbody;
        
        

        private float _coyoteTime = 0.2f;
        private float _coyoteTimeCounter;
        private float _currentSpeed;
        private Vector2 _currentMoveDirection;
        private Player _player;
        
        private BoxCollider2D _boxCollider2D;
        // private Rigidbody2D _playerRigidbody;

        private void OnValidate()
        {
            this.ValidateRefs();
            defaultGravityScale = _playerRigidbody.gravityScale;
        }
        private void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            gameInput.OnJump += Jump;
            _player.OnAccelerationStart += Accelerate;
            _player.OnAccelerationEnd += UnAccelerate;
        }
        
        private void OnDisable()
        {
            gameInput.OnJump -= Jump;
            _player.OnAccelerationStart -= Accelerate;
            _player.OnAccelerationEnd -= UnAccelerate;

        }

        public void Jump()
        {
            if (IsGrounded() || _coyoteTimeCounter>0)
            {
                _playerRigidbody.AddForce(Vector2.up*forceApplied, ForceMode2D.Impulse);
                _coyoteTimeCounter = 0;
            }
        }

        private void Update()
        {
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
            if (_playerRigidbody.velocity.y < 0)
            {
                _playerRigidbody.velocity += Vector2.up * Physics.gravity.y * fallMultiplyer * Time.deltaTime;
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
                RaycastHit2D ray = Physics2D.BoxCast(_boxCollider2D.bounds.center, 
                    _boxCollider2D.bounds.size-new Vector3(0,0.1f,0),
                    0f, 
                    moveDirection,extraWight, groundLayer);
                
                return ray.collider != null;
            }
        }

        private void Accelerate()
        {
            if (_player.IsReadyToAccelerate())
            {
                _playerRigidbody.gravityScale = 0f;
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0f);
                _playerRigidbody.AddForce(_currentMoveDirection*acceleratedSpeed, ForceMode2D.Impulse);   
            }
        }

        private void UnAccelerate()
        {
            _playerRigidbody.gravityScale = defaultGravityScale;
            _playerRigidbody.velocity = Vector2.zero;
        }
        

        private bool IsGrounded()
        {
            float extraHeight = 0.1f;
            
            Bounds bounds = _boxCollider2D.bounds;
            RaycastHit2D ray = Physics2D.BoxCast(bounds.center, 
                bounds.size,
                0f, 
                Vector2.down,extraHeight, groundLayer);
           
            return ray.collider != null;
        }
    }
}