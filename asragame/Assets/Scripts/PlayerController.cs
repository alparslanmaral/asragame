using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool canStopTime = false;
    private bool timeStopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleTimeStop();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Zaman durdu�unda unscaledDeltaTime kullanarak oyuncunun hareket etmesini sa�l�yoruz
        Vector3 move = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = move;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void HandleTimeStop()
    {
        if (canStopTime && Input.GetKeyDown(KeyCode.T)) // 'T' tu�u ile zaman� durdur
        {
            ToggleTimeStop();
        }
    }

    private void ToggleTimeStop()
    {
        if (timeStopped)
        {
            Time.timeScale = 1; // Zaman� devam ettir
            timeStopped = false;
        }
        else
        {
            Time.timeScale = 0; // Zaman� durdur
            timeStopped = true;
        }
    }

    public void EnableTimeStop()
    {
        canStopTime = true; // Zaman durdurma yetene�ini aktif eder
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
