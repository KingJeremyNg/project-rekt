using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    private Vector3 originalScale;
    public float pulseSize = 0.2f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time);
        float scaleX = originalScale.x * (sin * pulseSize + 1f);
        float scaleY = originalScale.y * (sin * pulseSize + 1f);
        float scaleZ = originalScale.z * (sin * pulseSize + 1f);
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
