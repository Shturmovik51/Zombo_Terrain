using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class ZombieEnemy : MonoBehaviour, ILiveEntity, ITakeDamage, ICloneable
    {
        [SerializeField] private int _health;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameManager _gameManager;

        private bool _isDead;
        private LayerMask _layerMask;
        public event Action OnEnemyDeath;
        private Rigidbody[] _dollRigidBodys;
        private Animator zombieEnemyAnimator;


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
            OnEnemyDeath?.Invoke();

            if (_spawnPoint != null)
                Clone();

            foreach (var rgBody in _dollRigidBodys) rgBody.isKinematic = false;
            zombieEnemyAnimator.enabled = false;
        }
    }
}