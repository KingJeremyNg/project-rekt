using UnityEngine;
using UnityEngine.AI;

public class Student : MonoBehaviour
{
    private float atk = 2f;
    private float hp = 10f;
    private float atkSpeed = 1f;
    private float attackRange = 0.7f;
    private float moveSpeed = 1f;
    private float lastAttackTime = 0f;
    public Transform target = null;
    public float distanceToTarget;
    private NavMeshAgent agent;

    private Vector3 initialPosition;
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
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death logic here
        Destroy(gameObject);
    }

    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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
