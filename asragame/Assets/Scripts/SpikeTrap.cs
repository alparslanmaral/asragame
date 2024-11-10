using UnityEngine;
using UnityEngine.SceneManagement; // Sahne ge�i�i i�in gerekli namespace
using System.Collections;

//
//         _______                    _____                   _______         
//        /::\    \                  /\    \                 /:::\    \        
//       /::::\    \                /::\    \               /:::::\    \       
//      /::::::\    \              /::::\    \             /:::::::\    \      
//     /::::::::\    \            /::::::\    \           /:::::::::\    \     
//    /:::/ ~~\::\    \          /:::/\:::\    \         /:::/~~ \:::\    \    
//   /:::/     \::\    \        /:::/__\:::\    \       /:::/     \:::\    \   
//  /:::/    /  \::\    \      /::::\   \:::\    \     /:::/    /  \:::\    \  
// /:::/____/    \::\____\    /::::::\   \:::\    \   /:::/____/    \:::\____\ 
// |:::|    |     |:::|   |  /:::/\:::\   \:::\    \  |:::|    |    |:::|    |
// |:::|____|     |:::|   | /:::/  \:::\   \:::\____\ |:::|____|    |:::|    |
// \:::\    \    /:::/    / \::/    \:::\  /:::/    / \:::\    \   /:::/    /
//  \:::\    \  /:::/    /   \/____/ \:::\/:::/    /   \:::\    \ /:::/    /
//   \:::\     /:::/    /             \::::::/    /     \:::\    /:::/    /
//    \:::\__ /:::/    /               \::::/    /       \:::\__ /::/    /
//     \:::::::::/    /                /:::/    /         \::::::::/    /
//      \:::::::/    /                /:::/    /           \::::::/    /
//       \:::::/    /                /:::/    /             \::::/    /
//        \:::/    /                /:::/    /               \::/    /
//         ~~  ___/                 \::/    /                 ~~ ___/
//                                   \/____/

public class SpikeTrap : MonoBehaviour
{
    public GameObject spikeUp;        // SpikeUp objesini buraya ba�layaca��z
    public float raiseHeight = 2f;    // SpikeUp'un yukar� ��kaca�� mesafe
    public float raiseSpeed = 2f;     // SpikeUp'un y�kselme h�z�
    public string sceneToLoad;        // Public olarak sahne ad�

    private Vector3 initialPosition;  // SpikeUp'un ba�lang�� pozisyonu
    private bool playerTriggered = false;  // Oyuncu temas etti mi?

    void Start()
    {
        if (spikeUp != null)
        {
            initialPosition = spikeUp.transform.position;  // SpikeUp'un ilk pozisyonunu kaydediyoruz
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTriggered = true;  // Oyuncu temas etti�inde tetiklenir
            StartCoroutine(WaitAndChangeScene()); // 2 saniye bekleyip sahneye ge�mek i�in Coroutine ba�lat
        }
    }

    // Coroutine ile 2 saniye bekleme i�lemi
    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(2f);  // 2 saniye bekle
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);  // Yeni sahneye ge�i�
        }
        else
        {

        }
    }

    void Update()
    {
        if (playerTriggered && spikeUp != null)
        {
            // SpikeUp'u yukar� kald�r
            Vector3 targetPosition = initialPosition + new Vector3(0, raiseHeight, 0);
            spikeUp.transform.position = Vector3.MoveTowards(spikeUp.transform.position, targetPosition, raiseSpeed * Time.deltaTime);

            // SpikeUp hedef y�ksekli�e ula�t�ysa tetiklemeyi kapat
            if (spikeUp.transform.position == targetPosition)
            {
                playerTriggered = false;  // �sterseniz burada false yapabilirsiniz veya ba�ka i�lemler ekleyebilirsiniz
            }
        }
    }
}
