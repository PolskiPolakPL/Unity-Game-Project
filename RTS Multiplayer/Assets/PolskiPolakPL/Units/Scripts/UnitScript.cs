using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class UnitScript : MonoBehaviour
{
    public Unit unit;
    public int currentHealth;
    public UnitType unitType;
    [SerializeField] FloatingBarScript healthBar;
    [SerializeField] GameObject rangeCircle;

    NavMeshAgent agent;
    void Start()
    {
        //AgentAI
        agent = gameObject.GetComponent<NavMeshAgent>();
        AiSensor aiSensor = agent.GetComponent<AiSensor>();
        agent.speed = unit.speed;
        aiSensor.viewRange = unit.attackRange;
        agent.stoppingDistance = 0.5f;
        //Health
        healthBar = GetComponentInChildren<FloatingBarScript>();
        currentHealth = unit.health;
        healthBar.UpdateBarValue(currentHealth, unit.health);
        healthBar.gameObject.SetActive(false);
        //Other
        unitType = unit.unitType;
        rangeCircle.GetComponent<CircleGenerator>().Rradious = unit.attackRange;

        //For Friendly Units Only
        if (UnitSelection.Instance!=null && gameObject.layer==7)
            UnitSelection.Instance.unitsList.Add(this.gameObject);
    }


    private void Update()
    {
        //if(agent.remainingDistance <= agent.stoppingDistance && !agent.isStopped)
        //{
            //agent.isStopped = true;
            //Debug.Log("HALT!");
        //}
    }

    private void OnDestroy()
    {
        if (UnitSelection.Instance == null)
            return;
        if (UnitSelection.Instance.unitsList.Contains(this.gameObject))
            UnitSelection.Instance.unitsList.Remove(this.gameObject);
        if (UnitSelection.Instance.unitsSelected.Contains(this.gameObject))
            UnitSelection.Instance.unitsSelected.Remove(this.gameObject);
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            if(currentHealth == unit.health)
                healthBar.gameObject.SetActive(true);
            currentHealth -= damage;
            healthBar.UpdateBarValue(currentHealth,unit.health);
        }
        else
        {
            KillYourself();
        }
    }
    void KillYourself()
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        rangeCircle.SetActive(true);
    }
    private void OnMouseExit()
    {
        rangeCircle.SetActive(false);
    }
}