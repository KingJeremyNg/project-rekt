using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance { get; private set; }
    public AudioSource soundFXObject;
    public AudioSource soundFXObjectGlobal;

    public AudioSource PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClip.length;
        Destroy(audioSource.gameObject, clipLength);
        return audioSource;
    }

    public AudioSource PlayRandomSound(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips.Length == 0) return null;
        int randomIndex = Random.Range(0, audioClips.Length);
        return PlaySound(audioClips[randomIndex], spawnTransform, volume);
    }

    public AudioSource PlayGlobalSound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObjectGlobal, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClip.length;
        Destroy(audioSource.gameObject, clipLength);
        return audioSource;
    }

    public AudioSource PlayGlobalRandomSound(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips.Length == 0) return null;
        int randomIndex = Random.Range(0, audioClips.Length);
        return PlayGlobalSound(audioClips[randomIndex], spawnTransform, volume);
    }

    void Awake()
    {
        Instance = this;
    }
}
