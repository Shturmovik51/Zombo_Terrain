using UnityEngine;

namespace ZomboTerrain
{
    [System.Serializable]
    public sealed class BuffSample
    {
        [SerializeField] private int _id;
        [SerializeField] private int _bonusValue;
        [SerializeField] private float _duration;
        [SerializeField] private BuffType _type;

        public int ID => _id;
        public int BonusValue => _bonusValue;
        public BuffType Type => _type;

        public float Duration { get => _duration; set => _duration = value; }
    }
}
