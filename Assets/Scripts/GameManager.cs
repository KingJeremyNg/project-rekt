using UnityEngine;
using System.Collections.Generic;

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
    public static GameManager Instance { get; private set; }
    public int currency = 200;
    public int currentWave = 0;
    public GameState currentState = GameState.MainMenu;
    public List<Transform> floorTiles = new List<Transform>();
    public float LengthOfTile = 1.5f;
    public Transform Principal;
    // public NodeGrid NodeGrid;
    // public PathFinding PathFinding;
    // public List<Node> path;
    // public List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        Instance = this;
        Principal = Object.FindFirstObjectByType<TeacherPrincipal>().transform;
        // CreateGridAndPath();
    }

    public void CreateGridAndPath()
    {
        // NodeGrid.CreateGrid();
        // path = PathFinding.FindPath(Principal.position, spawnPoints[0].position);
    }
}
