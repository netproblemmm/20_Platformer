using UnityEngine;

namespace PlatformerMVC
{
    public class CameraController
    {
        private float X;
        private float Y;

        private float offsetX = 1.5f;
        private float offsetY = 1.5f;

        private float camSpeed = 270;
        private Transform _playerTransform;
        private Transform _mCamTransform;

        public CameraController(Transform _player, Transform _camera)
        {
            _playerTransform = _player;
            _mCamTransform = _camera;
        }

        public void Update()
        {
            X = _playerTransform.position.x;
            Y = _playerTransform.position.y;

            _mCamTransform.position = Vector3.Lerp(_mCamTransform.position,
                                        new Vector3(X + offsetX, Y + offsetY, _mCamTransform.position.z),
                                        Time.deltaTime * camSpeed);
        }
    }
}

