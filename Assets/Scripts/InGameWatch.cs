using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameWatch
{
    private TextMeshProUGUI _timerText;
    private float _countDownSpeed;

    public InGameWatch(TextMeshProUGUI timerText, float countDownSpeed)
    {
        _timerText = timerText;
        _countDownSpeed = countDownSpeed;
    }

    private float _countDown;

    private float _seconds;
    private int _minutLeftNumber;
    private int _minutRightNumber;
    private int _hourLeftNumber;
    private int _hourRightNumber;
       
    public void TimeCountDown()
    {
        _countDown += Time.deltaTime;

        if(_countDown >= _countDownSpeed)
        {
            _seconds++;
            _countDown = 0;

            if (_seconds == 60)
            {
                _minutRightNumber++;
                _seconds = 0;

                if (_minutRightNumber > 9)
                {
                    _minutLeftNumber++;
                    _minutRightNumber = 0;

                    if (_minutLeftNumber > 5)
                    {
                        _hourRightNumber++;
                        _minutLeftNumber = 0;

                        if(_hourLeftNumber < 2)
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
        } 

        _timerText.text = $"{_hourLeftNumber}{_hourRightNumber} : {_minutLeftNumber}{_minutRightNumber}";
    }
}
