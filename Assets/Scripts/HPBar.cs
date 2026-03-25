using UnityEngine;

public class HPBar : MonoBehaviour
{
    private Teacher teacherScript;
    private Student studentScript;
    private Transform maxHpBar;
    private Transform currentHpBar;

    void Start()
    {
        teacherScript = GetComponentInParent<Teacher>();
        studentScript = GetComponentInParent<Student>();
        maxHpBar = transform.Find("Max");
        currentHpBar = transform.Find("Current");
    }

    public void UpdateHPBar()
    {
        maxHpBar.gameObject.SetActive(true);
        currentHpBar.gameObject.SetActive(true);
        if (teacherScript != null)
        {
            float hpPercent = teacherScript.hp / teacherScript.maxHp;
            currentHpBar.localScale = new Vector3(hpPercent * currentHpBar.localScale.x, currentHpBar.localScale.y, currentHpBar.localScale.z);
        }
        else if (studentScript != null)
        {
            float hpPercent = studentScript.hp / studentScript.maxHp;
            currentHpBar.localScale = new Vector3(hpPercent * currentHpBar.localScale.x, currentHpBar.localScale.y, currentHpBar.localScale.z);
        }
    }
}
