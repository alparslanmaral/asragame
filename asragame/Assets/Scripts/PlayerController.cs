using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2.5f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isCrouching;

    private CapsuleCollider playerCollider;
    private Vector3 originalScale;
    private float originalColliderHeight;
    private float crouchColliderHeight = 0.2f;  // K���lt�lm�� collider y�ksekli�i

    private Animator animator;  // Animasyonlar� kontrol etmek i�in

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
        originalScale = transform.localScale;
        originalColliderHeight = playerCollider.height;

        animator = GetComponent<Animator>();  // Animator bile�enini al
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Crouch i�lemi
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            isCrouching = true;
            playerCollider.height = crouchColliderHeight;  // Collider'� k���lt
            animator.SetBool("isCrouch", true);  // Crouch animasyonunu ba�lat
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            playerCollider.height = originalColliderHeight;  // Collider'� eski haline getir
            animator.SetBool("isCrouch", false);  // Crouch animasyonunu durdur
        }

        // Hareket h�z� ayarlama
        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        Vector3 move = new Vector3(horizontalInput * currentSpeed, rb.velocity.y, 0);
        rb.velocity = new Vector3(move.x * Time.timeScale, rb.velocity.y, rb.velocity.z);

        // Animator'a h�z parametresi g�nder
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("speed", speed);  // speed parametresini animator'a g�nder

        // Z�plama i�lemi
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Havada olma animasyonu
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
