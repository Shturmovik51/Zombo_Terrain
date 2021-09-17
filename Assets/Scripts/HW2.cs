using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class HW2 : MonoBehaviour
{
    private int a = 5;
    private int b = 10;

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
