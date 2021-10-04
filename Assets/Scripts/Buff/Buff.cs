using UnityEngine;

public class Buff
{
    private int _id;
    private int _bonusValue;
    private float _duration;
    private BuffType _type;
    public BuffMethods Method;

    public Buff(int id, int bonusValue, float duration, BuffType type /*BuffMethods method*/)
    {
        _id = id;
        _bonusValue = bonusValue;
        _duration = duration;
        _type = type;
        //_method = method;
    }

    public int ID => _id;
    public BuffType Type => _type;
    public int BonusValue { get => _bonusValue; set => _bonusValue = value; }
    public float Duration { get => _duration; set => _duration = value; }
}
