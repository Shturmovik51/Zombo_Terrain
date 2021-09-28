using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InGameWatch
{
    private TextMeshProUGUI _timerText;
    private float _countDownSpeed;
    public UnityAction OnSunRotation;    

    public InGameWatch(TextMeshProUGUI timerText, float countDownSpeed)
    {
        _timerText = timerText;
        _countDownSpeed = countDownSpeed;
    }

    private float _timeCountDown;

    private int _minutLeftNumber;
    private int _minutRightNumber;
    private int _hourLeftNumber;
    private int _hourRightNumber;
       
    public void TimeCountDown()
    {
        _timeCountDown += Time.deltaTime;

        if (_timeCountDown >= _countDownSpeed)
        {
            _minutRightNumber++;
            OnSunRotation?.Invoke();
            _timeCountDown-= _countDownSpeed;

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
         

        _timerText.text = $"{_hourLeftNumber}{_hourRightNumber} : {_minutLeftNumber}{_minutRightNumber}";
    }
}
