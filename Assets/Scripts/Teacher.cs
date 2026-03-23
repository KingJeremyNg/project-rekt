using UnityEngine;
using System.Collections;

public class Teacher : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float atkSpeed = 0.5f;
    public float attackRange = 5f;
    public float lastAttackTime = 0f;
    public Transform target = null;

    private int segments = 50;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform mainCamera;

    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false; // Keep the circle centered on the GameObject
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main.transform;
    }

    // Call this method to draw a circle with the specified attackRange of the teacher's attack
    public void DrawCircleRadius()
    {
        lineRenderer.positionCount = segments + 1;
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * attackRange;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * attackRange;
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

    public void DealDamage()
    {
        target.GetComponent<Student>().TakeDamage(atk);
    }

    // Teacher takes damage and checks for death
    public void TakeDamage(float damage)
    {
        hp -= damage;
        StartCoroutine(FlashColor(0.2f));
        if (hp <= 0) Die();
    }

    IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red; // Change to flash color
        yield return new WaitForSeconds(duration); // Wait for the specified time
        spriteRenderer.color = Color.white; // Change back to the original color
    }

    // When the teacher is defeated, clear the circle
    public void Die()
    {
        ClearCircleRadius();
        PlayDeathAnimation();
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }

    // This method finds the closest student within the specified range and returns its transform object. If no student is found, it returns null.
    public void FindTarget(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform newTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Student") && !collider.GetComponent<Student>().isDead)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    newTarget = collider.transform;
                }
            }
        }
        target = newTarget; // Store the target for use in the animation event
    }

    public void PlayAttackAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttacking", true);
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isAttacking", false);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }

    public void FaceDirectionByCamera()
    {
        if (target == null) return;
        Vector3 directionToTarget = target.position - transform.position;
        Vector3 cameraRight = mainCamera.right;
        float dotProduct = Vector3.Dot(directionToTarget, cameraRight);
        spriteRenderer.flipX = dotProduct > 0;
    }

    void Update()
    {
        if (target != null) FaceDirectionByCamera();
    }
}