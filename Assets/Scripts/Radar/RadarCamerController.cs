using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class RadarCamerController : IUpdatable, IInitialisible, IController
    {
        private Camera _radarCamera;
        private PlayerView _playerView;
        public RadarCamerController(Camera radarCamera, PlayerView playerView)
        {
            _radarCamera = radarCamera;
            _playerView = playerView;
        }

        public void Initialization()
        {
            _playerView.OnChangeRotation += CameraRotation;           
        }

        public void LocalUpdate(float deltaTime)
        {
            CameraPosition();
        }

        private void CameraPosition()
        {
            Vector3 newPos = _playerView.transform.position;
            newPos.y = _radarCamera.transform.position.y;
            _radarCamera.transform.position = newPos;            
        }

        private void CameraRotation(Transform transform)
        {
            _radarCamera.transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
            
        }

    }
}
