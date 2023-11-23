using UnityEngine;

public class ObjectiveInfluenceScript : MonoBehaviour
{
    [SerializeField] float flagHeight = 10f;
    [SerializeField] GameObject flag, selectionCircle;

    [Header("Influance Materials")]
    [SerializeField] Material friendlyMaterial;
    [SerializeField] Material neutralMaterial;
    [SerializeField] Material hostileMaterial;

    [Header("Influance Statistics")]
    [SerializeField] float influenceRadious = 5;
    [SerializeField] float maxInfuence = 100f;
    public InfluenceState influenceState;
    public float influence = 0f;
    [SerializeField] float drainAmount = 10f;
    [SerializeField] Transform friendlyUnits;
    [SerializeField] Transform enemyUnits;

    float drain = 0f, newDrain=0;
    Material currentMaterial;

    private void Awake()
    {
        selectionCircle.transform.localScale = new Vector3(influenceRadious, 1, influenceRadious);
    }

    private void Start()
    {
        flag.transform.position += new Vector3(0, flagHeight, 0);
        currentMaterial = neutralMaterial;
        influenceState = InfluenceState.NEUTRAL;
        flag.GetComponent<Renderer>().material = currentMaterial;
        selectionCircle.GetComponent<Renderer>().material = currentMaterial;
    }

    private void Update()
    {
        influence = Mathf.Clamp(influence - drain, -maxInfuence, maxInfuence);
        newDrain = 0;
        foreach(Transform enemy in enemyUnits)//change theese loops into OnColisionEnter & OnCollisionExit events?
        {
            if(Vector3.Distance(transform.position, enemy.position) <= influenceRadious)
            {
                newDrain += drainAmount;
            }
        }
        foreach (Transform friendly in friendlyUnits)
        {
            if (Vector3.Distance(transform.position, friendly.position) <= influenceRadious)
            {
                newDrain -= drainAmount;
            }
        }
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
        selectionCircle.GetComponent<Renderer>().material = currentMaterial;
        flag.GetComponent<Renderer>().material = currentMaterial;
        flag.transform.localPosition = new Vector3(-1,Mathf.Abs((influence / maxInfuence) * flagHeight), 0);
    }
}
