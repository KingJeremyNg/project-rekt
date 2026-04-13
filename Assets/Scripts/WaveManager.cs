using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public List<Transform> spawners;
    public float spawnInterval = 2f;
    public int currentWave = 0;
    public RectTransform waveMessage;
    private List<int> numberToSpawn = new List<int> { 10, 10, 10, 1 };

    void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.WaveInProgress)
        {
            SendWaveMessage();
        }
    }

    private void SendWaveMessage()
    {
        RectTransform obj = Instantiate(waveMessage);
        obj.GetChild(0).GetComponent<WaveAnnouncement>().Init("Wave #" + (currentWave + 1) + " start!");
    }

    void Update()
    {

    }
}
