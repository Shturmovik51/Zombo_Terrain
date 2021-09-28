using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject: MonoBehaviour, ICollectable
{
    [SerializeField] private int _buffID;

    private Buff _buff;    
    private List<BuffSample> _buffCollection;

    public Buff Buff { get => _buff; }

    private void Awake()
    {
        _buffCollection = Resources.Load<BuffBase>("DataBase/Buff Database").BuffSamples;

        for (int i = 0; i < _buffCollection.Count; i++)
        {
            if (_buffCollection[i].ID == _buffID)
            {
                _buff = new Buff(_buffCollection[i].ID, _buffCollection[i].BonusValue, _buffCollection[i].Duration, _buffCollection[i].Type);                
            }
        }
    }    

    public void SpecialDestroy()
    {

        Destroy(this.gameObject);
    }
    
}