using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform placementIndicatorPrefab;

    void Start()
    {
        foreach (Transform child in transform)
        {
            RaycastHit hit;
            if (Physics.Raycast(child.position, Vector3.up, out hit))
            {
                continue;
            }
            Instantiate(placementIndicatorPrefab, child.position, child.rotation, child);
        }
    }

    void Update()
    {

    }
}
