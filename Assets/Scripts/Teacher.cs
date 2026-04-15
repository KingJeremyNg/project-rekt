using UnityEngine;
using System.Collections;
using TMPro;

public class Teacher : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float maxHp = 10f;
    public float atkSpeed = 0.5f;
    public float attackRange = 5f;
    public float lastAttackTime = 0f;
    public Transform target = null;
    public bool isDead = false;

    public float yOffset = 0;
    // private int segments = 50;
    // private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform mainCamera;
    private HPBar hpBar;
    public GameObject DamagePopUpPrefab;
    public float DamagePopUpYOffset = 0.5f;

    public void Awake()
    {
        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.useWorldSpace = false; // Keep the circle centered on the GameObject
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main.transform;
        hpBar = GetComponentInChildren<HPBar>();
    }

    // // Call this method to draw a circle with the specified attackRange of the teacher's attack
    // public void DrawCircleRadius()
    // {
    //     lineRenderer.positionCount = segments + 1;
    //     float angle = 0f;
    //     for (int i = 0; i <= segments; i++)
    //     {
    //         float x = Mathf.Cos(Mathf.Deg2Rad * angle) * attackRange;
    //         float z = Mathf.Sin(Mathf.Deg2Rad * angle) * attackRange;
    //         lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
    //         angle += 360f / segments;
    //     }
    //     lineRenderer.SetPosition(segments, lineRenderer.GetPosition(0));
    // }

    // // Call this method to clear the circle when the teacher is deselected or dies
    // public void ClearCircleRadius()
    // {
    //     lineRenderer.positionCount = 0;
    // }

    // Call this method to perform the attack action, which instantiates a basketball projectile that moves towards the target student and deals damage upon impact.
    public void Attack()
    {
        lastAttackTime = Time.time;
        PlayAttackAnimation();
    }

    public void DealDamage()
    {
        if (target == null) return;
        float damage = atk * Random.Range(0.8f, 1.2f);
        target.GetComponent<Student>().TakeDamage(damage);
    }

    // Teacher takes damage and checks for death
    public void TakeDamage(float damage)
    {
        GameObject damagePopUp = Instantiate(DamagePopUpPrefab, transform.position, Quaternion.identity);
        damagePopUp.transform.Translate(Vector3.up * DamagePopUpYOffset);
        damagePopUp.GetComponentInChildren<TMP_Text>().text = "-" + (int)damage;
        hp -= damage;
        StartCoroutine(FlashColor(0.2f));
        hpBar.UpdateHPBar();
        if (hp <= 0) Die();
    }

    IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red; // Change to flash color
        yield return new WaitForSeconds(duration); // Wait for the specified time
        spriteRenderer.color = new Color(1f, 1f * (hp / maxHp), 1f * (hp / maxHp), 1f);
    }

    // When the teacher is defeated, clear the circle
    public void Die()
    {
        // ClearCircleRadius();
        PlayDeathAnimation();
        GameManager.Instance.score -= (int)maxHp;
        isDead = true;
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

    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
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
        transform.position -= Vector3.up * (yOffset - 0.15f);
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