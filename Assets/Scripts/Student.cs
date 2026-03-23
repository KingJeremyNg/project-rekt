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
    public AudioSource AudioSource;
    public AudioClip attackSound;

    private void Attack()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isAttacking", true);
    }

    public void DealDamage()
    {
        AudioSource.PlayOneShot(attackSound);
        target.GetComponent<Teacher>().TakeDamage(atk);
    }

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

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;
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
