using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField, Self] private GameInput gameInput;
        
        [SerializeField] private float forceApplied;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float movingSpeed;
        
        private BoxCollider2D _boxCollider2D;
        private Rigidbody2D _playerRigidbody;

        private void OnValidate()
        {
            this.ValidateRefs();
        }
        private void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }
        
        private void OnEnable()
        {
            gameInput.OnJump += Jump;
        }
        
        private void OnDisable()
        {
            gameInput.OnJump -= Jump;
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                _playerRigidbody.AddForce(Vector2.up*forceApplied, ForceMode2D.Impulse);   
            }
        }

        private void Update()
        {
            Movement(gameInput.MoveDirection);
        }

        public void Movement(Vector2 moveDirection)
        {
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