using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
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
