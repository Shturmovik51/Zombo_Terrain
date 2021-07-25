using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private IKcontroller ikController;
    private List<Transform> targets;
    private Transform nearestTarget;
    private void Start()
    {
        targets = new List<Transform>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targets.Add(col.transform);

            if (targets.Count == 1)
                ikController.TargetObject = targets[0];

            if (targets.Count == 2)
                StartCoroutine(NearestTargetChooser());
        }        
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targets.Remove(col.transform);

            if (targets.Count == 0)
                ikController.TargetObject = null;

            if (targets.Count == 1)
            {
                StopAllCoroutines();
                ikController.TargetObject = targets[0];
            }
        }
    }
    
    private IEnumerator NearestTargetChooser()
    {
        var minDist = Mathf.Infinity;

        for (int i = 0; i < targets.Count; i++)
        {
            var tempDist = (targets[i].transform.position - transform.position).magnitude;
                        
            if(tempDist < minDist)
            {
                minDist = tempDist;
                nearestTarget = targets[i];
            }
        }

        ikController.TargetObject = nearestTarget;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(NearestTargetChooser());
        yield break;
    }
}
