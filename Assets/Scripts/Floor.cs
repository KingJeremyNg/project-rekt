using UnityEngine;
using System.Collections.Generic;

public class Floor : MonoBehaviour
{
    public Transform placementIndicatorPrefab;
    public List<Transform> indicators = new List<Transform>();

    void Start()
    {
        GetIndicators();
    }

    public void GetIndicators(Transform tileToIgnore = null)
    {
        foreach (var indicator in indicators) Destroy(indicator.gameObject);
        indicators.Clear();
        GameObject[] floorTileObjects = GameObject.FindGameObjectsWithTag("FloorTile");
        foreach (GameObject tile in floorTileObjects)
        {
            if (tile.transform == tileToIgnore) continue;
            RaycastHit hit;
            if (Physics.Raycast(tile.transform.position, Vector3.up, out hit)) continue;
            var indicator = Instantiate(placementIndicatorPrefab, tile.transform.position, Quaternion.identity, tile.transform);
            indicators.Add(indicator);
            indicator.gameObject.SetActive(false);
        }
    }

    public void ShowIndicators()
    {
        foreach (var indicator in indicators)
        {
            indicator.gameObject.SetActive(true);
        }
    }

    public void HideIndicators()
    {
        foreach (var indicator in indicators)
        {
            indicator.gameObject.SetActive(false);
        }
    }
}
