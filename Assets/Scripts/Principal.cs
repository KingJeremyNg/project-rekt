using UnityEngine;

public class Principal : MonoBehaviour
{
    public float atk = 1f;
    public float hp = 10f;
    public float atkSpeed = 1f;
    public bool isAlive = true;
    public Transform target = null;
    public Material material;
    private float lastTakeDamageTime;

    private void Attack()
    {
        target.GetComponent<Student>().TakeDamage(atk);
    }

    public void TakeDamage(float damage)
    {
        material.color = Color.red;
        hp -= damage;
        lastTakeDamageTime = Time.time;
        if (hp <= 0)
        {
            Die();
        }
        print("Principal HP: " + hp);
    }

    private void Die()
    {
        // Implement death logic here
        isAlive = false;
        material.color = Color.purple;
        // Destroy(gameObject);
    }

    void Start()
    {
        material.color = Color.white; // Set initial color to white
    }

    void Update()
    {
        if (isAlive && Time.time - lastTakeDamageTime > 0.5f)
        {
            material.color = Color.white; // Reset color to white after being attacked
        }

        // Implement update logic here (e.g., movement, attack timing)
        // if (Vector3.Distance(target.transform.position, transform.position) < 1f) // Example attack range check
        // {
        //     if (Time.time % (1f / atkSpeed) < Time.deltaTime)
        //     {
        //         Attack();
        //     }
        // }
    }
}