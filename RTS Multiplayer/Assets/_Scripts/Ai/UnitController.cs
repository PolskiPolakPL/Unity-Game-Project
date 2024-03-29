using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitController : MonoBehaviour
{
    Camera cam;
    Ray ray;
    RaycastHit hit;
    NavMeshAgent agent;
    private void Start()
    {
        cam = Camera.main;
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                //Debug.Log(agent.remainingDistance);
                //agent.isStopped = false;
            }
        }
    }
}
