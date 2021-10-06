using UnityEngine;

namespace PlatformerMVC
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;

        public BulletController (LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(true);
            _view._transform.position = position;
            _view._rigidbody.velocity = Vector2.zero;
            _view._rigidbody.angularVelocity = 0;
            _view._rigidbody.AddForce(velocity, ForceMode2D.Impulse);

        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }
    }
}

