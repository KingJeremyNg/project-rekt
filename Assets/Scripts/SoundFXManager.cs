using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    public AudioSource soundFXObject;

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSound(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips.Length == 0) return;
        int randomIndex = Random.Range(0, audioClips.Length);
        PlaySound(audioClips[randomIndex], spawnTransform, volume);
    }

    void Awake()
    {
        Instance = this;
    }
}
