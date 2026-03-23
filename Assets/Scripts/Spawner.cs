using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] studentPrefabs;
    public float spawnInterval = 2f;
    private float lastSpawnTime = 0f;
    private Transform teacherPrincipalTransform;

    void Start()
    {
        teacherPrincipalTransform = FindFirstObjectByType<TeacherPrincipal>().transform;
    }

    public void SpawnStudent()
    {
        if (spawnPoints.Length == 0 || studentPrefabs.Length == 0) return;
        if (teacherPrincipalTransform == null) return;
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int studentIndex = Random.Range(0, studentPrefabs.Length);
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            GameObject student = Instantiate(studentPrefabs[studentIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            student.GetComponent<Student>().target = teacherPrincipalTransform;
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
