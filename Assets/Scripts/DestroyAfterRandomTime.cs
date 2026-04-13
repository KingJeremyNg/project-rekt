using UnityEngine;

public class DestroyAfterRandomTime : MonoBehaviour
{
    public float minTime = 1f;
    public float maxTime = 3f;

    void Start()
    {
        Destroy(gameObject, Random.Range(minTime, maxTime));
    }
}
