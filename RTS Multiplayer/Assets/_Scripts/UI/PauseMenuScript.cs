using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, unitsItemList;
    [SerializeField] BuildingScript buildingScript;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
                unitsItemList.SetActive(false);
                buildingScript.enabled = false;
                pauseMenu.SetActive(true);
                InputManager.Instance.currentState = Selection.BUILDING;//Goofy ahhh plasterek z jednoro¿cem na inny plasterek z jednoro¿cem na ranie postrza³owej
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void ResumeGame()
    {
        buildingScript.enabled = true;
        pauseMenu.SetActive(false);
        InputManager.Instance.currentState = Selection.NONE;
        Time.timeScale = 1;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu Scene");
        Time.timeScale = 1;
    }
}
