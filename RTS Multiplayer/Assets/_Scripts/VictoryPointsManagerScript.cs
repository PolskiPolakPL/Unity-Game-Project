using UnityEngine;
using UnityEngine.Events;

public class VictoryPointsManagerScript : MonoBehaviour
{
    [SerializeField] Transform dominationPointsParent;
    [SerializeField] int advantageMultiplayer = 1;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] PlayerAIScript enemyPlayerScript;
    [SerializeField] private int _friendlyPoints, _enemyPoints;
    public UnityEvent winEvent;
    public UnityEvent defeatEvent;
    private ObjectiveInfluenceScript[] _objectiveScripts;
    float _timer;
    private bool _hasGameEnded;
    // Start is called before the first frame update
    void Start()
    {
        _objectiveScripts = new ObjectiveInfluenceScript[dominationPointsParent.childCount];
        for(int i = 0; i < _objectiveScripts.Length; i++)
        {
            _objectiveScripts[i] = dominationPointsParent.GetChild(i).gameObject.GetComponent<ObjectiveInfluenceScript>();
        }
        _timer = 0;
        _hasGameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        AssignPoints();
        if (_timer < 1)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            CalculateAdvantage();
            _timer = 0;
        }
        
    }

    void AssignPoints()
    {
        _friendlyPoints = 0;
        _enemyPoints = 0;
        foreach(ObjectiveInfluenceScript objectiveScript in _objectiveScripts)
        {
            if(objectiveScript.influenceState == InfluenceState.FRIENDLY)
            {
                _friendlyPoints++;
            }
            else if(objectiveScript.influenceState == InfluenceState.HOSTILE)
            {
                _enemyPoints++;
            }
        }
    }

    void CalculateAdvantage()
    {
        if (_friendlyPoints > _enemyPoints)
        {
            UsePlayerAdvantage();
        }
        else if (_enemyPoints > _friendlyPoints)
        {
            UseEnemyAdvantage();
        }
        if (!_hasGameEnded)
        {
            if (enemyPlayerScript.hp <= 0)
            {
                HandleVictoryEvent();
            }
            else if (playerScript.hp <= 0)
            {
                HandleDefeatEvent();
            }
        }
    }

    void UsePlayerAdvantage()
    {
        int advantage = (_friendlyPoints - _enemyPoints)*advantageMultiplayer;
        enemyPlayerScript.LooseHealth(advantage);
    }
    void UseEnemyAdvantage()
    {
        int advantage = (_enemyPoints - _friendlyPoints) * advantageMultiplayer;
        playerScript.LooseHealth(advantage);
    }

    public void HandleVictoryEvent()
    {
        _hasGameEnded = true;
        winEvent.Invoke();
        Time.timeScale = 0;
    }
    public void HandleDefeatEvent()
    {
        _hasGameEnded = true;
        defeatEvent.Invoke();
        Time.timeScale = 0;
    }
}
