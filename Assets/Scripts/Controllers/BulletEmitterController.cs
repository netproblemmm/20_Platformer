using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class BulletEmitterController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;
        private int _currentIndex;
        private float _timeTillNextBullet;
        private const float _delay = 1;
        private const float _startSpeed = 9;

        private Vector3 Velocity;

        public BulletEmitterController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;
            foreach (LevelObjectView item in bulletViews)
            {
                _bullets.Add(new BulletController(item));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bullets[_currentIndex].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                Velocity = (_transform.up * - 1)*_startSpeed;
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, Velocity);
                _currentIndex++;
                if(_currentIndex >= _bullets.Count)
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}

