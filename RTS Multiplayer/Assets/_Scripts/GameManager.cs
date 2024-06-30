using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
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
    }

    //RtsGameManager
    public GameState currentGameState;
    [Header("Game Manager Events")]
    public UnityEvent winEvent;
    public UnityEvent defeatEvent;
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;


    void Start()
    {
        currentGameState = GameState.DEFAULT;
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.DEFAULT:
                {
                    currentGameState = HandleDefaultState();
                }break;
            case GameState.PAUSED:
                {
                    currentGameState = HandlePausedState();
                }
                break;
            case GameState.FINISHED:
                {
                    currentGameState = HandleFinishedState();
                }
                break;
        }
    }

    GameState HandleDefaultState()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke();
            return GameState.PAUSED;
        }
        return GameState.DEFAULT;
    }

    GameState HandlePausedState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
            return GameState.DEFAULT;
        }
        return GameState.PAUSED;
    }

    GameState HandleFinishedState()
    {
        return GameState.FINISHED;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SetGameState(GameState.DEFAULT);
        OnGameResumed?.Invoke();
    }

    //Used for 'Exit' Buttons in 'Pause Menu' and 'Victory/Defeat' Panels
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu Scene");
        Time.timeScale = 1;
    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
    }
}
