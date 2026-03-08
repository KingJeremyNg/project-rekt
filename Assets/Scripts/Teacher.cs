using UnityEngine;

public class Teacher : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int segments = 50;

    public void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false; // Keep the circle centered on the GameObject
    }

    public void DrawCircleRadius(float range)
    {
        lineRenderer.positionCount = segments + 1;
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * range;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * range;
            lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
            angle += 360f / segments;
        }
        lineRenderer.SetPosition(segments, lineRenderer.GetPosition(0));
    }

    public void ClearCircleRadius()
    {
        lineRenderer.positionCount = 0;
    }

    public void Die()
    {
        // Implement death logic here
        ClearCircleRadius();
        Destroy(gameObject);
    }

    public Transform FindTarget(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        Transform target = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Student"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = collider.transform;
                }
            }
        }
        return target;
    }
}