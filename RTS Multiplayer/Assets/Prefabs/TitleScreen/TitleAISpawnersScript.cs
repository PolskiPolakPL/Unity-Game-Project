using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitleAISpawnersScript : MonoBehaviour
{
    [SerializeField] GameObject unit;
    [SerializeField] Transform target;
    [SerializeField] List<Transform> spawners = new List<Transform>();
    [SerializeField] int maxUnits=1;
    NavMeshAgent agent;
    Transform spawner;
    public float timer = 0;
    private void Update()
    {
        if (timer < 1)
            timer += Time.deltaTime;
        else if (transform.childCount < maxUnits)
        {
            spawner = spawners[Random.Range(0, spawners.Count)];
            unit = Instantiate(unit, spawner.position, Quaternion.identity, transform);
            agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
            timer = 0;
        }
    }
}
