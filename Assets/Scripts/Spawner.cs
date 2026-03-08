using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] studentPrefabs;
    public float spawnInterval = 2f;
    private float lastSpawnTime = 0f;

    public void SpawnStudent()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int studentIndex = Random.Range(0, studentPrefabs.Length);
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            GameObject student = Instantiate(studentPrefabs[studentIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            student.GetComponent<Student>().target = FindFirstObjectByType<Principal>().transform; // Set the principal as the target for the student
        }
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnStudent();
            lastSpawnTime = Time.time;
        }
    }
}
