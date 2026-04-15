using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform mainMenu;
    public Transform narrativeScreen;
    public Transform gameUI;
    public Transform gameWinScreen;
    public Transform gameOverScreen;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                mainMenu.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.Narrative:
                mainMenu.gameObject.SetActive(false);
                narrativeScreen.gameObject.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.WavePreparation:
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(true);
                break;
            case GameState.WaveInProgress:
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(true);
                break;
            case GameState.GameWin:
                gameUI.gameObject.SetActive(false);
                gameWinScreen.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                gameUI.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }
}
