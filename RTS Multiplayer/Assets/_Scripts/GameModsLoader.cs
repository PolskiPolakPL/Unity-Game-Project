using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModsLoader : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider playerHPSlider;
    [SerializeField] Slider popCapSlider;
    [SerializeField] Slider moneySlider;
    [SerializeField] Slider passiveIncomeSlider;
    [SerializeField] Slider incomeRateSlider;
    private void Start()
    {
        playerHPSlider.value = playerStats.playerHP;
        popCapSlider.value = playerStats.popCap;
        moneySlider.value = playerStats.money;
        passiveIncomeSlider.value = playerStats.passiveIncome;
        incomeRateSlider.value = playerStats.passiveIncomeRate;
    }

    public void LoadGameMods()
    {
        playerStats.playerHP = (int)playerHPSlider.value;
        playerStats.popCap = (int)popCapSlider.value;
        playerStats.money = (int)moneySlider.value;
        playerStats.passiveIncome = (int)passiveIncomeSlider.value;
        playerStats.passiveIncomeRate = incomeRateSlider.value;
    }
}
