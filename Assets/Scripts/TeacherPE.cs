using UnityEngine;

public class TeacherPE : Teacher
{
    private float atk = 2f;
    private float hp = 10f;
    private float atkSpeed = 0.5f;
    private float attackRange = 5f;
    private float lastAttackTime = 0f;

    private Transform target = null;
    public Transform BasketballPrefab;

    new void Start()
    {
        base.Start();
        base.DrawCircleRadius(attackRange);
    }

    // Call this method to perform the attack action, which instantiates a basketball projectile that moves towards the target student and deals damage upon impact.
    private void Attack()
    {
        lastAttackTime = Time.time;
        Transform basketball = Instantiate(BasketballPrefab, transform.position + Vector3.up * target.localScale.y / 3f, Quaternion.identity);
        basketball.GetComponent<Basketball>().target = target;
        basketball.GetComponent<Basketball>().damage = atk;
    }

    // Call this method when the teacher takes damage
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            base.Die();
        }
    }

    void Update()
    {
        // Check if it's time to attack based on the attack speed
        if ((Time.time - lastAttackTime) > (1f / atkSpeed))
        {
            target = base.FindTarget(attackRange);
            if (target != null)
            {
                Attack();
            }
        }
    }
}