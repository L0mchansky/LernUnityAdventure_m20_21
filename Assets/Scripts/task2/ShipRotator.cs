using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class ShipRotator : MonoBehaviour
    {
        [SerializeField] private Transform _sail;

        [SerializeField] private float _rotationSpeed = 60f;
        [SerializeField] private float _sailRotationSpeed = 90f;
        [SerializeField] private float _maxSailAngle = 45f;

        private float _currentSailAngle;

        private void Update()
        {
            RotateShip();
            RotateSail();
        }

        private void RotateShip()
        {
            float input = GetShipRotationInput();

            transform.Rotate(Vector3.up, input * _rotationSpeed * Time.deltaTime);
        }

        private void RotateSail()
        {
            float input = GetSailRotationInput();

            _currentSailAngle += input * _sailRotationSpeed * Time.deltaTime;
            _currentSailAngle = Mathf.Clamp(_currentSailAngle, -_maxSailAngle, _maxSailAngle);

            _sail.localRotation = Quaternion.Euler(0, _currentSailAngle, 0);
        }

        private float GetShipRotationInput()
        {
            float input = 0;

            if (Input.GetKey(KeyCode.Q))
                input = -1f;

            if (Input.GetKey(KeyCode.W))
                input = 1f;

            return input;
        }

        private float GetSailRotationInput()
        {
            float input = 0;

            if (Input.GetKey(KeyCode.A))
                input = -1f;

            if (Input.GetKey(KeyCode.S))
                input = 1f;

            return input;
        }
    }
}
