using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class CollectableObject : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _buffID;

        public Action<Buff> OnApplyBuff;
        private List<BuffSample> _buffCollection;
        private Buff _buff;

        private void Awake()
        {
            _buffCollection = Resources.Load<BuffBase>("DataBase/Buff Database").BuffSamples;

            for (int i = 0; i < _buffCollection.Count; i++)
            {
                if (_buffCollection[i].ID == _buffID)
                {
                    _buff = new Buff(_buffCollection[i].ID, _buffCollection[i].BonusValue, _buffCollection[i].Duration,
                                        _buffCollection[i].Type);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnApplyBuff?.Invoke(_buff);
        }

        public void SpecialDestroy()
        {
            Destroy(gameObject);
        }
    }
}