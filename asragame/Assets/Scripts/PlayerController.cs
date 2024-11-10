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

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2.5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isCrouching;

    private CapsuleCollider playerCollider;
    private Vector3 originalScale;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float crouchColliderHeight = 0.4f; 

    private Animator animator;  

    private Quaternion targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
        originalScale = transform.localScale;
        originalColliderHeight = playerCollider.height;
        originalColliderCenter = playerCollider.center;

        animator = GetComponent<Animator>();  

        targetRotation = transform.rotation;  
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        
        if (horizontalInput > 0)
        {
            targetRotation = Quaternion.Euler(0, 90, 0); 
        }
        else if (horizontalInput < 0)
        {
            targetRotation = Quaternion.Euler(0, -90, 0); 
        }

        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            isCrouching = true;
            playerCollider.height = crouchColliderHeight;  
            playerCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y - (originalColliderHeight - crouchColliderHeight) / 2, originalColliderCenter.z); // Collider merkezini yukarý kaydýr
            animator.SetBool("isCrouch", true);  
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            playerCollider.height = originalColliderHeight;  
            playerCollider.center = originalColliderCenter;  
            animator.SetBool("isCrouch", false);  
        }

        
        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        Vector3 move = new Vector3(horizontalInput * currentSpeed, rb.velocity.y, 0);
        rb.velocity = new Vector3(move.x * Time.timeScale, rb.velocity.y, rb.velocity.z);

        
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("speed", speed);  

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        
        animator.SetBool("isFall", !isGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
