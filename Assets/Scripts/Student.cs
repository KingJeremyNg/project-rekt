using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Student : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float maxHp = 10f;
    public float atkSpeed = 1f;
    public float attackRange = 1.3f;
    public float moveSpeed = 1f;
    public float lastAttackTime = 0f;
    public Transform target = null;
    public float distanceToTarget;
    public bool isDead = false;

    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioClip attackSound;
    private Transform mainCamera;
    private HPBar hpBar;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        mainCamera = Camera.main.transform;
        hpBar = GetComponentInChildren<HPBar>();
    }

    private void Attack()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isAttacking", true);
    }

    public void DealDamage()
    {
        SoundFXManager.Instance.PlaySound(attackSound, transform, 0.2f); // TODO CHANGE VOLUME TO MATCH SLIDERS
        target.GetComponent<Teacher>().TakeDamage(atk);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        StartCoroutine(FlashColor(0.2f));
        hpBar.UpdateHPBar();
        if (hp <= 0) Die();
    }

    IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red; // Change to flash color
        yield return new WaitForSeconds(duration); // Wait for the specified time
        spriteRenderer.color = Color.white; // Change back to the original color
    }

    private void Die()
    {
        isDead = true;
        agent.destination = transform.position; // Stop moving
        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }

    public void CleanUp()
    {
        Destroy(gameObject);
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
        if (isDead) return;
        if (target != null) FaceDirectionByCamera();
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget < attackRange)
        {
            agent.destination = transform.position; // Stop moving
            if (Time.time - lastAttackTime > 1f / atkSpeed)
            {
                Attack();
            }
        }
        else
        {
            // Set the agent's destination
            agent.destination = target.position;
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isAttacking", false);
        }
    }
}
