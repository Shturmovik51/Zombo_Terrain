using UnityEngine;

[System.Serializable]
public struct Buff
{
    [SerializeField] private int _id;
    [SerializeField] private int _bonusValue;
    [SerializeField] private int _duration;
    [SerializeField] private BuffType _type; 

    public int ID => _id;
    public int BonusValue => _bonusValue;
    public BuffType Type => _type;

    public int Duration { get => _duration; set => _duration = value; }
}
