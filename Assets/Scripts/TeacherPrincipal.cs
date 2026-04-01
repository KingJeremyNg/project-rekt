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
        teacherScript.atkSpeed = 0.5f;
        teacherScript.attackRange = 1.5f;
    }

    // Call this method to perform the attack action, which instantiates a basketball projectile that moves towards the target student and deals damage upon impact.
    private void Attack()
    {
        teacherScript.lastAttackTime = Time.time;
        teacherScript.PlayAttackAnimation();
    }

    public void PlayPunchSound()
    {
        SoundFXManager.Instance.PlayRandomSound(punchSounds, transform, 0.1f); // TODO CHANGE VOLUME TO MATCH SLIDERS
    }

    void Update()
    {
        // Check if it's time to attack based on the attack speed
        if ((Time.time - teacherScript.lastAttackTime) > (1f / teacherScript.atkSpeed))
        {
            teacherScript.FindTarget(teacherScript.attackRange);
            if (teacherScript.target != null) Attack();
        }
        if (teacherScript.target == null) teacherScript.PlayIdleAnimation();
    }
}