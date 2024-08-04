using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMarker : MonoBehaviour
{
    [SerializeField] UnitSpawner spawner;
    [SerializeField] KeyCode triggerKey;
    // Start is called before the first frame update

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            MoveUnits();
        }
    }

    void MoveUnits()
    {
        foreach (Transform unit in spawner.unitsParent)
        {
            if (unit.IsDestroyed() || unit.GetComponent<UnitScript>().unitType != spawner.UnitType)
                continue;
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(transform.position);
        }
    }

}
