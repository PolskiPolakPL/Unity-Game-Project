using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class UnitScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Unit unit;
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        AiSensor aiSensor = agent.GetComponent<AiSensor>();
        agent.speed = unit.speed;
        aiSensor.viewRange = unit.attackRange;
    }
    void Start()
    {
        Instantiate(unit.unitPrefab,transform.position,Quaternion.identity,transform);
        UnitSelection.Instance.unitsList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitsList.Remove(this.gameObject);
        if (UnitSelection.Instance.unitsSelected.Contains(this.gameObject))
            UnitSelection.Instance.unitsSelected.Remove(this.gameObject);
    }
}