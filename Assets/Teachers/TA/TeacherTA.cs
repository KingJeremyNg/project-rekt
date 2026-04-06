using UnityEngine;

public class TeacherTA : MonoBehaviour
{
    private Teacher teacherScript;
    public AudioClip[] hitSounds;

    void Start()
    {
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 10f;
        teacherScript.hp = 10f;
        teacherScript.maxHp = 10f;
        teacherScript.atkSpeed = 0.5f;
        teacherScript.attackRange = 1f;
        teacherScript.yOffset = 0.72f;
        transform.position += Vector3.up * teacherScript.yOffset;
    }

    private void Attack()
    {
        teacherScript.lastAttackTime = Time.time;
        teacherScript.PlayAttackAnimation();
    }

    void Update()
    {
        // Check if it's time to attack based on the attack speed
        if ((Time.time - teacherScript.lastAttackTime) > (1f / teacherScript.atkSpeed))
        {
            teacherScript.FindTarget(teacherScript.attackRange);
            if (teacherScript.target != null) Attack();
        }
        if (teacherScript.target == null) teacherScript.PlayIdleAnimation();
    }
}
