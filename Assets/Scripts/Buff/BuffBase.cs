using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "New Buff Database", menuName = "Database / Buffs")]

    public class BuffBase : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<BuffSample> _buffSamples;
        [SerializeField] private BuffSample _currentBuffSample;
        private int _currentNumberInArray;

        public List<BuffSample> BuffSamples => _buffSamples;

        public void CreateItem()
        {
            if (_buffSamples == null)
                _buffSamples = new List<BuffSample>();
            BuffSample buffSample = new BuffSample();
            _buffSamples.Add(buffSample);
            _currentBuffSample = buffSample;
            _currentNumberInArray = _buffSamples.Count - 1;
        }

        public void RemoveItem()
        {
            if (_buffSamples == null)
                return;
            if (_buffSamples.Count > 1)
            {
                _buffSamples.Remove(_currentBuffSample);

                if (_currentNumberInArray > 0)
                    _currentNumberInArray--;
                else
                    _currentNumberInArray = 0;

                _currentBuffSample = _buffSamples[_currentNumberInArray];
            }

            else
            {
                _buffSamples.Remove(_currentBuffSample);
                CreateItem();
            }
        }

        public void NextItem()
        {
            if (_buffSamples.Count > _currentNumberInArray + 1)
            {
                _currentNumberInArray++;
                _currentBuffSample = _buffSamples[_currentNumberInArray];
            }
        }


        public void PrevItem()
        {
            if (_currentNumberInArray > 0)
            {
                _currentNumberInArray--;
                _currentBuffSample = _buffSamples[_currentNumberInArray];
            }
        }

        public BuffSample GetItemOfID(int id)
        {
            return _buffSamples.Find(buff => buff.ID == id);
        }

    }
}
