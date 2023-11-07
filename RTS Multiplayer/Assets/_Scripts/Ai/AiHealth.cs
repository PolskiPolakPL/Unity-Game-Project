using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float healthPoints)
    {
        health = Mathf.Min(health + healthPoints, maxHealth);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
