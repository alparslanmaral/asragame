using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    // Public de�i�ken, Unity Inspector'dan sahne ismini girebiliriz.
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        // E�er �arpan objenin tag'i "Player" ise
        if (other.CompareTag("Player"))
        {
            // Belirtilen sahneyi y�kle
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
