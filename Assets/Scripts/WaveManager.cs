using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public List<Transform> spawners;
    public float spawnInterval = 2f;
    private float lastSpawnTime = 0;
    public int currentWave = 0;
    public RectTransform waveMessage;
    private List<int> spawnNumbers = new List<int> { 10, 15, 20, 1 };
    private int numberRemaining = 10;

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
        if (GameManager.Instance.currentState != GameState.WaveInProgress) return;
        GameObject[] students = GameObject.FindGameObjectsWithTag("Student");
        if (currentWave >= 3 && students.Count() == 0) // JANK
        {
            GameManager.Instance.UpdateGameState(GameState.GameWin);
            return;
        }
        if (numberRemaining <= 0)
        {
            if (currentWave < 3) currentWave++;
            else return;
            numberRemaining = spawnNumbers[currentWave];
            SendWaveMessage();
        }
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            for (int i = 0; i <= currentWave; i++)
            {
                if (currentWave == 3 && i < 3) continue; // JANK
                spawners[i].gameObject.SetActive(true);
                spawners[i].GetComponent<Spawner>().SpawnStudent(1);
            }
            lastSpawnTime = Time.time;
            numberRemaining--;
        }
    }
}
