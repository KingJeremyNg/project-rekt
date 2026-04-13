using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] studentPrefabs;
    // public float spawnInterval = 2f;
    // private float lastSpawnTime = 0f;
    private float tileOffset = 0.5f;
    public Transform[] Barriers;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        foreach (Transform barrier in Barriers)
        {
            barrier.GetComponent<BreakObject>().Break();
        }
        rb.AddExplosionForce(500f, transform.position, 5f);
    }

    public void SpawnStudent(int count = 0)
    {
        if (studentPrefabs.Length == 0) return;
        if (TeacherPrincipal.Instance == null) return;
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-tileOffset, tileOffset);
            spawnPosition.z += Random.Range(-tileOffset, tileOffset);
            int studentIndex = Random.Range(0, studentPrefabs.Length);
            Instantiate(studentPrefabs[studentIndex], spawnPosition, transform.rotation);
        }
    }

    // void Update()
    // {
    //     if (GameManager.Instance.currentState != GameState.WaveInProgress) return;
    //     if (Time.time - lastSpawnTime >= spawnInterval)
    //     {
    //         SpawnStudent();
    //         lastSpawnTime = Time.time;
    //     }
    // }
}
