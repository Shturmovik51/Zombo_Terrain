using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAttackHitCheck : MonoBehaviour
{
    [SerializeField] private GameObject hitChecker;

    public void HitCheck()
    {
        hitChecker.SetActive(true);
    }
}
