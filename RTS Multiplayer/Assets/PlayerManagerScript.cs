using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManagerScript : MonoBehaviour
{
    public Player player;
    public TMP_Text populationTMP, moneyTMP, expTMP, timer;
    int minutes = 0, seconds = 0;
    [SerializeField] Transform units;
    int population;
    int money = 0;
    int exp = 0;

    private void Awake()
    {
        population = units.childCount;
        populationTMP.text = $"{population}/{player.popCap}";
        moneyTMP.text = $"{money}$";
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
            money += player.income;
        }
        
    }
    void refreshMoney()
    {
        moneyTMP.text = $"{money}$";
    }
}