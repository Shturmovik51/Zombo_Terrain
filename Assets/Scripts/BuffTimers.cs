using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimers : MonoBehaviour
{
    private IEnumerator BuffTimeDuration(int time, int parameter)
    {
        yield return new WaitForSeconds(time);
        parameter = 0;
        Debug.Log("CorStop");
    }
}
