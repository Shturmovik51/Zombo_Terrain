using System.Collections;
using UnityEngine;

public class BuffTimer
{
    public IEnumerator BuffTimeDuration(int time, int parameter)
    {
        yield return new WaitForSeconds(time);
        parameter = 0;
        Debug.Log("CorStop");
    }
}
