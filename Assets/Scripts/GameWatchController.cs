using System;
using UnityEngine.Events;

namespace ZomboTerrain
{
    public class GameWatchController : IUpdatable, IInitialisible, IController
    {
        private GameUIController _gameUIController;
        private float _countDownSpeed;
        public Action OnSunRotation;
        private event Action<string> OnChangeTimeText = delegate { };

        public GameWatchController(GameUIController gameUIController, float countDownSpeed)
        {
            _gameUIController = gameUIController;
            _countDownSpeed = countDownSpeed;
        }

        private float _timeCountDown;

        private int _minutLeftNumber;
        private int _minutRightNumber;
        private int _hourLeftNumber;
        private int _hourRightNumber;

        public void Initialization()
        {
            OnChangeTimeText += _gameUIController.ChangeTimeText;
        }

        public void LocalUpdate(float deltaTime)
        {
            TimeCountDown(deltaTime);
        }

        public void TimeCountDown(float deltaTime)
        {
            _timeCountDown += deltaTime;

            if (_timeCountDown >= _countDownSpeed)
            {
                _minutRightNumber++;
                OnSunRotation?.Invoke();
                _timeCountDown -= _countDownSpeed;

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
            OnChangeTimeText.Invoke(text);
        }
    }
}
