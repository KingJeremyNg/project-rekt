using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Narrator : MonoBehaviour
{
    private VisualElement ui;
    private Label text;
    private VisualElement portrait;
    private VisualElement screen;
    private float textSpeed = 30f;
    private float lastCharacterTime;

    private int actorIndex = 0;
    private int characterIndex = 0;
    private int currentLineIndex = 0;
    private int dialogueIndex = 0;

    public List<StyleBackground> portraits;
    public List<Transform> characters;
    public List<Vector3> characterPositions = new List<Vector3>
    {
        Vector3.zero,
        new Vector3(3f, 0f, -1.5f),
    };
    public List<List<string>> dialogueLines = new List<List<string>>
    {
        new List<string>
        {
            "Welcome to Project Rekt, a tower defense game where you play as a teacher defending your classroom from waves of unruly students.",
            "I'll guide you through the story and provide some tips along the way.",
            "In this game, you'll have access to various teachers, each with their own unique abilities and strengths. Choose wisely to create the best defense strategy!",
            "The students will come in waves, and each wave will be more challenging than the last. Be prepared to adapt your strategy as you progress.",
            "Remember, the key to success is to manage your resources effectively and make strategic decisions on which teachers to deploy.",
            "The first wave of students is approaching! Get ready to deploy your teachers and defend your classroom.",
            "Each teacher has a unique ability that can help you in different ways.",
            "The students will come in different types, each with their own strengths and weaknesses. Pay attention to their behavior and adjust your strategy accordingly.",
            "Remember, the key to victory is to stay calm and think strategically. You got this!"
        },
        new List<string>
        {
            "Hey, hey, hey! Look who's in charge now!",
            "You thought you could stop us? We've got numbers, we've got attitude, and we've got zero respect for authority!",
            "Your precious classroom? It's ours now. Every desk, every chair, every corner belongs to the delinquents!",
            "You can try to fight back with your teacher squad, but we're relentless. We keep coming, wave after wave!",
            "This is our domain now. Better get your defenses ready, because we're not going anywhere!",
            "Let's see how long you can last against the full force of student chaos!",
        },
    };

    // void Awake()
    // {
    //     GameManager.OnGameStateChanged += OnGameStateChanged;
    // }

    void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        text = ui.Q<Label>("text");
        portrait = ui.Q<VisualElement>("portrait");
        screen = ui.Q<VisualElement>("screen");
        text.AddManipulator(new Clickable(() => OnClick()));
        portrait.AddManipulator(new Clickable(() => OnClick()));
        screen.AddManipulator(new Clickable(() => OnClick()));
        GameManager.Instance.NarrativeCameraTarget = TeacherPrincipal.Instance.transform;
    }

    private void OnClick()
    {
        if (actorIndex >= characters.Count)
        {
            // End of dialogue, transition to next game state
            GameManager.Instance.UpdateGameState(GameState.WaveInProgress);
            return;
        }
        if (characterIndex < dialogueLines[dialogueIndex][currentLineIndex].Length)
        {
            characterIndex = dialogueLines[dialogueIndex][currentLineIndex].Length;
        }
        else if (currentLineIndex < dialogueLines[dialogueIndex].Count - 1)
        {
            currentLineIndex++;
            characterIndex = 0;
        }
        else if (currentLineIndex == dialogueLines[dialogueIndex].Count - 1)
        {
            actorIndex++;
            currentLineIndex = 0;
            characterIndex = 0;
            if (actorIndex < characters.Count)
            {
                dialogueIndex++;
                Transform actor = Instantiate(characters[actorIndex], characterPositions[actorIndex], Quaternion.identity);
                GameManager.Instance.NarrativeCameraTarget = actor;
            }
        }
        UpdateUI();
    }

    // void OnDestroy()
    // {
    //     GameManager.OnGameStateChanged -= OnGameStateChanged;
    // }

    // private void OnGameStateChanged(GameState newState)
    // {
    //     switch (newState)
    //     {
    //         case GameState.MainMenu:
    //             break;
    //         case GameState.Narrative:
    //             break;
    //         case GameState.WavePreparation:
    //             break;
    //         case GameState.WaveInProgress:
    //             break;
    //         case GameState.Paused:
    //             break;
    //         case GameState.GameOver:
    //             break;
    //     }
    // }

    void UpdateUI()
    {
        if (actorIndex >= characters.Count) return;
        portrait.style.backgroundImage = portraits[actorIndex];
        text.text = dialogueLines[dialogueIndex][currentLineIndex][..characterIndex];
    }

    void Update()
    {
        if ((Time.time - lastCharacterTime) > (1 / textSpeed))
        {
            lastCharacterTime = Time.time;
            if (currentLineIndex < dialogueLines[dialogueIndex].Count)
            {
                if (characterIndex < dialogueLines[dialogueIndex][currentLineIndex].Length)
                {
                    characterIndex++;
                    UpdateUI();
                }
            }
        }
    }
}
