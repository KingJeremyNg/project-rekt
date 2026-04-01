using UnityEngine;
// using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Student : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float maxHp = 10f;
    public float atkSpeed = 1f;
    public float attackRange = 1.3f;
    public float moveSpeed = 1f;
    public float idleTime = 1f;
    public int currencyReward = 10;
    public Transform target = null;
    public bool isDead = false;
    public float tileOffset = 0.25f;

    private float lastAttackTime = 0f;
    private float distanceToTarget;
    private List<Node> path;
    private int currentPathIndex = 0;
    private float lastDestinationReachTime = 0f;
    private bool isAtDestination = false;
    private float tileOffsetX;
    private float tileOffsetZ;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioClip attackSound;
    private Transform mainCamera;
    private HPBar hpBar;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mainCamera = Camera.main.transform;
        hpBar = GetComponentInChildren<HPBar>();
        path = PathFinding.Instance.FindPath(transform.position, TeacherPrincipal.Instance.transform.position);
        RandomTileOffset();
    }

    void RandomTileOffset()
    {
        tileOffsetX = Random.Range(-tileOffset, tileOffset);
        tileOffsetZ = Random.Range(-tileOffset, tileOffset);
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
        // agent.destination = transform.position; // Stop moving
        PlayDeathAnimation();
        GameManager.Instance.currency += currencyReward;
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

    public void FaceDirectionByMovement()
    {
        if (target == null) return;
        if (path != null && currentPathIndex < path.Count)
        {
            Vector3 currentNode = currentPathIndex > 0 ? path[currentPathIndex - 1].worldPosition : transform.position;
            Vector3 nextNode = path[currentPathIndex].worldPosition;
            Vector3 movementDirection = nextNode - currentNode;
            Vector3 cameraRight = mainCamera.right;
            float dotProduct = Vector3.Dot(movementDirection, cameraRight);
            spriteRenderer.flipX = dotProduct > 0;
        }
    }

    public void PlayMoveAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
    }

    public void PlayAttackAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }

    void Update()
    {
        if (isDead) return;
        if (animator.GetBool("isAttacking")) FaceDirectionByCamera();
        if (animator.GetBool("isMoving")) FaceDirectionByMovement();
        // Check if within attack range
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if ((distanceToTarget < attackRange) && (Time.time - lastAttackTime > 1f / atkSpeed))
        {
            PlayAttackAnimation();
            lastAttackTime = Time.time;
        }
        // Path towards target
        else
        {
            if (path != null && currentPathIndex < path.Count)
            {
                Vector3 nextPosition = path[currentPathIndex].worldPosition;
                nextPosition.x += tileOffsetX;
                nextPosition.z += tileOffsetZ;
                if (!isAtDestination)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
                    PlayMoveAnimation();
                }
                if (!isAtDestination && Vector3.Distance(transform.position, nextPosition) < 0.1f)
                {
                    isAtDestination = true;
                    PlayIdleAnimation();
                    lastDestinationReachTime = Time.time;
                }
                if (isAtDestination && (Time.time - lastDestinationReachTime > idleTime))
                {
                    isAtDestination = false;
                    currentPathIndex++;
                    RandomTileOffset();
                }
            }
        }
    }
}
