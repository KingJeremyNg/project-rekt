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
    private Transform selectedTeacher;
    private Camera mainCamera;
    private Floor Floor;


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
    }

    private void PlaceTeacher()
    {
        if (selectedTeacher == null) return;
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
    }
}