using System.Linq;
using UnityEngine;

public class StudentMutant : MonoBehaviour
{
    private Student studentScript;
    public Vector3[] checkpoints;
    private int checkpointIndex = 0;
    private Transform mainCamera;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        mainCamera = Camera.main.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        studentScript = GetComponent<Student>();
        studentScript.atk = 1000f;
        studentScript.hp = 5000f;
        studentScript.maxHp = 5000f;
        studentScript.atkSpeed = 0.5f;
        studentScript.attackRange = 2f;
        studentScript.moveSpeed = 0.7f;
        studentScript.currencyReward = 1000;
        studentScript.yOffset = 1.295f;
        transform.position += Vector3.up * studentScript.yOffset;
        studentScript.SetAttackAnimationSpeed(studentScript.atkSpeed);
        studentScript.SetMoveAnimationSpeed(studentScript.moveSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Breakable"))
        {
            other.gameObject.GetComponent<BreakObject>().Break();
        }
    }

    public void FaceDirectionByMovement()
    {
        Vector3 directionToTarget = checkpoints[checkpointIndex] - transform.position;
        Vector3 cameraRight = mainCamera.right;
        float dotProduct = Vector3.Dot(directionToTarget, cameraRight);
        spriteRenderer.flipX = dotProduct > 0;
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.WaveInProgress) return;
        if (studentScript.isDead) return;
        if (animator.GetBool("isMoving")) FaceDirectionByMovement();
        studentScript.FindTarget(studentScript.attackRange);
        if (studentScript.target != null && !studentScript.target.GetComponent<Teacher>().isDead)
        {
            if (Time.time - studentScript.lastAttackTime > 1f / studentScript.atkSpeed)
            {
                studentScript.PlayAttackAnimation();
                studentScript.lastAttackTime = Time.time;
            }
        }
        // Path towards target
        else
        {
            if (checkpointIndex < checkpoints.Count())
            {
                Vector3 nextPosition = checkpoints[checkpointIndex];
                nextPosition.y += studentScript.yOffset; // Apply yOffset to the next position

                if (Vector3.Distance(transform.position, nextPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextPosition, studentScript.moveSpeed * Time.deltaTime);
                    studentScript.PlayMoveAnimation();
                }
                else
                {
                    checkpointIndex++;
                }
            }
        }
    }
}
