using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;
        [SerializeField] private KeyCode _previousKey = KeyCode.A;
        [SerializeField] private KeyCode _nextKey = KeyCode.D;
        [SerializeField] private VirtualCameraSwitcher _cameraSwitcher;
        [SerializeField] private GameObject _explosionEffectPrefab;

        [SerializeField] private LayerMask _layerMaskGround;
        [SerializeField] private LayerMask _layerMaskEnv;

        [Header("Explosion params")]
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _force = 50f;

        private Ray _ray;
        private const float GizmoRayLength = 100f;

        private ObjectCapture _objectCapture;
        private Explosion _explosion;

        private const int LeftButtonClick = 0;
        private const int RightButtonClick = 1;

        private void Start()
        {
            _cameraSwitcher = new VirtualCameraSwitcher(_virtualCameras);

            _objectCapture = new ObjectCapture(_layerMaskEnv);
            _explosion = new Explosion(_layerMaskGround, _explosionEffectPrefab, _radius, _force);
        }

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            HandlingCamera();
            HandlingObjectCapture();
            HandlingExplosion();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 gizmoVector = _ray.direction * GizmoRayLength;
            Gizmos.DrawRay(_ray.origin, gizmoVector);

            if (Physics.Raycast(_ray, out RaycastHit hit))
            {
                Gizmos.DrawWireSphere(hit.point, _radius);
            }
        }

        private void HandlingCamera()
        {
            if (Input.GetKeyDown(_previousKey))
            {
                _cameraSwitcher.SwitchToPrevious();
            }

            if (Input.GetKeyDown(_nextKey))
            {
                _cameraSwitcher.SwitchToNext();
            }
        }

        private void HandlingObjectCapture()
        {
            if (Input.GetMouseButtonDown(LeftButtonClick))
            {
                _objectCapture.CaptureByRay(_ray);
            }

            if (Input.GetMouseButton(LeftButtonClick))
            {
                _objectCapture.MoveCapturedObject(_ray);
            }

            if (Input.GetMouseButtonUp(LeftButtonClick))
            {
                _objectCapture.Release();
            }
        }

        private void HandlingExplosion()
        {
            if (Input.GetMouseButtonUp(RightButtonClick))
            {
                _explosion.Explode(_ray);
            }
        }
    }
}