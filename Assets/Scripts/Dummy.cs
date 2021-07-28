using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Animator dummyAnimator;
    private CapsuleCollider dummyCollider;
    private Health dummyHealth;

    void Awake()
    {
        dummyAnimator = GetComponent<Animator>();
        dummyCollider = GetComponent<CapsuleCollider>();
        dummyHealth = GetComponent<Health>();
    }
    private void Start()
    {
        dummyHealth.DeathEntity += DummyDeath;
    }

    private void DummyDeath()
    {
        dummyCollider.enabled = false;
        dummyAnimator.enabled = false;

    }    
}
