using UnityEngine;
using System.Collections.Generic;

public class Basketball : MonoBehaviour
{
    private int bounces = 3;
    private float bounceRange = 3f;
    public Transform target = null;
    public float damage = 1f;
    private List<Transform> previousTargets = new List<Transform>();
    public AudioClip[] bounceSounds;

    private Transform FindNewTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bounceRange);
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
        SoundFXManager.Instance.PlayRandomSound(bounceSounds, transform, 1f); // TODO CHANGE VOLUME TO MATCH SLIDERS
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
        if (bounces > 0 && target != null)
        {
            var studentScript = target.GetComponent<Student>();
            if (studentScript == null || studentScript.isDead)
            {
                target = FindNewTarget();
                return;
            }

            Vector3 yOffset = Vector3.up * target.localScale.y / 1f;
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position + yOffset, Time.deltaTime * 10f);

            // Check if we have reached the target
            if (Vector3.Distance(transform.position, target.position + yOffset) < 0.1f)
            {
                studentScript.TakeDamage(damage);
                Bounce();
            }
        }
        else
        {
            // Destroy the basketball after it has bounced the specified number of times or if there are no valid targets
            Destroy(gameObject, (bounces > 0) ? 0f : 2f);
        }
    }
}
