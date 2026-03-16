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

    // Call this method to draw a circle with the specified range of the teacher's attack
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

    // Call this method to clear the circle when the teacher is deselected or dies
    public void ClearCircleRadius()
    {
        lineRenderer.positionCount = 0;
    }

    // When the teacher is defeated, clear the circle and destroy the GameObject
    public void Die()
    {
        ClearCircleRadius();
        Destroy(gameObject);
    }

    // This method finds the closest student within the specified range and returns its transform object. If no student is found, it returns null.
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