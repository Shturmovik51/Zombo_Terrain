using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyCycle
{
    private float _dayRotationSpeed;
    private float _timeJumpSpeed;
    private float _cloudColorChangeSpeed;
    private Color _sunSetCloudColor;
    private Color _dayCloudColor;
    private Material _cloudsMaterial;
    private Transform _directionalLight;

    private bool _isOnSpeedRotation;
    private Quaternion _sunPosition;
    public DailyCycle(float dayRotationSpeed, float timeJumpSpeed, float cloudColorChangeSpeed, Color sunSetCloudColor,
                        Color dayCloudColor, Material cloudsMaterial, Transform directionalLight)
    {
        _dayRotationSpeed = dayRotationSpeed;
        _timeJumpSpeed = timeJumpSpeed;
        _cloudColorChangeSpeed = cloudColorChangeSpeed;
        _sunSetCloudColor = sunSetCloudColor;
        _dayCloudColor = dayCloudColor;
        _cloudsMaterial = cloudsMaterial;
        _directionalLight = directionalLight;
    }

    public void DailyCycleTimeJump(Quaternion sunPosition)
    {
        _sunPosition = sunPosition;
        _isOnSpeedRotation = true;
    }

    public void DirectionLightRotation()
    {
        var rotationSpeed = _dayRotationSpeed;
        float angle;
        Vector3 axis;

        _directionalLight.rotation.ToAngleAxis(out angle, out axis);

        if (_isOnSpeedRotation)
        {
            rotationSpeed *= _timeJumpSpeed;

            if (Quaternion.Angle(_directionalLight.rotation, _sunPosition) < 1f)
            {               
                _isOnSpeedRotation = false;
            }
        }

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

        _directionalLight.Rotate(Vector3.left * rotationSpeed * Time.fixedDeltaTime);
    }

    //private void CloudsColor(Color currentCloudsColor)
    //{


    //    _cloudsMaterial.SetColor("_TintColor", dayToNightColor);
    //}

}
