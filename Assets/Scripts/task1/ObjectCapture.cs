using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class ObjectCapture
    {
        private LayerMask _layerMask;
        private Transform _captureGameobjectTransform;
        private IGrabble _grabbable;
        private Vector3 _captureOffset;
        private float _captureDistance;

        private bool IsCapture => _grabbable != null;

        public ObjectCapture(LayerMask layerMask)
        {
            _layerMask = layerMask;
        }

        public void CaptureByRay(Ray ray)
        {
            if (IsCapture) return;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask) == false)
                return;

            if (hit.transform.gameObject.TryGetComponent(out _grabbable))
            {
                _grabbable.OnGrab();
                _captureGameobjectTransform = hit.transform;
                _captureDistance = hit.distance;
                _captureOffset = _captureGameobjectTransform.position - hit.point;
            }
        }

        public void MoveCapturedObject(Ray ray)
        {
            if (IsCapture == false) return;

            Vector3 worldPoint = ray.GetPoint(_captureDistance);
            Vector3 newPosition = worldPoint + _captureOffset;

            _captureGameobjectTransform.position = newPosition;
        }

        public void Release()
        {
            if (IsCapture == false) return;

            _grabbable.OnRelease();
            _grabbable = null;
            _captureGameobjectTransform = null;
        }
    }
}