using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class WindController : MonoBehaviour
    {
        [SerializeField] private float _windAngle;
        [SerializeField] private float _windStrength = 1f;
        [SerializeField] private float _changeInterval = 10f;
        [SerializeField] private float _rotationSpeed = 45f;

        private float _timer;
        private float _targetAngle;

        private void Start()
        {
            _targetAngle = _windAngle;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _changeInterval)
            {
                _timer = 0f;
                _targetAngle = Random.Range(0f, 360f);
            }

            // плавный поворот к целевому углу
            _windAngle = Mathf.MoveTowardsAngle(
                _windAngle,
                _targetAngle,
                _rotationSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.LookRotation(Direction);
        }

        public Vector3 Direction =>
            Quaternion.Euler(0f, _windAngle, 0f) * Vector3.forward;

        public float Strength => _windStrength;
    }
}
