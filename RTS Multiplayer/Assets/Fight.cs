using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    Unit unit;
    UnitScript unitScript, enemyUnitScript;
    GameObject enemy;
    AiTargetingSystem aiTargeting;
    float timer = 0;

    private void Awake()
    {
        unitScript = gameObject.GetComponent<UnitScript>();
        unit = unitScript.unit;
        aiTargeting = gameObject.GetComponent<AiTargetingSystem>();
    }
    private void Update()
    {
        enemy = aiTargeting.target;
        if(enemy != null)
        {
            if(enemyUnitScript != enemy.GetComponent<UnitScript>())
            {
                enemyUnitScript = enemy.GetComponent<UnitScript>();
            }
            if(timer < 60/unit.attackSpeed)
            {
                timer += Time.deltaTime;
            }
            else
            {
                GalaFighter();
            }
        }
        else
        {
            enemyUnitScript = null;
        }
    }
    void GalaFighter()
    {
        Debug.Log($"Shot {enemy.name} for {unit.attackDamage}! \t Now he has {enemyUnitScript.currentHealth}HP!");
        enemyUnitScript.TakeDamage(unit.attackDamage);
        timer = 0;
    }
}
