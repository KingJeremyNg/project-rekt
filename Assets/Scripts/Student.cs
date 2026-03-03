using UnityEngine;

public class Student : MonoBehaviour
{
    public float atk = 2f;
    public float hp = 10f;
    public float atkSpeed = 1f;
    public Transform target = null;

    private void Attack()
    {
        target.GetComponent<Principal>().TakeDamage(atk);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death logic here
        Destroy(gameObject);
    }

    void Update()
    {
        // Implement update logic here (e.g., movement, attack timing)
        if (Vector3.Distance(target.position, transform.position) < 1f) // Example attack range check
        {
            if (Time.time % (1f / atkSpeed) < Time.deltaTime)
            {
                Attack();
            }
        }
    }
}
