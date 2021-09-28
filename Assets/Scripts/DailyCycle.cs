using UnityEngine;

public class DailyCycle
{    
    private float _timeJumpSpeed;
    private float _cloudColorChangeSpeed;
    private Color _sunSetCloudColor;
    private Color _dayCloudColor;
    private Material _cloudsMaterial;
    private Transform _directionalLight;
    private InGameWatch _inGameWatch;
    private const float _sunRotationPerMinute = 0.25f;

    private bool _isOnSpeedRotation;
    private Quaternion _sunPosition;
    public DailyCycle(float timeJumpSpeed, float cloudColorChangeSpeed, Color sunSetCloudColor, Color dayCloudColor,
                         Material cloudsMaterial, Transform directionalLight, InGameWatch inGameWatch)
    {     
        _timeJumpSpeed = timeJumpSpeed;
        _cloudColorChangeSpeed = cloudColorChangeSpeed;
        _sunSetCloudColor = sunSetCloudColor;
        _dayCloudColor = dayCloudColor;
        _cloudsMaterial = cloudsMaterial;
        _directionalLight = directionalLight;
        _inGameWatch = inGameWatch;
    }

    public void DailyCycleTimeJump(Quaternion sunPosition)
    {
        _sunPosition = sunPosition;
        _isOnSpeedRotation = true;
    }
    public void Enable()
    {
        _inGameWatch.OnSunRotation += SunRotation;
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

        _directionalLight.Rotate(Vector3.left * _sunRotationPerMinute);
    }
}
