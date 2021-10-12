using UnityEngine;

namespace ZomboTerrain
{
    public sealed class DailyCycleController : IInitialisible, IController
    {       
        private Transform _directionalLight;
        private DailyCycleModel _dailyCycleModel;
        private GameWatchController _gameWatchController;
        
        public DailyCycleController(DailyCycleModel dailyCycleModel, GameWatchController gameWatchController,
                                        Transform directionalLight)
        {
            _dailyCycleModel = dailyCycleModel;
            _gameWatchController = gameWatchController;
            _directionalLight = directionalLight;
        }
       
        public void Initialization()
        {
            _gameWatchController.OnSunRotation += SunRotation;
        }

        public void SunRotation()        
        {
            #region Cloud Color Change soon in Update
            //if (_isOnSpeedRotation)
            //{
            //    rotationSpeed *= _timeJumpSpeed;

            //    if (Quaternion.Angle(_directionalLight.rotation, _sunPosition) < 1f)
            //    {               
            //        _isOnSpeedRotation = false;
            //    }
            //}

            //if (angle < 15 || angle > 345 || angle > 165 && angle < 195)
            //{
            //    var dayToNightColor = Color.red;
            //    dayToNightColor = Color.Lerp(dayToNightColor, Color.yellow, _cloudColorChangeSpeed);
            //    _cloudsMaterial.SetColor("_TintColor", dayToNightColor);


            //    //var dayToNightColor = Color.Lerp(_cloudsMaterial.GetColor("_TintColor"), _sunSetCloudColor, _cloudColorChangeSpeed);
            //    //_cloudsMaterial.SetColor("_TintColor", dayToNightColor);
            //}
            //else
            //{
            //    var nightToDayColor = _sunSetCloudColor;
            //    nightToDayColor = Color.Lerp(nightToDayColor, _dayCloudColor, _cloudColorChangeSpeed);
            //    _cloudsMaterial.SetColor("_TintColor", nightToDayColor);
            //    Debug.Log("Da");
            //}
            #endregion
            _directionalLight.Rotate(Vector3.left * _dailyCycleModel.SunRotationPerMinute);
        }
    }
}
