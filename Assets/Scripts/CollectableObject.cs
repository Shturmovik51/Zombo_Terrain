using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject: MonoBehaviour, ICollectable
{
    [SerializeField] private BuffType _buffType;
    [SerializeField] private int _bonusValue;
    [SerializeField] private int _duration;

    private Buff _buff;
    public Buff Buff => _buff;

    private void Start()
    {
        _buff = new Buff(_buffType, _bonusValue, _duration);
    }

    public void SpecialDestroy()
    {
        Destroy(this);
    }
}