using UnityEngine;
using System.Collections.Generic;

public class Basketball : MonoBehaviour
{
    private int bounces = 3;
    public Transform target = null;
    public float damage = 1f;
    private List<Transform> previousTargets = new List<Transform>();

    public AudioSource AudioSource;
    public List<AudioClip> bounceSounds;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private Transform FindNewTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        Transform newTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Student") && !previousTargets.Contains(collider.transform))
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

    private void Bounce()
    {
        bounces--;
        // Play a random bounce sound
        int index = Random.Range(0, bounceSounds.Count);
        AudioSource.PlayOneShot(bounceSounds[index]);
        // Find a new target to bounce towards
        previousTargets.Add(target);
        Transform newTarget = FindNewTarget();
        if (newTarget != null)
        {
            target = newTarget;
        }
    }

    void Update()
    {
        if (bounces > 0)
        {
            if (target != null)
            {
                Vector3 yOffset = Vector3.up * target.localScale.y / 1f;
                // Move towards the target
                transform.position = Vector3.MoveTowards(transform.position, target.position + yOffset, Time.deltaTime * 10f);

                // Check if we have reached the target
                if (Vector3.Distance(transform.position, target.position + yOffset) < 0.1f)
                {
                    target.GetComponent<Student>().TakeDamage(damage);
                    Bounce();
                }
            }
        }
        else
        {
            // Destroy the basketball after it has bounced the specified number of times
            Destroy(gameObject, 2f);
        }
    }
}
