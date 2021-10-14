using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public sealed class DailyCycleController : IInitialisible, ICleanable, IController
    {
        private event Action _onStartTimeJump;
        private event Action _onEndTimeJump;

        private DailyCycleModel _dailyCycleModel;
        private GameWatchController _watch;
        private Color _currentCloudColor;
        private Button _currentActiveButton;

        private bool _isTimeJumpToDay;
        private bool _isTimeJumpToNight;
        private bool _isTimeJumpToSunSet;
        private bool _isTimeJumpToSunRise;


        public DailyCycleController(DailyCycleModel dailyCycleModel, GameWatchController watch)
        {
            _dailyCycleModel = dailyCycleModel;
            _watch = watch;            
        }
       
        public void Initialization()
        {
            _watch.OnSunRotation += SunRotation;
            _onStartTimeJump += _watch.SetTimeJump;
            _onEndTimeJump += _watch.SetNormalTime;
                       
            _currentCloudColor = _dailyCycleModel.NightCloudColor;
            _dailyCycleModel.CloudsMaterial.SetColor("_TintColor", _currentCloudColor);
        }

        public void CleanUp()
        {
            _watch.OnSunRotation -= SunRotation;
            _onStartTimeJump -= _watch.SetTimeJump;
            _onEndTimeJump -= _watch.SetNormalTime;
        }

        public void SunRotation()        
        {
            if (_watch.HourLeft == 0 && _watch.HourRight == 5)  //05:00
            {
                if (_isTimeJumpToSunRise)
                    _isTimeJumpToSunRise = SetNormalTime(_isTimeJumpToSunRise);
                
                _currentCloudColor = ChangeCloudColor(_dailyCycleModel.SunRiseCloudColor, _currentCloudColor);
            }
            if (_watch.HourLeft == 0 && _watch.HourRight == 6)  //06:00
            {  
                _currentCloudColor = ChangeCloudColor(_dailyCycleModel.DayCloudColor, _currentCloudColor);
            }
            if (_watch.HourLeft == 1 && _watch.HourRight == 2)  //12:00
            {
                if (_isTimeJumpToDay)
                    _isTimeJumpToDay = SetNormalTime(_isTimeJumpToDay);
            }
            if (_watch.HourLeft == 1 && _watch.HourRight == 8)  //18:00
            {                
                if (_isTimeJumpToSunSet)
                    _isTimeJumpToSunSet = SetNormalTime(_isTimeJumpToSunSet);

                _currentCloudColor = ChangeCloudColor(_dailyCycleModel.SunSetCloudColor, _currentCloudColor);
            }
            if (_watch.HourLeft == 1 && _watch.HourRight == 9)  //19:00
            {   
                _currentCloudColor = ChangeCloudColor(_dailyCycleModel.NightCloudColor, _currentCloudColor);
            }
            if (_watch.HourLeft == 0 && _watch.HourRight == 0)  //00:00
            {
                if (_isTimeJumpToNight)
                    _isTimeJumpToNight = SetNormalTime(_isTimeJumpToNight);
            }

            _dailyCycleModel.SunLight.transform.Rotate(Vector3.left * _dailyCycleModel.SunRotationPerMinute);
        }

        private Color ChangeCloudColor(Color newColor, Color currentColor)
        {
            currentColor = Color.Lerp(currentColor, newColor, _dailyCycleModel.CloudColorChangeSpeed);
            _dailyCycleModel.CloudsMaterial.SetColor("_TintColor", currentColor);
            return currentColor;
        }

        private bool SetNormalTime(bool currentTimeJump)
        {
            _onEndTimeJump?.Invoke();
            _currentActiveButton.image.color = Color.white;
            return !currentTimeJump;
        }

        public void SetDayTime(Button button)
        {
            ResetTimeJumps();
            ResetButton(button);

            _isTimeJumpToDay = true;
            _onStartTimeJump?.Invoke();
        }

        public void SetNightTime(Button button)
        {
            ResetTimeJumps();
            ResetButton(button);

            _isTimeJumpToNight = true;
            _onStartTimeJump?.Invoke();
        }

        public void SetSunSetTime(Button button)
        {
            ResetTimeJumps();
            ResetButton(button);
                      
            _isTimeJumpToSunSet = true;
            _onStartTimeJump?.Invoke();
        }

        public void SetSunRiseTime(Button button)
        {
            ResetTimeJumps();
            ResetButton(button);

            _isTimeJumpToSunRise = true;
            _onStartTimeJump?.Invoke();
        }

        private void ResetTimeJumps()
        {
            _isTimeJumpToDay = false;
            _isTimeJumpToNight = false;
            _isTimeJumpToSunSet = false;
            _isTimeJumpToSunRise = false;
        }

        private void ResetButton(Button button)
        {
            if (_currentActiveButton != null)
                _currentActiveButton.image.color = Color.white;
            _currentActiveButton = button;
            _currentActiveButton.image.color = Color.green;
        }
    }
}
