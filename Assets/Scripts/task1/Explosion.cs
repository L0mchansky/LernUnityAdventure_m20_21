using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _force = 50f;
        [SerializeField] private float _upwardsModifier = 1f;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private GameObject _explosionEffectPrefab;

        private const int RightButtonClick = 1;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(RightButtonClick))
            {
                Explode();
            }
        }

        private void Explode()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
                return;

            if (_explosionEffectPrefab != null)
                Instantiate(_explosionEffectPrefab, hit.point, Quaternion.identity);

            Collider[] targets = Physics.OverlapSphere(hit.point, _radius);

            foreach (Collider target in targets)
            {
                if (!target.TryGetComponent(out Rigidbody rigidbody))
                    continue;

                rigidbody.AddExplosionForce(_force, hit.point, _radius, _upwardsModifier, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.red;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Gizmos.DrawWireSphere(hit.point, _radius);
            }
        }
    }
}