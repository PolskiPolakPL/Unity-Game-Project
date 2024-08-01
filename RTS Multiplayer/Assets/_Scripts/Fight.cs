using UnityEngine;
using PolskiPolakPL.Utils.Timer;

public class Fight : MonoBehaviour
{
    Unit unit;
    UnitScript unitScript, enemyUnitScript;
    GameObject enemy;
    AiTargetingSystem aiTargeting;
    float accuracy;
    AudioSource audioSource;
    Timer attackTimer;
    float deltaTime;

    private void Start()
    {
        unitScript = gameObject.GetComponent<UnitScript>();
        unit = unitScript.unit;
        accuracy = unit.accuracy;
        aiTargeting = gameObject.GetComponent<AiTargetingSystem>();
        audioSource = gameObject.GetComponent<AudioSource>();
        attackTimer = new Timer(unit.attackSpeed, true);
        attackTimer.OnTimerEnd += AimAndShoot;
        attackTimer.Tick(Random.Range(unit.attackSpeed, unit.attackSpeed - 0.1f));
    }
    private void Update()
    {
        enemy = aiTargeting.target;
        deltaTime = Time.deltaTime;
        if(enemy != null)
        {
            if (enemyUnitScript != enemy.GetComponent<UnitScript>())
            {
                enemyUnitScript = enemy.GetComponent<UnitScript>();
            }
            if (attackTimer.IsPaused)
            {
                attackTimer.Resume();
            }
        }
        else
        {
            enemyUnitScript = null;
        }
        attackTimer.Tick(deltaTime);
    }

    void AimAndShoot()
    {
        if (enemy == null)
        {
            attackTimer.Pause();
            attackTimer.RemaningSeconds = 0.01f;
            return;
        }
        float distance = Vector3.Magnitude(transform.position - enemy.transform.position);
        accuracy = ((unit.accuracy - 100)/unit.attackRange)*distance+100;
        audioSource.PlayOneShot(unit.fireSound);
        if(accuracy > Random.Range(0f, 100f))
        {
            enemyUnitScript.TakeDamage(unit.attackDamage);
            Debug.Log($"Shot {enemy.name} for {unit.attackDamage} and with accuracy {accuracy}%!");
        }
        else
        {
            Debug.Log("Miss!");
        }
    }

    private void OnDestroy()
    {
        attackTimer.OnTimerEnd -= AimAndShoot;
    }
}
