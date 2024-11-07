using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2.5f;
    public float rotationSpeed = 5f; // Smooth d�n�� h�z�

    private Rigidbody rb;
    private bool isGrounded;
    private bool isCrouching;

    private CapsuleCollider playerCollider;
    private Vector3 originalScale;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float crouchColliderHeight = 0.4f;  // K���lt�lm�� collider y�ksekli�i

    private Animator animator;  // Animasyonlar� kontrol etmek i�in

    private Quaternion targetRotation; // Hedef rotasyon

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
        originalScale = transform.localScale;
        originalColliderHeight = playerCollider.height;
        originalColliderCenter = playerCollider.center;

        animator = GetComponent<Animator>();  // Animator bile�enini al

        targetRotation = transform.rotation;  // �lk hedef rotasyon
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Y�ne g�re karakteri 90 ve -90 derece aras�nda d�nd�rme
        if (horizontalInput > 0)
        {
            targetRotation = Quaternion.Euler(0, 90, 0); // Sa�a giderken hedef rotasyonu 90 derece yap
        }
        else if (horizontalInput < 0)
        {
            targetRotation = Quaternion.Euler(0, -90, 0); // Sola giderken hedef rotasyonu -90 derece yap
        }

        // Hedef rotasyona do�ru yumu�ak d�n��
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Crouch i�lemi
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            isCrouching = true;
            playerCollider.height = crouchColliderHeight;  // Collider'� k���lt
            playerCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y - (originalColliderHeight - crouchColliderHeight) / 2, originalColliderCenter.z); // Collider merkezini yukar� kayd�r
            animator.SetBool("isCrouch", true);  // Crouch animasyonunu ba�lat
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            playerCollider.height = originalColliderHeight;  // Collider'� eski haline getir
            playerCollider.center = originalColliderCenter;  // Collider merkezini eski haline getir
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
