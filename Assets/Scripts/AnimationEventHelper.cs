using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    private Student parentScript;

    void Start()
    {
        // Get a reference to the parent script when the game starts
        parentScript = GetComponentInParent<Student>();
        if (parentScript == null)
        {
            Debug.LogError("ParentScript not found on parent object!");
        }
    }

    // This public function will be called by the Animation Event
    public void DealDamage()
    {
        if (parentScript != null)
        {
            parentScript.DealDamage();
        }
    }
}
