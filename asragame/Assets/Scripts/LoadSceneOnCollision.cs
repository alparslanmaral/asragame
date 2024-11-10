using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    // Public deðiþken, Unity Inspector'dan sahne ismini girebiliriz.
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpan objenin tag'i "Player" ise
        if (other.CompareTag("Player"))
        {
            // Belirtilen sahneyi yükle
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
