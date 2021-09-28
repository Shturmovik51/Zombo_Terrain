using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class HW2 : MonoBehaviour
{
    [SerializeField] private int a;
    [SerializeField] private int b;

    private void Start()
    {
        Reverce();
    }

    public void Reverce()
    {
        Log($"a = {a}, b = {b}");

        a = a + b;
        b = a - b;
        a = a - b;

        Log($"a = {a}, b = {b}");
    }
}
