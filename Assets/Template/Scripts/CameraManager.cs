using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using CameraType = Template.Scripts.CameraType;

namespace Template.Scripts
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private CinemachineBrain cineMachineBrain;
        [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;
        
        private void DeactivateAllCameras()
        {
            foreach (var virtualCamera in virtualCameras)
            {
                virtualCamera.gameObject.SetActive(false);
            }
        }

        public void ChangeCamera(CameraType playerCameraType , float duration)
        {
            DeactivateAllCameras();

            cineMachineBrain.m_DefaultBlend.m_Time = duration;
            virtualCameras[(int)playerCameraType].gameObject.SetActive(true);
        }

        public void SetCameraFollowTarget(CameraType playerCameraType, Transform followTarget)
        {
            virtualCameras[(int)playerCameraType].Follow = followTarget;
        }
    }
}