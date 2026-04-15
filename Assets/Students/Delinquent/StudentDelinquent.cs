using UnityEngine;

public class StudentDelinquent : MonoBehaviour
{
    private Student studentScript;

    void Start()
    {
        studentScript = GetComponent<Student>();
        studentScript.atk = 50f;
        studentScript.hp = 200f;
        studentScript.maxHp = 200f;
        studentScript.atkSpeed = 1f;
        studentScript.attackRange = 1.2f;
        studentScript.moveSpeed = 1f;
        studentScript.currencyReward = 10;
        studentScript.yOffset = 0.7f;
        transform.position += Vector3.up * studentScript.yOffset;
        studentScript.SetAttackAnimationSpeed(studentScript.atkSpeed);
        studentScript.SetMoveAnimationSpeed(studentScript.moveSpeed);
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.WaveInProgress) return;
        studentScript.StandardMoveAttack();
    }
}
