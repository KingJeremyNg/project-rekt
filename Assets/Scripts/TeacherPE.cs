using UnityEngine;

public class TeacherPE : MonoBehaviour
{
    private Teacher teacherScript;
    public Transform BasketballPrefab;
    private float yOffset = 0.75f;

    void Start()
    {
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 2f;
        teacherScript.hp = 10f;
        teacherScript.maxHp = 10f;
        teacherScript.atkSpeed = 0.5f;
        teacherScript.attackRange = 5f;
        transform.position += Vector3.up * yOffset;
    }

    // Call this method to perform the attack action, which instantiates a basketball projectile that moves towards the target student and deals damage upon impact.
    private void Attack()
    {
        teacherScript.lastAttackTime = Time.time;
        teacherScript.PlayAttackAnimation();
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
            if (teacherScript.target != null) Attack();
        }
        if (teacherScript.target == null) teacherScript.PlayIdleAnimation();
    }
}