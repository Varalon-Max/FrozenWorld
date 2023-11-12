using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Animation
{
    public class CrossfadeAnimator: MonoBehaviour
    {
        [SerializeField] private MoveController _player;
        private Animator _anim;
        private SpriteRenderer _renderer;
        
        private bool _grounded;
        private float _lockedTill;
        private bool _jumpTriggered;
        private bool _landed;

        private void Awake() {
            _anim = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        }


        private void Start() {
            _player.Jumped += () => {
                _jumpTriggered = true;
            };
            _player.GroundedChanged += (grounded) => {
                _grounded = grounded;
            };
        }

        private void Update() {
            if (MoveController.Input.x != 0) _renderer.flipX = MoveController.Input.x < 0;

            var state = GetState();

            _jumpTriggered = false;
            _landed = false;

            if (state == _currentState) return;
            _anim.CrossFade(state, 0, 0);
            _currentState = state;
        }

        private int GetState() {
            if (Time.time < _lockedTill) return _currentState;

            // Priorities
            if (_jumpTriggered) return Jump;

            if (_grounded) return MoveController.Input.x == 0 ? Idle : Walk;
            return _player.Speed.y > 0 ? Jump : Fall;

            int LockState(int s, float t) {
                _lockedTill = Time.time + t;
                return s;
            }
        }

        #region Cached Properties

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Run");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Fall = Animator.StringToHash("Fall");

        #endregion
    }
}