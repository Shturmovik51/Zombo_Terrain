using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private int damageValue;

    private void OnEnable()
    {
        gameObject.SetActive(true);
        StartCoroutine(HitTimer());
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (GameManager.instance.HealthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.instance.HealthContainer[col.gameObject];
            health.TakeDamage(damageValue);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (GameManager.instance.HealthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.instance.HealthContainer[col.gameObject];
            health.TakeDamage(damageValue);
        }
    }

    private IEnumerator HitTimer()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
