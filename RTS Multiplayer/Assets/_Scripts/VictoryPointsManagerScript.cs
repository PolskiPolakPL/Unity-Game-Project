using UnityEngine;

public class VictoryPointsManagerScript : MonoBehaviour
{
    [SerializeField] Transform dominationPointsParent;
    [SerializeField] int advantageMultiplayer = 1;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] PlayerScript enemyPlayerScript;
    [SerializeField] public int FriendlyPoints { get; private set; }
    [SerializeField] public int EnemyPoints { get; private set; }
    private ObjectiveInfluenceScript[] _objectiveScripts;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _objectiveScripts = new ObjectiveInfluenceScript[dominationPointsParent.childCount];
        for(int i = 0; i < _objectiveScripts.Length; i++)
        {
            _objectiveScripts[i] = dominationPointsParent.GetChild(i).gameObject.GetComponent<ObjectiveInfluenceScript>();
        }
        _timer = 0;
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
        FriendlyPoints = 0;
        EnemyPoints = 0;
        foreach(ObjectiveInfluenceScript objectiveScript in _objectiveScripts)
        {
            if(objectiveScript.influenceState == InfluenceState.FRIENDLY)
            {
                FriendlyPoints++;
            }
            else if(objectiveScript.influenceState == InfluenceState.HOSTILE)
            {
                EnemyPoints++;
            }
        }
    }

    void CalculateAdvantage()
    {
        if (FriendlyPoints > EnemyPoints)
        {
            UsePlayerAdvantage();
        }
        else if (EnemyPoints > FriendlyPoints)
        {
            UseEnemyAdvantage();
        }
        if (GameManager.Instance.currentGameState!=GameState.FINISHED)
        {
            if (enemyPlayerScript.hp <= 0)
            {
                enemyPlayerScript.hp = 0;
                HandleVictoryEvent();
            }
            else if (playerScript.hp <= 0)
            {
                playerScript.hp = 0;
                HandleDefeatEvent();
            }
        }
    }

    void UsePlayerAdvantage()
    {
        int advantage = (FriendlyPoints - EnemyPoints)*advantageMultiplayer;
        enemyPlayerScript.LooseHealth(advantage);
    }
    void UseEnemyAdvantage()
    {
        int advantage = (EnemyPoints - FriendlyPoints) * advantageMultiplayer;
        playerScript.LooseHealth(advantage);
    }

    public void HandleVictoryEvent()
    {
        Time.timeScale = 0;
        GameManager.Instance.currentGameState = GameState.FINISHED;
        GameManager.Instance.winEvent.Invoke();
    }
    public void HandleDefeatEvent()
    {
        Time.timeScale = 0;
        GameManager.Instance.currentGameState = GameState.FINISHED;
        GameManager.Instance.defeatEvent.Invoke();
    }
}
