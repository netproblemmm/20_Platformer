using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed = 25;
        [SerializeField] private LevelObjectView _playerView;

        private SpriteAnimatorController _playerAnimator;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }
            if (_playerView)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            }

            _playerController = new PlayerController(_playerView, _playerAnimator);
        }

        private void Update()
        {
            _playerController.Update();
        }

        private void FixedUpdate()
        {
        
        }
    }
}

