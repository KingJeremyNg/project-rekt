using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements; // Make sure to include this namespace

public class Student : MonoBehaviour
{
    private float atk = 2f;
    private float hp = 10f;
    private float atkSpeed = 1f;
    private float attackRange = 0.5f;
    private float moveSpeed = 1f;
    public Transform target = null;
    private NavMeshAgent agent;

    private Vector3 initialPosition;
    private Animator animator;

    private void Attack()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isAttacking", true);
    }

    public void dealDamage()
    {
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
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Implement update logic here (e.g., movement, attack timing)
        if (Vector3.Distance(target.position, transform.position) < attackRange) // Example attack range check
        {
            agent.destination = transform.position; // Stop moving
            if (Time.time % (1f / atkSpeed) < Time.deltaTime)
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
