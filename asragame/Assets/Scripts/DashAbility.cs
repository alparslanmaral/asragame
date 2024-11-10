using UnityEngine;

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

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f; 
    public float dashCooldown = 1f; 
    public TimeControl timeControl; 
    private bool isDashing = false;
    private float dashTime;
    private Vector3 dashDirection; 

    void Start()
    {
        
        timeControl = FindObjectOfType<TimeControl>();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            dashDirection = Vector3.left; 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dashDirection = Vector3.right; 
        }

        
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && timeControl.abilityDuration >= timeControl.maxAbilityDuration * 0.2f)
        {
            Dash();
        }

        
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

        
        timeControl.abilityDuration -= timeControl.maxAbilityDuration * 0.2f;

        
        RaycastHit hit;
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        
        if (Physics.Raycast(transform.position, dashDirection, out hit, dashDistance))
        {
            dashTarget = hit.point - dashDirection * 0.1f;
        }

        
        transform.position = dashTarget;
    }
}
