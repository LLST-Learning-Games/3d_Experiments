using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CameraControl
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCameraBase> _allCameras;

        public static CameraManager Instance;

        private void Awake()
        {
            // Don't use singletons, singletons are bad mmmmk
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            
            Instance = this;
        }

        public void SetActiveCamera(CinemachineVirtualCameraBase _cameraToActivate)
        {
            foreach (var camera in _allCameras)
            {
                camera.gameObject.SetActive(camera == _cameraToActivate);
            }
        }
    }
}