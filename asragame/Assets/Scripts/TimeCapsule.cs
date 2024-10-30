using UnityEngine;

public class TimeCapsule : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().EnableTimeStop();
            Destroy(gameObject); // Timecapsule nesnesini ald�ktan sonra yok edin
        }
    }
}
