using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimerController : MonoBehaviour
{
    private BuffTimerModel _buffTimerModel;
    private BuffTimerView _buffTimerView;

    public BuffTimerView BuffTimerView { get => _buffTimerView; }

    public BuffTimerController(BuffTimerModel buffTimerModel, BuffTimerView buffTimerView)
    {
        _buffTimerModel = buffTimerModel;
        _buffTimerView = buffTimerView;
    }

    public void LocalUpdate()
    {
        


    }

    public void AddBuffToTimer(Buff buff)
    {
        
    }

    

}
