using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    //UI declarations
    [SerializeField] PlayerScript playerScript;
    [SerializeField] PlayerScript enemyScript;
    [Header("Timer")]
    [SerializeField] TMP_Text timer;
    private int _minutes = 0, _seconds = 0;
    [Header("Health")]
    [SerializeField] TMP_Text playerHpTMP;
    [SerializeField] Slider playerHpBar;
    [SerializeField] TMP_Text enemyHpTMP;
    [SerializeField] Slider enemyHpBar;
    [Header("Resources")]
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text populationTMP;
    [SerializeField] TMP_Text friendlyFlagTMP;
    [SerializeField] TMP_Text enemyFlagTMP;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePopulation();
        UpdateMoney();
        UpdatePlayerHP();
        if (enemyScript != null)
            UpdateEnemyPlayerHP();
        if (timer != null)
            StartCoroutine(TimerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopulation();
        UpdateMoney();
        if (enemyScript != null)
            UpdateEnemyPlayerHP();
        UpdatePlayerHP();
        UpdateFlags();
    }
    public void UpdatePlayerHP()
    {
        if (playerHpBar != null)
            playerHpBar.value = (float)playerScript.hp / (float)playerScript.maxHp;
        if (enemyHpTMP != null)
            playerHpTMP.text = playerScript.hp.ToString();
    }
    public void UpdateEnemyPlayerHP()
    {
        if (enemyHpBar != null)
            enemyHpBar.value = (float)enemyScript.hp / (float)enemyScript.maxHp;
        if (moneyTMP != null)
            enemyHpTMP.text = enemyScript.hp.ToString();
    }
    public void UpdateMoney()
    {
        if (moneyTMP != null)
            moneyTMP.text = $"{playerScript.money}$";
    }
    public void UpdateFlags()
    {
        if (friendlyFlagTMP)
            friendlyFlagTMP.text = GameManager.Instance.gameObject.GetComponent<VictoryPointsManagerScript>().FriendlyPoints.ToString();
        if (enemyFlagTMP)
            enemyFlagTMP.text = GameManager.Instance.gameObject.GetComponent<VictoryPointsManagerScript>().EnemyPoints.ToString();
    }
    public void UpdatePopulation()
    {
        if (populationTMP != null)
            populationTMP.text = $"{playerScript.population}/{playerScript.popCap}";
    }
    IEnumerator TimerCoroutine()
    {
        string min = "00", sec = "00";
        while (true)
        {
            yield return new WaitForSeconds(1);
            _seconds++;
            if (_seconds > 59)
            {
                _minutes++;
                _seconds = 0;
            }
            if (_seconds < 10)
            {
                sec = "0" + _seconds.ToString();
            }
            else
            {
                sec = _seconds.ToString();
            }
            if (_minutes < 10)
            {
                min = "0" + _minutes.ToString();
            }
            else
            {
                min = _minutes.ToString();
            }
            timer.text = min + ":" + sec;
        }

    }
}
