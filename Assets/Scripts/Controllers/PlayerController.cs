using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 100f;
        private float _animationSpeed = 10f;
        private float _movingThreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector3 _vectorRight = new Vector3(1, 0, 0);

        private bool isMoving;

        private float _jumpForce = 9f;
        private float _jumpThreshold = 1f;
        private float _yVelocity = 0f;
        private float _xVelocity = 0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;
        private readonly ContactPoller _contactPoller; 

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Idle, true, _animationSpeed);
            _contactPoller = new ContactPoller(_view._collider);
        }

        private void MoveTowards()
        {
            _xVelocity = _walkSpeed * Time.fixedDeltaTime * (_xAxisInput < 0 ? -1 : 1);
            _view._rigidbody.velocity = _view._rigidbody.velocity.Change(x: _xVelocity);
            _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public void Update()
        {
            _spriteAnimator.Update();
            _contactPoller.Update();

            _yVelocity = _view._rigidbody.velocity.y;
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;

            if (isMoving)
            {
                MoveTowards();
            }

            if (_contactPoller.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer, isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if(_isJump && Mathf.Abs(_yVelocity) <= _jumpThreshold)
                {
                    _view._rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Jump, false, _animationSpeed);
                }
            }
        }
    }
}

