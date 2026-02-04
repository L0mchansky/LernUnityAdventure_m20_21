using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class Explosion : IInteraction
    {
        private float _radius;
        private float _force;
        private float _upwardsModifier;
        private LayerMask _layerMask;

        private GameObject _explosionEffect;

        private Ray _ray;

        public Explosion(LayerMask layerMask, GameObject explosionEffect, float radius, float force, float upwardsModifier)
        {
            _explosionEffect = explosionEffect;
            _radius = radius;
            _force = force;
            _upwardsModifier = upwardsModifier;
            _layerMask = layerMask;
        }

        public void Interact(Ray ray)
        {
            _ray = ray;
            Explode();
        }

        private void Explode()
        {
            if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, _layerMask) == false)
                return;

            if (_explosionEffect != null)
            {
                _explosionEffect.transform.position = hit.point;
                _explosionEffect.SetActive(true);
            }

            Collider[] targets = Physics.OverlapSphere(hit.point, _radius);

            foreach (Collider target in targets)
            {
                if (!target.TryGetComponent(out Rigidbody rigidbody))
                    continue;

                rigidbody.AddExplosionForce(_force, hit.point, _radius, _upwardsModifier, ForceMode.Impulse);
            }
        }
    }
}