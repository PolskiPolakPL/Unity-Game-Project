using UnityEngine;

[RequireComponent(typeof(UnitScript))]
public class DropRewardScript : MonoBehaviour
{
    UnitScript unitScript;
    [SerializeField][Range(0,2)] float rewardMultiplier = 0.8f;
    private void Start()
    {
        unitScript = GetComponent<UnitScript>();
    }
    private void OnDestroy()
    {
        int reward = (int)Mathf.Round(unitScript.unit.cost * rewardMultiplier);
        if(GameManager.Instance!=null)
            GameManager.Instance.playerScript.GainMoney(reward);
    }
}
