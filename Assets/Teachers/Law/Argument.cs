using UnityEngine;

public class Argument : MonoBehaviour
{
    public Transform target = null;
    public float attackRange = 3f;
    public float damage = 1f;
    public AudioClip hitSound;

    private Transform FindNewTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        Transform newTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Student"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    newTarget = collider.transform;
                }
            }
        }
        return newTarget;
    }

    void Update()
    {
        var studentScript = target.GetComponent<Student>();
        if (studentScript == null || studentScript.isDead)
        {
            Destroy(gameObject);
        }
        Vector3 yOffset = Vector3.up * target.localScale.y * 0.75f;
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position + yOffset, Time.deltaTime * 10f);
        // Check if we have reached the target
        if (Vector3.Distance(transform.position, target.position + yOffset) < 0.1f)
        {
            damage *= Random.Range(0.8f, 1.2f);
            studentScript.TakeDamage(damage);
            // SoundFXManager.Instance.PlaySound(hitSound, transform, 1f); // TODO CHANGE VOLUME TO MATCH SLIDERS
            Destroy(gameObject);
        }
    }
}
