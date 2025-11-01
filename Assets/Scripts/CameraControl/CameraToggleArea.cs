using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CameraControl
{
    public class CameraToggleArea : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase _cameraToActivate;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter" + other.gameObject.name);
            CameraManager.Instance.SetActiveCamera(_cameraToActivate);
        }
    }
}