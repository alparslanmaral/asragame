using UnityEngine;

public class AffectedObjectController : MonoBehaviour
{
    private float originalSpeed = 1f;
    private float slowedSpeed = 0.01f; // 100 kat yavaþlatmak için

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.speed = originalSpeed;
        }
    }

    public void SetTimeSlow(bool isSlowed)
    {
        if (animator != null)
        {
            animator.speed = isSlowed ? slowedSpeed : originalSpeed;
        }

        if (rb != null && !rb.isKinematic) // Sadece kinematik deðilse uygulansýn
        {
            rb.velocity *= isSlowed ? slowedSpeed : originalSpeed / slowedSpeed;
            rb.angularVelocity *= isSlowed ? slowedSpeed : originalSpeed / slowedSpeed;
        }
    }
}
