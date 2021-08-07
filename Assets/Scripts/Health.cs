using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private UnityAction<int> updateHP;
    private UnityAction deathEntity;
    [SerializeField] private int currentHealth;

    public UnityAction<int> UpdateHP {get => updateHP;  set => updateHP = value;}
    public UnityAction DeathEntity {get => deathEntity; set => deathEntity = value;}
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;        
    }

    private void Start()
    {
        GameManager.instance.HealthContainer.Add(gameObject, this);
    }  

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        updateHP?.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            GameManager.instance.HealthContainer.Remove(gameObject);
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
