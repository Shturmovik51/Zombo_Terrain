using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<GameObject, Health> healthContainer;   
    public GUIwork gUIwork;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        gUIwork = GetComponent<GUIwork>();
        healthContainer = new Dictionary<GameObject, Health>();
    }   
}
