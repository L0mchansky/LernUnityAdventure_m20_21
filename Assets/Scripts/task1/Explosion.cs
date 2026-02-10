using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class Explosion
    {
        private float _radius;
        private float _force;
        private LayerMask _layerMask;
        private GameObject _explosionEffectPrefab;

        public Explosion(LayerMask layerMask, GameObject explosionEffectPrefab, float radius, float force)
        {
            _explosionEffectPrefab = explosionEffectPrefab;
            _radius = radius;
            _force = force;
            _layerMask = layerMask;
        }

        public void Explode(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask) == false)
                return;

            if (_explosionEffectPrefab != null)
            {
                Game.Instantiate(_explosionEffectPrefab, hit.point, Quaternion.identity);
            }

            Collider[] targets = Physics.OverlapSphere(hit.point, _radius);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IExplodable explodable) == false)
                    continue;

                Vector3 directionMove = (target.transform.position - hit.point).normalized;

                explodable.OnExplode(_force, directionMove);
            }
        }
    }
}