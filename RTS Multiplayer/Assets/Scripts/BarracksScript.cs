using UnityEngine;
using UnityEngine.AI;

public class BarracksScript : MonoBehaviour
{
    public Transform  target, parent;
    public GameObject conscriptPrefab, sniperPrefab, heavyPrefab;
    NavMeshAgent agent;
    public void recruitConscript()
    {
        GameObject unit = Instantiate(conscriptPrefab,transform.position,Quaternion.identity, parent);
        agent = unit.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
    public void recruitSniper()
    {
        GameObject unit = Instantiate(sniperPrefab, transform.position, Quaternion.identity, parent);
        agent = unit.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
    public void recruitHeavy()
    {
        GameObject unit = Instantiate(heavyPrefab, transform.position, Quaternion.identity, parent);
        agent = unit.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}