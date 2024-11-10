using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f; // Dash mesafesi
    public float dashCooldown = 1f; // Dash cooldown süresi
    public TimeControl timeControl; // TimeControl scriptine referans
    private bool isDashing = false;
    private float dashTime;
    private Vector3 dashDirection; // Dash yönü

    void Start()
    {
        // TimeControl bileþenini bulma
        timeControl = FindObjectOfType<TimeControl>();
    }

    void Update()
    {
        // Tuþlara basma kontrolü
        if (Input.GetKeyDown(KeyCode.A))
        {
            dashDirection = Vector3.left; // Sol yön
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dashDirection = Vector3.right; // Sað yön
        }

        // Dash iþlemi
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && timeControl.abilityDuration >= timeControl.maxAbilityDuration * 0.2f)
        {
            Dash();
        }

        // Dashing süresini kontrol et
        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashCooldown)
            {
                isDashing = false;
            }
        }
    }

    private void Dash()
    {
        isDashing = true;
        dashTime = 0f;

        // Güç barýndan %20 azaltma
        timeControl.abilityDuration -= timeControl.maxAbilityDuration * 0.2f;

        // Dash yönünde raycast ile engel kontrolü
        RaycastHit hit;
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        // Eðer bir engel varsa, engelin yanýna kadar git
        if (Physics.Raycast(transform.position, dashDirection, out hit, dashDistance))
        {
            dashTarget = hit.point - dashDirection * 0.1f; // Engelin hemen önünde dur
        }

        // Karakteri hedef konuma taþý
        transform.position = dashTarget;
    }
}
