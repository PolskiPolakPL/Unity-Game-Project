using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
