using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
