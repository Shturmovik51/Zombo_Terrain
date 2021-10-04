using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffTimerController
{
    public UnityAction<Buff> OnRemoveBuff;
    private BuffTimerModel _buffTimerModel;
    private BuffTimerView _buffTimerView;
    public BuffTimerController(BuffTimerModel buffTimerModel, BuffTimerView buffTimerView)
    {
        _buffTimerModel = buffTimerModel;
        _buffTimerView = buffTimerView;
    }

    public void LocalUpdate()
    {
        if (_buffTimerModel.IsActive)
        {
            var activeBuffs = _buffTimerModel.ActiveBuffs;

            for (int i = 0; i < activeBuffs.Count; i++)
            {
                activeBuffs[i].Duration -= Time.deltaTime;

                if(activeBuffs[i].Duration <= 0)
                {
                    activeBuffs[i].Method(-activeBuffs[i].BonusValue);
                    activeBuffs.Remove(activeBuffs[i]);
                }
            }

            if (activeBuffs.Count == 0)
                _buffTimerModel.IsActive = false;

            //var activeBuffs = _buffTimerModel.ActiveBuffs;
            //var buffsToDelete = _buffTimerModel.BuffsToDelete;

            //if (activeBuffs.Count == 0)
            //{
            //    _buffTimerModel.IsActive = false;
            //    return;
            //}

            //foreach (var buff in activeBuffs)
            //{
            //    buff.Key.Duration -= Time.deltaTime;

            //    if(buff.Key.Duration <= 0)
            //    {
            //        buff.Key.BonusValue = -buff.Key.BonusValue;
            //        buff.Value[buff.Key.Type](buff.Key);
            //        buffsToDelete.Add(buff.Key);
            //    }
            //}

            //foreach (var buff in buffsToDelete)
            //{
            //    activeBuffs.Remove(buff);
            //}
        } 
    }   

    public void AddBuffToTimer(Buff buff)
    {
        //if (_buffTimerModel.ActiveBuffs == null)
        //    _buffTimerModel.ActiveBuffs = new Dictionary<Buff, Dictionary<BuffType, BuffMethods>>();

        if (_buffTimerModel.ActiveBuffs == null)
            _buffTimerModel.ActiveBuffs = new List<Buff>();

        _buffTimerModel.ActiveBuffs.Add(buff);

        _buffTimerModel.IsActive = true;

        Debug.Log("Add");
    }
}
