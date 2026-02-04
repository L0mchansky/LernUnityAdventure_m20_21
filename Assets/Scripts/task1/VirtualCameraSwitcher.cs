using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class VirtualCameraSwitcher : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;

        [SerializeField] private KeyCode _previousKey = KeyCode.A;
        [SerializeField] private KeyCode _nextKey = KeyCode.D;

        private const int ActiveCameraPriority = 10;

        private int _currentIndex = 0;

        private void Start()
        {
            SetActiveCamera(_currentIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_previousKey))
            {
                SwitchToPrevious();
            }

            if (Input.GetKeyDown(_nextKey))
            {
                SwitchToNext();
            }
        }

        private bool HasOnlyOneCamera => _virtualCameras == null || _virtualCameras.Count <= 1;

        private void SwitchToPrevious()
        {
            if (HasOnlyOneCamera)
                return;

            _currentIndex--;

            if (_currentIndex < 0)
                _currentIndex = _virtualCameras.Count - 1;

            SetActiveCamera(_currentIndex);
        }

        private void SwitchToNext()
        {
            if (HasOnlyOneCamera)
                return;

            _currentIndex++;

            if (_currentIndex >= _virtualCameras.Count)
                _currentIndex = 0;

            SetActiveCamera(_currentIndex);
        }

        private void SetActiveCamera(int index)
        {
            for (int i = 0; i < _virtualCameras.Count; i++)
            {
                _virtualCameras[i].Priority = (i == index) ? ActiveCameraPriority : 0;
            }
        }
    }
}