using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    private float speed = 1f;
    private TMP_Text textMesh;

    void Start()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - Time.deltaTime);
    }
}