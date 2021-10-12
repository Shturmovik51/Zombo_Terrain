using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZomboTerrain
{
    public sealed class BuffTimerController : IUpdatable, IController
    {
        public UnityAction<Buff> OnRemoveBuff;
        private BuffTimerModel _buffTimerModel;
        private BuffTimerView _buffTimerView;
        public BuffTimerController(BuffTimerModel buffTimerModel, BuffTimerView buffTimerView)
        {
            _buffTimerModel = buffTimerModel;
            _buffTimerView = buffTimerView;
        }

        public void LocalUpdate(float deltaTime)
        {
            if (_buffTimerModel.IsActive)
            {
                var activeBuffs = _buffTimerModel.ActiveBuffs;

                for (int i = 0; i < activeBuffs.Count; i++)
                {
                    activeBuffs[i].Duration -= deltaTime;

                    if (activeBuffs[i].Duration <= 0)
                    {
                        activeBuffs[i].Method(-activeBuffs[i].BonusValue);
                        activeBuffs.Remove(activeBuffs[i]);
                    }
                }

                if (activeBuffs.Count == 0)
                    _buffTimerModel.IsActive = false;
            }
        }

        public void AddBuffToTimer(Buff buff)
        {            
            if (_buffTimerModel.ActiveBuffs == null)
                _buffTimerModel.ActiveBuffs = new List<Buff>();

            _buffTimerModel.ActiveBuffs.Add(buff);

            _buffTimerModel.IsActive = true;

            Debug.Log("Add");
        }
    }
}
