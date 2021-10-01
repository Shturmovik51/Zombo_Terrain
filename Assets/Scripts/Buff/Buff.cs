using UnityEngine;

[System.Serializable]
public class Buff
{
    [SerializeField] private int _id;
    [SerializeField] private int _bonusValue;
    [SerializeField] private float _duration;
    [SerializeField] private BuffType _type; 

    public Buff(int id, int bonusValue, float duration, BuffType type)
    {
        _id = id;
        _bonusValue = bonusValue;
        _duration = duration;
        _type = type;
    }

    public int ID => _id;
    public BuffType Type => _type;
    public int BonusValue { get => _bonusValue; set => _bonusValue = value; }
    public float Duration { get => _duration; set => _duration = value; }
}
