using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Student : MonoBehaviour
{
    private float atk = 2f;
    private float hp = 10f;
    private float atkSpeed = 1f;
    private float attackRange = 1.3f;
    private float moveSpeed = 1f;
    private float lastAttackTime = 0f;
    public Transform target = null;
    public float distanceToTarget;
    private NavMeshAgent agent;
    private bool isDead = false;

    private Vector3 initialPosition;
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
        target.GetComponent<Principal>().TakeDamage(atk);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        StartCoroutine(FlashColor(Color.red, 0.2f));
        if (hp <= 0) Die();
    }

    IEnumerator FlashColor(Color flashColor, float duration)
    {
        spriteRenderer.color = flashColor; // Change to flash color
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
        initialPosition = transform.position;
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
