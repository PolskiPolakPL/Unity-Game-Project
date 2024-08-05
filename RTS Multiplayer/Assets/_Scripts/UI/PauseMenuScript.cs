using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] UnityEvent OnGamePaused;
    [SerializeField] UnityEvent OnGameResumed;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
                OnGamePaused?.Invoke();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
        Time.timeScale = 1;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu Scene");
        Time.timeScale = 1;
    }
}
