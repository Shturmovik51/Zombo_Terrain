using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObj: MonoBehaviour
{
    [SerializeField] private Buff buff;

    //private void Start()
    //{
    //    GameManager.instance.BuffContainer.Add(gameObject, buff);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.instance.AddBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public int additiveBonus;
    public int multipleBonus;
    public int duration;
}
public enum BuffType
{
    Speed, Jump
}