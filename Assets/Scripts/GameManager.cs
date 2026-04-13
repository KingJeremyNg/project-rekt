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
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;

    public int currency = 200;
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
            case GameState.MainMenu:
                break;
            case GameState.Narrative:
                break;
            case GameState.WavePreparation:
                HandleWavePreparation();
                break;
            case GameState.WaveInProgress:
                HandleWaveInProgress();
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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
