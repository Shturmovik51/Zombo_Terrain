using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon model", menuName = "Database / Weapon")]
public class WeaponModelData : ScriptableObject
{
    [SerializeField] private int _hitsCountInCollection;
    [SerializeField] private int _shootDamage;
    [SerializeField] private int _hitImpulseForce;
    [SerializeField] private int _maxMagazineAmmoCount;
    [SerializeField] private float _shootFlashTime;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private GameObject _bodyHitEffect;
    [SerializeField] private GameObject _sandHitEffect;
    [SerializeField] private GameObject _shootEffect;

    public int HitsCountInCollection => _hitsCountInCollection;
    public int ShootDamage => _shootDamage;
    public int HitImpulseForce => _hitImpulseForce;
    public int MaxMagazineAmmoCount => _maxMagazineAmmoCount;
    public float ShootFlashTime => _shootFlashTime;
    public GameObject WeaponPrefab => _weaponPrefab;
    public GameObject BodyHitEffect => _bodyHitEffect;
    public GameObject SandHitEffect => _sandHitEffect;
    public GameObject ShootEffect => _shootEffect;
}
