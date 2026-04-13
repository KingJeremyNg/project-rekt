using UnityEngine;
using TMPro;

public class WaveAnnouncement : MonoBehaviour
{
    private RectTransform rectTransform;
    private float speed = 1500f;
    public AudioClip schoolBell;
    private AudioSource audioSource;
    public TMP_Text textMesh;
    public string text;

    void Awake()
    {
        audioSource = SoundFXManager.Instance.PlayGlobalSound(schoolBell, transform, 0.3f);
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(string _text)
    {
        text = _text;
    }

    void Update()
    {
        textMesh.text = text;
        if (rectTransform.anchoredPosition.x > 0 || audioSource == null)
        {
            rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;
        }
    }
}
