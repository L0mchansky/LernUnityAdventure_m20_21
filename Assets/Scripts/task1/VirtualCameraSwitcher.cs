using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class VirtualCameraSwitcher
    {
        private List<CinemachineVirtualCamera> _virtualCameras;

        private int _currentIndex = 0;
        private const int ActiveCameraPriority = 10;

        public VirtualCameraSwitcher(List<CinemachineVirtualCamera> virtualCameras)
        {
            _virtualCameras = virtualCameras;
            SetActiveCamera(_currentIndex);
        }

        private bool HasOnlyOneCamera => _virtualCameras == null || _virtualCameras.Count <= 1;

        public void SwitchToPrevious()
        {
            if (HasOnlyOneCamera)
                return;

            _currentIndex--;

            if (_currentIndex < 0)
                _currentIndex = _virtualCameras.Count - 1;

            SetActiveCamera(_currentIndex);
        }

        public void SwitchToNext()
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