using UnityEngine;

public enum GameState
{
    MainMenu,
    WavePreparation,
    WaveInProgress,
    PlacementMode,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public int currency = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
