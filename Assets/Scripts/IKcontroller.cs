using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKcontroller : MonoBehaviour
{
    [SerializeField] private Animator zombyAnimator;
    [SerializeField] private Transform leftHandTouchPoint;
    [SerializeField] private Transform rightHandTouchPoint;
    private Transform targetObject;
    private Transform currentTargetObject;
    private float headWeightValue;

    public Transform TargetObject { get { return targetObject; } set { targetObject = value; } }
   
    private void OnAnimatorIK(int layerIndex)
    {
        if (targetObject != currentTargetObject)
        {
            headWeightValue = 0;
            currentTargetObject = targetObject;
        }

        if(targetObject != null)
        {
            headWeightValue = Mathf.Lerp(headWeightValue, 1, 0.05f);
            zombyAnimator.SetLookAtWeight(headWeightValue);
            zombyAnimator.SetLookAtPosition(targetObject.position);            
        }
        else
        {
            headWeightValue = 0;
            zombyAnimator.SetLookAtWeight(headWeightValue);
        }
    }
   
    private void ChangeWeightOfBodypart(AvatarIKGoal avatarIKGoal, float value)
    {
        zombyAnimator.SetIKPositionWeight(avatarIKGoal, value);
        zombyAnimator.SetIKRotationWeight(avatarIKGoal, value);
    }
}
