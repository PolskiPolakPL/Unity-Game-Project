using UnityEngine;
using UnityEngine.AI;

public class BarracksScript : MonoBehaviour
{
    [SerializeField] PlayerScript PlayerScript;
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
        if (PlayerScript.money >= conscript.cost && population < PlayerScript.popCap)
        {
            PlayerScript.SpendMoney(conscript.cost);
            PlayerUiManagerScript.Instance.UpdateMoney();
            GameObject unit = Instantiate(conscriptPrefab,transform.position,Quaternion.identity, parent);
            PlayerUiManagerScript.Instance.UpdatePopulation();
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
    public void recruitSniper()
    {
        if(PlayerScript.money >= sniper.cost && population < PlayerScript.popCap)
        {
            PlayerScript.SpendMoney(sniper.cost);
            PlayerUiManagerScript.Instance.UpdateMoney();
            GameObject unit = Instantiate(sniperPrefab, transform.position, Quaternion.identity, parent);
            PlayerUiManagerScript.Instance.UpdatePopulation();
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
    public void recruitHeavy()
    {
        if(PlayerScript.money >= heavy.cost && population < PlayerScript.popCap)
        {
            PlayerScript.SpendMoney(heavy.cost);
            PlayerUiManagerScript.Instance.UpdateMoney();
            GameObject unit = Instantiate(heavyPrefab, transform.position, Quaternion.identity, parent);
            PlayerUiManagerScript.Instance.UpdatePopulation();
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
        }
    }
}