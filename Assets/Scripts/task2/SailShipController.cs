using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class SailShipController : MonoBehaviour
    {
        [SerializeField] private Transform _sail;
        [SerializeField] private WindController _wind;

        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 5f;

        [SerializeField] private Rigidbody _rigidbody;

        private float _currentSpeed;

        private void FixedUpdate()
        {
            ApplyWindForce();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_sail.position, _sail.position + _sail.forward * 3f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _wind.Direction * 3f);
        }

        private void ApplyWindForce()
        {
            if (_wind == null || _sail == null)
                return;

            Vector3 windDirection = _wind.Direction.normalized;
            Vector3 shipDirection = transform.forward.normalized;
            Vector3 sailNormal = _sail.forward.normalized;

            float windToSail = Vector3.Dot(windDirection, sailNormal);
            float sailToShip = Vector3.Dot(sailNormal, shipDirection);

            if (windToSail <= 0 || sailToShip <= 0)
            {
                UpdateCurrentSpeed(0);
                ApplyVelocity(shipDirection);
                return;
            }

            float power = windToSail * sailToShip * _wind.Strength;

            UpdateCurrentSpeed(power);
            ApplyVelocity(shipDirection);
        }

        private void UpdateCurrentSpeed(float power)
        {
            float targetSpeed = power <= 0 ? 0 : _maxSpeed * power;

            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration * Time.fixedDeltaTime);
        }

        private void ApplyVelocity(Vector3 shipDirection)
        {
            Vector3 velocity = shipDirection * _currentSpeed;

            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
    }
}
