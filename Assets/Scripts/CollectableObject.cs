using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject: MonoBehaviour, ICollectable
{
    [SerializeField] private int _buffID;

    private Buff _buff;    
    private List<Buff> _buffCollection;

    public Buff Buff { get => _buff; }

    private void Awake()
    {
        _buffCollection = Resources.Load<BuffBase>("DataBase/Buff Database").Buffs;

        for (int i = 0; i < _buffCollection.Count; i++)
        {
            if (_buffCollection[i].ID == _buffID)
            {                
                _buff = _buffCollection[i]; // добавить новый класс
            }
        }
    }    

    public void SpecialDestroy()
    {

        Destroy(this.gameObject);
    }
    
}