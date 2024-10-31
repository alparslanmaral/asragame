using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2.5f;  
    private Rigidbody rb;
    private bool isGrounded;
    private bool isCrouching;

    private Vector3 originalScale;  
    private float crouchScaleY = 0.5f;  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalScale = transform.localScale;  
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, crouchScaleY, originalScale.z);  
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            transform.localScale = originalScale;  
        }

        
        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        Vector3 move = new Vector3(horizontalInput * currentSpeed, rb.velocity.y, 0);

        
        rb.velocity = new Vector3(move.x * Time.timeScale, rb.velocity.y, rb.velocity.z);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
