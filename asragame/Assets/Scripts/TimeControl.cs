using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//
//         _______                    _____                   _______         
//        /::\    \                  /\    \                 /:::\    \        
//       /::::\    \                /::\    \               /:::::\    \       
//      /::::::\    \              /::::\    \             /:::::::\    \      
//     /::::::::\    \            /::::::\    \           /:::::::::\    \     
//    /:::/ ~~\::\    \          /:::/\:::\    \         /:::/~~ \:::\    \    
//   /:::/     \::\    \        /:::/__\:::\    \       /:::/     \:::\    \   
//  /:::/    /  \::\    \      /::::\   \:::\    \     /:::/    /  \:::\    \  
// /:::/____/    \::\____\    /::::::\   \:::\    \   /:::/____/    \:::\____\ 
// |:::|    |     |:::|   |  /:::/\:::\   \:::\    \  |:::|    |    |:::|    |
// |:::|____|     |:::|   | /:::/  \:::\   \:::\____\ |:::|____|    |:::|    |
// \:::\    \    /:::/    / \::/    \:::\  /:::/    / \:::\    \   /:::/    /
//  \:::\    \  /:::/    /   \/____/ \:::\/:::/    /   \:::\    \ /:::/    /
//   \:::\     /:::/    /             \::::::/    /     \:::\    /:::/    /
//    \:::\__ /:::/    /               \::::/    /       \:::\__ /::/    /
//     \:::::::::/    /                /:::/    /         \::::::::/    /
//      \:::::::/    /                /:::/    /           \::::::/    /
//       \:::::/    /                /:::/    /             \::::/    /
//        \:::/    /                /:::/    /               \::/    /
//         ~~  ___/                 \::/    /                 ~~ ___/
//                                   \/____/

public class TimeControl : MonoBehaviour
{
    public float maxAbilityDuration = 5f;
    public float abilityDuration;
    public float rechargeRadius = 1f;
    public LayerMask timeCapsuleLayer;
    public Slider abilityBar;
    public GameObject abilityEffectObject; 

    private bool isTimeSlowed = false;
    private bool isNearTimeCapsule = false;
    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    private List<Animator> affectedAnimators = new List<Animator>();
    private List<Animation> affectedAnimations = new List<Animation>();
    private List<EnemyFollow> enemyFollowScripts = new List<EnemyFollow>();

    void Start()
    {
        abilityDuration = maxAbilityDuration;
        abilityBar.maxValue = maxAbilityDuration;
        abilityBar.value = abilityDuration;
        FindAffectedObjects();

        if (abilityEffectObject != null)
        {
            abilityEffectObject.SetActive(false); 
        }
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

        if (abilityEffectObject != null)
        {
            abilityEffectObject.SetActive(isTimeSlowed); 
        }

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

        
        foreach (var enemyFollow in enemyFollowScripts)
        {
            enemyFollow.enabled = !isTimeSlowed;
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

            var enemyFollow = obj.GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollowScripts.Add(enemyFollow);
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
