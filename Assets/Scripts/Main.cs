using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;

        [SerializeField] private QuestView _questView;

        private SpriteAnimatorController _playerAnimator;

        private PlayerController _playerController;
        private CannonController _cannon;
        private BulletEmitterController _bulletEmitterController;
        private CameraController _cameraController;
        private QuestConfiguratorController _questConfigurator;

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }
            if (_playerView)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }

            _playerController = new PlayerController(_playerView, _playerAnimator);
            _cannon = new CannonController(_cannonView._muzzleTransform, _playerView.transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
            _cameraController = new CameraController(_playerView._transform, Camera.main.transform);
            _questConfigurator = new QuestConfiguratorController(_questView);
            _questConfigurator.Start();
        }

        private void Update()
        {
            _playerController.Update();
            _cannon.Update();
            _bulletEmitterController.Update();
            _cameraController.Update();
        }

        private void FixedUpdate()
        {
        
        }
    }
}

