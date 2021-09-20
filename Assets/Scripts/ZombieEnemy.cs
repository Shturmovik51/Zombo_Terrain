using System;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour, ILiveEntity, ITakeDamage, ICloneable
{    
    private Rigidbody[] _dollRigidBodys;
    private Animator zombieEnemyAnimator;
    private LayerMask _layerMask;
    private bool _isDead;

    [SerializeField] private int _health;
    [SerializeField] private Transform _spawnPoint;

    public int Health { get => _health; }

    public void AddDamage(int damageValue)
    {
        _health -= damageValue;

        if (_health <= 0 && !_isDead)
            DeathZombieEnemy();
    }

    public object Clone()
    {   
        var pointInSphere = UnityEngine.Random.insideUnitSphere * 6 + _spawnPoint.position;
        var pointInSurface = new Vector3(pointInSphere.x, _spawnPoint.position.y, pointInSphere.z);

        Ray ray = new Ray(pointInSurface, Vector3.down);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask))
        {
            pointInSurface = hit.point;
        }

        return Instantiate(this, pointInSurface, transform.rotation);
    }

    private void Awake()
    {        
        _dollRigidBodys = GetComponentsInChildren<Rigidbody>();
        zombieEnemyAnimator = GetComponent<Animator>();
        _layerMask = LayerMask.GetMask("Ground");

        foreach (var rgBody in _dollRigidBodys)
        {
            rgBody.tag = "Enemy";
            rgBody.isKinematic = true;
        }
    }

    private void DeathZombieEnemy()
    {
        _isDead = true;    
        foreach (var rgBody in _dollRigidBodys) rgBody.isKinematic = false;
        zombieEnemyAnimator.enabled = false;

        if (_spawnPoint != null)
            Clone();
    }    
}
