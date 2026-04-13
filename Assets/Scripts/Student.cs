using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Student : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float maxHp = 10f;
    public float atkSpeed = 1f;
    public float attackRange = 1.3f;
    public float moveSpeed = 1f;
    public int currencyReward = 10;
    public Transform target = null;
    public bool isDead = false;
    public float tileOffset = 0.25f;
    public float yOffset = 0f;

    private float lastAttackTime = 0f;
    // private float distanceToTarget;
    private List<Node> path;
    private int currentPathIndex = 0;
    private float tileOffsetX;
    private float tileOffsetZ;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioClip attackSound;
    private Transform mainCamera;
    private HPBar hpBar;
    public GameObject DamagePopUpPrefab;
    public float DamagePopUpYOffset = 0.5f;
    public GameObject MoneyPopUpPrefab;
    public float MoneyPopUpYOffset = 1.7f;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        hpBar = GetComponentInChildren<HPBar>();
    }

    void Start()
    {
        mainCamera = Camera.main.transform;
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
        SoundFXManager.Instance.PlaySound(attackSound, transform, 1f); // TODO CHANGE VOLUME TO MATCH SLIDERS
        target.GetComponent<Teacher>().TakeDamage(atk);
    }

    public void TakeDamage(float damage)
    {
        GameObject damagePopUp = Instantiate(DamagePopUpPrefab, transform.position, Quaternion.identity);
        damagePopUp.transform.Translate(Vector3.up * DamagePopUpYOffset);
        damagePopUp.GetComponentInChildren<TMP_Text>().text = "-" + damage;
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
        GameObject moneyPopUp = Instantiate(MoneyPopUpPrefab, transform.position, Quaternion.identity);
        moneyPopUp.transform.Translate(Vector3.up * MoneyPopUpYOffset);
        moneyPopUp.GetComponentInChildren<TMP_Text>().text = "+$" + currencyReward;
        isDead = true;
        GameManager.Instance.currency += currencyReward;
        PlayDeathAnimation();
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
        if (path == null || currentPathIndex >= path.Count) return;
        Vector3 currentNode = currentPathIndex > 0 ? path[currentPathIndex - 1].worldPosition : transform.position;
        Vector3 nextNode = path[currentPathIndex].worldPosition;
        Vector3 movementDirection = nextNode - currentNode;
        Vector3 cameraRight = mainCamera.right;
        float dotProduct = Vector3.Dot(movementDirection, cameraRight);
        spriteRenderer.flipX = dotProduct > 0;
    }

    public void SetAttackAnimationSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
    }

    public void SetMoveAnimationSpeed(float speed)
    {
        animator.SetFloat("moveSpeed", speed);
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
        transform.position -= Vector3.up * (yOffset - 0.15f);
    }

    // This method finds the closest teacher within the specified range and returns its transform object. If no teacher is found, it returns null.
    public void FindTarget(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform newTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Teacher") && !collider.GetComponent<Teacher>().isDead)
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

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.WaveInProgress) return;
        if (isDead) return;
        if (animator.GetBool("isAttacking")) FaceDirectionByCamera();
        if (animator.GetBool("isMoving")) FaceDirectionByMovement();
        // Check if any teacher is within attack range
        FindTarget(attackRange);
        if (target != null && !target.GetComponent<Teacher>().isDead)
        {
            if (Time.time - lastAttackTime > 1f / atkSpeed)
            {
                PlayAttackAnimation();
                lastAttackTime = Time.time;
            }
        }
        // Path towards target
        else
        {
            if (path != null && currentPathIndex < path.Count)
            {
                Vector3 nextPosition = path[currentPathIndex].worldPosition;
                nextPosition.y += yOffset; // Apply yOffset to the next position
                nextPosition.x += tileOffsetX;
                nextPosition.z += tileOffsetZ;

                if (Vector3.Distance(transform.position, nextPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
                    PlayMoveAnimation();
                }
                else
                {
                    currentPathIndex++;
                    RandomTileOffset();
                }
            }
        }
    }
}
