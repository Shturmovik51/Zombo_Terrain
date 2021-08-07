using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class Dummy : MonoBehaviour
{
    private Animator dummyAnimator;
    private Health dummyHealth;

    void Awake()
    {
        dummyAnimator = GetComponent<Animator>();
        dummyHealth = GetComponent<Health>();
    }
    private void Start()
    {
        dummyHealth.DeathEntity += DummyDeath;
    }

    private void DummyDeath()
    {
        dummyAnimator.enabled = false;
    }    
}
