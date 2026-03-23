using UnityEngine;
using System.Collections.Generic;

public class TeacherPrincipal : MonoBehaviour
{
    private Teacher teacherScript;
    public AudioSource AudioSource;
    public List<AudioClip> punchSounds;

    void Start()
    {
        teacherScript = GetComponent<Teacher>();
        teacherScript.atk = 10f;
        teacherScript.hp = 100f;
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
        int index = Random.Range(0, punchSounds.Count);
        AudioSource.PlayOneShot(punchSounds[index]);
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