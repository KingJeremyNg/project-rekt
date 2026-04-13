using UnityEngine;

public class TeacherLaw : MonoBehaviour
{
    private Teacher teacherScript;
    public Transform argumentPrefab;

    void Start()
    {
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 10f;
        teacherScript.hp = 10f;
        teacherScript.maxHp = 10f;
        teacherScript.atkSpeed = 0.5f;
        teacherScript.attackRange = 5f;
        teacherScript.yOffset = 0.7f;
        transform.position += Vector3.up * teacherScript.yOffset;
        teacherScript.SetAnimationSpeed(teacherScript.atkSpeed);
    }

    public void ShootArgument()
    {
        if (teacherScript.target == null) return;
        Transform argument = Instantiate(argumentPrefab, transform.position + Vector3.up * teacherScript.target.localScale.y / 3f, Quaternion.identity);
        argument.GetComponent<Argument>().target = teacherScript.target;
        argument.GetComponent<Argument>().damage = teacherScript.atk;
    }

    void Update()
    {
        // Check if it's time to attack based on the attack speed
        if ((Time.time - teacherScript.lastAttackTime) > (1f / teacherScript.atkSpeed))
        {
            teacherScript.FindTarget(teacherScript.attackRange);
            if (teacherScript.target != null) teacherScript.Attack();
        }
        if (teacherScript.target == null) teacherScript.PlayIdleAnimation();
    }
}
