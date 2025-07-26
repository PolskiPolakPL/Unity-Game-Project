using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialContentController : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;

    int currentGameTipIndex = 0;

    List<GameObject> gameTips = new List<GameObject>();
    private void Start()
    {
        foreach (Transform child in transform)
        {
            gameTips.Add(child.gameObject);
        }
    }

    public void GetNextGameTip()
    {
        currentGameTipIndex++;
        if(currentGameTipIndex < gameTips.Count)
        SwitchGameTip(currentGameTipIndex);
        prevButton.interactable = true;
    }

    public void GetPrevGameTip()
    {
        currentGameTipIndex--;
        SwitchGameTip(currentGameTipIndex);
        nextButton.interactable = true;
    }

    void SwitchGameTip(int currentGameTipIndex)
    {
        foreach(GameObject gameTip in gameTips)
        {
            gameTip.SetActive(false);
        }
        gameTips[currentGameTipIndex].gameObject.SetActive(true);
        if(currentGameTipIndex == 0)
        {
            prevButton.interactable = false;
        }
        if(currentGameTipIndex == gameTips.Count - 1)
        {
            nextButton.interactable = false;
        }
    }
}
