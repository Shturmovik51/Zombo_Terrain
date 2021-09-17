using System;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class ZombieEnemy : MonoBehaviour, ICloneable
{
    private Health zombieEnemyHealth;
    private Rigidbody[] dollRGBodys;
    private Animator zombieEnemyAnimator;
    private LayerMask layerMask;
    [SerializeField] private Transform spawnPoint;

    public object Clone()
    {   
        var pointInSphere = UnityEngine.Random.insideUnitSphere * 6 + spawnPoint.position;
        var pointInSurface = new Vector3(pointInSphere.x, spawnPoint.position.y, pointInSphere.z);

        Ray ray = new Ray(pointInSurface, Vector3.down);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
        {
            pointInSurface = hit.point;
        }

        return Instantiate(this, pointInSurface, transform.rotation);
    }

    private void Awake()
    {
        zombieEnemyHealth = GetComponent<Health>();
        dollRGBodys = GetComponentsInChildren<Rigidbody>();
        zombieEnemyAnimator = GetComponent<Animator>();
        zombieEnemyHealth.DeathEntity += DeathZombieEnemy;
        layerMask = LayerMask.GetMask("Ground");

        foreach (var rgBody in dollRGBodys)
        {
            rgBody.tag = "Enemy";
            rgBody.isKinematic = true;
        }
    }

    private void DeathZombieEnemy()
    {
        if (spawnPoint != null)
            Clone();        
        foreach (var rgBody in dollRGBodys) rgBody.isKinematic = false;
        zombieEnemyAnimator.enabled = false;
    }    
}
