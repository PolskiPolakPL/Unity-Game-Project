using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
struct Squad
{
    public GameObject unit;
    public int squadSize;
    public float spawnCooldown;
    public Squad(GameObject unit, int squadSize, float spawnCooldown)
    {
        this.unit = unit;
        this.squadSize = squadSize;
        this.spawnCooldown = spawnCooldown;
    }
}

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] List<Squad> squadList = new List<Squad>();
    [SerializeField] Transform EnemyUnits;
    [SerializeField] Transform InfluencePoints;
    List<Transform> targetsList = new List<Transform> ();
    float timer = 0;
    float currentCooldown = 0;
    int squadIndex;
    NavMeshAgent agent;
    ObjectiveInfluenceScript objectiveScript;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            foreach(Transform target in InfluencePoints)
            {
                ScanTargets();
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("You forgot to reference targets");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < currentCooldown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if(EnemyUnits.childCount < 10)
            {
                ScanTargets();
                currentCooldown = SpawnRandomSquad();
                timer = 0;
            }
        }
    }

    Vector3 SelectRandomTarget()
    {
        if (targetsList != null && targetsList.Count > 0)
        {
            return targetsList[UnityEngine.Random.Range(0, targetsList.Count)].position;
        }
        else
        {
            Debug.LogError("Destination Not Found!");
            return Vector3.zero;
        }
    }
    GameObject SpawnSquadMember(GameObject memberPrefab)
    {
        return Instantiate(memberPrefab, transform.position, transform.rotation, EnemyUnits);
    }
    void SetSquadUnitsDestination(GameObject squadMember, Vector3 target)
    {
        if(targetsList != null && targetsList.Count > 0)
        {
            agent = squadMember.GetComponent<NavMeshAgent>();
            agent.SetDestination(target);
            agent.isStopped = false;
        }
    }
    float SpawnRandomSquad()
    {
        squadIndex = UnityEngine.Random.Range(0, squadList.Count);
        Squad currentSquad = squadList[squadIndex];
        Vector3 squadTarget = SelectRandomTarget();
        for(int i = 0; i < currentSquad.squadSize; i++)
        {
            GameObject unit = SpawnSquadMember(currentSquad.unit);
            SetSquadUnitsDestination(unit, squadTarget);
        }
        return currentSquad.spawnCooldown;
    }
    void ScanTargets()
    {
        foreach (Transform target in InfluencePoints)
        {
            objectiveScript = target.GetComponent<ObjectiveInfluenceScript>();
            if (objectiveScript.influenceState != InfluenceState.HOSTILE && !targetsList.Contains(target))
            {
                targetsList.Add(target);
            }
            else if(objectiveScript.influenceState == InfluenceState.HOSTILE && targetsList.Contains(target))
            {
                targetsList.Remove(target);
            }
        }
    }
}
