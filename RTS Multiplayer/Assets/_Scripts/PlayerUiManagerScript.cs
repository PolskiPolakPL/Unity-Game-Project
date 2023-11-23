using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUiManagerScript : MonoBehaviour
{
    //singleton declarations
    private static PlayerUiManagerScript instance;
    public static PlayerUiManagerScript Instance { get { return instance; } }

    //UI declarations
    [SerializeField] PlayerScript playerScript;
    [SerializeField] PlayerAIScript enemyScript;
    [Header("Timer")]
    [SerializeField] TMP_Text timer;
    private int _minutes = 0, _seconds = 0;
    [Header("Health")]
    [SerializeField] TMP_Text playerHpTMP;
    [SerializeField] Slider playerHpBar;
    [SerializeField] TMP_Text enemyHpTMP;
    [SerializeField] Slider enemyHpBar;
    [Header("Resources")]
    [SerializeField] float incomeTime = 1;
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text populationTMP;

    private void Awake()
    {
        //je¿eli istnieje inna instancja tej klasy, zniszcz tê instancjê
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePopulation();
        UpdateMoney();
        UpdateEnemyPlayerHP();
        UpdatePlayerHP();
        StartCoroutine(TimerCoroutine());
        StartCoroutine(passiveIncomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopulation();
        UpdateMoney();
    }
    public void UpdatePlayerHP()
    {
        playerHpBar.value = (float)playerScript.hp / (float)playerScript.maxHp;
        playerHpTMP.text = playerScript.hp.ToString();
    }
    public void UpdateEnemyPlayerHP()
    {
        enemyHpBar.value = (float)enemyScript.hp / (float)enemyScript.maxHp;
        Debug.Log($"Enemy HP: {(float)enemyScript.hp}/{(float)enemyScript.maxHp}hp");
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

    IEnumerator passiveIncomeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(incomeTime);
            playerScript.GainMoney(playerScript.income);
        }
        
    }
}
