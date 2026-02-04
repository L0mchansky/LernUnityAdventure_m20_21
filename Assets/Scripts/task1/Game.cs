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
        [SerializeField] private GameObject _explosionEffect;

        [SerializeField] private LayerMask _layerMaskGround;
        [SerializeField] private LayerMask _layerMaskEnv;

        [Header("Explosion Interaction")]
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _force = 50f;
        [SerializeField] private float _upwardsModifier = 1f;

        private Ray _ray;
        private const float GizmoRayLength = 100f;

        private const int LeftButtonClick = 0;
        private const int RightButtonClick = 1;
        private Player _player;

        private void Start()
        {
            _cameraSwitcher = new VirtualCameraSwitcher(_virtualCameras);

            ObjectCapture objectCapture = new ObjectCapture(_layerMaskEnv);
            Explosion explosion = new Explosion(_layerMaskGround, _explosionEffect, _radius, _force, _upwardsModifier);

            _player = new Player(objectCapture, explosion);
        }

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetKeyDown(_previousKey))
            {
                _cameraSwitcher.SwitchToPrevious();
            }

            if (Input.GetKeyDown(_nextKey))
            {
                _cameraSwitcher.SwitchToNext();
            }

            if (Input.GetMouseButton(LeftButtonClick))
            {
                _player.PrimaryInteract(_ray);
            }

            if (Input.GetMouseButton(RightButtonClick))
            {
                _player.SecondaryInteract(_ray);
            }
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
    }
}
