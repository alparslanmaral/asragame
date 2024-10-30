using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Zamandan ba��ms�z hareket
        Vector3 move = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);

        // unscaledTime ile X eksenindeki h�z, zaman yava�latmadan etkilenmeyecek
        rb.velocity = new Vector3(move.x * Time.timeScale, rb.velocity.y, rb.velocity.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
