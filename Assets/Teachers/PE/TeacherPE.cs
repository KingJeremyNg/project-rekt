using UnityEngine;

public class TeacherPE : MonoBehaviour
{
    private Teacher teacherScript;
    public Transform BasketballPrefab;

    void Start()
    {
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 2f;
        teacherScript.hp = 10f;
        teacherScript.maxHp = 10f;
        teacherScript.atkSpeed = 1f;
        teacherScript.attackRange = 5f;
        teacherScript.yOffset = 0.75f;
        transform.position += Vector3.up * teacherScript.yOffset;
        teacherScript.SetAnimationSpeed(teacherScript.atkSpeed);
    }

    public void ShootBasketball()
    {
        if (teacherScript.target == null) return;
        Transform basketball = Instantiate(BasketballPrefab, transform.position + Vector3.up * teacherScript.target.localScale.y / 3f, Quaternion.identity);
        basketball.GetComponent<Basketball>().target = teacherScript.target;
        basketball.GetComponent<Basketball>().damage = teacherScript.atk;
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