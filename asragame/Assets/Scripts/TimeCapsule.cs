using UnityEngine;

public class TimeCapsule : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().EnableTimeStop();
            Destroy(gameObject); // Timecapsule nesnesini aldýktan sonra yok edin
        }
    }
}
