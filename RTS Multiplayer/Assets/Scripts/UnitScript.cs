using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class UnitScript : MonoBehaviour
{
    public Unit unit;
    public GameObject model;
    public bool isDead = false;

    NavMeshAgent agent;
    [SerializeField] int currentHealth;
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        AiSensor aiSensor = agent.GetComponent<AiSensor>();
        agent.speed = unit.speed;
        aiSensor.viewRange = unit.longRange;
    }
    void Start()
    {
        Instantiate(model,transform.position,Quaternion.identity,transform);
        if(gameObject.layer==7)
            UnitSelection.Instance.unitsList.Add(this.gameObject);
        currentHealth = unit.health;
    }

    private void OnDestroy()
    {
        if (UnitSelection.Instance.unitsList.Contains(this.gameObject))
            UnitSelection.Instance.unitsList.Remove(this.gameObject);
        if (UnitSelection.Instance.unitsSelected.Contains(this.gameObject))
            UnitSelection.Instance.unitsSelected.Remove(this.gameObject);
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            KillYourself();
        }
    }
    void KillYourself()
    {
        isDead = true;
        Destroy(gameObject, 1);
    }
}