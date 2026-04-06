using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public AudioSource audioSource;
    private float musicVolume = 0.03f;
    public AudioClip dialogueMusic;
    public AudioClip battleMusic;

    void Awake()
    {
        Instance = this;
    }

    public void PlayDialogueMusic()
    {
        if (audioSource.clip == dialogueMusic) return;
        audioSource.clip = dialogueMusic;
        audioSource.volume = musicVolume;
        audioSource.Play();
    }

    public void PlayBattleMusic()
    {
        if (audioSource.clip == battleMusic) return;
        audioSource.clip = battleMusic;
        audioSource.volume = musicVolume;
        audioSource.Play();
    }
}
