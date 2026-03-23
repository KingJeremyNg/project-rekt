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
    public int currency = 200;
    public int currentWave = 0;
    public GameState currentState = GameState.MainMenu;
    public List<Transform> floorTiles = new List<Transform>();
    public float LengthOfTile = 1.5f;
    public Transform Principal;

    void Start()
    {
        // Find all floor tiles in the scene and add them to the list
        GameObject[] floorTileObjects = GameObject.FindGameObjectsWithTag("FloorTile");
        foreach (GameObject tile in floorTileObjects)
        {
            floorTiles.Add(tile.transform);
        }
        // LengthOfTile = floorTiles[0].GetComponent<Renderer>().bounds.size.x;
        Principal = Object.FindFirstObjectByType<TeacherPrincipal>().transform;
    }

    public void CalculatePathForAllStudents()
    {

    }

    void Update()
    {

    }
}
