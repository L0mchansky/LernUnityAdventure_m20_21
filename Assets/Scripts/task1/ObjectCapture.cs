using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class ObjectCapture : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        private const int LeftButtonClick = 0;
        private Transform _captureGameobjectTransform;
        private Camera _camera;
        private Vector3 _captureOffset;
        private float _captureDistance;
        private const float GizmoRayLength = 100f;

        private bool IsCapture => _captureGameobjectTransform != null;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(LeftButtonClick) && IsCapture == false)
                CaptureByRay();

            if (Input.GetMouseButton(LeftButtonClick) && IsCapture)
                MoveCapturedObject();

            if (Input.GetMouseButtonUp(LeftButtonClick))
                _captureGameobjectTransform = null;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false) return;

            Gizmos.color = Color.red;

            Ray cameraRay = GetCameraRay();
            Vector3 gizmoVector = cameraRay.direction * GizmoRayLength;
            Gizmos.DrawRay(cameraRay.origin, gizmoVector);
        }

        private void MoveCapturedObject()
        {
            Ray cameraRay = GetCameraRay();

            Vector3 worldPoint = cameraRay.GetPoint(_captureDistance);
            Vector3 newPosition = worldPoint + _captureOffset;

            _captureGameobjectTransform.position = newPosition;
        }

        private void CaptureByRay()
        {
            Ray cameraRay = GetCameraRay();

            if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                _captureGameobjectTransform = hit.transform;

                _captureDistance = hit.distance;

                _captureOffset = _captureGameobjectTransform.position - hit.point;
            }
        }


        private Ray GetCameraRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }

    }
}
