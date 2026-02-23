using UnityEngine;
using UnityEngine.AI; // Make sure to include this namespace

public class PathFinding : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Set the agent's destination
        if (goal != null)
            agent.destination = goal.position;
    }

    // You could also update the destination dynamically, e.g., on mouse click
}
