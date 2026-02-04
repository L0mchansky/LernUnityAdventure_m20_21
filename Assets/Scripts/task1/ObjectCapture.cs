using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class ObjectCapture : IInteraction
    {
        private LayerMask _layerMask;
        private Transform _captureGameobjectTransform;
        private Vector3 _captureOffset;
        private float _captureDistance;
        private Ray _ray;

        private bool IsCapture => _captureGameobjectTransform != null;

        public ObjectCapture(LayerMask layerMask)
        {
            _layerMask = layerMask;
        }

        public void Interact(Ray ray)
        {
            _ray = ray;

            CaptureByRay();
            MoveCapturedObject();
        }

        private void MoveCapturedObject()
        {
            if (IsCapture == false) return;

            Vector3 worldPoint = _ray.GetPoint(_captureDistance);
            Vector3 newPosition = worldPoint + _captureOffset;

            _captureGameobjectTransform.position = newPosition;
        }

        private void CaptureByRay()
        {
            if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform.Equals(_captureGameobjectTransform)) return;

                _captureGameobjectTransform = hit.transform;

                _captureDistance = hit.distance;

                _captureOffset = _captureGameobjectTransform.position - hit.point;
            }
            else
            {
                _captureGameobjectTransform = null;
            }
        }
    }
}
