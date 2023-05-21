using UnityEngine;
using UnityEngine.AI;

public class BarracksScript : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Unit conscript, sniper, heavy;
    public Transform  target, parent;
    public GameObject conscriptPrefab, sniperPrefab, heavyPrefab;
    NavMeshAgent agent;
    int population;
    private void Update()
    {
        population = parent.childCount;
    }
    public void recruitConscript()
    {
        if (player.budget >= conscript.cost && population < player.popCap)
        {
            player.budget -= conscript.cost;
            GameObject unit = Instantiate(conscriptPrefab,transform.position,Quaternion.identity, parent);
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
    public void recruitSniper()
    {
        if(player.budget >= sniper.cost && population < player.popCap)
        {
            player.budget -= sniper.cost;
            GameObject unit = Instantiate(sniperPrefab, transform.position, Quaternion.identity, parent);
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
    public void recruitHeavy()
    {
        if(player.budget >= heavy.cost && population < player.popCap)
        {
            player.budget -= heavy.cost;
            GameObject unit = Instantiate(heavyPrefab, transform.position, Quaternion.identity, parent);
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
}