using UnityEngine;

public class TeacherPE : Teacher
{
    private float atk = 2f;
    private float hp = 10f;
    private float atkSpeed = 1f;
    private float attackRange = 5f;
    private float lastAttackTime = 0f;
    private Transform target = null;
    public Transform BasketballPrefab;

    new void Start()
    {
        base.Start();
        base.DrawCircleRadius(attackRange);
    }

    private void Attack()
    {
        print("TeacherPE attacks!");
        lastAttackTime = Time.time;
        Transform basketball = Instantiate(BasketballPrefab, transform.position + Vector3.up * target.localScale.y / 3f, Quaternion.identity);
        basketball.GetComponent<Basketball>().target = target;
    }

    private void DealDamage()
    {
        target.GetComponent<Student>().TakeDamage(atk);
    }

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