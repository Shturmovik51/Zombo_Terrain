using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Dictionary<GameObject, Health> healthContainer;       
    public Dictionary<GameObject, Health> HealthContainer {get => healthContainer;  set => healthContainer = value;}

    //private Dictionary<GameObject, Buff> buffContainer;
    //public Dictionary<GameObject, Buff> BuffContainer { get => buffContainer; set => buffContainer = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
       
        healthContainer = new Dictionary<GameObject, Health>();
    }   
}
