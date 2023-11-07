using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ResourcesManagerScript : MonoBehaviour
{
    [SerializeField] int maxHP = 500;
    [SerializeField] Slider playerHpBar;
    [SerializeField] Slider enemyHpBar;
    [SerializeField] TMP_Text playerHpTMP, enemyHpTMP;
    public int friendlyPoints, enemyPoints;
    public UnityEvent winEvent;
    public UnityEvent loseEvent;
    bool hasGameEnded = false;
    int enemyHP, playerHP;
    float timer;

    private void Start()
    {
        friendlyPoints = 0;
        enemyPoints = 0;
        playerHP = maxHP;
        playerHpBar.value = playerHP;
        enemyHP = maxHP;
        enemyHpBar.value = enemyHP;
        playerHpTMP.text = maxHP.ToString();
        enemyHpTMP.text = maxHP.ToString();
    }
    void Update()
    {
        friendlyPoints = 0;
        enemyPoints = 0;
        foreach(Transform resourcePoint in transform)
        {
            InfluenceState influenceState = resourcePoint.GetComponent<ObjectiveInfluenceScript>().influenceState;
            if(influenceState == InfluenceState.FRIENDLY)
            {
                friendlyPoints++;
            }
            else if(influenceState == InfluenceState.HOSTILE)
            {
                enemyPoints++;
            }
        }
        if (timer < 1)
            timer += Time.deltaTime;
        else
        {
            if(friendlyPoints > enemyPoints)
            {
                enemyHP -= friendlyPoints-enemyPoints;
                enemyHpBar.value = (float)enemyHP/(float)maxHP;
                enemyHpTMP.text = enemyHP.ToString();
            }
            else if(enemyPoints > friendlyPoints)
            {
                playerHP -= enemyPoints-friendlyPoints;
                playerHpBar.value = (float)playerHP/(float)maxHP;
                playerHpTMP.text = playerHP.ToString();
            }
            timer = 0;
        }
        if (!hasGameEnded)
        {
            if(enemyHP <= 0)
            {
                hasGameEnded = true;
                winEvent.Invoke();
                Time.timeScale = 0;
            }
            else if(playerHP <= 0)
            {
                hasGameEnded = true;
                loseEvent.Invoke();
                Time.timeScale = 0;
            }
        }
    }
}
