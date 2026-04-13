using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform mainMenu;
    public Transform narrativeScreen;
    public Transform gameUI;
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
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(false);
                Time.timeScale = 0f;
                break;
            case GameState.Narrative:
                mainMenu.gameObject.SetActive(false);
                narrativeScreen.gameObject.SetActive(true);
                gameUI.gameObject.SetActive(false);
                Time.timeScale = 1f;
                break;
            case GameState.WavePreparation:
                mainMenu.gameObject.SetActive(false);
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.WaveInProgress:
                mainMenu.gameObject.SetActive(false);
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                mainMenu.gameObject.SetActive(false);
                narrativeScreen.gameObject.SetActive(false);
                gameUI.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }
}
