using UnityEngine;

public class Fight : MonoBehaviour
{
    Unit unit;
    UnitScript unitScript, enemyUnitScript;
    GameObject enemy;
    AiTargetingSystem aiTargeting;
    float timer = 0, accuracy;
    AudioSource audioSource;

    private void Start()
    {
        unitScript = gameObject.GetComponent<UnitScript>();
        unit = unitScript.unit;
        aiTargeting = gameObject.GetComponent<AiTargetingSystem>();
        audioSource = gameObject.GetComponent<AudioSource>();
        accuracy = unit.accuracy;
        timer = Random.Range(unit.attackSpeed, unit.attackSpeed - 0.1f);
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
            if(timer < unit.attackSpeed)
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
        float distance = Vector3.Magnitude(transform.position - enemy.transform.position);
        audioSource.PlayOneShot(unit.fireSound);
        accuracy = ((unit.accuracy - 100)/unit.attackRange)*distance+100;
        if(accuracy > Random.Range(0f, 100f))
        {
            enemyUnitScript.TakeDamage(unit.attackDamage);
            Debug.Log($"Shot {enemy.name} for {unit.attackDamage} and with accuracy {accuracy}%!");
        }
        else
        {
            Debug.Log("Miss!");
        }
        timer = 0;
    }
}
