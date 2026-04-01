using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] studentPrefabs;
    public float spawnInterval = 2f;
    private float lastSpawnTime = 0f;
    private float tileOffset = 0.5f;

    public void SpawnStudent()
    {
        if (spawnPoints.Length == 0 || studentPrefabs.Length == 0) return;
        if (TeacherPrincipal.Instance == null) return;
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int studentIndex = Random.Range(0, studentPrefabs.Length);
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            Vector3 spawnPosition = spawnPoints[spawnIndex].position;
            spawnPosition.x += Random.Range(-tileOffset, tileOffset);
            spawnPosition.z += Random.Range(-tileOffset, tileOffset);
            GameObject student = Instantiate(studentPrefabs[studentIndex], spawnPosition, spawnPoints[spawnIndex].rotation);
            student.GetComponent<Student>().target = TeacherPrincipal.Instance.transform;
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
