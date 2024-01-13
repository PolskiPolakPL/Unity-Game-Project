using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUiManagerScript : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        UpdatePopulation();
        UpdateMoney();
        UpdatePlayerHP();
        UpdateEnemyPlayerHP();
        StartCoroutine(TimerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopulation();
        UpdateMoney();
        UpdateEnemyPlayerHP();
        UpdatePlayerHP();
    }
    public void UpdatePlayerHP()
    {
        playerHpBar.value = (float)playerScript.hp / (float)playerScript.maxHp;
        playerHpTMP.text = playerScript.hp.ToString();
    }
    public void UpdateEnemyPlayerHP()
    {
        enemyHpBar.value = (float)enemyScript.hp / (float)enemyScript.maxHp;
        enemyHpTMP.text = enemyScript.hp.ToString();
    }
    public void UpdateMoney()
    {
        moneyTMP.text = $"{playerScript.money}$";
    }
    public void UpdateAmmo()
    {

    }
    public void UpdatePopulation()
    {
        populationTMP.text = $"{playerScript.population}/{playerScript.popCap}";
    }
    IEnumerator TimerCoroutine()
    {
        string min="00", sec="00";
        while (true)
        {
            yield return new WaitForSeconds(1);
            _seconds++;
            if(_seconds > 59)
            {
                _minutes++;
                _seconds = 0;
            }
            if (_seconds < 10)
            {
                sec="0"+_seconds.ToString();
            }
            else
            {
                sec = _seconds.ToString();
            }
            if(_minutes < 10)
            {
                min="0"+_minutes.ToString();
            }
            else
            {
                min=_minutes.ToString();
            }
            timer.text = min + ":" + sec;
        }
        
    }
}
