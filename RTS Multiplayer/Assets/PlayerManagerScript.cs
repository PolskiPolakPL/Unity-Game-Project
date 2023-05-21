using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerManagerScript : MonoBehaviour
{
    private static PlayerManagerScript instance;
    public static PlayerManagerScript Instance { get { return instance; } }
    public Player player;
    public TMP_Text populationTMP, moneyTMP, expTMP, timer;
    int minutes = 0, seconds = 0;
    [SerializeField] Transform units;
    public int population;
    public int exp = 0;

    private void Awake()
    {
        //je¿eli istnieje instancja tej klasy, zniszcz tê instancjê
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        player.budget = 0;
        player.experience = 0;
        population = units.childCount;
        populationTMP.text = $"{population}/{player.popCap}";
        moneyTMP.text = $"{player.budget}$";
        expTMP.text = $"{exp} exp";
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerCoroutine());
        StartCoroutine(passiveIncomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (population != units.childCount)
        {
            population = units.childCount;
            populationTMP.text = $"{population}/{player.popCap}";
        }
        refreshMoney();
    }
    IEnumerator TimerCoroutine()
    {
        string min="00", sec="00";
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            if(seconds > 59)
            {
                minutes++;
                seconds = 0;
            }
            if (seconds < 10)
            {
                sec="0"+seconds.ToString();
            }
            else
            {
                sec = seconds.ToString();
            }
            if(minutes < 10)
            {
                min="0"+minutes.ToString();
            }
            else
            {
                min=minutes.ToString();
            }
            timer.text = min + ":" + sec;
        }
        
    }

    IEnumerator passiveIncomeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(player.incomeTime);
            player.budget += player.income;
        }
        
    }
    void refreshMoney()
    {
        moneyTMP.text = $"{player.budget}$";
    }
}
