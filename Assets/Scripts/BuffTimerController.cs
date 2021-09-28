using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffTimerController : MonoBehaviour
{
    private BuffTimerModel _buffTimerModel;
    private BuffTimerView _buffTimerView;

    public UnityAction<Buff> OnRemoveBuff;

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

            if (activeBuffs.Count == 0)
                _buffTimerModel.IsActive = false;

            for (int i = 0; i < activeBuffs.Count; i++)
            {
                activeBuffs[i].Duration -= Time.deltaTime;

                Debug.Log(activeBuffs[i].Duration);

                if (activeBuffs[i].Duration <= 0)
                {
                    OnRemoveBuff?.Invoke(activeBuffs[i]);
                    activeBuffs.Remove(activeBuffs[i]);
                }
            }
        }
    }

    public void AddBuffToTimer(Buff buff)
    {
        if (_buffTimerModel.ActiveBuffs == null)
            _buffTimerModel.ActiveBuffs = new List<Buff>();

        _buffTimerModel.ActiveBuffs.Add(buff);

        _buffTimerModel.IsActive = true;
    }
}
