using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþi için gerekli namespace
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
    public GameObject spikeUp;        // SpikeUp objesini buraya baðlayacaðýz
    public float raiseHeight = 2f;    // SpikeUp'un yukarý çýkacaðý mesafe
    public float raiseSpeed = 2f;     // SpikeUp'un yükselme hýzý
    public string sceneToLoad;        // Public olarak sahne adý

    private Vector3 initialPosition;  // SpikeUp'un baþlangýç pozisyonu
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
            playerTriggered = true;  // Oyuncu temas ettiðinde tetiklenir
            StartCoroutine(WaitAndChangeScene()); // 2 saniye bekleyip sahneye geçmek için Coroutine baþlat
        }
    }

    // Coroutine ile 2 saniye bekleme iþlemi
    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(2f);  // 2 saniye bekle
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);  // Yeni sahneye geçiþ
        }
        else
        {

        }
    }

    void Update()
    {
        if (playerTriggered && spikeUp != null)
        {
            // SpikeUp'u yukarý kaldýr
            Vector3 targetPosition = initialPosition + new Vector3(0, raiseHeight, 0);
            spikeUp.transform.position = Vector3.MoveTowards(spikeUp.transform.position, targetPosition, raiseSpeed * Time.deltaTime);

            // SpikeUp hedef yüksekliðe ulaþtýysa tetiklemeyi kapat
            if (spikeUp.transform.position == targetPosition)
            {
                playerTriggered = false;  // Ýsterseniz burada false yapabilirsiniz veya baþka iþlemler ekleyebilirsiniz
            }
        }
    }
}
