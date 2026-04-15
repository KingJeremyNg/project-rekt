using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    private VisualElement ui;
    private VisualElement Screen;
    private VisualElement Teacher1;
    private VisualElement Teacher2;
    private VisualElement Teacher3;
    public List<Transform> teachers;
    private List<int> costs = new List<int> { 150, 100, 200 };
    private Transform selectedTeacher;
    private int teacherIndex;
    private Camera mainCamera;
    private Floor Floor;

    private bool previewMode = false;
    private Transform previewTransform;
    private Vector3 placementPosition;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    void Start()
    {
        mainCamera = Camera.main;
        Floor = GameObject.Find("Floor").GetComponent<Floor>();
    }

    void OnEnable()
    {
        Screen = ui.Q<VisualElement>("Screen");
        Screen.AddManipulator(new Clickable(() => OnScreenClicked()));
        Teacher1 = ui.Q<VisualElement>("Teacher1");
        Teacher1.AddManipulator(new Clickable(() => OnTeacherClicked(0)));
        Teacher2 = ui.Q<VisualElement>("Teacher2");
        Teacher2.AddManipulator(new Clickable(() => OnTeacherClicked(1)));
        Teacher3 = ui.Q<VisualElement>("Teacher3");
        Teacher3.AddManipulator(new Clickable(() => OnTeacherClicked(2)));
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
        if (GameManager.Instance.currency < costs[teacherNumber]) return;
        teacherIndex = teacherNumber;
        Floor.ShowIndicators();
        selectedTeacher = teachers[teacherNumber];
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
                GameManager.Instance.currency -= costs[teacherIndex];
                Transform FloorTileTransform = hit.collider.transform.parent;
                Instantiate(selectedTeacher, FloorTileTransform.position, Quaternion.identity);
                Floor.GetIndicators(tileToIgnore: FloorTileTransform);
            }
        }
    }

    private void UpdateUI()
    {
        // ui.Q<Label>("WaveInfo").text = $"Wave: {GameManager.Instance.currentWave}";
        ui.Q<ProgressBar>("HP").highValue = TeacherPrincipal.Instance.GetComponent<Teacher>().maxHp;
        ui.Q<ProgressBar>("HP").value = TeacherPrincipal.Instance.GetComponent<Teacher>().hp;
        ui.Q<Label>("Money").text = GameManager.Instance.currency.ToString();
        ui.Q<Label>("Score").text = GameManager.Instance.score.ToString();
        ui.Q<Label>("WaveInfo").text = "Wave\n#" + (WaveManager.Instance.currentWave + 1);
        ui.Q<Label>("Teacher1Cost").text = costs[0].ToString();
        ui.Q<Label>("Teacher2Cost").text = costs[1].ToString();
        ui.Q<Label>("Teacher3Cost").text = costs[2].ToString();
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