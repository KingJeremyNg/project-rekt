using UnityEngine;
using System.Collections.Generic;

public enum GameState
{
    MainMenu,
    Narrative,
    WavePreparation,
    WaveInProgress,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currency = 200;
    public int currentWave = 0;
    public GameState currentState = GameState.MainMenu;
    public List<Transform> floorTiles = new List<Transform>();
    public float LengthOfTile = 1.5f;
    public Transform Principal;

    void Start()
    {
        Instance = this;
        Principal = Object.FindFirstObjectByType<TeacherPrincipal>().transform;
        MusicManager.Instance.PlayDialogueMusic();
        MusicManager.Instance.PlayBattleMusic();
    }
}
