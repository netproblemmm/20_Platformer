using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 8f;
        private float _animationSpeed = 10f;
        private float _movingThreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector3 _vectorRight = new Vector3(1, 0, 0);

        private bool isMoving;

        private float _jumpForce = 9f;
        private float _jumpThreshold = 1f;
        private float _g = -9.8f;
        private float _groundLevel = -1.8f;
        private float _yVelocity = 0f;
        private float _xVelocity = 0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;
        // pooler

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        private void MoveTowards()
        {
            _view.transform.position += _vectorRight * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1: 1));
            _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view.transform.position.y <= _groundLevel && _yVelocity <= 0;
        }

        public void Update()
        {
            _spriteAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;

            if (isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer , isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if(_isJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpForce;
                }
                else if(_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view._transform.position = _view._transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _view._transform.position += Vector3.up * (_yVelocity * Time.deltaTime);
            }
        }
    }
}

