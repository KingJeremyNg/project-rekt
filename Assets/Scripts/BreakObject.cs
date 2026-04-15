using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject brokenVersion;
    public AudioClip breakSound;
    public Quaternion rotationOverride;

    public void Break()
    {
        if (rotationOverride != Quaternion.identity)
        {
            Instantiate(brokenVersion, transform.position, rotationOverride);
        }
        else
        {
            Instantiate(brokenVersion, transform.position, transform.rotation);
        }
        if (breakSound != null)
        {
            SoundFXManager.Instance.PlaySound(breakSound, transform, 0.5f); // TODO CHANGE VOLUME TO MATCH SLIDERS
        }
        Destroy(gameObject);
    }
}
