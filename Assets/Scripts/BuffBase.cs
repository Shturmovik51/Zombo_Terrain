using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Database", menuName = "Database / Buffs")]

public class BuffBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Buff> _buffs;
    [SerializeField] private Buff _currentBuff;
    private int _currentNumberInArray;

    public List<Buff> Buffs { get => _buffs; }

    public void CreateItem()
    {
        if (_buffs == null)
            _buffs = new List<Buff>();
        Buff buff = new Buff();
        _buffs.Add(buff);
        _currentBuff = buff;
        _currentNumberInArray = _buffs.Count - 1;
    }

    public void RemoveItem()
    {
        if (_buffs == null)
            return;
        //if (_currentBuff == null)
        //    return;
        if (_buffs.Count > 1)
        {
            _buffs.Remove(_currentBuff);

            if (_currentNumberInArray > 0)
                _currentNumberInArray--;
            else
                _currentNumberInArray = 0;

            _currentBuff = _buffs[_currentNumberInArray];
        }

        else
        {
            _buffs.Remove(_currentBuff);
            CreateItem();
        }
    }

    public void NextItem()
    {
        if (_buffs.Count > _currentNumberInArray + 1)
        {
            _currentNumberInArray++;
            _currentBuff = _buffs[_currentNumberInArray];
        }
    }


    public void PrevItem()
    {
        if (_currentNumberInArray > 0)
        {
            _currentNumberInArray--;
            _currentBuff = _buffs[_currentNumberInArray];
        }
    }

    public Buff GetItemOfID(int id)
    {
        return _buffs.Find(buff => buff.ID == id);
    }

}
