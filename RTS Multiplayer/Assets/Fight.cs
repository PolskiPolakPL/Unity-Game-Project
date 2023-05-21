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
        if(unit.accuracy > Random.Range(0f, 1f))
        {
            enemyUnitScript.TakeDamage(unit.attackDamage);
            Debug.Log($"Shot {enemy.name} for {unit.attackDamage}! \t Now he has {enemyUnitScript.currentHealth}HP!");
        }
        else
        {
            Debug.Log("Miss!");
        }
        timer = 0;
    }
}
