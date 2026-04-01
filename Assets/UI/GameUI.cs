using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class GameUI : MonoBehaviour
{
    private VisualElement ui;
    private VisualElement Screen;
    private VisualElement Teacher1;
    private VisualElement Teacher2;
    private VisualElement Teacher3;
    public List<Transform> teachers;
    private Transform selectedTeacher;
    private Camera mainCamera;
    private Floor Floor;

    private bool previewMode = false;
    private Transform previewTransform;
    private Vector3 placementPosition;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        mainCamera = Camera.main;
        Floor = GameObject.Find("Floor").GetComponent<Floor>();
    }

    void OnEnable()
    {
        Screen = ui.Q<VisualElement>("Screen");
        Screen.AddManipulator(new Clickable(() => OnScreenClicked()));
        Teacher1 = ui.Q<VisualElement>("Teacher1");
        Teacher1.AddManipulator(new Clickable(() => OnTeacherClicked(1)));
        Teacher2 = ui.Q<VisualElement>("Teacher2");
        Teacher2.AddManipulator(new Clickable(() => OnTeacherClicked(2)));
        Teacher3 = ui.Q<VisualElement>("Teacher3");
        Teacher3.AddManipulator(new Clickable(() => OnTeacherClicked(3)));
        UpdateUI();
    }

    private void OnScreenClicked()
    {
        PlaceTeacher();
        Floor.HideIndicators();
        selectedTeacher = null;
    }

    private void OnTeacherClicked(int teacherNumber)
    {
        Floor.ShowIndicators();
        selectedTeacher = teachers[teacherNumber - 1];
        PreviewTeacher();
    }

    private void PreviewTeacher()
    {
        if (selectedTeacher == null) return;
        previewMode = true;
        previewTransform = Instantiate(selectedTeacher.GetChild(0), placementPosition, Quaternion.identity);
        previewTransform.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // Semi-transparent preview
    }

    private void ClearPreview()
    {
        previewMode = false;
        Destroy(previewTransform.gameObject);
        previewTransform = null;
    }

    private void PlaceTeacher()
    {
        if (selectedTeacher == null) return;
        ClearPreview();
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("FloorIndicator"))
            {
                Transform FloorTileTransform = hit.collider.transform.parent;
                Instantiate(selectedTeacher, FloorTileTransform.position, Quaternion.identity);
                Floor.GetIndicators(tileToIgnore: FloorTileTransform);
            }
        }
        GameManager.Instance.CreateGridAndPath();
    }

    private void UpdateUI()
    {
        // ui.Q<Label>("WaveInfo").text = $"Wave: {GameManager.Instance.currentWave}";
        ui.Q<ProgressBar>("HP").highValue = TeacherPrincipal.Instance.GetComponent<Teacher>().maxHp;
        ui.Q<ProgressBar>("HP").value = TeacherPrincipal.Instance.GetComponent<Teacher>().hp;
        ui.Q<Label>("Money").text = GameManager.Instance.currency.ToString();
    }

    void Update()
    {
        UpdateUI();
        if (previewMode)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            placementPosition = ray.origin + ray.direction * 10f;
            previewTransform.position = placementPosition;
        }
    }
}