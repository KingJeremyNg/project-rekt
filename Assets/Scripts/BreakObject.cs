using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject brokenVersion;
    public AudioClip breakSound;

    public void Break()
    {
        Instantiate(brokenVersion, transform.position, transform.rotation);
        SoundFXManager.Instance.PlaySound(breakSound, transform, 0.5f); // TODO CHANGE VOLUME TO MATCH SLIDERS
        Destroy(gameObject);
    }
}
