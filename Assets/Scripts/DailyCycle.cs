using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyCycle : MonoBehaviour
{
    [SerializeField] float dayRotationSpeed;
    [SerializeField] private float timeJump;
    [SerializeField] private Renderer cloudsMaterial;
    [SerializeField] private Color sunSetCloudColor;

    private Quaternion enteredDayTime;    
    private Color dayCloudColor;
    private bool isOnSpeedRotation;

    private void Start()
    {
        dayCloudColor = cloudsMaterial.material.GetColor("_TintColor"); 
    }


    private void FixedUpdate()
    {
        DirLightRotation();
    }

    private void DirLightRotation()
    {
        var rotationSpeed = dayRotationSpeed;
        float angle;
        Vector3 axis;

        transform.rotation.ToAngleAxis(out angle, out axis);
        Debug.Log(angle);        

        if (isOnSpeedRotation)
        {
            rotationSpeed *= timeJump;

            if (Quaternion.Angle(transform.rotation, enteredDayTime) < 1f)
            {               
                isOnSpeedRotation = false;
            }
        }

        if (angle < 15 || angle > 345 || angle > 165 && angle < 195)
        {
            var dayToNightColor = Color.Lerp(cloudsMaterial.material.GetColor("_TintColor"), sunSetCloudColor, 0.01f);
            cloudsMaterial.material.SetColor("_TintColor", dayToNightColor);
        }
        else
        {
            var nightToDayColor = Color.Lerp(cloudsMaterial.material.GetColor("_TintColor"), dayCloudColor, 0.01f);
            cloudsMaterial.material.SetColor("_TintColor", nightToDayColor);
        }

        transform.Rotate(Vector3.left * rotationSpeed * Time.fixedDeltaTime);
    }

    public void DailyCycleTimeJump(Quaternion jump)
    {
        enteredDayTime = jump;
        isOnSpeedRotation = true;
    }
}
