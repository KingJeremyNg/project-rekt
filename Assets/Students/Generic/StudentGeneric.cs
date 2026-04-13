using UnityEngine;

public class StudentGeneric : MonoBehaviour
{
    private Student studentScript;

    void Start()
    {
        studentScript = GetComponent<Student>();
        studentScript.atk = 2f;
        studentScript.hp = 10f;
        studentScript.maxHp = 10f;
        studentScript.atkSpeed = 1f;
        studentScript.attackRange = 1.3f;
        studentScript.moveSpeed = 1f;
        studentScript.currencyReward = 10;
        studentScript.yOffset = 0.55f;
        transform.position += Vector3.up * studentScript.yOffset;
        studentScript.SetAttackAnimationSpeed(studentScript.atkSpeed);
        studentScript.SetMoveAnimationSpeed(studentScript.moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
