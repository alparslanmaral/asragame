using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour
{
    public float maxAbilityDuration = 5f;
    public float abilityDuration;
    public float rechargeRadius = 1f;
    public LayerMask timeCapsuleLayer;
    public Slider abilityBar;

    private bool isTimeSlowed = false;
    private bool isNearTimeCapsule = false;
    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    private List<Animator> affectedAnimators = new List<Animator>();
    private List<Animation> affectedAnimations = new List<Animation>();

    void Start()
    {
        abilityDuration = maxAbilityDuration;
        abilityBar.maxValue = maxAbilityDuration;
        abilityBar.value = abilityDuration;
        FindAffectedObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && abilityDuration > 0)
        {
            ToggleTimeSlow();
        }

        if (isTimeSlowed)
        {
            abilityDuration -= Time.unscaledDeltaTime;
            if (abilityDuration <= 0)
            {
                ToggleTimeSlow();
                abilityDuration = 0;
            }
        }

        isNearTimeCapsule = Physics.CheckSphere(transform.position, rechargeRadius, timeCapsuleLayer);

        if (isNearTimeCapsule && !isTimeSlowed)
        {
            abilityDuration = maxAbilityDuration;
        }

        abilityBar.value = abilityDuration;
    }

    private void ToggleTimeSlow()
    {
        isTimeSlowed = !isTimeSlowed;
        float timeScale = isTimeSlowed ? 0.01f : 1f;

        foreach (var rb in affectedRigidbodies)
        {
            rb.velocity *= timeScale;
            rb.angularVelocity *= timeScale;

            rb.useGravity = !isTimeSlowed;
        }

        foreach (var anim in affectedAnimators)
        {
            anim.speed = timeScale;
        }

        foreach (var animation in affectedAnimations)
        {
            foreach (AnimationState state in animation)
            {
                state.speed = timeScale;
            }
        }
    }

    private void FindAffectedObjects()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] puzzles = GameObject.FindGameObjectsWithTag("Puzzle");

        foreach (var obj in enemies)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                affectedRigidbodies.Add(rb);
            }

            var anim = obj.GetComponent<Animator>();
            if (anim != null)
            {
                affectedAnimators.Add(anim);
            }

            var animation = obj.GetComponent<Animation>();
            if (animation != null)
            {
                affectedAnimations.Add(animation);
            }
        }

        foreach (var obj in puzzles)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                affectedRigidbodies.Add(rb);
            }

            var anim = obj.GetComponent<Animator>();
            if (anim != null)
            {
                affectedAnimators.Add(anim);
            }

            var animation = obj.GetComponent<Animation>();
            if (animation != null)
            {
                affectedAnimations.Add(animation);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rechargeRadius);
    }
}
