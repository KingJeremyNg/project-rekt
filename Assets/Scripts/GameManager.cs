using UnityEngine;
using System.Collections.Generic;
using System;

public enum GameState
{
    MainMenu,
    Narrative,
    WavePreparation,
    WaveInProgress,
    Paused,
    GameWin,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;

    public int score = 0;
    public int currency = 300;
    public List<Transform> floorTiles = new List<Transform>();
    public float LengthOfTile = 1.5f;
    public Transform NarrativeCameraTarget;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MusicManager.Instance.PlayDialogueMusic();
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        print("Changing game state - " + newState);
        currentState = newState;
        switch (newState)
        {
            case GameState.WavePreparation:
                HandleWavePreparation();
                break;
            case GameState.WaveInProgress:
                HandleWaveInProgress();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleWavePreparation()
    {
        NarrativeCameraTarget = null;
        CameraControls.Instance.ResetCamera();
    }

    private void HandleWaveInProgress()
    {
        NarrativeCameraTarget = null;
        CameraControls.Instance.ResetCamera();
    }
}
