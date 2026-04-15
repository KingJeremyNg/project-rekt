using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    private VisualElement ui;
    private Button returnButton;
    private Button quitButton;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    void OnEnable()
    {
        ui.Q<Label>("score").text = GameManager.Instance.score.ToString();
        returnButton = ui.Q<Button>("return");
        returnButton.clickable.clicked += OnPlayButtonClicked;
        quitButton = ui.Q<Button>("quit");
        quitButton.clickable.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
