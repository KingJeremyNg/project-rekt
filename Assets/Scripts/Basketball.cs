using UnityEngine;
using System.Collections.Generic;

public class Basketball : MonoBehaviour
{
    private int bounces = 3;
    public Transform target = null;
    private List<Transform> previousTargets = new List<Transform>();

    public AudioSource AudioSource;
    public List<AudioClip> bounceSounds;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private Transform FindNewTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        List<Transform> potentialTargets = new List<Transform>();

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Student") && !previousTargets.Contains(collider.transform))
            {
                potentialTargets.Add(collider.transform);
            }
        }

        if (potentialTargets.Count > 0)
        {
            int index = Random.Range(0, potentialTargets.Count);
            return potentialTargets[index];
        }
        return null;
    }

    private void Bounce()
    {
        bounces--;

        // Play a random bounce sound
        if (bounceSounds.Count > 0)
        {
            int index = Random.Range(0, bounceSounds.Count);
            AudioSource.PlayOneShot(bounceSounds[index]);
        }

        // Find a new target to bounce towards
        Transform newTarget = FindNewTarget();
        if (newTarget != null)
        {
            target = newTarget;
            previousTargets.Add(target);
        }
    }

    void Update()
    {
        if (bounces > 0)
        {
            if (target != null)
            {
                Vector3 yOffset = Vector3.up * target.localScale.y / 2f;
                // Move towards the target
                transform.position = Vector3.MoveTowards(transform.position, target.position + yOffset, Time.deltaTime * 5f);

                // Check if we have reached the target
                if (Vector3.Distance(transform.position, target.position + yOffset) < 0.1f)
                {
                    Bounce();
                }
            }
        }
        else
        {
            // Destroy the basketball after it has bounced the specified number of times
            Destroy(gameObject);
        }
    }
}
