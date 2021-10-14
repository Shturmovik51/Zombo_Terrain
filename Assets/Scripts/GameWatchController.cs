using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class GameWatchController : IUpdatable, IInitialisible, ICleanable, IController
    {
        public event Action OnSunRotation;
        private event Action<string> _onChangeTimeText = delegate { };
        private float _timeSpeed;
        private float _timeJumpSpeed;
        private float _currentTimeSpeed;
        private float _timeCountDown;
        private int _minutLeftNumber;
        private int _minutRightNumber;
        private int _hourLeftNumber;
        private int _hourRightNumber;
        public int MinutLeft => _minutLeftNumber;
        public int MinutRight => _minutRightNumber;
        public int HourLeft => _hourLeftNumber;
        public int HourRight => _hourRightNumber;

        private GamePanelController _gamePanelController;

        public GameWatchController(float countDownSpeed, float timeJumpSpeed, GamePanelController gamePanelController)
        {
            _gamePanelController = gamePanelController;
            _timeSpeed = countDownSpeed;
            _timeJumpSpeed = timeJumpSpeed;
        }

        public void Initialization()
        {
            _onChangeTimeText += _gamePanelController.ChangeTimeText;
            _currentTimeSpeed = _timeSpeed;
        }

        public void CleanUp()
        {
            _onChangeTimeText -= _gamePanelController.ChangeTimeText;
        }

        public void LocalUpdate(float deltaTime)
        {
            TimeCountDown(deltaTime);
        }

        public void TimeCountDown(float deltaTime)
        {
            _timeCountDown += deltaTime;

            if (_timeCountDown >= _currentTimeSpeed)
            {
                _minutRightNumber++;
                OnSunRotation?.Invoke();
                _timeCountDown -= _currentTimeSpeed;

                if (_minutRightNumber > 9)
                {
                    _minutLeftNumber++;
                    _minutRightNumber = 0;

                    if (_minutLeftNumber > 5)
                    {
                        _hourRightNumber++;
                        _minutLeftNumber = 0;

                        if (_hourLeftNumber < 2)
                        {
                            if (_hourRightNumber > 9)
                            {
                                _hourLeftNumber++;
                                _hourRightNumber = 0;
                            }
                        }
                        else
                        {
                            if (_hourRightNumber > 3)
                            {
                                _hourLeftNumber = 0;
                                _hourRightNumber = 0;
                            }
                        }
                    }
                }
            }

            ChangeTimeText($"{_hourLeftNumber}{_hourRightNumber} : {_minutLeftNumber}{_minutRightNumber}");
        }

        private void ChangeTimeText(string text)
        {
            _onChangeTimeText.Invoke(text);
        }

        public void SetTimeJump()
        {
            _currentTimeSpeed = _timeJumpSpeed;
        }
        public void SetNormalTime()
        {
            _currentTimeSpeed = _timeSpeed;
            Debug.Log(_currentTimeSpeed);
        }

    }
}
