using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float maxAbilityDuration = 5f;   
    public float abilityDuration;           
    public float rechargeRadius = 1f;       
    public LayerMask timeCapsuleLayer;      
    public Slider abilityBar;               

    private bool isTimeStopped = false;
    private bool isNearTimeCapsule = false;

    void Start()
    {
        abilityDuration = maxAbilityDuration; 
        abilityBar.maxValue = maxAbilityDuration; 
        abilityBar.value = abilityDuration;       
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && abilityDuration > 0)
        {
            ToggleTimeStop();
        }


        if (isTimeStopped)
        {
            abilityDuration -= Time.unscaledDeltaTime;
            if (abilityDuration <= 0)
            {

                ToggleTimeStop();
                abilityDuration = 0;
            }
        }


        isNearTimeCapsule = Physics.CheckSphere(transform.position, rechargeRadius, timeCapsuleLayer);


        if (isNearTimeCapsule && !isTimeStopped)
        {
            abilityDuration = maxAbilityDuration;
        }


        abilityBar.value = abilityDuration;
    }


    private void ToggleTimeStop()
    {
        isTimeStopped = !isTimeStopped;
        Time.timeScale = isTimeStopped ? 0f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rechargeRadius);
    }
}
