using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    private Student StudentScript;
    private Teacher TeacherScript;

    void Start()
    {
        // Get a reference to the parent script when the game starts
        StudentScript = GetComponentInParent<Student>();
        TeacherScript = GetComponentInParent<Teacher>();
    }

    // This public function will be called by the Animation Event
    public void DealDamage()
    {
        if (StudentScript != null) StudentScript.DealDamage();
        if (TeacherScript != null) TeacherScript.DealDamage();
    }

    public void CleanUp()
    {
        if (StudentScript != null) StudentScript.CleanUp();
        if (TeacherScript != null) TeacherScript.CleanUp();
    }

    public void ShootBasketball()
    {
        TeacherPE teacherPE = GetComponentInParent<TeacherPE>();
        if (teacherPE != null)
        {
            teacherPE.ShootBasketball();
        }
    }

    public void ShootArgument()
    {
        TeacherLaw teacherLaw = GetComponentInParent<TeacherLaw>();
        if (teacherLaw != null)
        {
            teacherLaw.ShootArgument();
        }
    }

    public void PlayPunchSound()
    {
        TeacherPrincipal teacherPrincipal = GetComponentInParent<TeacherPrincipal>();
        if (teacherPrincipal != null)
        {
            teacherPrincipal.PlayPunchSound();
        }
    }
}
