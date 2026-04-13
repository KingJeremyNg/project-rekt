using UnityEngine;

public class TeacherPrincipal : MonoBehaviour
{
    public static TeacherPrincipal Instance { get; private set; }
    private Teacher teacherScript;
    public AudioClip[] punchSounds;

    void Start()
    {
        Instance = this;
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 10f;
        teacherScript.hp = 100f;
        teacherScript.maxHp = 100f;
        teacherScript.atkSpeed = 1f;
        teacherScript.attackRange = 1.5f;
        teacherScript.SetAnimationSpeed(teacherScript.atkSpeed);
    }

    public void PlayPunchSound()
    {
        SoundFXManager.Instance.PlayRandomSound(punchSounds, transform, 1f); // TODO CHANGE VOLUME TO MATCH SLIDERS
    }

    void Update()
    {
        // Check if it's time to attack based on the attack speed
        if ((Time.time - teacherScript.lastAttackTime) > (1f / teacherScript.atkSpeed))
        {
            teacherScript.FindTarget(teacherScript.attackRange);
            if (teacherScript.target != null) teacherScript.Attack();
        }
        if (teacherScript.target == null) teacherScript.PlayIdleAnimation();
    }
}