using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public UnityAction deathEntity;
    public UnityAction<int> updateHP;
    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value;} }
    
    private void Start()
    {
        GameManager.instance.healthContainer.Add(gameObject, this);
        currentHealth = maxHealth;
    }    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        updateHP?.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            GameManager.instance.healthContainer.Remove(gameObject);
            deathEntity?.Invoke();
            //Destroy(this);
        }
    }
    public void HealthUp(int bonusHealth)
    {
        currentHealth += bonusHealth;
        updateHP?.Invoke(currentHealth);

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
