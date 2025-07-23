using UnityEngine;

public class ObjectiveInfluenceScript : MonoBehaviour
{
    [SerializeField] float flagHeight = 10f;
    [SerializeField] GameObject flag, circle;

    [Header("Influance Materials")]
    [SerializeField] Material friendlyMaterial;
    [SerializeField] Material neutralMaterial;
    [SerializeField] Material hostileMaterial;

    [Header("Influance Statistics")]
    float influenceRadious = 5;
    [SerializeField] float maxInfuence = 100f;
    public InfluenceState influenceState;
    public float influence = 0f;
    [SerializeField] float drainAmount = 10f;

    [SerializeField] AiSensor sensor;
    float drain = 0f, newDrain=0;
    Material currentMaterial;

    private void Start()
    {
        influenceRadious = circle.GetComponent<CircleGenerator>().Rradious;
        sensor.viewRange = influenceRadious;
        flag.transform.position += new Vector3(0, flagHeight, 0);
        currentMaterial = neutralMaterial;
        influenceState = InfluenceState.NEUTRAL;
        flag.GetComponent<Renderer>().material = currentMaterial;
        circle.GetComponent<Renderer>().material = currentMaterial;
    }

    private void Update()
    {
        influence = Mathf.Clamp(influence - drain, -maxInfuence, maxInfuence);
        newDrain = 0;
        try
        {
            newDrain += CountUnits(LayerMask.NameToLayer("Hostile")) * drainAmount;
            newDrain -= CountUnits(LayerMask.NameToLayer("Friendly")) * drainAmount;
        }
        catch (MissingReferenceException) { }
        
        if(newDrain == 0)
        {
            switch (influenceState)
            {
                case InfluenceState.NEUTRAL:
                    {
                        if(influence > 0 && influence > drainAmount)
                        {
                            newDrain += drainAmount;
                        }
                        else if(influence < 0 && Mathf.Abs(influence) > drainAmount)
                        {
                            newDrain -= drainAmount;
                        }
                    }break;

                case InfluenceState.FRIENDLY:
                    {
                        newDrain -= drainAmount;
                    }break;

                case InfluenceState.HOSTILE:
                    {
                        newDrain += drainAmount;
                    }break;
            }
        }
        drain = newDrain * Time.deltaTime;
        SetFlag();
    }

    int CountUnits(LayerMask targetLayer)
    {
        int count = 0;
        foreach(GameObject unit in sensor.visibleObjects)
        {
            if(unit.layer == targetLayer)
                count++;
        }
        return count;
    }

    void SetFlag()
    {
        if(Mathf.Abs(influence) < maxInfuence/10)
        {
            currentMaterial = neutralMaterial;
            influenceState = InfluenceState.NEUTRAL;
        }
        else if (influence == maxInfuence)
        {
            currentMaterial = friendlyMaterial;
            influenceState = InfluenceState.FRIENDLY;
        }
        else if(influence == -maxInfuence)
        {
            influenceState = InfluenceState.HOSTILE;
            currentMaterial = hostileMaterial;
        }
        circle.GetComponent<Renderer>().material = currentMaterial;
        flag.GetComponent<Renderer>().material = currentMaterial;
        flag.transform.localPosition = new Vector3(-1,Mathf.Abs((influence / maxInfuence) * flagHeight), 0);
    }
}
