using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKcontroller : MonoBehaviour
{
    [SerializeField] private Animator zombyAnimator;
    [SerializeField] private Transform obstacleObserverRH;
    [SerializeField] private Transform obstacleObserverLH;
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private float rayDist;
    [SerializeField] private Transform rightHandPoint;
    [SerializeField] private Transform leftHandPoint;

    private Transform targetObject;
    private Transform currentTargetObject;

    private float headWeightValue;
    private float leftHandWeightValue;
    private float rightHandWeightValue;
    
    private Transform obstacleObj;

    public Transform TargetObject {get => targetObject; set => targetObject = value;}
   
    private void OnAnimatorIK(int layerIndex)
    {     
        if(targetObject != null)
        {
            ChangeTarget();
            headWeightValue = Mathf.Lerp(headWeightValue, 1, 0.05f);
            zombyAnimator.SetLookAtWeight(headWeightValue);
            zombyAnimator.SetLookAtPosition(targetObject.position);            
        }
        else
        {
            headWeightValue = 0;
            zombyAnimator.SetLookAtWeight(headWeightValue);
        }

        if(Physics.Raycast(obstacleObserverRH.position, obstacleObserverRH.transform.forward, out var hitRH, rayDist, rayLayer))
        {
            rightHandPoint.position = hitRH.point;
            rightHandWeightValue = Mathf.Lerp(rightHandWeightValue, 1, 0.01f);
            ChangeWeightOfBodypart(AvatarIKGoal.RightHand, rightHandWeightValue);
            ChangePosOfHands(AvatarIKGoal.RightHand, rightHandPoint);
        }
        else if (rightHandWeightValue > 0)
        {
            rightHandWeightValue = Mathf.Lerp(rightHandWeightValue, 0, 0.01f);
            ChangeWeightOfBodypart(AvatarIKGoal.RightHand, rightHandWeightValue);
        }

        if (Physics.Raycast(obstacleObserverLH.position, obstacleObserverLH.transform.forward, out var hitLH, rayDist, rayLayer))
        {
            leftHandPoint.position = hitLH.point;
            leftHandWeightValue = Mathf.Lerp(leftHandWeightValue, 1, 0.01f);
            ChangeWeightOfBodypart(AvatarIKGoal.LeftHand, leftHandWeightValue);
            ChangePosOfHands(AvatarIKGoal.LeftHand, leftHandPoint);
        }
        else if (leftHandWeightValue > 0)
        {
            leftHandWeightValue = Mathf.Lerp(leftHandWeightValue, 0, 0.01f);
            ChangeWeightOfBodypart(AvatarIKGoal.LeftHand, leftHandWeightValue);
        }
    }

    private void ChangeTarget()
    {
        if (targetObject != currentTargetObject)         
        {
            headWeightValue = 0;
            currentTargetObject = targetObject;
        }
    }
   
    private void ChangeWeightOfBodypart(AvatarIKGoal avatarIKGoal, float value)
    {
        zombyAnimator.SetIKPositionWeight(avatarIKGoal, value);
        zombyAnimator.SetIKRotationWeight(avatarIKGoal, value);
    }

    private void ChangePosOfHands(AvatarIKGoal avatarIKGoal, Transform ArmPoint)
    {
        zombyAnimator.SetIKPosition(avatarIKGoal, ArmPoint.position);
        zombyAnimator.SetIKRotation(avatarIKGoal, ArmPoint.rotation);
    }
}
