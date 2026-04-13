using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement ui;
    private Button PlayButton;
    private Button QuitButton;
    // public Transform gameUI;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    void OnEnable()
    {
        PlayButton = ui.Q<Button>("play");
        PlayButton.clickable.clicked += OnPlayButtonClicked;
        QuitButton = ui.Q<Button>("quit");
        QuitButton.clickable.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        // gameUI.gameObject.SetActive(true); // Show the game UI
        // gameObject.SetActive(false); // Hide the main menu
        // Time.timeScale = 1f; // Unpause the game
        GameManager.Instance.UpdateGameState(GameState.Narrative);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}