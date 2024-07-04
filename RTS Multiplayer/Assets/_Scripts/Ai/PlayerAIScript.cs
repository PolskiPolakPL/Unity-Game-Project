using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerScript))]
public class PlayerAIScript : MonoBehaviour
{
    [SerializeField] PlayerScript playerScript;
    [SerializeField] Transform unitsParent;
    [SerializeField] Transform victoryPoints;
    [SerializeField] Transform sniperTargets;
    float unitStoppingDistance = 2;
    int remaningPopulation;
    [SerializeField] List<Transform> availableUnitsList = new List<Transform>();
    [SerializeField] List<Transform> buisyUnitsList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        remaningPopulation = CalculateRemaningPopulation();
        RemoveDeadUnits();
        GetAvailableUnits();
    }

    int CalculateRemaningPopulation()
    {
        return playerScript.popCap - playerScript.population;
    }

    void GetAvailableUnits()
    {
        UnitScript _unitscript;
        ObjectiveInfluenceScript _objectiveScript;
        //for each unit...
        foreach (Transform unitChildT in unitsParent)
        {
            _unitscript = unitChildT.GetComponent<UnitScript>();
            //if it's a Sniper...
            if(_unitscript.unitType == UnitType.SNIPER)
            {
                //for each sniper point...
                foreach(Transform sniperPoint in sniperTargets)
                {
                    //is already at the position...
                    if (Vector3.Distance(unitChildT.position, sniperPoint.position) <= unitStoppingDistance)
                    {
                        //is on 'available' list
                        if (availableUnitsList.Contains(unitChildT))
                        {
                            availableUnitsList.Remove(unitChildT);
                        }
                        //is NOT on 'buisy' list
                        if (!buisyUnitsList.Contains(unitChildT))
                        {
                            buisyUnitsList.Add(unitChildT);
                        }
                        break;
                    }
                    //is NOT at the position...
                    else
                    {
                        //is on 'buisy' list...
                        if (buisyUnitsList.Contains(unitChildT))
                        {
                            break;
                        }
                        //is NOT on 'available' list...
                        if (!availableUnitsList.Contains(unitChildT))
                        {
                            availableUnitsList.Add(unitChildT);
                        }
                    }
                }
            }
            //if it's not a Sniper...
            else
            {
                //for each victory point...
                foreach (Transform vpTarget in victoryPoints)
                {
                    _objectiveScript = vpTarget.GetComponent<ObjectiveInfluenceScript>();
                    //is already at the position...
                    if(Vector3.Distance(unitChildT.position, vpTarget.position) <= unitStoppingDistance)
                    {
                        //victory point is already captured
                        if(_objectiveScript.influenceState == InfluenceState.HOSTILE)
                        {
                            //is NOT conscript
                            if (_unitscript.unitType != UnitType.CONSCRIPT)
                            {
                                //is on 'available' list
                                if (availableUnitsList.Contains(unitChildT))
                                {
                                    availableUnitsList.Remove(unitChildT);
                                }
                                //is NOT on 'buisy' list
                                if (!buisyUnitsList.Contains(unitChildT))
                                {
                                    buisyUnitsList.Add(unitChildT);
                                }
                                break;
                            }
                            //IS conscript and is NOT on 'available' list
                            if (!availableUnitsList.Contains(unitChildT))
                            {
                                availableUnitsList.Add(unitChildT);
                            }
                            //IS conscript and is on 'buisy' list
                            if (buisyUnitsList.Contains(unitChildT))
                            {
                                buisyUnitsList.Remove(unitChildT);
                            }
                        }
                    }
                    //is NOT at the position...
                    else
                    {
                        //is on 'buisy' list...
                        if (buisyUnitsList.Contains(unitChildT))
                        {
                            break;
                        }
                        //is NOT on 'available' list...
                        if (!availableUnitsList.Contains(unitChildT))
                        {
                            availableUnitsList.Add(unitChildT);
                        }
                    }
                }
            }
        }
    }

    void RemoveDeadUnits()
    {
        foreach(Transform unit in availableUnitsList)
        {
            if (unit == null)
            {
                availableUnitsList.Remove(unit);
                return;
            }
        }
        foreach(Transform unit in buisyUnitsList)
        {
            if (unit == null)
            {
                availableUnitsList.Remove(unit);
                return;
            }
        }
    }
}
