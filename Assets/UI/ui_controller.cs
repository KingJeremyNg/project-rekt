using UnityEngine;
using UnityEngine.UIElements;

public class ui_controller : MonoBehaviour
{
    public VisualElement ui;

    public Button teacherPE;
    public Button teacherLaw;
    public Button blockade;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        teacherPE = ui.Q<Button>("teacher_pe");
        teacherPE.clicked += OnTeacherPEClicked;
        teacherLaw = ui.Q<Button>("teacher_law");
        teacherLaw.clicked += OnTeacherLawClicked;
        blockade = ui.Q<Button>("blockade");
        blockade.clicked += OnBlockadeClicked;
    }

    public void OnTeacherPEClicked()
    {
        Debug.Log("Teacher PE button clicked");
        // Implement the logic to select Teacher PE
    }

    public void OnTeacherLawClicked()
    {
        Debug.Log("Teacher Law button clicked");
        // Implement the logic to select Teacher Law
    }

    public void OnBlockadeClicked()
    {
        Debug.Log("Blockade button clicked");
        // Implement the logic to select Blockade
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
