using UnityEngine;

[RequireComponent(typeof(Health))]

public class ZombieEnemy : MonoBehaviour
{
    private Health zombieEnemyHealth;
    private Rigidbody[] dollRGBodys;
    private Animator zombieEnemyAnimator;

    private void Awake()
    {
        zombieEnemyHealth = GetComponent<Health>();
        dollRGBodys = GetComponentsInChildren<Rigidbody>();
        zombieEnemyAnimator = GetComponent<Animator>();
        zombieEnemyHealth.DeathEntity += DeathZombieEnemy;

        foreach (var rgBody in dollRGBodys)
        {
            rgBody.tag = "Enemy";
            rgBody.isKinematic = true;
        }
    }

    private void DeathZombieEnemy()
    {
        foreach (var rgBody in dollRGBodys) rgBody.isKinematic = false;
        zombieEnemyAnimator.enabled = false;
    }

    
}
