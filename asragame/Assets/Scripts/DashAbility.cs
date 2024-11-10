using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f; // Dash mesafesi
    public float dashCooldown = 1f; // Dash cooldown s�resi
    public TimeControl timeControl; // TimeControl scriptine referans
    private bool isDashing = false;
    private float dashTime;
    private Vector3 dashDirection; // Dash y�n�

    void Start()
    {
        // TimeControl bile�enini bulma
        timeControl = FindObjectOfType<TimeControl>();
    }

    void Update()
    {
        // Tu�lara basma kontrol�
        if (Input.GetKeyDown(KeyCode.A))
        {
            dashDirection = Vector3.left; // Sol y�n
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dashDirection = Vector3.right; // Sa� y�n
        }

        // Dash i�lemi
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && timeControl.abilityDuration >= timeControl.maxAbilityDuration * 0.2f)
        {
            Dash();
        }

        // Dashing s�resini kontrol et
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

        // G�� bar�ndan %20 azaltma
        timeControl.abilityDuration -= timeControl.maxAbilityDuration * 0.2f;

        // Dash y�n�nde raycast ile engel kontrol�
        RaycastHit hit;
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        // E�er bir engel varsa, engelin yan�na kadar git
        if (Physics.Raycast(transform.position, dashDirection, out hit, dashDistance))
        {
            dashTarget = hit.point - dashDirection * 0.1f; // Engelin hemen �n�nde dur
        }

        // Karakteri hedef konuma ta��
        transform.position = dashTarget;
    }
}
